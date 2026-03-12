"""Div 构建器，提供流畅 API，支持完美的类型提示。

本模块只支持两种构建方式：
1. 上下文管理器（推荐用于嵌套结构）
2. 方法链 + done()（推荐用于简单结构）
"""

from __future__ import annotations

from typing import TYPE_CHECKING, Any, Self

from ..models.elements import Div

if TYPE_CHECKING:
    from .card_body_builder import CardBodyBuilder


class DivBuilder:
    """Div 构建器，提供流畅 API。

    示例:
        >>> # 方式一：上下文管理器
        >>> with builder.body().div() as d:
        ...     d.text(PlainText(content="Text in div"))
        >>>
        >>> # 方式二：方法链 + done()
        >>> builder.body().div().text(PlainText(content="Text")).done()
    """

    def __init__(self, parent: CardBodyBuilder | None = None) -> None:
        """初始化 Div 构建器。

        参数:
            parent: 父构建器，用于嵌套构建。
        """
        self._div = Div()
        self._parent = parent

    def __enter__(self) -> Self:
        """进入上下文管理器。

        返回:
            自身，供上下文内使用。
        """
        return self

    def __exit__(self, *args: Any) -> None:
        """退出上下文管理器，将 Div 添加到父构建器。"""
        if self._parent is not None:
            self._parent.add_element(self._div)

    def text(self, text: Any) -> Self:
        """设置文本内容。

        参数:
            text: 文本元素（PlainText 或 Markdown）。

        返回:
            自身，用于链式调用。
        """
        self._div.text = text
        return self

    def add_field(self, field: Any) -> Self:
        """添加字段。

        参数:
            field: 要添加的字段。

        返回:
            自身，用于链式调用。
        """
        if self._div.fields is None:
            self._div.fields = []
        self._div.fields.append(field)
        return self

    def extra(self, extra: dict[str, Any]) -> Self:
        """设置额外数据。

        参数:
            extra: 额外数据字典。

        返回:
            自身，用于链式调用。
        """
        self._div.extra = extra
        return self

    def element_id(self, element_id: str) -> Self:
        """设置元素 ID。

        参数:
            element_id: 元素 ID。

        返回:
            自身，用于链式调用。
        """
        self._div.element_id = element_id
        return self

    def margin(self, margin: str) -> Self:
        """设置外边距。

        参数:
            margin: 外边距值。

        返回:
            自身，用于链式调用。
        """
        self._div.margin = margin
        return self

    def done(self) -> CardBodyBuilder:
        """完成 Div 构建，返回父构建器。

        返回:
            父构建器实例。

        异常:
            RuntimeError: 如果没有使用父构建器。
        """
        if self._parent is None:
            raise RuntimeError("done() 只能在嵌套构建时调用")
        self._parent.add_element(self._div)
        return self._parent

    def build(self) -> Div:
        """构建 Div 元素。

        返回:
            构建好的 Div 实例。
        """
        return self._div