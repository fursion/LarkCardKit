"""Markdown element for markdown content."""

from __future__ import annotations

from dataclasses import dataclass, field

from .base import Element


@dataclass
class Markdown(Element):
    """Markdown element for markdown-formatted content.

    This is used for text that requires markdown formatting.
    The tag is 'lark_md' for Lark markdown format.
    """

    tag: str = field(default="lark_md", init=False)
    content: str = ""
    text_size: str | None = None
    text_color: str | None = None