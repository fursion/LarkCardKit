"""图片构建器，提供流畅 API，支持完美的类型提示。

本模块只支持两种构建方式：
1. 上下文管理器（推荐用于嵌套结构）
2. 方法链 + done()（推荐用于简单结构）
"""

from __future__ import annotations

from typing import TYPE_CHECKING, Any, Self

from ..enums import ImageSize
from ..models.elements import Image

if TYPE_CHECKING:
    from .card_body_builder import CardBodyBuilder


class ImageBuilder:
    """图片构建器，提供流畅 API。

    示例:
        >>> # 方式一：上下文管理器
        >>> with builder.body().image() as img:
        ...     img.src("https://example.com/image.png")
        ...     img.alt("Image description").size(ImageSize.MEDIUM)
        >>>
        >>> # 方式二：方法链 + done()
        >>> builder.body().image().src("https://example.com/img.png").alt("alt").done()
    """

    def __init__(self, parent: CardBodyBuilder | None = None) -> None:
        """初始化图片构建器。

        参数:
            parent: 父构建器，用于嵌套构建。
        """
        self._image = Image()
        self._parent = parent

    def __enter__(self) -> Self:
        """进入上下文管理器。

        返回:
            自身，供上下文内使用。
        """
        return self

    def __exit__(self, *args: Any) -> None:
        """退出上下文管理器，将图片添加到父构建器。"""
        if self._parent is not None:
            self._parent.add_element(self._image)

    def src(self, src: str) -> Self:
        """设置图片源 URL。

        参数:
            src: 图片源 URL。

        返回:
            自身，用于链式调用。
        """
        self._image.src = src
        return self

    def alt(self, alt: str) -> Self:
        """设置替代文本。

        参数:
            alt: 替代文本。

        返回:
            自身，用于链式调用。
        """
        self._image.alt = alt
        return self

    def preview_src(self, src: str) -> Self:
        """设置预览图片 URL。

        参数:
            src: 预览图片 URL。

        返回:
            自身，用于链式调用。
        """
        self._image.preview_src = src
        return self

    def size(self, size: ImageSize | str) -> Self:
        """设置图片大小。

        参数:
            size: 图片大小（small, medium, large）。

        返回:
            自身，用于链式调用。
        """
        self._image.size = size.value if isinstance(size, ImageSize) else size
        return self

    def mode(self, mode: str) -> Self:
        """设置显示模式。

        参数:
            mode: 显示模式。

        返回:
            自身，用于链式调用。
        """
        self._image.mode = mode
        return self

    def width(self, width: str) -> Self:
        """设置图片宽度。

        参数:
            width: 图片宽度。

        返回:
            自身，用于链式调用。
        """
        self._image.width = width
        return self

    def height(self, height: str) -> Self:
        """设置图片高度。

        参数:
            height: 图片高度。

        返回:
            自身，用于链式调用。
        """
        self._image.height = height
        return self

    def corner_radius(self, radius: str) -> Self:
        """设置圆角半径。

        参数:
            radius: 圆角半径。

        返回:
            自身，用于链式调用。
        """
        self._image.corner_radius = radius
        return self

    def element_id(self, element_id: str) -> Self:
        """设置元素 ID。

        参数:
            element_id: 元素 ID。

        返回:
            自身，用于链式调用。
        """
        self._image.element_id = element_id
        return self

    def done(self) -> CardBodyBuilder:
        """完成图片构建，返回父构建器。

        返回:
            父构建器实例。

        异常:
            RuntimeError: 如果没有使用父构建器。
        """
        if self._parent is None:
            raise RuntimeError("done() 只能在嵌套构建时调用")
        self._parent.add_element(self._image)
        return self._parent

    def build(self) -> Image:
        """构建图片元素。

        返回:
            构建好的 Image 实例。
        """
        return self._image