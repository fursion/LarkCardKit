"""Card link builder for fluent API with better type hints."""

from __future__ import annotations

from typing import TYPE_CHECKING

from ..models.card_link import CardLink

if TYPE_CHECKING:
    from .card_builder import CardBuilder


class CardLinkBuilder:
    """Card link builder for fluent API.

    Example:
        >>> builder.card_link()
        ...     .url("https://example.com")
        ...     .pc_url("https://pc.example.com")
        ...     .done()
    """

    def __init__(self, card_builder: CardBuilder | None = None) -> None:
        """Initialize the card link builder.

        Args:
            card_builder: Parent card builder for nested building.
        """
        self._link = CardLink()
        self._card_builder = card_builder

    def url(self, url: str) -> CardLinkBuilder:
        """Set the default URL.

        Args:
            url: URL to open when clicking the card.

        Returns:
            Self for method chaining.
        """
        self._link.url = url
        return self

    def pc_url(self, url: str) -> CardLinkBuilder:
        """Set the PC-specific URL.

        Args:
            url: URL to open on PC clients.

        Returns:
            Self for method chaining.
        """
        self._link.pc_url = url
        return self

    def ios_url(self, url: str) -> CardLinkBuilder:
        """Set the iOS-specific URL.

        Args:
            url: URL to open on iOS clients.

        Returns:
            Self for method chaining.
        """
        self._link.ios_url = url
        return self

    def android_url(self, url: str) -> CardLinkBuilder:
        """Set the Android-specific URL.

        Args:
            url: URL to open on Android clients.

        Returns:
            Self for method chaining.
        """
        self._link.android_url = url
        return self

    def done(self) -> CardBuilder:
        """Finish building the link and return to the card builder.

        Returns:
            Parent CardBuilder instance.

        Raises:
            RuntimeError: If not used with a parent CardBuilder.
        """
        if self._card_builder is None:
            raise RuntimeError("done() can only be called when using nested building")
        self._card_builder._set_card_link(self._link)
        return self._card_builder

    def build(self) -> CardLink:
        """Build the card link.

        Returns:
            Built CardLink instance.
        """
        return self._link