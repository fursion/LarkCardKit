import type { ElementBase } from './Element';

/**
 * Markdown 文本对象（飞书卡片 2.0 格式）
 * tag: "lark_md"
 * 用于 CardHeader.title 等，不用于 TextDiv.text
 */
export interface MarkdownText extends ElementBase {
  tag: 'lark_md';
  content: string;
  text_size?: string | number;
  text_color?: string;
  text_align?: string;
  notation?: boolean;
  width?: string;
  lines?: number;
}
