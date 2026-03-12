"""Align type enumeration."""

from enum import Enum


class AlignType(str, Enum):
    """Align type enumeration for alignment."""

    LEFT = "left"
    CENTER = "center"
    RIGHT = "right"