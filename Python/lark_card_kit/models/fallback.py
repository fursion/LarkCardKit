"""Fallback model for fallback content."""

from __future__ import annotations

from dataclasses import dataclass
from typing import Any


@dataclass
class Fallback:
    """Fallback content for clients that don't support card 2.0.

    Provides alternative text content when card rendering is not available.
    """

    title: Any = None  # PlainText
    content: Any = None  # PlainText

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        result: dict[str, Any] = {}

        if self.title:
            result["title"] = self.title.to_dict() if hasattr(self.title, "to_dict") else self.title
        if self.content:
            result["content"] = self.content.to_dict() if hasattr(self.content, "to_dict") else self.content

        return result