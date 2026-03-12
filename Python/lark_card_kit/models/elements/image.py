"""Image element for displaying images."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element


@dataclass
class Image(Element):
    """Image element for displaying images.

    Displays an image from a URL.
    """

    tag: str = field(default="img", init=False)
    src: str = ""
    alt: str | None = None
    preview_src: str | None = None
    size: str | None = None
    mode: str | None = None
    width: str | None = None
    height: str | None = None
    corner_radius: str | None = None
    hover_tips: Any = None
    behaviors: list[Any] | None = None