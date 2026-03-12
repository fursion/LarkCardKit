"""Button size enumeration."""

from enum import Enum


class ButtonSize(str, Enum):
    """Button size enumeration for button sizing."""

    TINY = "tiny"
    SMALL = "small"
    MEDIUM = "medium"
    LARGE = "large"