import type { TextTag } from '../models/elements';

export class TextTagBuilder {
  private _tag: TextTag = { tag: 'text_tag', text: { tag: 'plain_text', content: '' } };

  /**
   * 设置纯文本内容
   */
  text(text: string): this {
    this._tag.text = { tag: 'plain_text', content: text };
    return this;
  }

  /**
   * 设置 Markdown 文本内容（飞书 Markdown 格式）
   */
  textWithMarkdown(content: string): this {
    this._tag.text = { tag: 'lark_md', content };
    return this;
  }

  color(color: string): this {
    this._tag.color = color;
    return this;
  }

  build(): TextTag {
    return this._tag;
  }
}
