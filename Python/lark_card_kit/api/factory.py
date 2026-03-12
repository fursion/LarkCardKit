"""工厂函数，用于快速创建元素。

本模块提供便捷的工厂函数，用于创建常用的卡片元素，无需使用完整的构建器模式。

示例:
    >>> from lark_card_kit.api import card, button, plain_text
    >>> from lark_card_kit import ButtonType
    >>>
    >>> # 快速创建卡片（使用上下文管理器）
    >>> with card(title="Welcome", template="blue") as builder:
    ...     with builder.body() as b:
    ...         b.add_element(plain_text("Hello, World!"))
    ...         b.add_element(button("Click Me", type=ButtonType.PRIMARY))
    >>> my_card = builder.build()
"""

from __future__ import annotations

from typing import Any

from ..builders import CardBuilder
from ..enums import ButtonType, ButtonSize
from ..models.elements import (
    PlainText,
    Markdown,
    Button,
    Image,
    TextTag,
    Hr,
)


def card(
    *,
    title: str | None = None,
    title_markdown: str | None = None,
    subtitle: str | None = None,
    subtitle_markdown: str | None = None,
    template: str | None = None,
) -> CardBuilder:
    """快速创建带可选头部的卡片。

    这是一个便捷函数，创建一个 CardBuilder 并可选择性地配置头部标题和模板。

    参数:
        title: 可选的纯文本标题。
        title_markdown: 可选的 Markdown 标题（优先于 title）。
        subtitle: 可选的纯文本副标题。
        subtitle_markdown: 可选的 Markdown 副标题（优先于 subtitle）。
        template: 可选的模板样式（如 'blue', 'green', 'red'）。

    返回:
        CardBuilder 实例，可用于进一步配置。

    示例:
        >>> from lark_card_kit.api import card
        >>>
        >>> # 带标题的简单卡片
        >>> with card(title="Welcome") as builder:
        ...     with builder.body() as b:
        ...         b.plain_text("Hello")
        >>> my_card = builder.build()
        >>>
        >>> # 带模板的卡片
        >>> with card(title="Alert", template="red") as builder:
        ...     with builder.body() as b:
        ...         b.plain_text("Warning!")
        >>> my_card = builder.build()
    """
    builder = CardBuilder.create()

    if title is not None or title_markdown is not None or subtitle is not None or subtitle_markdown is not None or template is not None:
        with builder.header() as h:
            if title_markdown is not None:
                h.title_markdown(title_markdown)
            elif title is not None:
                h.title(title)
            if subtitle_markdown is not None:
                h.subtitle_markdown(subtitle_markdown)
            elif subtitle is not None:
                h.subtitle(subtitle)
            if template is not None:
                h.template(template)

    return builder


def button(
    text: str,
    *,
    type: ButtonType | str = ButtonType.DEFAULT,
    size: ButtonSize | str | None = None,
    callback: dict[str, Any] | None = None,
    url: str | None = None,
    disabled: bool = False,
    width: str | None = None,
    name: str | None = None,
) -> Button:
    """快速创建按钮元素。

    参数:
        text: 按钮文本内容。
        type: 按钮类型（primary, default, danger, text）。默认为 DEFAULT。
        size: 可选的按钮大小（tiny, small, medium, large）。
        callback: 可选的回调值字典。
        url: 可选的点击时打开的 URL。
        disabled: 是否禁用按钮。默认为 False。
        width: 可选的按钮宽度（如 '100px', 'auto'）。
        name: 可选的按钮名称（用于表单提交）。

    返回:
        Button 元素实例。

    示例:
        >>> from lark_card_kit.api import button
        >>> from lark_card_kit import ButtonType
        >>>
        >>> # 简单按钮
        >>> btn = button("Click Me")
        >>>
        >>> # 带回调的主要按钮
        >>> btn = button("Submit", type=ButtonType.PRIMARY, callback={"action": "submit"})
        >>>
        >>> # 打开 URL 的按钮
        >>> btn = button("Learn More", url="https://example.com")
    """
    btn = Button()
    btn.text = PlainText(content=text)
    btn.type = type.value if isinstance(type, ButtonType) else type

    if size is not None:
        btn.size = size.value if isinstance(size, ButtonSize) else size
    if callback is not None:
        from ..models.behaviors import CallbackBehavior
        btn.behaviors = [CallbackBehavior(value=callback)]
    if url is not None:
        from ..models.behaviors import OpenUrlBehavior
        if btn.behaviors is None:
            btn.behaviors = []
        btn.behaviors.append(OpenUrlBehavior(url=url))
    if disabled:
        btn.disabled = True
    if width is not None:
        btn.width = width
    if name is not None:
        btn.name = name

    return btn


def plain_text(
    content: str,
    *,
    size: str | None = None,
    color: str | None = None,
) -> PlainText:
    """快速创建纯文本元素。

    参数:
        content: 文本内容。
        size: 可选的文本大小。
        color: 可选的文本颜色。

    返回:
        PlainText 元素实例。

    示例:
        >>> from lark_card_kit.api import plain_text
        >>>
        >>> text = plain_text("Hello, World!")
        >>> colored = plain_text("Important!", color="red")
    """
    return PlainText(content=content, text_size=size, text_color=color)


def markdown(
    content: str,
    *,
    size: str | None = None,
    color: str | None = None,
) -> Markdown:
    """快速创建 Markdown 元素。

    参数:
        content: Markdown 内容。
        size: 可选的文本大小。
        color: 可选的文本颜色。

    返回:
        Markdown 元素实例。

    示例:
        >>> from lark_card_kit.api import markdown
        >>>
        >>> md = markdown("**Bold** and *italic*")
    """
    return Markdown(content=content, text_size=size, text_color=color)


def image(
    src: str,
    *,
    alt: str | None = None,
    size: str | None = None,
    width: str | None = None,
    height: str | None = None,
) -> Image:
    """快速创建图片元素。

    参数:
        src: 图片源 URL。
        alt: 可选的替代文本。
        size: 可选的图片大小（small, medium, large）。
        width: 可选的图片宽度。
        height: 可选的图片高度。

    返回:
        Image 元素实例。

    示例:
        >>> from lark_card_kit.api import image
        >>>
        >>> img = image("https://example.com/photo.png", alt="Photo")
    """
    img = Image()
    img.src = src
    if alt is not None:
        img.alt = alt
    if size is not None:
        img.size = size
    if width is not None:
        img.width = width
    if height is not None:
        img.height = height
    return img


def text_tag(
    text: str,
    *,
    color: str | None = None,
) -> TextTag:
    """快速创建文本标签元素。

    参数:
        text: 标签文本。
        color: 可选的标签颜色。

    返回:
        TextTag 元素实例。

    示例:
        >>> from lark_card_kit.api import text_tag
        >>>
        >>> tag = text_tag("New", color="blue")
    """
    return TextTag(text=text, color=color)


def hr() -> Hr:
    """快速创建分割线元素。

    返回:
        Hr 元素实例。

    示例:
        >>> from lark_card_kit.api import hr
        >>>
        >>> divider = hr()
    """
    return Hr()


__all__ = [
    "card",
    "button",
    "plain_text",
    "markdown",
    "image",
    "text_tag",
    "hr",
]