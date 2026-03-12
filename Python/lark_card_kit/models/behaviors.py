"""Behavior models for interactive elements."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any


@dataclass
class CallbackBehavior:
    """Callback behavior for button actions.

    Sends a callback to the bot when triggered.
    """

    type: str = "callback"
    value: dict[str, Any] = field(default_factory=dict)

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        result: dict[str, Any] = {"type": self.type}
        if self.value:
            result["value"] = self.value
        return result


@dataclass
class OpenUrlBehavior:
    """Open URL behavior for button actions.

    Opens a URL when triggered.
    """

    type: str = "open_url"
    url: str = ""

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        return {"type": self.type, "url": self.url}


# Type alias for behavior types
Behaviors = CallbackBehavior | OpenUrlBehavior