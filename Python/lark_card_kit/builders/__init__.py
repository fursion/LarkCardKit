"""构建器模块。

提供用于构建飞书卡片的流畅 API。

本模块移除了 Lambda 表达式支持，只保留两种构建方式：
1. 上下文管理器（推荐用于嵌套结构）
2. 方法链 + done()（推荐用于简单结构）
"""

from .card_builder import CardBuilder
from .card_header_builder import CardHeaderBuilder
from .card_body_builder import CardBodyBuilder
from .card_config_builder import CardConfigBuilder
from .card_link_builder import CardLinkBuilder
from .fallback_builder import FallbackBuilder
from .button_builder import ButtonBuilder
from .input_builder import InputBuilder
from .select_builder import SelectBuilder
from .form_builder import FormBuilder
from .column_set_builder import ColumnSetBuilder, ColumnBuilder
from .div_builder import DivBuilder
from .image_builder import ImageBuilder
from .table_builder import TableBuilder, TableColumnBuilder, TableRowBuilder
from .text_builder import TextBuilder
from .plain_text_builder import PlainTextBuilder
from .markdown_builder import MarkdownBuilder
from .text_tag_builder import TextTagBuilder
from .hr_builder import HrBuilder

__all__ = [
    # 构建器
    "CardBuilder",
    "CardHeaderBuilder",
    "CardBodyBuilder",
    "CardConfigBuilder",
    "CardLinkBuilder",
    "FallbackBuilder",
    "ButtonBuilder",
    "InputBuilder",
    "SelectBuilder",
    "FormBuilder",
    "ColumnSetBuilder",
    "ColumnBuilder",
    "DivBuilder",
    "ImageBuilder",
    "TableBuilder",
    "TableColumnBuilder",
    "TableRowBuilder",
    "TextBuilder",
    "PlainTextBuilder",
    "MarkdownBuilder",
    "TextTagBuilder",
    "HrBuilder",
]