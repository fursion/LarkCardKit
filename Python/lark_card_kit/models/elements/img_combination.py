"""ImgCombination element for image combinations."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element


@dataclass
class ImgCombination(Element):
    """Image combination element for combining multiple images.

    Displays multiple images in a combined layout.
    """

    tag: str = field(default="img_combination", init=False)
    images: list[Any] = field(default_factory=list)
    mode: str | None = None
    width: str | None = None