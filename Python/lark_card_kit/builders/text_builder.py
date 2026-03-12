"""Text builder for plain text and markdown text elements."""

from __future__ import annotations

from ..models.elements import PlainText, MarkdownText


class TextBuilder:
    """Text builder for creating text elements.

    Supports both plain text and markdown text.
    """

    def __init__(self, use_markdown: bool = False) -> None:
        """Initialize the text builder.

        Args:
            use_markdown: Whether to use markdown format.
        """
        self._use_markdown = use_markdown
        self._content = ""
        self._text_size: str | None = None
        self._text_color: str | None = None
        self._text_align: str | None = None

    def content(self, content: str) -> TextBuilder:
        """Set the text content.

        Args:
            content: Text content.

        Returns:
            Self for method chaining.
        """
        self._content = content
        return self

    def size(self, size: str) -> TextBuilder:
        """Set the text size.

        Args:
            size: Text size (e.g., 'normal', 'small', 'large').

        Returns:
            Self for method chaining.
        """
        self._text_size = size
        return self

    def color(self, color: str) -> TextBuilder:
        """Set the text color.

        Args:
            color: Text color.

        Returns:
            Self for method chaining.
        """
        self._text_color = color
        return self

    def align(self, align: str) -> TextBuilder:
        """Set the text alignment.

        Args:
            align: Text alignment (e.g., 'left', 'center', 'right').

        Returns:
            Self for method chaining.
        """
        self._text_align = align
        return self

    def build(self) -> PlainText | MarkdownText:
        """Build the text element.

        Returns:
            Built text element.
        """
        if self._use_markdown:
            return MarkdownText(
                content=self._content,
                text_size=self._text_size,
                text_color=self._text_color,
            )
        return PlainText(
            content=self._content,
            text_size=self._text_size,
            text_color=self._text_color,
            text_align=self._text_align,
        )