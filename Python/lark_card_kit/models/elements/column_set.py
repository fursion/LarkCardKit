"""ColumnSet and Column elements for multi-column layouts."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element


@dataclass
class Column(Element):
    """Column element for column set.

    Contains a list of elements arranged vertically.
    """

    tag: str = field(default="column", init=False)
    width: str | None = None
    vertical_align: str | None = None
    elements: list[Any] = field(default_factory=list)


@dataclass
class ColumnSet(Element):
    """Column set element for multi-column layouts.

    Contains multiple columns arranged horizontally.
    """

    tag: str = field(default="column_set", init=False)
    columns: list[Column] = field(default_factory=list)
    margin: str | None = None
    background_style: str | None = None
    horizontal_spacing: str | None = None