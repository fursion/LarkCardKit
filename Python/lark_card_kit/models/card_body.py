"""CardBody model for card body configuration."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any


@dataclass
class CardBody:
    """Card body configuration.

    Contains the main content elements of the card.
    """

    elements: list[Any] = field(default_factory=list)
    vertical_spacing: str | None = None
    horizontal_spacing: str | None = None
    padding: str | None = None
    direction: str | None = None
    horizontal_align: str | None = None
    vertical_align: str | None = None

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        result: dict[str, Any] = {
            "elements": [
                elem.to_dict() if hasattr(elem, "to_dict") else elem
                for elem in self.elements
            ]
        }

        if self.vertical_spacing:
            result["vertical_spacing"] = self.vertical_spacing
        if self.horizontal_spacing:
            result["horizontal_spacing"] = self.horizontal_spacing
        if self.padding:
            result["padding"] = self.padding
        if self.direction:
            result["direction"] = self.direction
        if self.horizontal_align:
            result["horizontal_align"] = self.horizontal_align
        if self.vertical_align:
            result["vertical_align"] = self.vertical_align

        return result