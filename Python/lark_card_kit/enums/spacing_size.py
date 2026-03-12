"""Spacing size enumeration."""

from enum import Enum


class SpacingSize(str, Enum):
    """Spacing size enumeration for layout spacing."""

    SMALL = "small"
    MEDIUM = "medium"
    LARGE = "large"