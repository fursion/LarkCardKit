import type { ElementBase } from './Element';

export interface PlainText extends ElementBase {
  tag: 'plain_text';
  content: string;
  text_size?: string | number;
  text_color?: string;
  text_align?: string;
  notation?: boolean;
  width?: string;
  lines?: number;
  element_id?: string;
  margin?: string;
}
