"""Text tag builder for fluent API."""

from __future__ import annotations

from ..models.elements import TextTag


class TextTagBuilder:
    """Text tag builder for fluent API."""

    def __init__(self) -> None:
        """Initialize the text tag builder."""
        self._tag = TextTag()

    def text(self, text: str) -> TextTagBuilder:
        """Set the tag text.

        Args:
            text: Tag text.

        Returns:
            Self for method chaining.
        """
        self._tag.text = text
        return self

    def color(self, color: str) -> TextTagBuilder:
        """Set the tag color.

        Args:
            color: Tag color.

        Returns:
            Self for method chaining.
        """
        self._tag.color = color
        return self

    def size(self, size: str) -> TextTagBuilder:
        """Set the tag size.

        Args:
            size: Tag size.

        Returns:
            Self for method chaining.
        """
        self._tag.size = size
        return self

    def icon(self, icon: str) -> TextTagBuilder:
        """Set the tag icon.

        Args:
            icon: Tag icon token.

        Returns:
            Self for method chaining.
        """
        self._tag.icon = icon
        return self

    def element_id(self, element_id: str) -> TextTagBuilder:
        """Set the element ID.

        Args:
            element_id: Element ID.

        Returns:
            Self for method chaining.
        """
        self._tag.element_id = element_id
        return self

    def build(self) -> TextTag:
        """Build the text tag element.

        Returns:
            Built TextTag instance.
        """
        return self._tag