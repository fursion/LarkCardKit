"""Checker element for checklist items."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element
from .plain_text import PlainText


@dataclass
class Checker(Element):
    """Checker element for checklist items.

    Displays a checklist item that can be checked or unchecked.
    """

    tag: str = field(default="checker", init=False)
    name: str | None = None
    text: PlainText | None = None
    checked: bool | None = None
    disabled: bool | None = None
    behaviors: list[Any] | None = None