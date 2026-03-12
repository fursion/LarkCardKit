"""Select element for dropdown selection."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element
from .button import ConfirmConfig
from .plain_text import PlainText


@dataclass
class SelectOption:
    """Option for select element."""

    text: PlainText | None = None
    value: str = ""

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        result: dict[str, Any] = {"value": self.value}
        if self.text:
            result["text"] = self.text.to_dict()
        return result


@dataclass
class Select(Element):
    """Select element for dropdown selection.

    Used for single or multiple selection from a list of options.
    """

    tag: str = field(default="select_static", init=False)
    name: str | None = None
    placeholder: PlainText | None = None
    options: list[SelectOption] = field(default_factory=list)
    selected_value: str | None = None
    disabled: bool | None = None
    disabled_tips: PlainText | None = None
    confirm: ConfirmConfig | None = None
    width: str | None = None
    option_width: str | None = None
    multiple: bool | None = None