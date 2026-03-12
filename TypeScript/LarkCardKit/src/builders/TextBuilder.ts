import type { PlainText, MarkdownText } from '../models/elements';

export class TextBuilder {
  private _text: PlainText | MarkdownText = { tag: 'plain_text', content: '' };

  /**
   * 设置文本标签
   * @param tag - 'plain_text' 或 'lark_md'
   */
  tag(tag: 'plain_text' | 'lark_md'): this {
    if (tag === 'lark_md') {
      this._text = { tag: 'lark_md', content: '' };
    } else {
      this._text = { tag: 'plain_text', content: '' };
    }
    return this;
  }

  /**
   * 设置为 Markdown 标签（快捷方法）
   */
  asMarkdown(): this {
    this._text = { tag: 'lark_md', content: '' };
    return this;
  }

  /**
   * 设置为 PlainText 标签（快捷方法）
   */
  asPlainText(): this {
    this._text = { tag: 'plain_text', content: '' };
    return this;
  }

  content(content: string): this {
    this._text.content = content;
    return this;
  }

  textSize(size: string | number): this {
    this._text.text_size = size;
    return this;
  }

  textColor(color: string): this {
    this._text.text_color = color;
    return this;
  }

  textAlign(align: string): this {
    this._text.text_align = align;
    return this;
  }

  notation(value: boolean): this {
    this._text.notation = value;
    return this;
  }

  width(value: string): this {
    this._text.width = value;
    return this;
  }

  lines(value: number): this {
    if (this._text.tag === 'lark_md') {
      this._text.lines = value;
    }
    return this;
  }

  elementId(id: string): this {
    this._text.element_id = id;
    return this;
  }

  margin(value: string): this {
    if (this._text.tag === 'plain_text') {
      this._text.margin = value;
    }
    return this;
  }

  /**
   * 构建文本对象
   */
  build(): PlainText | MarkdownText {
    return this._text;
  }
}
