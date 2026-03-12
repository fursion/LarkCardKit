"""Hr element for horizontal rule."""

from __future__ import annotations

from dataclasses import dataclass, field

from .base import Element


@dataclass
class Hr(Element):
    """Horizontal rule element for visual separation.

    Creates a horizontal line to separate content sections.
    """

    tag: str = field(default="hr", init=False)