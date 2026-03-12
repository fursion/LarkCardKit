"""Div element for content division."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element


@dataclass
class Div(Element):
    """Div element for content division.

    A container element that can hold other elements.
    """

    tag: str = field(default="div", init=False)
    text: Any = None
    fields: list[Any] | None = None
    extra: dict[str, Any] | None = None