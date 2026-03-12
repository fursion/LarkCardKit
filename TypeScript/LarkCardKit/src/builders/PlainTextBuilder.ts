import type { PlainText } from '../models/elements';

export class PlainTextBuilder {
  private _text: PlainText = { tag: 'plain_text', content: '' };

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

  build(): PlainText {
    return this._text;
  }
}
