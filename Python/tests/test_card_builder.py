"""LarkCardKit 测试。"""

import pytest

from lark_card_kit import (
    CardBuilder,
    Card,
    CardHeader,
    CardBody,
    PlainText,
    Markdown,
    Button,
    ButtonType,
    ButtonSize,
    InputType,
    TemplateParameterFiller,
    Form,
    ElementFinder,
)
from lark_card_kit.api import (
    card,
    button,
    plain_text,
    markdown,
    image,
    text_tag,
    hr,
)


class TestContextManager:
    """测试上下文管理器风格构建。"""

    def test_context_manager_card(self) -> None:
        """测试使用上下文管理器创建卡片。"""
        with CardBuilder.create() as builder:
            with builder.header() as h:
                h.title("欢迎").template("blue")
            with builder.body() as b:
                b.plain_text("你好，世界！")

        my_card = builder.build()
        assert isinstance(my_card, Card)
        assert my_card.header is not None
        assert my_card.header.title is not None
        assert my_card.header.title.content == "欢迎"
        assert my_card.header.template == "blue"
        assert len(my_card.body.elements) == 1

    def test_nested_context_managers(self) -> None:
        """测试嵌套上下文管理器。"""
        with CardBuilder.create() as builder:
            with builder.body() as b:
                with b.button() as btn:
                    btn.text("点击").type(ButtonType.PRIMARY)

        my_card = builder.build()
        assert len(my_card.body.elements) == 1
        button = my_card.body.elements[0]
        assert isinstance(button, Button)
        assert button.text is not None
        assert button.text.content == "点击"

    def test_context_manager_with_form(self) -> None:
        """测试表单上下文管理器。"""
        with CardBuilder.create() as builder:
            with builder.body() as b:
                with b.form() as f:
                    f.name("login_form")
                    with f.input() as i:
                        i.name("username").placeholder("输入用户名")
                    with f.input() as i:
                        i.name("password").input_type(InputType.PASSWORD)

        my_card = builder.build()
        form = my_card.body.elements[0]
        assert isinstance(form, Form)
        assert form.name == "login_form"
        assert len(form.elements) == 2

    def test_context_manager_with_column_set(self) -> None:
        """测试列集合上下文管理器。"""
        with CardBuilder.create() as builder:
            with builder.body() as b:
                with b.column_set() as cs:
                    with cs.column() as c:
                        c.plain_text("左")
                    with cs.column() as c:
                        c.plain_text("右")

        my_card = builder.build()
        assert len(my_card.body.elements) == 1

    def test_context_manager_with_table(self) -> None:
        """测试表格上下文管理器。"""
        with CardBuilder.create() as builder:
            with builder.body() as b:
                with b.table() as t:
                    with t.column() as col:
                        col.name("name").display_name("名称")
                    with t.row() as r:
                        r.cell("项目1").cell("100")

        my_card = builder.build()
        assert len(my_card.body.elements) == 1


class TestMethodChain:
    """测试方法链 + done() 风格构建。"""

    def test_method_chain_card(self) -> None:
        """测试使用方法链创建卡片。"""
        builder = CardBuilder.create()
        builder.header().title("欢迎").template("blue").done()
        builder.body().plain_text("你好").done()

        my_card = builder.build()
        assert isinstance(my_card, Card)
        assert my_card.header is not None
        assert my_card.header.title is not None
        assert my_card.header.title.content == "欢迎"

    def test_method_chain_with_nested_elements(self) -> None:
        """测试嵌套元素的方法链。"""
        builder = CardBuilder.create()

        body = builder.body()
        body.plain_text("文本")

        form = body.form()
        form.name("test_form")
        form.input().name("field1").placeholder("输入").done()
        form.done()

        body.done()

        my_card = builder.build()
        assert len(my_card.body.elements) == 2


