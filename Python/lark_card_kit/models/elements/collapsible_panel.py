"""CollapsiblePanel element for expandable content."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element


@dataclass
class CollapsiblePanelHeader:
    """Header for collapsible panel."""

    title: Any = None
    icon: str | None = None
    expanded_icon: str | None = None

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        result: dict[str, Any] = {}
        if self.title:
            result["title"] = self.title.to_dict() if hasattr(self.title, "to_dict") else self.title
        if self.icon:
            result["icon"] = self.icon
        if self.expanded_icon:
            result["expanded_icon"] = self.expanded_icon
        return result


@dataclass
class CollapsiblePanel(Element):
    """Collapsible panel element for expandable content.

    Allows content to be shown/hidden by expanding/collapsing.
    """

    tag: str = field(default="collapsible_panel", init=False)
    header: CollapsiblePanelHeader | None = None
    elements: list[Any] = field(default_factory=list)
    expanded: bool | None = None
    padding: str | None = None