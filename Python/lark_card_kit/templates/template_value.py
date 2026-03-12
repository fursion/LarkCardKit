"""Template value types for deferred evaluation."""

from __future__ import annotations

from dataclasses import dataclass


@dataclass
class TemplateValue:
    """Template value for deferred parameter evaluation.

    Represents a value that should be filled from template parameters.
    """

    key: str
    default: str | None = None

    def resolve(self, filler: TemplateParameterFiller) -> str:
        """Resolve the template value using the filler.

        Args:
            filler: Template parameter filler.

        Returns:
            Resolved string value.
        """
        # Import here to avoid circular import
        from .template_filler import TemplateParameterFiller

        value = filler.get_value(self.key)
        if value is not None:
            return str(value)
        return self.default or ""


# Import for type hints
from .template_filler import TemplateParameterFiller