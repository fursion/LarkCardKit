"""MarkdownText element for markdown content in text fields."""

from __future__ import annotations

from dataclasses import dataclass, field

from .base import Element


@dataclass
class MarkdownText(Element):
    """Markdown text element for markdown-formatted content.

    Similar to Markdown but used in different contexts like headers.
    """

    tag: str = field(default="lark_md", init=False)
    content: str = ""
    text_size: str | None = None
    text_color: str | None = None