"""Card config builder for fluent API with better type hints."""

from __future__ import annotations

from typing import TYPE_CHECKING, Any

from ..models.card_config import CardConfig, CardStyle

if TYPE_CHECKING:
    from .card_builder import CardBuilder


class CardConfigBuilder:
    """Card config builder for fluent API.

    Example:
        >>> builder.config()
        ...     .update_multi(True)
        ...     .enable_forward(True)
        ...     .done()
    """

    def __init__(self, card_builder: CardBuilder | None = None) -> None:
        """Initialize the card config builder.

        Args:
            card_builder: Parent card builder for nested building.
        """
        self._config = CardConfig()
        self._card_builder = card_builder

    def update_multi(self, value: bool) -> CardConfigBuilder:
        """Set whether to enable multi-message updates.

        Args:
            value: Whether to enable multi-message updates.

        Returns:
            Self for method chaining.
        """
        self._config.update_multi = value
        return self

    def streaming_mode(self, value: bool) -> CardConfigBuilder:
        """Set whether to enable streaming mode.

        Args:
            value: Whether to enable streaming mode.

        Returns:
            Self for method chaining.
        """
        self._config.streaming_mode = value
        return self

    def enable_forward(self, value: bool) -> CardConfigBuilder:
        """Set whether to enable forwarding.

        Args:
            value: Whether to enable forwarding.

        Returns:
            Self for method chaining.
        """
        self._config.enable_forward = value
        return self

    def width_mode(self, mode: str) -> CardConfigBuilder:
        """Set the width mode.

        Args:
            mode: Width mode (e.g., 'auto', 'fill').

        Returns:
            Self for method chaining.
        """
        self._config.width_mode = mode
        return self

    def locales(self, locales: list[str]) -> CardConfigBuilder:
        """Set the supported locales.

        Args:
            locales: List of locale codes.

        Returns:
            Self for method chaining.
        """
        self._config.locales = locales
        return self

    def style(self, *, header: dict[str, Any] | None = None, body: dict[str, Any] | None = None) -> CardConfigBuilder:
        """Set the card style.

        Args:
            header: Header style configuration.
            body: Body style configuration.

        Returns:
            Self for method chaining.
        """
        self._config.style = CardStyle(header=header, body=body)
        return self

    def done(self) -> CardBuilder:
        """Finish building the config and return to the card builder.

        Returns:
            Parent CardBuilder instance.

        Raises:
            RuntimeError: If not used with a parent CardBuilder.
        """
        if self._card_builder is None:
            raise RuntimeError("done() can only be called when using nested building")
        self._card_builder._set_config(self._config)
        return self._card_builder

    def build(self) -> CardConfig:
        """Build the card config.

        Returns:
            Built CardConfig instance.
        """
        return self._config