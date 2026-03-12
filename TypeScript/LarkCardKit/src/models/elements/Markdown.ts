import type { ElementBase } from './Element';

export interface Markdown extends ElementBase {
  tag: 'markdown';
  content: string;
  text_size?: string | number;
  text_color?: string;
}
