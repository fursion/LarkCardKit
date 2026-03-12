"""Plain text builder for fluent API."""

from __future__ import annotations

from ..models.elements import PlainText


class PlainTextBuilder:
    """Plain text builder for fluent API."""

    def __init__(self) -> None:
        """Initialize the plain text builder."""
        self._text = PlainText()

    def content(self, content: str) -> PlainTextBuilder:
        """Set the text content.

        Args:
            content: Text content.

        Returns:
            Self for method chaining.
        """
        self._text.content = content
        return self

    def size(self, size: str) -> PlainTextBuilder:
        """Set the text size.

        Args:
            size: Text size.

        Returns:
            Self for method chaining.
        """
        self._text.text_size = size
        return self

    def color(self, color: str) -> PlainTextBuilder:
        """Set the text color.

        Args:
            color: Text color.

        Returns:
            Self for method chaining.
        """
        self._text.text_color = color
        return self

    def align(self, align: str) -> PlainTextBuilder:
        """Set the text alignment.

        Args:
            align: Text alignment.

        Returns:
            Self for method chaining.
        """
        self._text.text_align = align
        return self

    def notation(self, notation: bool) -> PlainTextBuilder:
        """Set whether to enable notation.

        Args:
            notation: Whether to enable notation.

        Returns:
            Self for method chaining.
        """
        self._text.notation = notation
        return self

    def width(self, width: str) -> PlainTextBuilder:
        """Set the text width.

        Args:
            width: Text width.

        Returns:
            Self for method chaining.
        """
        self._text.width = width
        return self

    def element_id(self, element_id: str) -> PlainTextBuilder:
        """Set the element ID.

        Args:
            element_id: Element ID.

        Returns:
            Self for method chaining.
        """
        self._text.element_id = element_id
        return self

    def build(self) -> PlainText:
        """Build the plain text element.

        Returns:
            Built PlainText instance.
        """
        return self._text