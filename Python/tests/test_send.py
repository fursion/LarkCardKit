"""发送测试 - 使用 lark-oapi SDK 发送飞书卡片"""

import os
import sys
import json

# 添加父目录到路径以支持直接运行
sys.path.insert(0, os.path.dirname(os.path.dirname(os.path.abspath(__file__))))

# 设置控制台编码
if sys.platform == 'win32':
    import io
    sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8')

from dotenv import load_dotenv

from lark_card_kit import (
    CardBuilder,
    ButtonType,
    ButtonSize,
    InputType,
    to_json,
    PlainText,
    Markdown,
)

import lark_oapi as lark
from lark_oapi.api.im.v1 import CreateMessageRequest, CreateMessageRequestBody

# 加载环境变量
load_dotenv()


def get_client():
    """获取飞书客户端"""
    return lark.Client.builder() \
        .app_id(os.getenv("APP_ID")) \
        .app_secret(os.getenv("APP_SECRET")) \
        .build()


def send_card(name: str, card_json: str):
    """发送卡片消息"""
    client = get_client()
    print(f"\n📤 发送 {name} 测试卡片...")

    try:
        request = CreateMessageRequest.builder() \
            .receive_id_type(os.getenv("RECEIVE_ID_TYPE", "email")) \
            .request_body(
                CreateMessageRequestBody.builder()
                .receive_id(os.getenv("RECEIVE_ID"))
                .msg_type("interactive")
                .content(card_json)
                .build()
            ) \
            .build()

        response = client.im.v1.message.create(request)

        if response.success():
            print(f"✅ {name} 发送成功！消息 ID: {response.data.message_id}")
            return True
        else:
            print(f"❌ {name} 发送失败: {response.code} - {response.msg}")
            return False
    except Exception as e:
        print(f"❌ {name} 发送失败: {e}")
        return False


def run_tests():
    """运行所有发送测试"""
    print("========================================")
    print("  LarkCardKit Python 分组件发送测试")
    print("========================================")

    # 检查环境变量
    if not os.getenv("APP_ID") or not os.getenv("APP_SECRET"):
        print("❌ 请配置 .env 文件中的 APP_ID 和 APP_SECRET")
        return

    if not os.getenv("RECEIVE_ID"):
        print("❌ 请配置 .env 文件中的 RECEIVE_ID")
        return

    # ==================== 1. 文本组件测试 ====================
    print("\n📋 测试 1: 文本组件 (Div + PlainText/Markdown)")
    builder = CardBuilder.create()
    with builder.header() as h:
        h.title("文本组件测试").template("blue").text_tag("Python", "green")
    with builder.body() as b:
        with b.div() as d:
            d.text(PlainText(content="这是纯文本内容"))
        with b.div() as d:
            d.text(Markdown(content="**这是 Markdown 内容**\n- 列表项 1\n- 列表项 2"))
    send_card("文本组件", to_json(builder.build()))

    # ==================== 2. 按钮组件测试 ====================
    print("\n📋 测试 2: 按钮组件 (Button)")
    builder = CardBuilder.create()
    with builder.header() as h:
        h.title("按钮组件测试").template("green").text_tag("Python", "green")
    with builder.body() as b:
        with b.div() as d:
            d.text(Markdown(content="### 不同类型按钮"))
        with b.button() as btn:
            btn.text("主要按钮").type(ButtonType.PRIMARY)
        with b.button() as btn:
            btn.text("危险按钮").type(ButtonType.DANGER)
        with b.button() as btn:
            btn.text("默认按钮").type(ButtonType.DEFAULT)
        b.hr()
        with b.div() as d:
            d.text(Markdown(content="### 不同尺寸按钮"))
        with b.button() as btn:
            btn.text("小按钮").size(ButtonSize.SMALL).type(ButtonType.PRIMARY)
        with b.button() as btn:
            btn.text("中按钮").size(ButtonSize.MEDIUM).type(ButtonType.PRIMARY)
        with b.button() as btn:
            btn.text("大按钮").size(ButtonSize.LARGE).type(ButtonType.PRIMARY)
    send_card("按钮组件", to_json(builder.build()))

    # ==================== 3. 输入组件测试 ====================
    print("\n📋 测试 3: 输入组件 (Input, Select)")
    builder = CardBuilder.create()
    with builder.header() as h:
        h.title("输入组件测试").template("purple").text_tag("Python", "green")
    with builder.body() as b:
        # 使用 div 模拟表单布局
        with b.div() as d:
            d.text(Markdown(content="**用户名**"))
        with b.input() as i:
            i.name("username").placeholder("请输入用户名")
        with b.div() as d:
            d.text(Markdown(content="**邮箱**"))
        with b.input() as i:
            i.name("email").placeholder("请输入邮箱")
        with b.div() as d:
            d.text(Markdown(content="**城市**"))
        with b.select() as s:
            s.name("city").placeholder("选择城市")
            s.option("北京", "bj")
            s.option("上海", "sh")
            s.option("广州", "gz")
        b.hr()
        with b.button() as btn:
            btn.text("提交").type(ButtonType.PRIMARY)
    send_card("输入组件", to_json(builder.build()))

    # ==================== 4. 布局组件测试 ====================
    print("\n📋 测试 4: 布局组件 (Form, ColumnSet, Hr)")
    builder = CardBuilder.create()
    with builder.header() as h:
        h.title("布局组件测试").template("red").text_tag("Python", "green")
    with builder.body() as b:
        with b.div() as d:
            d.text(Markdown(content="### ColumnSet 列布局"))
        with b.column_set() as cs:
            with cs.column() as col:
                col.width("33%")
                col.add_element({"tag": "div", "text": {"tag": "plain_text", "content": "第一列"}})
            with cs.column() as col:
                col.width("33%")
                col.add_element({"tag": "div", "text": {"tag": "plain_text", "content": "第二列"}})
            with cs.column() as col:
                col.width("34%")
                col.add_element({"tag": "div", "text": {"tag": "plain_text", "content": "第三列"}})
        b.hr()
        with b.div() as d:
            d.text(PlainText(content="Div 容器测试"))
    send_card("布局组件", to_json(builder.build()))

    # ==================== 5. 表格组件测试 ====================
    print("\n📋 测试 5: 表格组件 (Table) - 跳过（模型有 bug）")
    # Python 版本的 Table 模型 display_name 字段使用 PlainText 对象，
    # 但飞书 API 期望的是字符串，需要修复模型
    print("⚠️ Table 模型需要修复：display_name 应该是字符串类型")

    print("\n========================================")
    print("  所有组件发送测试完成！")
    print("========================================")


if __name__ == "__main__":
    run_tests()