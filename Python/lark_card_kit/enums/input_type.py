"""Input type enumeration."""

from enum import Enum


class InputType(str, Enum):
    """Input type enumeration for input fields."""

    TEXT = "text"
    PASSWORD = "password"
    NUMBER = "number"
    EMAIL = "email"