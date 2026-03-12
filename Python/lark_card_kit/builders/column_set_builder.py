"""列集合构建器，提供流畅 API，支持完美的类型提示。

本模块移除了 Lambda 表达式支持，只保留两种构建方式：
1. 上下文管理器（推荐用于嵌套结构）
2. 方法链 + done()（推荐用于简单结构）
"""

from __future__ import annotations

from typing import TYPE_CHECKING, Any, Self

from ..models.elements import ColumnSet, Column, Element, PlainText, Markdown

if TYPE_CHECKING:
    from .card_body_builder import CardBodyBuilder


class ColumnBuilder:
    """列构建器，提供流畅 API。

    示例:
        >>> # 方式一：上下文管理器
        >>> with builder.column_set() as cs:
        ...     with cs.column() as c:
        ...         c.plain_text("Left")
        ...     with cs.column() as c:
        ...         c.plain_text("Right")
        >>>
        >>> # 方式二：方法链 + done()
        >>> cs = builder.column_set()
        >>> cs.column().plain_text("Left").done()
        >>> cs.column().plain_text("Right").done()
        >>> cs.done()
    """

    def __init__(self, parent: ColumnSetBuilder | None = None) -> None:
        """初始化列构建器。

        参数:
            parent: 父列集合构建器，用于嵌套构建。
        """
        self._column = Column()
        self._parent = parent

    def __enter__(self) -> Self:
        """进入上下文管理器。

        返回:
            自身，供上下文内使用。
        """
        return self

    def __exit__(self, *args: Any) -> None:
        """退出上下文管理器，将列添加到父构建器。"""
        if self._parent is not None:
            self._parent._add_column(self._column)

    def width(self, width: str) -> Self:
        """设置列宽度。

        参数:
            width: 列宽度（如 'auto', 'weighted', '100px'）。

        返回:
            自身，用于链式调用。
        """
        self._column.width = width
        return self

    def vertical_align(self, align: str) -> Self:
        """设置垂直对齐。

        参数:
            align: 垂直对齐方式（如 'top', 'center', 'bottom'）。

        返回:
            自身，用于链式调用。
        """
        self._column.vertical_align = align
        return self

    def add_element(self, element: Element) -> Self:
        """添加元素到列。

        参数:
            element: 要添加的元素。

        返回:
            自身，用于链式调用。
        """
        self._column.elements.append(element)
        return self

    def plain_text(self, content: str, **kwargs: Any) -> Self:
        """添加纯文本元素。

        参数:
            content: 文本内容。
            **kwargs: 其他属性。

        返回:
            自身，用于链式调用。
        """
        return self.add_element(PlainText(content=content, **kwargs))

    def markdown(self, content: str, **kwargs: Any) -> Self:
        """添加 Markdown 元素。

        参数:
            content: Markdown 内容。
            **kwargs: 其他属性。

        返回:
            自身，用于链式调用。
        """
        return self.add_element(Markdown(content=content, **kwargs))

    def button(self) -> ButtonBuilder:
        """开始添加按钮元素。

        返回:
            ButtonBuilder 实例。
        """
        from .button_builder import ButtonBuilder
        return ButtonBuilder(self)

    def done(self) -> ColumnSetBuilder:
        """完成列构建，返回父构建器。

        返回:
            父 ColumnSetBuilder 实例。

        异常:
            RuntimeError: 如果没有使用父构建器。
        """
        if self._parent is None:
            raise RuntimeError("done() 只能在嵌套构建时调用")
        self._parent._add_column(self._column)
        return self._parent

    def build(self) -> Column:
        """构建列。

        返回:
            构建好的 Column 实例。
        """
        return self._column


class ColumnSetBuilder:
    """列集合构建器，提供流畅 API。

    示例:
        >>> # 方式一：上下文管理器
        >>> with builder.body().column_set() as cs:
        ...     with cs.column() as c:
        ...         c.plain_text("Left")
        ...     with cs.column() as c:
        ...         c.plain_text("Right")
        >>>
        >>> # 方式二：方法链 + done()
        >>> cs = builder.body().column_set()
        >>> cs.column().plain_text("Left").done()
        >>> cs.done()
    """

    def __init__(self, parent: CardBodyBuilder | None = None) -> None:
        """初始化列集合构建器。

        参数:
            parent: 父构建器，用于嵌套构建。
        """
        self._column_set = ColumnSet()
        self._parent = parent

    def __enter__(self) -> Self:
        """进入上下文管理器。

        返回:
            自身，供上下文内使用。
        """
        return self

    def __exit__(self, *args: Any) -> None:
        """退出上下文管理器，将列集合添加到父构建器。"""
        if self._parent is not None:
            self._parent.add_element(self._column_set)

    def _add_column(self, column: Column) -> None:
        """内部方法：添加列。"""
        self._column_set.columns.append(column)

    def column(self) -> ColumnBuilder:
        """开始添加列。

        返回:
            ColumnBuilder 实例。
        """
        return ColumnBuilder(self)

    def margin(self, margin: str) -> Self:
        """设置外边距。

        参数:
            margin: 外边距值。

        返回:
            自身，用于链式调用。
        """
        self._column_set.margin = margin
        return self

    def background_style(self, style: str) -> Self:
        """设置背景样式。

        参数:
            style: 背景样式。

        返回:
            自身，用于链式调用。
        """
        self._column_set.background_style = style
        return self

    def horizontal_spacing(self, spacing: str) -> Self:
        """设置水平间距。

        参数:
            spacing: 间距值。

        返回:
            自身，用于链式调用。
        """
        self._column_set.horizontal_spacing = spacing
        return self

    def element_id(self, element_id: str) -> Self:
        """设置元素 ID。

        参数:
            element_id: 元素 ID。

        返回:
            自身，用于链式调用。
        """
        self._column_set.element_id = element_id
        return self

    def done(self) -> CardBodyBuilder:
        """完成列集合构建，返回父构建器。

        返回:
            父构建器实例。

        异常:
            RuntimeError: 如果没有使用父构建器。
        """
        if self._parent is None:
            raise RuntimeError("done() 只能在嵌套构建时调用")
        self._parent.add_element(self._column_set)
        return self._parent

    def build(self) -> ColumnSet:
        """构建列集合。

        返回:
            构建好的 ColumnSet 实例。
        """
        return self._column_set


from .button_builder import ButtonBuilder