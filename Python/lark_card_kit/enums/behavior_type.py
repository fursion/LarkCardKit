"""Behavior type enumeration."""

from enum import Enum


class BehaviorType(str, Enum):
    """Behavior type enumeration for interactive behaviors."""

    CALLBACK = "callback"
    OPEN_URL = "open_url"