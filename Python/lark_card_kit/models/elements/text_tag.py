"""TextTag element for status tags."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element


@dataclass
class TextTag(Element):
    """Text tag element for status indicators.

    Displays a colored tag/label for status indication.
    """

    tag: str = field(default="text_tag", init=False)
    text: str | None = None
    color: str | None = None
    size: str | None = None
    icon: str | None = None

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary for JSON serialization.

        The text field should be a PlainText object for the API.
        """
        result = {"tag": self.tag}

        if self.text is not None:
            result["text"] = {"tag": "plain_text", "content": self.text}

        if self.color is not None:
            result["color"] = self.color

        if self.size is not None:
            result["size"] = self.size

        if self.icon is not None:
            result["icon"] = self.icon

        if self.element_id is not None:
            result["element_id"] = self.element_id

        return result