"""卡片头部构建器，提供流畅 API，支持完美的类型提示。

本模块只支持两种构建方式：
1. 上下文管理器（推荐用于嵌套结构）
2. 方法链 + done()（推荐用于简单结构）
"""

from __future__ import annotations

from typing import TYPE_CHECKING, Any, Self

from ..models.card_header import CardHeader, HeaderIcon
from ..models.elements import PlainText, MarkdownText, TextTag

if TYPE_CHECKING:
    from .card_builder import CardBuilder


class CardHeaderBuilder:
    """卡片头部构建器，提供流畅 API。

    示例:
        >>> # 方式一：上下文管理器
        >>> with builder.header() as h:
        ...     h.title("Welcome").template("blue")
        >>>
        >>> # 方式二：方法链 + done()
        >>> builder.header().title("Welcome").template("blue").done()
    """

    def __init__(self, card_builder: CardBuilder | None = None) -> None:
        """初始化卡片头部构建器。

        参数:
            card_builder: 父卡片构建器，用于嵌套构建。
        """
        self._header = CardHeader()
        self._card_builder = card_builder

    def __enter__(self) -> Self:
        """进入上下文管理器。

        返回:
            自身，供上下文内使用。
        """
        return self

    def __exit__(self, *args: Any) -> None:
        """退出上下文管理器，将头部应用到父构建器。"""
        if self._card_builder is not None:
            self._card_builder._set_header(self._header)

    def title(self, content: str) -> Self:
        """设置标题为纯文本。

        参数:
            content: 标题内容。

        返回:
            自身，用于链式调用。
        """
        self._header.title = PlainText(content=content)
        return self

    def title_markdown(self, content: str) -> Self:
        """设置标题为 Markdown 格式。

        参数:
            content: Markdown 标题内容。

        返回:
            自身，用于链式调用。
        """
        self._header.title = MarkdownText(content=content)
        return self

    def subtitle(self, content: str) -> Self:
        """设置副标题为纯文本。

        参数:
            content: 副标题内容。

        返回:
            自身，用于链式调用。
        """
        self._header.subtitle = PlainText(content=content)
        return self

    def subtitle_markdown(self, content: str) -> Self:
        """设置副标题为 Markdown 格式。

        参数:
            content: Markdown 副标题内容。

        返回:
            自身，用于链式调用。
        """
        self._header.subtitle = MarkdownText(content=content)
        return self

    def template(self, template: str) -> Self:
        """设置头部模板样式。

        参数:
            template: 模板样式（如 'blue', 'green', 'red', 'wathet', 'turquoise', 'yellow', 'orange', 'carmine', 'violet', 'purple', 'indigo', 'grey', 'default'）。

        返回:
            自身，用于链式调用。
        """
        self._header.template = template
        return self

    def icon(self, token: str, color: str | None = None) -> Self:
        """使用 token 设置头部图标。

        参数:
            token: 图标 token（如 'chat-forbidden', 'calendar'）。
            color: 图标颜色（如 'blue', 'green'）。

        返回:
            自身，用于链式调用。
        """
        self._header.icon = HeaderIcon(token=token, color=color)
        return self

    def icon_image(self, img_key: str) -> Self:
        """使用自定义图片设置头部图标。

        参数:
            img_key: 自定义图片 key。

        返回:
            自身，用于链式调用。
        """
        self._header.icon = HeaderIcon(img_key=img_key)
        return self

    def text_tag(self, text: str, color: str | None = None) -> Self:
        """向头部添加文本标签。

        参数:
            text: 标签文本。
            color: 标签颜色。

        返回:
            自身，用于链式调用。
        """
        if self._header.text_tag_list is None:
            self._header.text_tag_list = []

        self._header.text_tag_list.append(TextTag(text=text, color=color))
        return self

    def padding(self, padding: str) -> Self:
        """设置头部内边距。

        参数:
            padding: 内边距值（如 '10px', 'default'）。

        返回:
            自身，用于链式调用。
        """
        self._header.padding = padding
        return self

    def done(self) -> CardBuilder:
        """完成头部构建，返回卡片构建器。

        返回:
            父 CardBuilder 实例。

        异常:
            RuntimeError: 如果没有使用父 CardBuilder。
        """
        if self._card_builder is None:
            raise RuntimeError("done() 只能在嵌套构建时调用")
        self._card_builder._set_header(self._header)
        return self._card_builder

    def build(self) -> CardHeader:
        """构建卡片头部。

        返回:
            构建好的 CardHeader 实例。
        """
        return self._header