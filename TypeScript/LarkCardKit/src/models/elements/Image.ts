import type { ElementBase } from './Element';
import type { PlainText } from './PlainText';

export interface Image extends ElementBase {
  tag: 'img';
  img_key: string;
  alt?: PlainText;
  size?: string;
  mode?: string;
  width?: string;
  height?: string;
  preview?: boolean;
  hover_tips?: PlainText;
  click_url?: string;
}
