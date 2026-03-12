"""PersonList element for displaying multiple persons."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element


@dataclass
class PersonList(Element):
    """Person list element for displaying multiple people.

    Shows multiple people's avatars and names.
    """

    tag: str = field(default="person_list", init=False)
    persons: list[Any] = field(default_factory=list)
    show_name: bool | None = None
    show_avatar: bool | None = None
    size: str | None = None