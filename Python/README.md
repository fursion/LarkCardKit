# LarkCardKit Python

A Python library for building Feishu/Lark card 2.0 messages.

## Installation

```bash
pip install lark-card-kit
```

## Quick Start

```python
from lark_card_kit import CardBuilder, ButtonType

# Create a simple card
card = (CardBuilder.create()
    .header(lambda h: h.title("Welcome"))
    .body(lambda b: b
        .plain_text("Hello, World!")
        .button(lambda btn: btn
            .text("Click Me")
            .type(ButtonType.PRIMARY)
            .callback({"action": "click"})))
    .build())

# Convert to JSON
json_str = card.to_json(indent=2)
print(json_str)
```

## Features

- **Fluent API**: Chain method calls for readable card construction
- **Type Safety**: Full type hints for static analysis
- **Template Parameters**: Support for `${key}` placeholder syntax
- **JSON Serialization**: Automatic conversion to Lark card 2.0 format

## Documentation

### Building Cards

Use `CardBuilder` to construct cards with a fluent API:

```python
from lark_card_kit import CardBuilder, ButtonType, ButtonSize

card = (CardBuilder.create()
    .config(lambda c: c
        .update_multi(True)
        .enable_forward(True))
    .header(lambda h: h
        .title("Card Title")
        .subtitle("Subtitle")
        .template("blue")
        .icon(token="chat-forbidden", color="blue"))
    .body(lambda b: b
        .plain_text("Simple text content")
        .markdown("**Bold** and *italic*")
        .button(lambda btn: btn
            .text("Primary Button")
            .type(ButtonType.PRIMARY)
            .size(ButtonSize.MEDIUM))
        .hr()
        .form(lambda f: f
            .name("user_form")
            .input(lambda i: i
                .name("username")
                .placeholder("Enter username"))
            .input(lambda i: i
                .name("password")
                .input_type("password"))))
    .build())
```

### Template Parameters

Use `${key}` placeholders in text content:

```python
card = (CardBuilder.create()
    .header(lambda h: h.title("Hello, ${name}!"))
    .body(lambda b: b.plain_text("Welcome, ${name:Guest}!"))
    .set_parameter("name", "John")
    .build())
```

### Elements

The library supports all Lark card 2.0 elements:

- **Text**: `PlainText`, `Markdown`, `MarkdownText`
- **Input**: `Input`, `Select`, `DatePicker`, `PickerTime`, `PickerDatetime`
- **Container**: `Form`, `ColumnSet`, `Div`, `CollapsiblePanel`
- **Interactive**: `Button`, `Overflow`, `Checker`
- **Display**: `Image`, `Table`, `TextTag`, `Chart`, `Person`, `PersonList`

### Enums

Use enums for type-safe configuration:

```python
from lark_card_kit import ButtonType, ButtonSize, InputType

ButtonType.PRIMARY    # "primary"
ButtonType.DEFAULT    # "default"
ButtonType.DANGER     # "danger"
ButtonType.TEXT       # "text"

ButtonSize.TINY       # "tiny"
ButtonSize.SMALL      # "small"
ButtonSize.MEDIUM     # "medium"
ButtonSize.LARGE      # "large"

InputType.TEXT        # "text"
InputType.PASSWORD    # "password"
InputType.NUMBER      # "number"
InputType.EMAIL       # "email"
```

## License

MIT License