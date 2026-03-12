"""JSON serializer utilities."""

from __future__ import annotations

import json
from typing import Any


def to_json(obj: Any, indent: int | None = None, ensure_ascii: bool = False) -> str:
    """Serialize an object to JSON.

    Args:
        obj: Object to serialize. Can be a model with to_dict() method or a dict.
        indent: Number of spaces for indentation. None for compact output.
        ensure_ascii: Whether to escape non-ASCII characters.

    Returns:
        JSON string representation.
    """
    if hasattr(obj, "to_dict"):
        data = obj.to_dict()
    elif isinstance(obj, dict):
        data = obj
    else:
        data = obj

    return json.dumps(data, ensure_ascii=ensure_ascii, indent=indent)


def to_dict(obj: Any) -> dict[str, Any]:
    """Convert an object to a dictionary.

    Args:
        obj: Object to convert. Can be a model with to_dict() method or a dict.

    Returns:
        Dictionary representation.
    """
    if hasattr(obj, "to_dict"):
        return obj.to_dict()
    if isinstance(obj, dict):
        return obj
    raise ValueError(f"Cannot convert {type(obj)} to dict")