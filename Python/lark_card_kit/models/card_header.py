"""CardHeader model for card header configuration."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any


@dataclass
class HeaderIcon:
    """Header icon configuration."""

    tag: str = "standard_icon"
    token: str | None = None
    color: str | None = None
    img_key: str | None = None

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        result: dict[str, Any] = {"tag": self.tag}
        if self.token:
            result["token"] = self.token
        if self.color:
            result["color"] = self.color
        if self.img_key:
            result["img_key"] = self.img_key
        return result


@dataclass
class CardHeader:
    """Card header configuration.

    Displays the card title and optional subtitle at the top of the card.
    """

    title: Any = None  # PlainText or MarkdownText
    subtitle: Any = None  # PlainText or MarkdownText
    text_tag_list: list[Any] | None = None
    template: str | None = None
    icon: HeaderIcon | None = None
    padding: str | None = None

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        result: dict[str, Any] = {}

        if self.title:
            result["title"] = self.title.to_dict() if hasattr(self.title, "to_dict") else self.title
        if self.subtitle:
            result["subtitle"] = self.subtitle.to_dict() if hasattr(self.subtitle, "to_dict") else self.subtitle
        if self.text_tag_list:
            result["text_tag_list"] = [
                tag.to_dict() if hasattr(tag, "to_dict") else tag
                for tag in self.text_tag_list
            ]
        if self.template:
            result["template"] = self.template
        if self.icon:
            result["icon"] = self.icon.to_dict()
        if self.padding:
            result["padding"] = self.padding

        return result