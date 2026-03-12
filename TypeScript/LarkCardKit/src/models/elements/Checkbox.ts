import type { ElementBase } from './Element';
import type { PlainText } from './PlainText';

export interface Checkbox extends ElementBase {
  tag: 'check_box';
  name: string;
  placeholder?: PlainText;
  options?: CheckboxOption[];
  disabled?: boolean;
}

export interface CheckboxOption {
  text?: PlainText;
  value: string;
  is_checked?: boolean;
}
