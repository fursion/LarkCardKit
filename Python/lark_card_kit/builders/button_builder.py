"""按钮构建器，提供流畅 API，支持完美的类型提示。

本模块只支持两种构建方式：
1. 上下文管理器（推荐用于嵌套结构）
2. 方法链 + done()（推荐用于简单结构）
"""

from __future__ import annotations

from typing import TYPE_CHECKING, Any, Self

from ..enums import ButtonType, ButtonSize
from ..models.elements import Button, PlainText, ConfirmConfig
from ..models.behaviors import CallbackBehavior, OpenUrlBehavior

if TYPE_CHECKING:
    from .card_body_builder import CardBodyBuilder
    from .form_builder import FormBuilder
    from .column_set_builder import ColumnBuilder


class ButtonBuilder:
    """按钮构建器，提供流畅 API。

    示例:
        >>> # 方式一：上下文管理器
        >>> with builder.body().button() as btn:
        ...     btn.text("Click Me").type(ButtonType.PRIMARY).callback({"action": "click"})
        >>>
        >>> # 方式二：方法链 + done()
        >>> builder.body().button().text("Click").type(ButtonType.PRIMARY).done()
    """

    def __init__(self, parent: CardBodyBuilder | FormBuilder | ColumnBuilder | None = None) -> None:
        """初始化按钮构建器。

        参数:
            parent: 父构建器，用于嵌套构建。
        """
        self._button = Button()
        self._parent = parent

    def __enter__(self) -> Self:
        """进入上下文管理器。

        返回:
            自身，供上下文内使用。
        """
        return self

    def __exit__(self, *args: Any) -> None:
        """退出上下文管理器，将按钮添加到父构建器。"""
        if self._parent is not None:
            self._parent.add_element(self._button)

    def text(self, content: str) -> Self:
        """设置按钮文本。

        参数:
            content: 按钮文本内容。

        返回:
            自身，用于链式调用。
        """
        self._button.text = PlainText(content=content)
        return self

    def type(self, button_type: ButtonType | str) -> Self:
        """设置按钮类型。

        参数:
            button_type: 按钮类型（primary, default, danger, text）。

        返回:
            自身，用于链式调用。
        """
        self._button.type = button_type.value if isinstance(button_type, ButtonType) else button_type
        return self

    def size(self, size: ButtonSize | str) -> Self:
        """设置按钮大小。

        参数:
            size: 按钮大小（tiny, small, medium, large）。

        返回:
            自身，用于链式调用。
        """
        self._button.size = size.value if isinstance(size, ButtonSize) else size
        return self

    def width(self, width: str) -> Self:
        """设置按钮宽度。

        参数:
            width: 按钮宽度（如 '100px', 'auto'）。

        返回:
            自身，用于链式调用。
        """
        self._button.width = width
        return self

    def icon(self, icon: Any) -> Self:
        """设置按钮图标。

        参数:
            icon: 图标配置。

        返回:
            自身，用于链式调用。
        """
        self._button.icon = icon
        return self

    def disabled(self, disabled: bool = True) -> Self:
        """设置按钮是否禁用。

        参数:
            disabled: 是否禁用。

        返回:
            自身，用于链式调用。
        """
        self._button.disabled = disabled
        return self

    def disabled_tips(self, tips: str) -> Self:
        """设置禁用提示。

        参数:
            tips: 禁用时显示的提示。

        返回:
            自身，用于链式调用。
        """
        self._button.disabled_tips = PlainText(content=tips)
        return self

    def hover_tips(self, tips: str) -> Self:
        """设置悬停提示。

        参数:
            tips: 悬停时显示的提示。

        返回:
            自身，用于链式调用。
        """
        self._button.hover_tips = PlainText(content=tips)
        return self

    def name(self, name: str) -> Self:
        """设置按钮名称（用于表单提交）。

        参数:
            name: 按钮名称。

        返回:
            自身，用于链式调用。
        """
        self._button.name = name
        return self

    def form_action_type(self, action_type: str) -> Self:
        """设置表单操作类型。

        参数:
            action_type: 表单操作类型（如 'submit', 'cancel'）。

        返回:
            自身，用于链式调用。
        """
        self._button.form_action_type = action_type
        return self

    def confirm(self, title: str, text: str) -> Self:
        """设置确认对话框。

        参数:
            title: 确认对话框标题。
            text: 确认对话框内容。

        返回:
            自身，用于链式调用。
        """
        self._button.confirm = ConfirmConfig(
            title=PlainText(content=title),
            text=PlainText(content=text)
        )
        return self

    def callback(self, value: dict[str, Any]) -> Self:
        """添加回调行为。

        参数:
            value: 回调值字典。

        返回:
            自身，用于链式调用。
        """
        if self._button.behaviors is None:
            self._button.behaviors = []
        self._button.behaviors.append(CallbackBehavior(value=value))
        return self

    def open_url(self, url: str) -> Self:
        """添加打开 URL 行为。

        参数:
            url: 点击时打开的 URL。

        返回:
            自身，用于链式调用。
        """
        if self._button.behaviors is None:
            self._button.behaviors = []
        self._button.behaviors.append(OpenUrlBehavior(url=url))
        return self

    def element_id(self, element_id: str) -> Self:
        """设置元素 ID。

        参数:
            element_id: 元素 ID。

        返回:
            自身，用于链式调用。
        """
        self._button.element_id = element_id
        return self

    def done(self) -> CardBodyBuilder | FormBuilder | ColumnBuilder:
        """完成按钮构建，返回父构建器。

        返回:
            父构建器实例。

        异常:
            RuntimeError: 如果没有使用父构建器。
        """
        if self._parent is None:
            raise RuntimeError("done() 只能在嵌套构建时调用")
        self._parent.add_element(self._button)
        return self._parent

    def build(self) -> Button:
        """构建按钮元素。

        返回:
            构建好的 Button 实例。
        """
        return self._button