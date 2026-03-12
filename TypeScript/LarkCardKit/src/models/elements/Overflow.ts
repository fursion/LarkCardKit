import type { ElementBase } from './Element';
import type { PlainText } from './PlainText';

export interface Overflow extends ElementBase {
  tag: 'overflow';
  options?: OverflowOption[];
}

export interface OverflowOption {
  text?: PlainText;
  value: string;
}
