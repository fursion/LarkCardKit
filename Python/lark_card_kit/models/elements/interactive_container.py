"""InteractiveContainer element for interactive wrappers."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element


@dataclass
class InteractiveContainer(Element):
    """Interactive container element for making content interactive.

    Wraps other elements to add interactive behaviors.
    """

    tag: str = field(default="interactive_container", init=False)
    elements: list[Any] = field(default_factory=list)
    behaviors: list[Any] | None = None
    padding: str | None = None
    margin: str | None = None
    background_style: str | None = None