"""表单构建器，提供流畅 API，支持完美的类型提示。

本模块移除了 Lambda 表达式支持，只保留两种构建方式：
1. 上下文管理器（推荐用于嵌套结构）
2. 方法链 + done()（推荐用于简单结构）
"""

from __future__ import annotations

from typing import TYPE_CHECKING, Any, Self

from ..models.elements import Form, Element

if TYPE_CHECKING:
    from .card_body_builder import CardBodyBuilder


class FormBuilder:
    """表单构建器，提供流畅 API。

    示例:
        >>> # 方式一：上下文管理器
        >>> with builder.body().form() as f:
        ...     f.name("login_form")
        ...     with f.input() as i:
        ...         i.name("username").placeholder("Enter username")
        >>>
        >>> # 方式二：方法链 + done()
        >>> form = builder.body().form()
        >>> form.name("login_form")
        >>> form.input().name("username").done()
        >>> form.done()
    """

    def __init__(self, parent: CardBodyBuilder | None = None) -> None:
        """初始化表单构建器。

        参数:
            parent: 父构建器，用于嵌套构建。
        """
        self._form = Form()
        self._parent = parent

    def __enter__(self) -> Self:
        """进入上下文管理器。

        返回:
            自身，供上下文内使用。
        """
        return self

    def __exit__(self, *args: Any) -> None:
        """退出上下文管理器，将表单添加到父构建器。"""
        if self._parent is not None:
            self._parent.add_element(self._form)

    def name(self, name: str) -> Self:
        """设置表单名称。

        参数:
            name: 表单名称。

        返回:
            自身，用于链式调用。
        """
        self._form.name = name
        return self

    def add_element(self, element: Element) -> Self:
        """添加元素到表单。

        参数:
            element: 要添加的元素。

        返回:
            自身，用于链式调用。
        """
        self._form.elements.append(element)
        return self

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

    def direction(self, direction: str) -> Self:
        """设置表单方向。

        参数:
            direction: 表单方向（如 'vertical', 'horizontal'）。

        返回:
            自身，用于链式调用。
        """
        self._form.direction = direction
        return self

    def padding(self, padding: str) -> Self:
        """设置内边距。

        参数:
            padding: 内边距值。

        返回:
            自身，用于链式调用。
        """
        self._form.padding = padding
        return self

    def vertical_spacing(self, spacing: str) -> Self:
        """设置垂直间距。

        参数:
            spacing: 间距值。

        返回:
            自身，用于链式调用。
        """
        self._form.vertical_spacing = spacing
        return self

    def horizontal_spacing(self, spacing: str) -> Self:
        """设置水平间距。

        参数:
            spacing: 间距值。

        返回:
            自身，用于链式调用。
        """
        self._form.horizontal_spacing = spacing
        return self

    def element_id(self, element_id: str) -> Self:
        """设置元素 ID。

        参数:
            element_id: 元素 ID。

        返回:
            自身，用于链式调用。
        """
        self._form.element_id = element_id
        return self

    def done(self) -> CardBodyBuilder:
        """完成表单构建，返回父构建器。

        返回:
            父构建器实例。

        异常:
            RuntimeError: 如果没有使用父构建器。
        """
        if self._parent is None:
            raise RuntimeError("done() 只能在嵌套构建时调用")
        self._parent.add_element(self._form)
        return self._parent

    def build(self) -> Form:
        """构建表单元素。

        返回:
            构建好的 Form 实例。
        """
        return self._form


from .input_builder import InputBuilder
from .select_builder import SelectBuilder