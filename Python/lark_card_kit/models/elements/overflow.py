"""Overflow element for overflow menu."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element
from .button import ConfirmConfig
from .plain_text import PlainText


@dataclass
class OverflowOption:
    """Overflow menu option."""

    text: PlainText | None = None
    value: str = ""

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        result: dict[str, Any] = {"value": self.value}
        if self.text:
            result["text"] = self.text.to_dict()
        return result


@dataclass
class Overflow(Element):
    """Overflow menu element for additional options.

    Displays a dropdown menu with additional actions.
    """

    tag: str = field(default="overflow", init=False)
    name: str | None = None
    options: list[OverflowOption] = field(default_factory=list)
    placeholder: PlainText | None = None
    disabled: bool | None = None
    confirm: ConfirmConfig | None = None
    width: str | None = None