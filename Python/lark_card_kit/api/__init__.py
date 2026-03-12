"""API 模块，提供便捷的工厂函数。

本模块提供快速创建元素的工厂函数，无需使用完整的构建器模式。

示例:
    >>> from lark_card_kit.api import card, button, plain_text
    >>> from lark_card_kit import ButtonType
    >>>
    >>> # 快速创建卡片
    >>> my_card = (card(title="Welcome", template="blue")
    ...     .body(lambda b: b
    ...         .add_element(plain_text("Hello"))
    ...         .add_element(button("Click", type=ButtonType.PRIMARY)))
    ...     .build())
"""

from .factory import (
    card,
    button,
    plain_text,
    markdown,
    image,
    text_tag,
    hr,
)

__all__ = [
    "card",
    "button",
    "plain_text",
    "markdown",
    "image",
    "text_tag",
    "hr",
]