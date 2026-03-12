"""PickerTime element for time selection."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element
from .button import ConfirmConfig
from .plain_text import PlainText


@dataclass
class PickerTime(Element):
    """Time picker element for time selection.

    Allows users to select a time.
    """

    tag: str = field(default="time_picker", init=False)
    name: str | None = None
    placeholder: PlainText | None = None
    initial_time: str | None = None
    value: str | None = None
    disabled: bool | None = None
    disabled_tips: PlainText | None = None
    confirm: ConfirmConfig | None = None
    width: str | None = None
    behaviors: list[Any] | None = None