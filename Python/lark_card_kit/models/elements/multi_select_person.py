"""MultiSelectPerson element for multiple person selection."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element
from .button import ConfirmConfig
from .plain_text import PlainText


@dataclass
class MultiSelectPerson(Element):
    """Multi-person selector element for multiple person selection.

    Allows users to select multiple people from the organization.
    """

    tag: str = field(default="multi_select_person", init=False)
    name: str | None = None
    placeholder: PlainText | None = None
    selected_values: list[str] | None = None
    disabled: bool | None = None
    disabled_tips: PlainText | None = None
    confirm: ConfirmConfig | None = None
    width: str | None = None
    behaviors: list[Any] | None = None