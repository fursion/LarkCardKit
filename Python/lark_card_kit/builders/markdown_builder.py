"""Markdown builder for fluent API."""

from __future__ import annotations

from ..models.elements import Markdown


class MarkdownBuilder:
    """Markdown builder for fluent API."""

    def __init__(self) -> None:
        """Initialize the markdown builder."""
        self._markdown = Markdown()

    def content(self, content: str) -> MarkdownBuilder:
        """Set the markdown content.

        Args:
            content: Markdown content.

        Returns:
            Self for method chaining.
        """
        self._markdown.content = content
        return self

    def size(self, size: str) -> MarkdownBuilder:
        """Set the text size.

        Args:
            size: Text size.

        Returns:
            Self for method chaining.
        """
        self._markdown.text_size = size
        return self

    def color(self, color: str) -> MarkdownBuilder:
        """Set the text color.

        Args:
            color: Text color.

        Returns:
            Self for method chaining.
        """
        self._markdown.text_color = color
        return self

    def element_id(self, element_id: str) -> MarkdownBuilder:
        """Set the element ID.

        Args:
            element_id: Element ID.

        Returns:
            Self for method chaining.
        """
        self._markdown.element_id = element_id
        return self

    def build(self) -> Markdown:
        """Build the markdown element.

        Returns:
            Built Markdown instance.
        """
        return self._markdown