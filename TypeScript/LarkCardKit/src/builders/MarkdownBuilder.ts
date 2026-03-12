import type { Markdown } from '../models/elements';

export class MarkdownBuilder {
  private _markdown: Markdown = { tag: 'markdown', content: '' };

  content(content: string): this {
    this._markdown.content = content;
    return this;
  }

  textSize(size: string | number): this {
    this._markdown.text_size = size;
    return this;
  }

  textColor(color: string): this {
    this._markdown.text_color = color;
    return this;
  }

  build(): Markdown {
    return this._markdown;
  }
}