class TestFactoryFunctions:
    """测试工厂函数。"""

    def test_card_factory(self) -> None:
        """测试 card() 工厂函数。"""
        with card(title="欢迎", template="blue") as builder:
            with builder.body() as b:
                b.plain_text("你好，世界！")

        my_card = builder.build()

        assert isinstance(my_card, Card)
        assert my_card.header is not None
        assert my_card.header.title is not None
        assert my_card.header.title.content == "欢迎"
        assert my_card.header.template == "blue"

    def test_card_factory_with_markdown_title(self) -> None:
        """测试带 Markdown 标题的卡片工厂。"""
        my_card = card(title_markdown="**欢迎**").build()
        assert my_card.header is not None
        assert my_card.header.title is not None

    def test_button_factory(self) -> None:
        """测试 button() 工厂函数。"""
        btn = button("点击", type=ButtonType.PRIMARY)

        assert isinstance(btn, Button)
        assert btn.text is not None
        assert btn.text.content == "点击"
        assert btn.type == "primary"

    def test_button_factory_with_callback(self) -> None:
        """测试带回调的按钮工厂。"""
        btn = button("提交", type=ButtonType.PRIMARY, callback={"action": "submit"})

        assert btn.behaviors is not None
        assert len(btn.behaviors) == 1

    def test_button_factory_with_url(self) -> None:
        """测试带 URL 的按钮工厂。"""
        btn = button("了解更多", url="https://example.com")

        assert btn.behaviors is not None
        assert len(btn.behaviors) == 1

    def test_plain_text_factory(self) -> None:
        """测试 plain_text() 工厂函数。"""
        text = plain_text("你好，世界！")

        assert isinstance(text, PlainText)
        assert text.content == "你好，世界！"

    def test_plain_text_factory_with_style(self) -> None:
        """测试带样式的纯文本工厂。"""
        text = plain_text("重要！", size="large", color="red")

        assert text.text_size == "large"
        assert text.text_color == "red"

    def test_markdown_factory(self) -> None:
        """测试 markdown() 工厂函数。"""
        md = markdown("**粗体** 和 *斜体*")

        assert isinstance(md, Markdown)
        assert md.content == "**粗体** 和 *斜体*"

    def test_image_factory(self) -> None:
        """测试 image() 工厂函数。"""
        img = image("https://example.com/photo.png", alt="照片")

        assert img.src == "https://example.com/photo.png"
        assert img.alt == "照片"

    def test_text_tag_factory(self) -> None:
        """测试 text_tag() 工厂函数。"""
        tag = text_tag("新", color="blue")

        assert tag.text == "新"
        assert tag.color == "blue"

    def test_hr_factory(self) -> None:
        """测试 hr() 工厂函数。"""
        divider = hr()

        assert divider.tag == "hr"

    def test_factory_in_card(self) -> None:
        """测试在卡片中使用工厂函数。"""
        with card(title="工厂函数卡片") as builder:
            with builder.body() as b:
                b.add_element(plain_text("你好"))
                b.add_element(button("点击", type=ButtonType.PRIMARY))
                b.add_element(hr())

        my_card = builder.build()

        assert len(my_card.body.elements) == 3


class TestTemplateParameterFiller:
    """测试模板参数填充器。"""

    def test_fill_simple_placeholder(self) -> None:
        """测试填充简单占位符。"""
        filler = TemplateParameterFiller()
        filler.set_parameter("name", "张三")

        result = filler.fill_string("你好，${name}！")
        assert result == "你好，张三！"

    def test_fill_with_default(self) -> None:
        """测试带默认值的填充。"""
        filler = TemplateParameterFiller()

        result = filler.fill_string("你好，${name:访客}！")
        assert result == "你好，访客！"

    def test_fill_multiple_placeholders(self) -> None:
        """测试填充多个占位符。"""
        filler = TemplateParameterFiller()
        filler.set_parameters({"first": "张", "last": "三"})

        result = filler.fill_string("${first}${last}")
        assert result == "张三"

    def test_fill_nested_value(self) -> None:
        """测试填充嵌套值。"""
        filler = TemplateParameterFiller()
        filler.set_parameter("user", {"name": "张三"})

        result = filler.fill_string("你好，${user.name}！")
        assert result == "你好，张三！"


