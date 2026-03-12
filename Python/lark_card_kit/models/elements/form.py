"""Form element for form container."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element


@dataclass
class Form(Element):
    """Form container element.

    Groups input elements together for form submission.
    """

    tag: str = field(default="form", init=False)
    name: str = ""
    direction: str | None = None
    padding: str | None = None
    vertical_spacing: str | None = None
    horizontal_spacing: str | None = None
    horizontal_align: str | None = None
    vertical_align: str | None = None
    elements: list[Any] = field(default_factory=list)