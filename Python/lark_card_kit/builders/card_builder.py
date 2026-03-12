"""卡片构建器，提供流畅 API 构建卡片，支持完美的类型提示。

本模块移除了 Lambda 表达式支持，只保留两种构建方式：
1. 上下文管理器（推荐用于嵌套结构）
2. 方法链 + done()（推荐用于简单结构）
"""

from __future__ import annotations

from typing import Any, Callable, Self, TypeVar

from ..models.card import Card
from ..models.elements import Element
from ..templates.template_filler import TemplateParameterFiller
from ..utils.element_finder import ElementFinder
from ..utils.template_helper import fill_template
from .card_header_builder import CardHeaderBuilder
from .card_body_builder import CardBodyBuilder
from .card_config_builder import CardConfigBuilder
from .card_link_builder import CardLinkBuilder
from .fallback_builder import FallbackBuilder

E = TypeVar("E", bound=Element)


class CardBuilder:
    """卡片构建器，提供流畅 API 构建卡片。

    提供可链式调用的方法来构建飞书卡片。

    示例:
        >>> # 方式一：上下文管理器（推荐）
        >>> with CardBuilder.create() as builder:
        ...     with builder.header() as h:
        ...         h.title("Welcome").template("blue")
        ...     with builder.body() as b:
        ...         b.plain_text("Hello, World!")
        >>> card = builder.build()
        >>>
        >>> # 方式二：方法链 + done()
        >>> builder = CardBuilder.create()
        >>> builder.header().title("Welcome").done()
        >>> builder.body().plain_text("Hello").done()
        >>> card = builder.build()
        >>>
        >>> # 方式三：构建后修改组件
        >>> builder = CardBuilder.create()
        >>> with builder.body() as b:
        ...     with b.button() as btn:
        ...         btn.text("Click").element_id("btn1")
        >>> builder.update_element("btn1", lambda e: setattr(e, "disabled", True))
        >>> card = builder.build()
    """

    def __init__(self) -> None:
        """初始化卡片构建器。"""
        self._card = Card()
        self._filler = TemplateParameterFiller()
        self._template_applied = False

    @classmethod
    def create(cls) -> Self:
        """创建新的卡片构建器。

        返回:
            新的 CardBuilder 实例。
        """
        return cls()

    def __enter__(self) -> Self:
        """进入上下文管理器。

        返回:
            自身，供上下文内使用。
        """
        return self

    def __exit__(self, *args: Any) -> None:
        """退出上下文管理器。"""
        pass

    def config(self) -> CardConfigBuilder:
        """开始配置卡片设置。

        返回:
            CardConfigBuilder 实例。
        """
        return CardConfigBuilder(self)

    def header(self) -> CardHeaderBuilder:
        """开始配置卡片头部。

        返回:
            CardHeaderBuilder 实例。
        """
        return CardHeaderBuilder(self)

    def body(self) -> CardBodyBuilder:
        """开始配置卡片主体。

        返回:
            CardBodyBuilder 实例。
        """
        return CardBodyBuilder(self)

    def card_link(self) -> CardLinkBuilder:
        """开始配置卡片链接。

        返回:
            CardLinkBuilder 实例。
        """
        return CardLinkBuilder(self)

    def fallback(self) -> FallbackBuilder:
        """开始配置降级内容。

        返回:
            FallbackBuilder 实例。
        """
        return FallbackBuilder(self)

    def set_parameter(self, key: str, value: Any) -> Self:
        """设置模板参数。

        参数:
            key: 参数键。
            value: 参数值。

        返回:
            自身，用于链式调用。
        """
        self._filler.set_parameter(key, value)
        return self

    def set_parameters(self, params: dict[str, Any]) -> Self:
        """设置多个模板参数。

        参数:
            params: 参数键值对字典。

        返回:
            自身，用于链式调用。
        """
        self._filler.set_parameters(params)
        return self

    def _set_config(self, config: Any) -> None:
        """内部方法：设置卡片配置。"""
        self._card.config = config

    def _set_header(self, header: Any) -> None:
        """内部方法：设置卡片头部。"""
        self._card.header = header

    def _set_body(self, body: Any) -> None:
        """内部方法：设置卡片主体。"""
        self._card.body = body

    def _set_card_link(self, link: Any) -> None:
        """内部方法：设置卡片链接。"""
        self._card.card_link = link

    def _set_fallback(self, fallback: Any) -> None:
        """内部方法：设置降级内容。"""
        self._card.fallback = fallback

    def find_element(self, element_id: str) -> Element | None:
        """通过 ID 查找组件。

        参数:
            element_id: 组件 ID。

        返回:
            找到的组件，如果未找到则返回 None。

        示例:
            >>> button = builder.find_element("btn1")
            >>> if button:
            ...     button.disabled = True
        """
        if self._card.body is None:
            return None
        finder = ElementFinder(self._card.body.elements)
        return finder.find_by_id(element_id)

    def find_element_as(self, element_id: str, element_type: type[E]) -> E | None:
        """通过 ID 查找组件并转换为指定类型。

        参数:
            element_id: 组件 ID。
            element_type: 期望的组件类型。

        返回:
            找到的组件（如果类型匹配），如果未找到或类型不匹配则返回 None。

        示例:
            >>> from lark_card_kit import Button
            >>> button = builder.find_element_as("btn1", Button)
            >>> if button:
            ...     button.disabled = True
        """
        if self._card.body is None:
            return None
        finder = ElementFinder(self._card.body.elements)
        return finder.find_by_id_as(element_id, element_type)

    def update_element(self, element_id: str, updater: Callable[[Element], None]) -> Self:
        """通过 ID 查找组件并修改。

        参数:
            element_id: 组件 ID。
            updater: 修改组件的函数。

        返回:
            自身，用于链式调用。

        示例:
            >>> builder.update_element("btn1", lambda e: setattr(e, "disabled", True))
        """
        if self._card.body is not None:
            finder = ElementFinder(self._card.body.elements)
            finder.update_by_id(element_id, updater)
        return self

    def update_element_as(
        self,
        element_id: str,
        element_type: type[E],
        updater: Callable[[E], None],
    ) -> Self:
        """通过 ID 查找组件（指定类型）并修改。

        参数:
            element_id: 组件 ID。
            element_type: 期望的组件类型。
            updater: 修改组件的函数。

        返回:
            自身，用于链式调用。

        示例:
            >>> from lark_card_kit import Button
            >>> builder.update_element_as("btn1", Button, lambda b: b.disabled = True)
        """
        if self._card.body is not None:
            finder = ElementFinder(self._card.body.elements)
            finder.update_by_id_as(element_id, element_type, updater)
        return self

    def get_finder(self) -> ElementFinder | None:
        """获取 ElementFinder 实例用于高级查找操作。

        返回:
            ElementFinder 实例，如果 body 为空则返回 None。

        示例:
            >>> finder = builder.get_finder()
            >>> if finder:
            ...     buttons = finder.find_by_tag("button")
            ...     finder.update_all_by_tag("button", lambda e: setattr(e, "disabled", True))
        """
        if self._card.body is None:
            return None
        return ElementFinder(self._card.body.elements)

    def build(self) -> Card:
        """构建卡片。

        返回:
            构建好的 Card 实例。
        """
        if self._filler.has_parameters() and not self._template_applied:
            fill_template(self._card, self._filler)
            self._template_applied = True

        return self._card

    def to_json(self, indent: int | None = None) -> str:
        """构建并序列化卡片为 JSON。

        参数:
            indent: 缩进空格数。

        返回:
            卡片的 JSON 字符串表示。
        """
        return self.build().to_json(indent=indent)