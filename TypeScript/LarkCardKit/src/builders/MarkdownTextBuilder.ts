import type { MarkdownText } from '../models/elements';

export class MarkdownTextBuilder {
  private _text: MarkdownText = { tag: 'lark_md', content: '' };

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
    this._text.lines = value;
    return this;
  }

  build(): MarkdownText {
    return this._text;
  }
}
