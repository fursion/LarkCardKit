"""Card model - the root model for Lark cards."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .card_config import CardConfig
from .card_header import CardHeader
from .card_body import CardBody
from .card_link import CardLink
from .fallback import Fallback


@dataclass
class Card:
    """Root model for Lark card 2.0 messages.

    Represents a complete Lark card message conforming to the 2.0 specification.
    """

    schema: str = field(default="2.0", init=False)
    config: CardConfig | None = None
    card_link: CardLink | None = None
    header: CardHeader | None = None
    body: CardBody = field(default_factory=CardBody)
    fallback: Fallback | None = None

    def to_dict(self) -> dict[str, Any]:
        """Convert the card to a dictionary for JSON serialization.

        Returns:
            Dictionary representation of the card.
        """
        result: dict[str, Any] = {"schema": self.schema}

        if self.config:
            result["config"] = self.config.to_dict()
        if self.card_link:
            result["card_link"] = self.card_link.to_dict()
        if self.header:
            result["header"] = self.header.to_dict()
        if self.body:
            result["body"] = self.body.to_dict()
        if self.fallback:
            result["fallback"] = self.fallback.to_dict()

        return result

    def to_json(self, indent: int | None = None) -> str:
        """Convert the card to a JSON string.

        Args:
            indent: Number of spaces for indentation. None for compact output.

        Returns:
            JSON string representation of the card.
        """
        import json

        return json.dumps(self.to_dict(), ensure_ascii=False, indent=indent)