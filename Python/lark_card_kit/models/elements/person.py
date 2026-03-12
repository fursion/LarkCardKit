"""Person element for displaying person information."""

from __future__ import annotations

from dataclasses import dataclass, field

from .base import Element


@dataclass
class Person(Element):
    """Person element for displaying a person's information.

    Shows a person's avatar, name, and other details.
    """

    tag: str = field(default="person", init=False)
    user_id: str | None = None
    name: str | None = None
    avatar: str | None = None
    size: str | None = None