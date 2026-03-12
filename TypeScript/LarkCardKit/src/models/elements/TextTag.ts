import type { ElementBase, TextObject } from './Element';

export interface TextTag extends ElementBase {
  tag: 'text_tag';
  text?: TextObject;
  color?: string;
}
