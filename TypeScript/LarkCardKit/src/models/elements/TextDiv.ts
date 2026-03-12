import type { ElementBase } from './Element';
import type { PlainText } from './PlainText';

/**
 * 普通文本组件（飞书卡片 2.0 格式）
 * 用于展示普通文本内容，支持设置布局属性
 * text 属性只能是 PlainText (tag: "plain_text")
 */
export interface TextDiv extends ElementBase {
  tag: 'div';
  text?: PlainText;
  direction?: 'vertical' | 'horizontal';
  padding?: string;
  vertical_spacing?: string;
  horizontal_spacing?: string;
  horizontal_align?: string;
  vertical_align?: string;
  background_style?: string;
  element_id?: string;
  margin?: string;
  width?: string;
}
