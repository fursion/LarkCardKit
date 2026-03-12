"""Image size enumeration."""

from enum import Enum


class ImageSize(str, Enum):
    """Image size enumeration for image elements."""

    SMALL = "small"
    MEDIUM = "medium"
    LARGE = "large"