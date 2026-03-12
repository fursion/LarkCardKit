"""卡片主体构建器，提供流畅 API，支持完美的类型提示。

本模块移除了 Lambda 表达式支持，只保留两种构建方式：
1. 上下文管理器（推荐用于嵌套结构）
2. 方法链 + done()（推荐用于简单结构）
"""

from __future__ import annotations

from typing import TYPE_CHECKING, Any, Self

from ..models.card_body import CardBody
from ..models.elements import Element, Hr, PlainText, Markdown, TextTag

if TYPE_CHECKING:
    from .card_builder import CardBuilder


class CardBodyBuilder:
    """卡片主体构建器，提供流畅 API。

    示例:
        >>> # 方式一：上下文管理器
        >>> with builder.body() as b:
        ...     b.plain_text("Hello")
        ...     with b.button() as btn:
        ...         btn.text("Click").type(ButtonType.PRIMARY)
        >>>
        >>> # 方式二：方法链 + done()
        >>> body = builder.body()
        >>> body.plain_text("Hello")
        >>> body.button().text("Click").done()
        >>> body.done()
    """

    def __init__(self, card_builder: CardBuilder | None = None) -> None:
        """初始化卡片主体构建器。

        参数:
            card_builder: 父卡片构建器，用于嵌套构建。
        """
        self._body = CardBody()
        self._card_builder = card_builder

    def __enter__(self) -> Self:
        """进入上下文管理器。

        返回:
            自身，供上下文内使用。
        """
        return self

    def __exit__(self, *args: Any) -> None:
        """退出上下文管理器，将主体应用到父构建器。"""
        if self._card_builder is not None:
            self._card_builder._set_body(self._body)

    def add_element(self, element: Element) -> Self:
        """添加元素到主体。

        参数:
            element: 要添加的元素。

        返回:
            自身，用于链式调用。
        """
        self._body.elements.append(element)
        return self

    def plain_text(self, content: str, *, size: str | None = None, color: str | None = None) -> Self:
        """添加纯文本元素。

        参数:
            content: 文本内容。
            size: 文本大小（可选）。
            color: 文本颜色（可选）。

        返回:
            自身，用于链式调用。
        """
        element = PlainText(content=content, text_size=size, text_color=color)
        return self.add_element(element)

    def markdown(self, content: str, *, size: str | None = None, color: str | None = None) -> Self:
        """添加 Markdown 元素。

        参数:
            content: Markdown 内容。
            size: 文本大小（可选）。
            color: 文本颜色（可选）。

        返回:
            自身，用于链式调用。
        """
        element = Markdown(content=content, text_size=size, text_color=color)
        return self.add_element(element)

    def button(self) -> ButtonBuilder:
        """开始添加按钮元素。

        返回:
            ButtonBuilder 实例。
        """
        from .button_builder import ButtonBuilder
        return ButtonBuilder(self)

    def input(self) -> InputBuilder:
        """开始添加输入框元素。

        返回:
            InputBuilder 实例。
        """
        from .input_builder import InputBuilder
        return InputBuilder(self)

    def select(self) -> SelectBuilder:
        """开始添加下拉选择元素。

        返回:
            SelectBuilder 实例。
        """
        from .select_builder import SelectBuilder
        return SelectBuilder(self)

    def form(self) -> FormBuilder:
        """开始添加表单元素。

        返回:
            FormBuilder 实例。
        """
        from .form_builder import FormBuilder
        return FormBuilder(self)

    def column_set(self) -> ColumnSetBuilder:
        """开始添加列集合元素。

        返回:
            ColumnSetBuilder 实例。
        """
        from .column_set_builder import ColumnSetBuilder
        return ColumnSetBuilder(self)

    def div(self) -> DivBuilder:
        """开始添加 Div 元素。

        返回:
            DivBuilder 实例。
        """
        from .div_builder import DivBuilder
        return DivBuilder(self)

    def image(self) -> ImageBuilder:
        """开始添加图片元素。

        返回:
            ImageBuilder 实例。
        """
        from .image_builder import ImageBuilder
        return ImageBuilder(self)

    def table(self) -> TableBuilder:
        """开始添加表格元素。

        返回:
            TableBuilder 实例。
        """
        from .table_builder import TableBuilder
        return TableBuilder(self)

    def text_tag(self, text: str, color: str | None = None) -> Self:
        """添加文本标签元素。

        参数:
            text: 标签文本。
            color: 标签颜色。

        返回:
            自身，用于链式调用。
        """
        return self.add_element(TextTag(text=text, color=color))

    def hr(self) -> Self:
        """添加分割线元素。

        返回:
            自身，用于链式调用。
        """
        return self.add_element(Hr())

    def vertical_spacing(self, spacing: str) -> Self:
        """设置垂直间距。

        参数:
            spacing: 间距值。

        返回:
            自身，用于链式调用。
        """
        self._body.vertical_spacing = spacing
        return self

    def horizontal_spacing(self, spacing: str) -> Self:
        """设置水平间距。

        参数:
            spacing: 间距值。

        返回:
            自身，用于链式调用。
        """
        self._body.horizontal_spacing = spacing
        return self

    def padding(self, padding: str) -> Self:
        """设置内边距。

        参数:
            padding: 内边距值。

        返回:
            自身，用于链式调用。
        """
        self._body.padding = padding
        return self

    def done(self) -> CardBuilder:
        """完成主体构建，返回卡片构建器。

        返回:
            父 CardBuilder 实例。

        异常:
            RuntimeError: 如果没有使用父 CardBuilder。
        """
        if self._card_builder is None:
            raise RuntimeError("done() 只能在嵌套构建时调用")
        self._card_builder._set_body(self._body)
        return self._card_builder

    def build(self) -> CardBody:
        """构建卡片主体。

        返回:
            构建好的 CardBody 实例。
        """
        return self._body


# 在文件末尾导入，避免循环导入
from .button_builder import ButtonBuilder
from .input_builder import InputBuilder
from .select_builder import SelectBuilder
from .form_builder import FormBuilder
from .column_set_builder import ColumnSetBuilder
from .div_builder import DivBuilder
from .image_builder import ImageBuilder
from .table_builder import TableBuilder