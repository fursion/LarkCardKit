"""
LarkCardKit - 用于构建飞书卡片 2.0 消息的 Python 库。

本库提供流畅 API 来构建符合卡片 2.0 规范的飞书卡片。

支持两种构建方式：

1. 上下文管理器（推荐用于嵌套结构）
    >>> from lark_card_kit import CardBuilder, ButtonType
    >>>
    >>> with CardBuilder.create() as builder:
    ...     with builder.header() as h:
    ...         h.title("Welcome").template("blue")
    ...     with builder.body() as b:
    ...         b.plain_text("Hello, World!")
    ...         with b.button() as btn:
    ...             btn.text("Click Me").type(ButtonType.PRIMARY)
    >>> card = builder.build()

2. 方法链 + done()（推荐用于简单结构）
    >>> builder = CardBuilder.create()
    >>> builder.header().title("Welcome").done()
    >>> body = builder.body()
    >>> body.plain_text("Hello")
    >>> body.button().text("Click").done()
    >>> body.done()
    >>> card = builder.build()

3. 工厂函数（快速创建简单元素）
    >>> from lark_card_kit.api import card, button, plain_text
    >>> my_card = card(title="Welcome")
    >>> my_card.body().add_element(plain_text("Hello")).done()
    >>> card = my_card.build()
"""

from .models import (
    Card,
    CardHeader,
    CardBody,
    CardConfig,
    CardLink,
    Fallback,
    HeaderIcon,
    Behaviors,
    CallbackBehavior,
    OpenUrlBehavior,
)
from .models.elements import (
    Element,
    PlainText,
    Markdown,
    MarkdownText,
    Button,
    Input,
    Select,
    Form,
    ColumnSet,
    Column,
    Div,
    Hr,
    Image,
    Table,
    TextTag,
    DatePicker,
    PickerTime,
    PickerDatetime,
    SelectPerson,
    MultiSelectPerson,
    Checkbox,
    Person,
    PersonList,
    Overflow,
    Checker,
    Chart,
    CollapsiblePanel,
    Loop,
    InteractiveContainer,
)
from .enums import (
    ButtonType,
    ButtonSize,
    InputType,
    ImageSize,
    SpacingSize,
    AlignType,
    BehaviorType,
)
from .builders import (
    CardBuilder,
    CardHeaderBuilder,
    CardBodyBuilder,
    CardConfigBuilder,
    CardLinkBuilder,
    FallbackBuilder,
    TextBuilder,
    PlainTextBuilder,
    MarkdownBuilder,
    ButtonBuilder,
    InputBuilder,
    SelectBuilder,
    FormBuilder,
    ColumnSetBuilder,
    ColumnBuilder,
    DivBuilder,
    ImageBuilder,
    TableBuilder,
    TextTagBuilder,
    HrBuilder,
)
from .templates import (
    TemplateParameterFiller,
    TemplateOptions,
    TemplateValue,
)
from .utils import (
    to_json,
    to_dict,
    ElementFinder,
    fill_template,
    fill_template_with_params,
)
from . import api

__version__ = "0.1.0"

__all__ = [
    # 模型
    "Card",
    "CardHeader",
    "CardBody",
    "CardConfig",
    "CardLink",
    "Fallback",
    "HeaderIcon",
    "Behaviors",
    "CallbackBehavior",
    "OpenUrlBehavior",
    # 元素
    "Element",
    "PlainText",
    "Markdown",
    "MarkdownText",
    "Button",
    "Input",
    "Select",
    "Form",
    "ColumnSet",
    "Column",
    "Div",
    "Hr",
    "Image",
    "Table",
    "TextTag",
    "DatePicker",
    "PickerTime",
    "PickerDatetime",
    "SelectPerson",
    "MultiSelectPerson",
    "Checkbox",
    "Person",
    "PersonList",
    "Overflow",
    "Checker",
    "Chart",
    "CollapsiblePanel",
    "Loop",
    "InteractiveContainer",
    # 枚举
    "ButtonType",
    "ButtonSize",
    "InputType",
    "ImageSize",
    "SpacingSize",
    "AlignType",
    "BehaviorType",
    # 构建器
    "CardBuilder",
    "CardHeaderBuilder",
    "CardBodyBuilder",
    "CardConfigBuilder",
    "CardLinkBuilder",
    "FallbackBuilder",
    "TextBuilder",
    "PlainTextBuilder",
    "MarkdownBuilder",
    "ButtonBuilder",
    "InputBuilder",
    "SelectBuilder",
    "FormBuilder",
    "ColumnSetBuilder",
    "ColumnBuilder",
    "DivBuilder",
    "ImageBuilder",
    "TableBuilder",
    "TextTagBuilder",
    "HrBuilder",
    # 模板
    "TemplateParameterFiller",
    "TemplateOptions",
    "TemplateValue",
    # 工具
    "to_json",
    "to_dict",
    "ElementFinder",
    "fill_template",
    "fill_template_with_params",
    # API 模块
    "api",
]