class TestElements:
    """测试元素模型。"""

    def test_plain_text_to_dict(self) -> None:
        """测试纯文本序列化。"""
        text = PlainText(content="你好")
        result = text.to_dict()

        assert result["tag"] == "plain_text"
        assert result["content"] == "你好"

    def test_markdown_to_dict(self) -> None:
        """测试 Markdown 序列化。"""
        md = Markdown(content="**粗体**")
        result = md.to_dict()

        assert result["tag"] == "lark_md"
        assert result["content"] == "**粗体**"

    def test_button_to_dict(self) -> None:
        """测试按钮序列化。"""
        btn = Button(
            text=PlainText(content="点击"),
            type="primary",
        )
        result = btn.to_dict()

        assert result["tag"] == "button"
        assert result["type"] == "primary"
        assert result["text"]["content"] == "点击"

    def test_element_with_none_values_excluded(self) -> None:
        """测试 None 值在序列化时被排除。"""
        text = PlainText(content="你好", text_size=None, text_color=None)
        result = text.to_dict()

        assert "text_size" not in result
        assert "text_color" not in result


class TestEnums:
    """测试枚举类型。"""

    def test_button_type_values(self) -> None:
        """测试按钮类型枚举值。"""
        assert ButtonType.PRIMARY.value == "primary"
        assert ButtonType.DEFAULT.value == "default"
        assert ButtonType.DANGER.value == "danger"
        assert ButtonType.TEXT.value == "text"

    def test_button_size_values(self) -> None:
        """测试按钮大小枚举值。"""
        assert ButtonSize.TINY.value == "tiny"
        assert ButtonSize.SMALL.value == "small"
        assert ButtonSize.MEDIUM.value == "medium"
        assert ButtonSize.LARGE.value == "large"

    def test_input_type_values(self) -> None:
        """测试输入类型枚举值。"""
        assert InputType.TEXT.value == "text"
        assert InputType.PASSWORD.value == "password"
        assert InputType.NUMBER.value == "number"
        assert InputType.EMAIL.value == "email"


class TestTemplateFilling:
    """测试卡片模板填充。"""

    def test_fill_card_template(self) -> None:
        """测试填充卡片模板参数。"""
        with CardBuilder.create() as builder:
            with builder.header() as h:
                h.title("你好，${name}！")
            with builder.body() as b:
                b.plain_text("欢迎，${name:访客}！")
            builder.set_parameter("name", "张三")

        my_card = builder.build()

        assert my_card.header is not None
        assert my_card.header.title is not None
        assert my_card.header.title.content == "你好，张三！"
        assert my_card.body.elements[0].content == "欢迎，张三！"


