"""Button element for interactive buttons."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element
from .plain_text import PlainText


@dataclass
class ConfirmConfig:
    """Confirmation dialog configuration for buttons."""

    title: PlainText | None = None
    text: PlainText | None = None

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        result: dict[str, Any] = {}
        if self.title:
            result["title"] = self.title.to_dict()
        if self.text:
            result["text"] = self.text.to_dict()
        return result


@dataclass
class Button(Element):
    """Button element for interactive buttons.

    Buttons can trigger actions like callbacks or URL opens.
    """

    tag: str = field(default="button", init=False)
    type: str | None = None
    size: str | None = None
    width: str | None = None
    text: PlainText | None = None
    icon: Any = None
    hover_tips: PlainText | None = None
    disabled: bool | None = None
    disabled_tips: PlainText | None = None
    confirm: ConfirmConfig | None = None
    behaviors: list[Any] | None = None
    name: str | None = None
    form_action_type: str | None = None
    onclick: Any = None