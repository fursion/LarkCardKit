"""Fallback builder for fluent API with better type hints."""

from __future__ import annotations

from typing import TYPE_CHECKING

from ..models.fallback import Fallback
from ..models.elements import PlainText

if TYPE_CHECKING:
    from .card_builder import CardBuilder


class FallbackBuilder:
    """Fallback builder for fluent API.

    Example:
        >>> builder.fallback()
        ...     .title("Fallback Title")
        ...     .content("Fallback content text")
        ...     .done()
    """

    def __init__(self, card_builder: CardBuilder | None = None) -> None:
        """Initialize the fallback builder.

        Args:
            card_builder: Parent card builder for nested building.
        """
        self._fallback = Fallback()
        self._card_builder = card_builder

    def title(self, content: str) -> FallbackBuilder:
        """Set the fallback title.

        Args:
            content: Title content.

        Returns:
            Self for method chaining.
        """
        self._fallback.title = PlainText(content=content)
        return self

    def content(self, content: str) -> FallbackBuilder:
        """Set the fallback content.

        Args:
            content: Fallback content.

        Returns:
            Self for method chaining.
        """
        self._fallback.content = PlainText(content=content)
        return self

    def done(self) -> CardBuilder:
        """Finish building the fallback and return to the card builder.

        Returns:
            Parent CardBuilder instance.

        Raises:
            RuntimeError: If not used with a parent CardBuilder.
        """
        if self._card_builder is None:
            raise RuntimeError("done() can only be called when using nested building")
        self._card_builder._set_fallback(self._fallback)
        return self._card_builder

    def build(self) -> Fallback:
        """Build the fallback.

        Returns:
            Built Fallback instance.
        """
        return self._fallback