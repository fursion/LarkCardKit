"""Test initialization."""

from lark_card_kit import __version__


def test_version() -> None:
    """Test that the version is defined."""
    assert __version__ == "0.1.0"