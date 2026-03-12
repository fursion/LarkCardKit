"""Button type enumeration."""

from enum import Enum


class ButtonType(str, Enum):
    """Button type enumeration for button styling."""

    PRIMARY = "primary"
    DEFAULT = "default"
    DANGER = "danger"
    TEXT = "text"