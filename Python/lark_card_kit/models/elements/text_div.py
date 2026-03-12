"""TextDiv element for text with div styling."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element


@dataclass
class TextDiv(Element):
    """Text div element for styled text content.

    Combines text with div container styling.
    """

    tag: str = field(default="text_div", init=False)
    text: Any = None
    fields: list[Any] | None = None