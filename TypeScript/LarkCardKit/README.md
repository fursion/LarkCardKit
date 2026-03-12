# LarkCardKit

TypeScript library for building Lark Card 2.0 messages. Provides a fluent builder API for constructing interactive cards for Feishu/Lark messaging platform.

## Installation

```bash
npm install lark-card-kit
```

## Quick Start

```typescript
import { CardBuilder, ButtonType } from 'lark-card-kit';

// Build a simple card
const card = CardBuilder.create()
  .header(h => h
    .title('Welcome')
    .template('blue'))
  .body(b => b
    .plainText('Hello, World!')
    .button(btn => btn
      .text('Click Me')
      .type(ButtonType.Primary)
      .onClick({ action: 'click' })))
  .build();

// Convert to JSON for API
const json = CardBuilder.create()
  .header(h => h.title('My Card'))
  .body(b => b.plainText('Content'))
  .toJson();
```

## Features

- **Fluent Builder API**: Chain methods for readable card construction
- **Full TypeScript Support**: Complete type definitions for all components
- **Lark Card 2.0 Compliant**: Outputs JSON conforming to Lark Card 2.0 specification
- **Template Parameters**: Support for dynamic content with parameter substitution
- **Element Operations**: Find, modify, and replace elements in constructed cards

## Supported Components

### Text Elements
- `plainText()` - Plain text content
- `markdown()` - Markdown formatted content

### Interactive Elements
- `button()` - Interactive buttons with callbacks or URL actions
- `input()` - Text input fields
- `select()` - Dropdown selection
- `datePicker()` - Date picker
- `checkbox()` - Checkbox options
- `pickerTime()` - Time picker
- `pickerDatetime()` - Date-time picker

### Layout Elements
- `div()` - Container with layout options
- `form()` - Form container
- `columnSet()` - Multi-column layout
- `hr()` - Horizontal divider

### Advanced Elements
- `table()` - Data table
- `person()` - Person display
- `personList()` - Person list
- `selectPerson()` - Person selector
- `multiSelectPerson()` - Multi-person selector
- `overflow()` - Overflow menu
- `image()` - Image display
- `interactiveContainer()` - Interactive container

## Builder Pattern

```typescript
// Create a card with header and body
const card = CardBuilder.create()
  .config(c => c
    .updateMulti(true)
    .enableForward(true))
  .header(h => h
    .title('Card Title')
    .subtitle('Subtitle')
    .template('blue')
    .icon('smile'))
  .body(b => b
    .verticalSpacing('medium')
    .markdown('**Bold text** and *italic*')
    .hr()
    .div(d => d
      .horizontal()
      .button(btn => btn.text('OK').submit())
      .button(btn => btn.text('Cancel').reset())))
  .build();

// Output JSON
console.log(card);
// or
const json = CardBuilder.create()
  .header(h => h.title('Title'))
  .body(b => b.plainText('Content'))
  .toJson();
```

## Template Parameters

```typescript
const card = CardBuilder.create()
  .header(h => h.title('${title}'))
  .body(b => b.plainText('Hello, ${userName}!'))
  .setParameter('title', 'Welcome')
  .setParameter('userName', 'John')
  .build();
// Result: title = "Welcome", content = "Hello, John!"
```

## Element Operations

```typescript
const builder = CardBuilder.create()
  .body(b => b
    .plainText('Text')
    .button(btn => btn.elementId('btn1').text('Button')));

// Find element
const button = builder.findElementById('btn1');

// Modify element
builder.modifyElement('btn1', el => {
  if (el.tag === 'button') {
    el.text = { tag: 'plain_text', content: 'New Text' };
  }
});

// Replace element
builder.replaceElement('btn1', { tag: 'hr' });
```

## License

MIT
