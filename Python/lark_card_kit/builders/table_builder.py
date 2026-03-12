"""表格构建器，提供流畅 API，支持完美的类型提示。

本模块只支持两种构建方式：
1. 上下文管理器（推荐用于嵌套结构）
2. 方法链 + done()（推荐用于简单结构）
"""

from __future__ import annotations

from typing import TYPE_CHECKING, Any, Self

from ..models.elements import Table, TableColumn, TableRow
from ..models.elements.plain_text import PlainText

if TYPE_CHECKING:
    from .card_body_builder import CardBodyBuilder


class TableColumnBuilder:
    """表格列构建器，提供流畅 API。

    示例:
        >>> # 上下文管理器风格
        >>> with builder.body().table() as t:
        ...     with t.column() as c:
        ...         c.name("col1").display_name("Column 1")
    """

    def __init__(self, parent: TableBuilder) -> None:
        """初始化表格列构建器。

        参数:
            parent: 父表格构建器。
        """
        self._column = TableColumn()
        self._parent = parent

    def __enter__(self) -> Self:
        """进入上下文管理器。

        返回:
            自身，供上下文内使用。
        """
        return self

    def __exit__(self, *args: Any) -> None:
        """退出上下文管理器，将列添加到父构建器。"""
        self._parent._add_column(self._column)

    def name(self, name: str) -> Self:
        """设置列名称。

        参数:
            name: 列名称。

        返回:
            自身，用于链式调用。
        """
        self._column.name = name
        return self

    def display_name(self, name: str) -> Self:
        """设置显示名称。

        参数:
            name: 显示名称。

        返回:
            自身，用于链式调用。
        """
        self._column.display_name = PlainText(content=name)
        return self

    def width(self, width: str) -> Self:
        """设置列宽度。

        参数:
            width: 列宽度。

        返回:
            自身，用于链式调用。
        """
        self._column.width = width
        return self

    def horizontal_align(self, align: str) -> Self:
        """设置水平对齐。

        参数:
            align: 水平对齐方式。

        返回:
            自身，用于链式调用。
        """
        self._column.horizontal_align = align
        return self

    def done(self) -> TableBuilder:
        """完成列构建，返回表格构建器。

        返回:
            父 TableBuilder 实例。
        """
        self._parent._add_column(self._column)
        return self._parent


class TableRowBuilder:
    """表格行构建器，提供流畅 API。

    示例:
        >>> # 上下文管理器风格
        >>> with builder.body().table() as t:
        ...     with t.row() as r:
        ...         r.cell("Value 1").cell("Value 2")
    """

    def __init__(self, parent: TableBuilder) -> None:
        """初始化表格行构建器。

        参数:
            parent: 父表格构建器。
        """
        self._row = TableRow()
        self._parent = parent

    def __enter__(self) -> Self:
        """进入上下文管理器。

        返回:
            自身，供上下文内使用。
        """
        return self

    def __exit__(self, *args: Any) -> None:
        """退出上下文管理器，将行添加到父构建器。"""
        self._parent._add_row(self._row)

    def cell(self, content: Any) -> Self:
        """添加单元格。

        参数:
            content: 单元格内容。

        返回:
            自身，用于链式调用。
        """
        self._row.cells.append(content)
        return self

    def cells(self, cells: list[Any]) -> Self:
        """设置所有单元格。

        参数:
            cells: 单元格内容列表。

        返回:
            自身，用于链式调用。
        """
        self._row.cells = cells
        return self

    def done(self) -> TableBuilder:
        """完成行构建，返回表格构建器。

        返回:
            父 TableBuilder 实例。
        """
        self._parent._add_row(self._row)
        return self._parent


class TableBuilder:
    """表格构建器，提供流畅 API。

    示例:
        >>> # 方式一：上下文管理器
        >>> with builder.body().table() as t:
        ...     with t.column() as c:
        ...         c.name("name").display_name("Name")
        ...     with t.row() as r:
        ...         r.cell("Item 1").cell("100")
        >>>
        >>> # 方式二：方法链 + done()
        >>> t = builder.body().table()
        >>> t.column().name("name").display_name("Name").done()
        >>> t.row().cell("Item 1").cell("100").done()
        >>> t.done()
    """

    def __init__(self, parent: CardBodyBuilder | None = None) -> None:
        """初始化表格构建器。

        参数:
            parent: 父构建器，用于嵌套构建。
        """
        self._table = Table()
        self._parent = parent

    def __enter__(self) -> Self:
        """进入上下文管理器。

        返回:
            自身，供上下文内使用。
        """
        return self

    def __exit__(self, *args: Any) -> None:
        """退出上下文管理器，将表格添加到父构建器。"""
        if self._parent is not None:
            self._parent.add_element(self._table)

    def _add_column(self, column: TableColumn) -> None:
        """内部方法：添加列。"""
        self._table.columns.append(column)

    def _add_row(self, row: TableRow) -> None:
        """内部方法：添加行。"""
        self._table.rows.append(row)

    def column(self) -> TableColumnBuilder:
        """开始添加列。

        返回:
            TableColumnBuilder 实例。
        """
        return TableColumnBuilder(self)

    def row(self) -> TableRowBuilder:
        """开始添加行。

        返回:
            TableRowBuilder 实例。
        """
        return TableRowBuilder(self)

    def page_size(self, size: int) -> Self:
        """设置分页大小。

        参数:
            size: 分页大小。

        返回:
            自身，用于链式调用。
        """
        self._table.page_size = size
        return self

    def row_height(self, height: str) -> Self:
        """设置行高。

        参数:
            height: 行高。

        返回:
            自身，用于链式调用。
        """
        self._table.row_height = height
        return self

    def element_id(self, element_id: str) -> Self:
        """设置元素 ID。

        参数:
            element_id: 元素 ID。

        返回:
            自身，用于链式调用。
        """
        self._table.element_id = element_id
        return self

    def done(self) -> CardBodyBuilder:
        """完成表格构建，返回父构建器。

        返回:
            父构建器实例。

        异常:
            RuntimeError: 如果没有使用父构建器。
        """
        if self._parent is None:
            raise RuntimeError("done() 只能在嵌套构建时调用")
        self._parent.add_element(self._table)
        return self._parent

    def build(self) -> Table:
        """构建表格元素。

        返回:
            构建好的 Table 实例。
        """
        return self._table