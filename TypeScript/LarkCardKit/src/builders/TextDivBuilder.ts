import type { TextDiv } from '../models/elements/TextDiv';
import type { PlainText } from '../models/elements/PlainText';
import { PlainTextBuilder } from './PlainTextBuilder';

/**
 * TextDiv 构建器（普通文本组件）
 * text 属性只能是 PlainText (tag: "plain_text")
 */
export class TextDivBuilder {
  private _textDiv: TextDiv = { tag: 'div' };
  /**
   * 设置 PlainText 对象（可配置）
   */
  Text(configure: (builder: PlainTextBuilder) => void): this {
    const builder = new PlainTextBuilder();
    configure(builder);
    this._textDiv.text = builder.build();
    return this;
  }

  /**
   * 设置布局方向
   */
  direction(value: 'vertical' | 'horizontal'): this {
    this._textDiv.direction = value;
    return this;
  }

  /**
   * 设置为垂直布局
   */
  vertical(): this {
    this._textDiv.direction = 'vertical';
    return this;
  }

  /**
   * 设置为水平布局
   */
  horizontal(): this {
    this._textDiv.direction = 'horizontal';
    return this;
  }

  /**
   * 设置内边距
   */
  padding(padding: string): this {
    this._textDiv.padding = padding;
    return this;
  }

  /**
   * 设置垂直间距
   */
  verticalSpacing(spacing: string): this {
    this._textDiv.vertical_spacing = spacing;
    return this;
  }

  /**
   * 设置水平间距
   */
  horizontalSpacing(spacing: string): this {
    this._textDiv.horizontal_spacing = spacing;
    return this;
  }

  /**
   * 设置水平对齐方式
   */
  horizontalAlign(align: 'left' | 'center' | 'right'): this {
    this._textDiv.horizontal_align = align;
    return this;
  }

  /**
   * 设置垂直对齐方式
   */
  verticalAlign(align: 'top' | 'center' | 'bottom'): this {
    this._textDiv.vertical_align = align;
    return this;
  }

  /**
   * 设置背景样式
   */
  backgroundStyle(style: string): this {
    this._textDiv.background_style = style;
    return this;
  }

  /**
   * 设置元素 ID
   */
  elementId(id: string): this {
    this._textDiv.element_id = id;
    return this;
  }

  /**
   * 设置外边距
   */
  margin(margin: string): this {
    this._textDiv.margin = margin;
    return this;
  }

  /**
   * 设置宽度
   */
  width(width: string): this {
    this._textDiv.width = width;
    return this;
  }

  /**
   * 构建 TextDiv 对象
   */
  build(): TextDiv {
    return this._textDiv;
  }
}

/**
 * @deprecated 已重命名为 TextDivBuilder
 */
export const DivBuilder = TextDivBuilder;
