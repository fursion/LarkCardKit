"""CardLink model for card link configuration."""

from __future__ import annotations

from dataclasses import dataclass
from typing import Any


@dataclass
class CardLink:
    """Card link configuration.

    Defines URLs for navigation when clicking the card.
    """

    url: str | None = None
    pc_url: str | None = None
    ios_url: str | None = None
    android_url: str | None = None

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        result: dict[str, Any] = {}

        if self.url:
            result["url"] = self.url
        if self.pc_url:
            result["pc_url"] = self.pc_url
        if self.ios_url:
            result["ios_url"] = self.ios_url
        if self.android_url:
            result["android_url"] = self.android_url

        return result