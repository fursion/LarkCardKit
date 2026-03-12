"""Checkbox element for checkbox selection."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element
from .plain_text import PlainText


@dataclass
class CheckboxOption:
    """Checkbox option definition."""

    text: PlainText | None = None
    value: str = ""

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        result: dict[str, Any] = {"value": self.value}
        if self.text:
            result["text"] = self.text.to_dict()
        return result


@dataclass
class Checkbox(Element):
    """Checkbox element for multiple selections.

    Allows users to select multiple options from a list.
    """

    tag: str = field(default="checkbox", init=False)
    name: str | None = None
    options: list[CheckboxOption] = field(default_factory=list)
    selected_values: list[str] | None = None
    disabled: bool | None = None
    width: str | None = None