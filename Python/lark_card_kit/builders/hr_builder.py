"""Horizontal rule builder for fluent API."""

from __future__ import annotations

from ..models.elements import Hr


class HrBuilder:
    """Horizontal rule builder for fluent API."""

    def __init__(self) -> None:
        """Initialize the HR builder."""
        self._hr = Hr()

    def element_id(self, element_id: str) -> HrBuilder:
        """Set the element ID.

        Args:
            element_id: Element ID.

        Returns:
            Self for method chaining.
        """
        self._hr.element_id = element_id
        return self

    def margin(self, margin: str) -> HrBuilder:
        """Set the margin.

        Args:
            margin: Margin value.

        Returns:
            Self for method chaining.
        """
        self._hr.margin = margin
        return self

    def build(self) -> Hr:
        """Build the horizontal rule element.

        Returns:
            Built Hr instance.
        """
        return self._hr