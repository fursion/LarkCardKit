"""Template parameter filler for ${key} placeholder replacement."""

from __future__ import annotations

import re
from dataclasses import dataclass
from typing import Any


@dataclass
class TemplateOptions:
    """Options for template parameter filling."""

    keep_unmatched_placeholders: bool = False


class TemplateParameterFiller:
    """Template parameter filler for replacing ${key} placeholders.

    Supports ${key} and ${key:default} syntax for placeholder replacement.

    Example:
        >>> filler = TemplateParameterFiller()
        >>> filler.set_parameter("name", "John")
        >>> filler.fill_string("Hello, ${name}!")
        'Hello, John!'
        >>> filler.fill_string("Hello, ${missing:Guest}!")
        'Hello, Guest!'
    """

    _placeholder_pattern = re.compile(r"\$\{(?P<key>[^}:]+)(?::(?P<default>[^}]*))?\}")

    def __init__(self, options: TemplateOptions | None = None) -> None:
        """Initialize the template parameter filler.

        Args:
            options: Template options for controlling placeholder behavior.
        """
        self._parameters: dict[str, Any] = {}
        self._options = options or TemplateOptions()

    def set_parameter(self, key: str, value: Any) -> TemplateParameterFiller:
        """Set a parameter value.

        Args:
            key: Parameter key.
            value: Parameter value.

        Returns:
            Self for method chaining.
        """
        self._parameters[key] = value
        return self

    def set_parameters(self, params: dict[str, Any]) -> TemplateParameterFiller:
        """Set multiple parameter values.

        Args:
            params: Dictionary of parameter key-value pairs.

        Returns:
            Self for method chaining.
        """
        self._parameters.update(params)
        return self

    def get_value(self, key: str) -> Any:
        """Get a parameter value.

        Args:
            key: Parameter key.

        Returns:
            Parameter value or None if not found.
        """
        return self._parameters.get(key)

    def fill_string(self, input_str: str | None) -> str:
        """Fill placeholders in a string.

        Args:
            input_str: Input string with potential ${key} placeholders.

        Returns:
            String with placeholders replaced by parameter values.
        """

        def replace(match: re.Match[str]) -> str:
            key = match.group("key")
            default = match.group("default")

            value = self._get_nested_value(key)

            if value is None:
                if default is not None:
                    return default
                return match.group(0) if self._options.keep_unmatched_placeholders else ""

            return str(value)

        return self._placeholder_pattern.sub(replace, input_str or "")

    def _get_nested_value(self, path: str) -> Any:
        """Get a nested value using dot notation.

        Args:
            path: Dot-separated path (e.g., "user.name").

        Returns:
            Nested value or None if not found.
        """
        parts = path.split(".")
        current: Any = self._parameters.get(parts[0])

        for part in parts[1:]:
            if current is None:
                return None

            if isinstance(current, dict):
                current = current.get(part)
            elif hasattr(current, part):
                current = getattr(current, part)
            else:
                return None

        return current

    def has_parameter(self, key: str) -> bool:
        """Check if a parameter exists.

        Args:
            key: Parameter key.

        Returns:
            True if parameter exists, False otherwise.
        """
        return key in self._parameters

    def has_parameters(self) -> bool:
        """Check if any parameters are set.

        Returns:
            True if parameters are set, False otherwise.
        """
        return len(self._parameters) > 0

    def clear(self) -> None:
        """Clear all parameters."""
        self._parameters.clear()