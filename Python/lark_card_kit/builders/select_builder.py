"""下拉选择构建器，提供流畅 API，支持完美的类型提示。

本模块只支持两种构建方式：
1. 上下文管理器（推荐用于嵌套结构）
2. 方法链 + done()（推荐用于简单结构）
"""

from __future__ import annotations

from typing import TYPE_CHECKING, Any, Self

from ..models.elements import Select, SelectOption, PlainText

if TYPE_CHECKING:
    from .card_body_builder import CardBodyBuilder
    from .form_builder import FormBuilder


class SelectBuilder:
    """下拉选择构建器，提供流畅 API。

    示例:
        >>> # 方式一：上下文管理器
        >>> with builder.body().select() as s:
        ...     s.name("country").placeholder("Select country")
        ...     s.option("China", "cn").option("USA", "us")
        >>>
        >>> # 方式二：方法链 + done()
        >>> builder.body().select().name("country").option("China", "cn").done()
    """

    def __init__(self, parent: CardBodyBuilder | FormBuilder | None = None) -> None:
        """初始化下拉选择构建器。

        参数:
            parent: 父构建器，用于嵌套构建。
        """
        self._select = Select()
        self._parent = parent

    def __enter__(self) -> Self:
        """进入上下文管理器。

        返回:
            自身，供上下文内使用。
        """
        return self

    def __exit__(self, *args: Any) -> None:
        """退出上下文管理器，将下拉选择添加到父构建器。"""
        if self._parent is not None:
            self._parent.add_element(self._select)

    def name(self, name: str) -> Self:
        """设置下拉选择名称（用于表单提交）。

        参数:
            name: 下拉选择名称。

        返回:
            自身，用于链式调用。
        """
        self._select.name = name
        return self

    def placeholder(self, placeholder: str) -> Self:
        """设置占位文本。

        参数:
            placeholder: 占位文本。

        返回:
            自身，用于链式调用。
        """
        self._select.placeholder = PlainText(content=placeholder)
        return self

    def option(self, text: str, value: str) -> Self:
        """添加选项。

        参数:
            text: 选项显示文本。
            value: 选项值。

        返回:
            自身，用于链式调用。
        """
        self._select.options.append(SelectOption(text=PlainText(content=text), value=value))
        return self

    def options(self, options: list[tuple[str, str]]) -> Self:
        """添加多个选项。

        参数:
            options: (文本, 值) 元组列表。

        返回:
            自身，用于链式调用。
        """
        for text, value in options:
            self._select.options.append(SelectOption(text=PlainText(content=text), value=value))
        return self

    def selected_value(self, value: str) -> Self:
        """设置选中值。

        参数:
            value: 选中值。

        返回:
            自身，用于链式调用。
        """
        self._select.selected_value = value
        return self

    def multiple(self, multiple: bool = True) -> Self:
        """设置是否多选。

        参数:
            multiple: 是否多选。

        返回:
            自身，用于链式调用。
        """
        self._select.multiple = multiple
        return self

    def disabled(self, disabled: bool = True) -> Self:
        """设置是否禁用。

        参数:
            disabled: 是否禁用。

        返回:
            自身，用于链式调用。
        """
        self._select.disabled = disabled
        return self

    def width(self, width: str) -> Self:
        """设置宽度。

        参数:
            width: 宽度。

        返回:
            自身，用于链式调用。
        """
        self._select.width = width
        return self

    def element_id(self, element_id: str) -> Self:
        """设置元素 ID。

        参数:
            element_id: 元素 ID。

        返回:
            自身，用于链式调用。
        """
        self._select.element_id = element_id
        return self

    def done(self) -> CardBodyBuilder | FormBuilder:
        """完成下拉选择构建，返回父构建器。

        返回:
            父构建器实例。

        异常:
            RuntimeError: 如果没有使用父构建器。
        """
        if self._parent is None:
            raise RuntimeError("done() 只能在嵌套构建时调用")
        self._parent.add_element(self._select)
        return self._parent

    def build(self) -> Select:
        """构建下拉选择元素。

        返回:
            构建好的 Select 实例。
        """
        return self._select