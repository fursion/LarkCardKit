"""Table element for displaying tabular data."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element
from .plain_text import PlainText


@dataclass
class TableColumn:
    """Table column definition."""

    name: str = ""
    display_name: PlainText | None = None
    width: str | None = None
    horizontal_align: str | None = None

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        result: dict[str, Any] = {}
        if self.name:
            result["name"] = self.name
        if self.display_name:
            result["display_name"] = self.display_name.to_dict()
        if self.width:
            result["width"] = self.width
        if self.horizontal_align:
            result["horizontal_align"] = self.horizontal_align
        return result


@dataclass
class TableRow:
    """Table row data."""

    cells: list[Any] = field(default_factory=list)

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        return {"cells": [self._serialize_cell(cell) for cell in self.cells]}

    def _serialize_cell(self, cell: Any) -> Any:
        """Serialize a cell value."""
        if hasattr(cell, "to_dict"):
            return cell.to_dict()
        return cell


@dataclass
class Table(Element):
    """Table element for displaying tabular data.

    Displays data in a structured table format.
    """

    tag: str = field(default="table", init=False)
    columns: list[TableColumn] = field(default_factory=list)
    rows: list[TableRow] = field(default_factory=list)
    header_style: Any = None
    page_size: int | None = None
    row_height: str | None = None
    margin: str | None = None