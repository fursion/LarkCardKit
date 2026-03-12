"""PlainTextElement for standalone plain text."""

from __future__ import annotations

from dataclasses import dataclass, field

from .base import Element


@dataclass
class PlainTextElement(Element):
    """Plain text element for standalone use.

    Similar to PlainText but used as a standalone element in the card body.
    """

    tag: str = field(default="plain_text", init=False)
    content: str = ""
    text_size: str | None = None
    text_color: str | None = None
    text_align: str | None = None