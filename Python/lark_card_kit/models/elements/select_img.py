"""SelectImg element for image selection."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element
from .button import ConfirmConfig
from .plain_text import PlainText


@dataclass
class SelectImg(Element):
    """Image selector element for selecting images.

    Allows users to select images from a predefined set.
    """

    tag: str = field(default="select_img", init=False)
    name: str | None = None
    placeholder: PlainText | None = None
    selected_value: str | None = None
    disabled: bool | None = None
    confirm: ConfirmConfig | None = None
    width: str | None = None
    options: list[Any] = field(default_factory=list)