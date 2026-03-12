"""Loop element for repeating content."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element


@dataclass
class Loop(Element):
    """Loop element for repeating template content.

    Repeats a template element for each item in data.
    """

    tag: str = field(default="loop", init=False)
    template: Any = None
    data_key: str | None = None