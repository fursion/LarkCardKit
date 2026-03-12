"""Input element for text input fields."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element
from .button import ConfirmConfig
from .plain_text import PlainText


@dataclass
class Input(Element):
    """Input element for text input fields.

    Used for collecting user input in forms.
    """

    tag: str = field(default="input", init=False)
    name: str | None = None
    required: bool | None = None
    placeholder: PlainText | None = None
    default_value: str | None = None
    input_type: str | None = None
    label: PlainText | None = None
    label_position: str | None = None
    max_length: int | None = None
    rows: int | None = None
    auto_resize: bool | None = None
    max_rows: int | None = None
    width: str | None = None
    disabled: Any = None
    disabled_tips: PlainText | None = None
    confirm: ConfirmConfig | None = None
    show_icon: bool | None = None
    behaviors: list[Any] | None = None