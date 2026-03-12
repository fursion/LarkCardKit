"""Element base class for all card elements."""

from __future__ import annotations

from dataclasses import dataclass, field, fields
from typing import Any


@dataclass
class Element:
    """Base class for all card elements.

    All card elements inherit from this class and must define a unique tag.
    """

    tag: str = field(default="", init=False)
    element_id: str | None = None
    margin: str | None = None

    def to_dict(self) -> dict[str, Any]:
        """Convert the element to a dictionary for JSON serialization.

        Returns:
            Dictionary representation of the element with None values excluded.
        """
        result: dict[str, Any] = {}

        # Get all field values
        for f in fields(self):
            value = getattr(self, f.name)
            if value is not None:
                # Use the field name directly (dataclass handles this)
                result[f.name] = self._serialize_value(value)

        # Ensure tag is always first
        if self.tag:
            result = {"tag": self.tag, **{k: v for k, v in result.items() if k != "tag"}}

        return result

    def _serialize_value(self, value: Any) -> Any:
        """Serialize a value to a JSON-compatible format.

        Args:
            value: The value to serialize.

        Returns:
            The serialized value.
        """
        if hasattr(value, "to_dict"):
            return value.to_dict()
        if isinstance(value, list):
            return [self._serialize_value(item) for item in value]
        if isinstance(value, dict):
            return {k: self._serialize_value(v) for k, v in value.items()}
        return value


# Type alias for elements that can contain text
TextObject = Any  # Union[PlainText, MarkdownText] - defined after those classes