class TestElementFinder:
    """测试通过 ID 查找和修改组件。"""

    def test_find_element_by_id(self) -> None:
        """测试通过 ID 查找组件。"""
        with CardBuilder.create() as builder:
            with builder.body() as b:
                with b.button() as btn:
                    btn.text("点击").element_id("btn1")
                with b.button() as btn:
                    btn.text("取消").element_id("btn2")

        button = builder.find_element("btn1")
        assert button is not None
        assert button.text is not None
        assert button.text.content == "点击"

    def test_find_element_not_found(self) -> None:
        """测试查找不存在的组件。"""
        with CardBuilder.create() as builder:
            with builder.body() as b:
                b.plain_text("Hello")

        element = builder.find_element("nonexistent")
        assert element is None

    def test_find_element_as(self) -> None:
        """测试通过 ID 查找组件并转换为指定类型。"""
        with CardBuilder.create() as builder:
            with builder.body() as b:
                with b.button() as btn:
                    btn.text("点击").element_id("btn1")

        button = builder.find_element_as("btn1", Button)
        assert button is not None
        assert button.text is not None
        assert button.text.content == "点击"

    def test_find_element_as_wrong_type(self) -> None:
        """测试类型不匹配时返回 None。"""
        with CardBuilder.create() as builder:
            with builder.body() as b:
                b.plain_text("Hello")

        from lark_card_kit import PlainText as PT
        element = builder.find_element_as("nonexistent", PT)
        assert element is None

    def test_update_element_by_id(self) -> None:
        """测试通过 ID 修改组件。"""
        with CardBuilder.create() as builder:
            with builder.body() as b:
                with b.button() as btn:
                    btn.text("点击").element_id("btn1")

        builder.update_element("btn1", lambda e: setattr(e, "disabled", True))

        my_card = builder.build()
        button = my_card.body.elements[0]
        assert isinstance(button, Button)
        assert button.disabled is True

    def test_update_element_as(self) -> None:
        """测试通过 ID 修改组件（指定类型）。"""
        with CardBuilder.create() as builder:
            with builder.body() as b:
                with b.button() as btn:
                    btn.text("点击").element_id("btn1")

        builder.update_element_as("btn1", Button, lambda b: setattr(b, "disabled", True))

        my_card = builder.build()
        button = my_card.body.elements[0]
        assert isinstance(button, Button)
        assert button.disabled is True

    def test_update_element_chain(self) -> None:
        """测试链式调用修改多个组件。"""
        with CardBuilder.create() as builder:
            with builder.body() as b:
                with b.button() as btn:
                    btn.text("按钮1").element_id("btn1")
                with b.button() as btn:
                    btn.text("按钮2").element_id("btn2")

        builder.update_element("btn1", lambda e: setattr(e, "disabled", True)) \
               .update_element("btn2", lambda e: setattr(e, "disabled", True))

        my_card = builder.build()
        assert my_card.body.elements[0].disabled is True
        assert my_card.body.elements[1].disabled is True

    def test_update_nested_element(self) -> None:
        """测试修改嵌套组件。"""
        with CardBuilder.create() as builder:
            with builder.body() as b:
                with b.form() as f:
                    f.name("form1")
                    with f.input() as i:
                        i.name("field1").element_id("input1")

        builder.update_element("input1", lambda e: setattr(e, "required", True))

        my_card = builder.build()
        form = my_card.body.elements[0]
        assert isinstance(form, Form)
        input_field = form.elements[0]
        assert input_field.required is True

    def test_get_finder(self) -> None:
        """测试获取 ElementFinder 实例。"""
        with CardBuilder.create() as builder:
            with builder.body() as b:
                with b.button() as btn:
                    btn.text("点击").element_id("btn1")
                with b.button() as btn:
                    btn.text("取消").element_id("btn2")

        finder = builder.get_finder()
        assert finder is not None

        buttons = finder.find_by_tag("button")
        assert len(buttons) == 2

        finder.update_all_by_tag("button", lambda e: setattr(e, "disabled", True))

        my_card = builder.build()
        assert my_card.body.elements[0].disabled is True
        assert my_card.body.elements[1].disabled is True


class TestElementFinderStandalone:
    """测试独立的 ElementFinder 类。"""

    def test_find_by_tag(self) -> None:
        """测试通过 tag 查找组件。"""
        from lark_card_kit.utils import ElementFinder

        elements = [
            PlainText(content="Hello"),
            Button(text=PlainText(content="Click"), type="primary"),
            Button(text=PlainText(content="Submit"), type="default"),
        ]

        finder = ElementFinder(elements)
        buttons = finder.find_by_tag("button")

        assert len(buttons) == 2

    def test_update_all_by_tag(self) -> None:
        """测试批量修改组件。"""
        from lark_card_kit.utils import ElementFinder

        elements = [
            Button(text=PlainText(content="Click"), type="primary"),
            Button(text=PlainText(content="Submit"), type="default"),
        ]

        finder = ElementFinder(elements)
        count = finder.update_all_by_tag("button", lambda e: setattr(e, "disabled", True))

        assert count == 2
        assert elements[0].disabled is True
        assert elements[1].disabled is True


if __name__ == "__main__":
    pytest.main([__file__])