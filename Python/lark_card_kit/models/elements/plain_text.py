"""PlainText element for simple text content."""

from __future__ import annotations

from dataclasses import dataclass, field

from .base import Element


@dataclass
class PlainText(Element):
    """Plain text element for simple text content.

    This is used for text that doesn't require markdown formatting.
    """

    tag: str = field(default="plain_text", init=False)
    content: str = ""
    text_size: str | None = None
    text_color: str | None = None
    text_align: str | None = None
    notation: bool | None = None
    width: str | None = None