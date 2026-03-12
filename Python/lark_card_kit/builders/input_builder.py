"""输入框构建器，提供流畅 API，支持完美的类型提示。

本模块只支持两种构建方式：
1. 上下文管理器（推荐用于嵌套结构）
2. 方法链 + done()（推荐用于简单结构）
"""

from __future__ import annotations

from typing import TYPE_CHECKING, Any, Self

from ..enums import InputType
from ..models.elements import Input, PlainText

if TYPE_CHECKING:
    from .card_body_builder import CardBodyBuilder
    from .form_builder import FormBuilder


class InputBuilder:
    """输入框构建器，提供流畅 API。

    示例:
        >>> # 方式一：上下文管理器
        >>> with builder.body().input() as i:
        ...     i.name("username").placeholder("Enter username").required()
        >>>
        >>> # 方式二：方法链 + done()
        >>> builder.body().input().name("username").placeholder("Enter").done()
    """

    def __init__(self, parent: CardBodyBuilder | FormBuilder | None = None) -> None:
        """初始化输入框构建器。

        参数:
            parent: 父构建器，用于嵌套构建。
        """
        self._input = Input()
        self._parent = parent

    def __enter__(self) -> Self:
        """进入上下文管理器。

        返回:
            自身，供上下文内使用。
        """
        return self

    def __exit__(self, *args: Any) -> None:
        """退出上下文管理器，将输入框添加到父构建器。"""
        if self._parent is not None:
            self._parent.add_element(self._input)

    def name(self, name: str) -> Self:
        """设置输入框名称（用于表单提交）。

        参数:
            name: 输入框名称。

        返回:
            自身，用于链式调用。
        """
        self._input.name = name
        return self

    def placeholder(self, placeholder: str) -> Self:
        """设置占位文本。

        参数:
            placeholder: 占位文本。

        返回:
            自身，用于链式调用。
        """
        self._input.placeholder = PlainText(content=placeholder)
        return self

    def default_value(self, value: str) -> Self:
        """设置默认值。

        参数:
            value: 默认值。

        返回:
            自身，用于链式调用。
        """
        self._input.default_value = value
        return self

    def input_type(self, input_type: InputType | str) -> Self:
        """设置输入类型。

        参数:
            input_type: 输入类型（text, password, number, email）。

        返回:
            自身，用于链式调用。
        """
        self._input.input_type = input_type.value if isinstance(input_type, InputType) else input_type
        return self

    def label(self, label: str) -> Self:
        """设置标签。

        参数:
            label: 标签文本。

        返回:
            自身，用于链式调用。
        """
        self._input.label = PlainText(content=label)
        return self

    def label_position(self, position: str) -> Self:
        """设置标签位置。

        参数:
            position: 标签位置（如 'left', 'top'）。

        返回:
            自身，用于链式调用。
        """
        self._input.label_position = position
        return self

    def required(self, required: bool = True) -> Self:
        """设置是否必填。

        参数:
            required: 是否必填。

        返回:
            自身，用于链式调用。
        """
        self._input.required = required
        return self

    def max_length(self, max_length: int) -> Self:
        """设置最大长度。

        参数:
            max_length: 最大长度。

        返回:
            自身，用于链式调用。
        """
        self._input.max_length = max_length
        return self

    def rows(self, rows: int) -> Self:
        """设置多行输入的行数。

        参数:
            rows: 行数。

        返回:
            自身，用于链式调用。
        """
        self._input.rows = rows
        return self

    def auto_resize(self, auto_resize: bool = True) -> Self:
        """设置是否自动调整大小。

        参数:
            auto_resize: 是否自动调整大小。

        返回:
            自身，用于链式调用。
        """
        self._input.auto_resize = auto_resize
        return self

    def max_rows(self, max_rows: int) -> Self:
        """设置最大行数。

        参数:
            max_rows: 最大行数。

        返回:
            自身，用于链式调用。
        """
        self._input.max_rows = max_rows
        return self

    def width(self, width: str) -> Self:
        """设置输入框宽度。

        参数:
            width: 输入框宽度。

        返回:
            自身，用于链式调用。
        """
        self._input.width = width
        return self

    def disabled(self, disabled: bool = True) -> Self:
        """设置是否禁用。

        参数:
            disabled: 是否禁用。

        返回:
            自身，用于链式调用。
        """
        self._input.disabled = disabled
        return self

    def disabled_tips(self, tips: str) -> Self:
        """设置禁用提示。

        参数:
            tips: 禁用时显示的提示。

        返回:
            自身，用于链式调用。
        """
        self._input.disabled_tips = PlainText(content=tips)
        return self

    def element_id(self, element_id: str) -> Self:
        """设置元素 ID。

        参数:
            element_id: 元素 ID。

        返回:
            自身，用于链式调用。
        """
        self._input.element_id = element_id
        return self

    def done(self) -> CardBodyBuilder | FormBuilder:
        """完成输入框构建，返回父构建器。

        返回:
            父构建器实例。

        异常:
            RuntimeError: 如果没有使用父构建器。
        """
        if self._parent is None:
            raise RuntimeError("done() 只能在嵌套构建时调用")
        self._parent.add_element(self._input)
        return self._parent

    def build(self) -> Input:
        """构建输入框元素。

        返回:
            构建好的 Input 实例。
        """
        return self._input