"""Chart element for data visualization."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any

from .base import Element


@dataclass
class Chart(Element):
    """Chart element for data visualization.

    Displays charts and graphs for data presentation.
    """

    tag: str = field(default="chart", init=False)
    chart_type: str | None = None
    data: dict[str, Any] | None = None
    width: str | None = None
    height: str | None = None
    title: str | None = None