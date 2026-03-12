import type { CardHeader, HeaderIcon } from '../models';
import type { PlainText, MarkdownText, TextTag } from '../models/elements';
import { TextTagBuilder } from './TextTagBuilder';
import { TextBuilder } from './TextBuilder';

export class CardHeaderBuilder {
  private _header: CardHeader = {};

  /**
   * 设置标题（简单字符串，默认使用 plain_text）
   */
  title(title: string): this;
  /**
   * 设置标题（使用 TextBuilder 配置）
   */
  title(configure: (builder: TextBuilder) => void): this;
  title(titleOrConfigure: string | ((builder: TextBuilder) => void)): this {
    if (typeof titleOrConfigure === 'string') {
      this._header.title = { tag: 'plain_text', content: titleOrConfigure };
    } else {
      const builder = new TextBuilder();
      titleOrConfigure(builder);
      this._header.title = builder.build() as PlainText | MarkdownText;
    }
    return this;
  }

  /**
   * 设置副标题（简单字符串，默认使用 plain_text）
   */
  subtitle(subtitle: string): this;
  /**
   * 设置副标题（使用 TextBuilder 配置）
   */
  subtitle(configure: (builder: TextBuilder) => void): this;
  subtitle(subtitleOrConfigure: string | ((builder: TextBuilder) => void)): this {
    if (typeof subtitleOrConfigure === 'string') {
      this._header.subtitle = { tag: 'plain_text', content: subtitleOrConfigure };
    } else {
      const builder = new TextBuilder();
      subtitleOrConfigure(builder);
      this._header.subtitle = builder.build() as PlainText | MarkdownText;
    }
    return this;
  }

  template(template: string): this {
    this._header.template = template;
    return this;
  }

  icon(token: string, color?: string): this {
    const icon: HeaderIcon = { tag: 'standard_icon', token };
    if (color) icon.color = color;
    this._header.icon = icon;
    return this;
  }

  customIcon(imgKey: string): this {
    this._header.icon = { tag: 'custom_icon', img_key: imgKey };
    return this;
  }

  textTag(configure: (builder: TextTagBuilder) => void): this {
    const builder = new TextTagBuilder();
    configure(builder);
    this._header.text_tag_list ??= [];
    this._header.text_tag_list.push(builder.build());
    return this;
  }

  padding(padding: string): this {
    this._header.padding = padding;
    return this;
  }

  build(): CardHeader {
    return this._header;
  }
}
