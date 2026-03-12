import type { ElementBase } from './Element';
import type { PlainText } from './PlainText';
import type { ConfirmConfig } from './Button';
import type { OpenUrlBehavior, CallbackBehavior } from './Behaviors';

export interface Input extends ElementBase {
  tag: 'input';
  name: string;
  input_type?: string;
  required?: boolean;
  placeholder?: PlainText;
  label?: PlainText;
  default_value?: string;
  disabled?: boolean | string;
  disabled_tips?: PlainText;
  confirm?: ConfirmConfig;
  max_length?: number;
  max_lines?: number;
  show_icon?: boolean;
  behaviors?: (OpenUrlBehavior | CallbackBehavior)[];
}
