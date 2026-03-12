"""SelectPerson element for person selection."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element
from .button import ConfirmConfig
from .plain_text import PlainText


@dataclass
class SelectPerson(Element):
    """Person selector element for single person selection.

    Allows users to select a single person from the organization.
    """

    tag: str = field(default="select_person", init=False)
    name: str | None = None
    placeholder: PlainText | None = None
    selected_value: str | None = None
    disabled: bool | None = None
    disabled_tips: PlainText | None = None
    confirm: ConfirmConfig | None = None
    width: str | None = None
    behaviors: list[Any] | None = None