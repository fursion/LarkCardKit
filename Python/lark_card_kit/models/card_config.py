"""CardConfig model for card configuration."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any


@dataclass
class StreamingSummary:
    """Streaming summary configuration."""

    content: str = "生成中"
    i18n_content: dict[str, str] | None = None

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        result: dict[str, Any] = {"content": self.content}
        if self.i18n_content:
            result["i18n_content"] = self.i18n_content
        return result


@dataclass
class CardStyle:
    """Card style configuration."""

    header: dict[str, Any] | None = None
    body: dict[str, Any] | None = None

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        result: dict[str, Any] = {}
        if self.header:
            result["header"] = self.header
        if self.body:
            result["body"] = self.body
        return result


@dataclass
class StreamingConfig:
    """Streaming configuration for real-time updates."""

    layout: str | None = None
    thinking_process: dict[str, Any] | None = None

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        result: dict[str, Any] = {}
        if self.layout:
            result["layout"] = self.layout
        if self.thinking_process:
            result["thinking_process"] = self.thinking_process
        return result


@dataclass
class CardConfig:
    """Card configuration settings.

    Contains update strategy, streaming mode, and other configuration options.
    """

    update_multi: bool = True
    streaming_mode: bool | None = None
    streaming_config: StreamingConfig | None = None
    summary: StreamingSummary | None = None
    locales: list[str] | None = None
    enable_forward: bool | None = None
    width_mode: str | None = None
    use_custom_translation: bool | None = None
    enable_forward_interaction: bool | None = None
    style: CardStyle | None = None

    def to_dict(self) -> dict[str, Any]:
        """Convert to dictionary."""
        result: dict[str, Any] = {"update_multi": self.update_multi}

        if self.streaming_mode is not None:
            result["streaming_mode"] = self.streaming_mode
        if self.streaming_config:
            result["streaming_config"] = self.streaming_config.to_dict()
        if self.summary:
            result["summary"] = self.summary.to_dict()
        if self.locales:
            result["locales"] = self.locales
        if self.enable_forward is not None:
            result["enable_forward"] = self.enable_forward
        if self.width_mode:
            result["width_mode"] = self.width_mode
        if self.use_custom_translation is not None:
            result["use_custom_translation"] = self.use_custom_translation
        if self.enable_forward_interaction is not None:
            result["enable_forward_interaction"] = self.enable_forward_interaction
        if self.style:
            result["style"] = self.style.to_dict()

        return result