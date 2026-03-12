import type { ElementBase } from './Element';
import type { PlainText } from './PlainText';
import type { ConfirmConfig } from './Button';
import type { OpenUrlBehavior, CallbackBehavior } from './Behaviors';

export interface Select extends ElementBase {
  tag: 'select_static';
  name: string;
  placeholder?: PlainText;
  label?: PlainText;
  options?: SelectOption[];
  initial_option?: string;
  multiple?: boolean;
  disabled?: boolean;
  option_template_key?: string;
  confirm?: ConfirmConfig;
  behaviors?: (OpenUrlBehavior | CallbackBehavior)[];
}

export interface SelectOption {
  text?: PlainText;
  value: string;
  description?: PlainText;
  is_selected?: boolean;
}
