import type { ElementBase } from './Element';
import type { PlainText } from './PlainText';

export interface Checker extends ElementBase {
  tag: 'checker';
  name: string;
  text?: PlainText;
  checked?: boolean;
  disabled?: boolean;
}
