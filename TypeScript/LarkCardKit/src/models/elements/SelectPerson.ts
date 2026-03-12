import type { ElementBase } from './Element';
import type { PlainText } from './PlainText';
import type { ConfirmConfig } from './Button';
import type { OpenUrlBehavior, CallbackBehavior } from './Behaviors';

export interface SelectPerson extends ElementBase {
  tag: 'select_person';
  name: string;
  placeholder?: PlainText;
  label?: PlainText;
  selected_options?: PersonSelectedOption[];
  multiple?: boolean;
  disabled?: boolean;
  allowed_user_ids?: string[];
  confirm?: ConfirmConfig;
  behaviors?: (OpenUrlBehavior | CallbackBehavior)[];
}

export interface MultiSelectPerson extends ElementBase {
  tag: 'multi_select_person';
  name: string;
  placeholder?: PlainText;
  label?: PlainText;
  selected_options?: PersonSelectedOption[];
  disabled?: boolean;
  allowed_user_ids?: string[];
  confirm?: ConfirmConfig;
  behaviors?: (OpenUrlBehavior | CallbackBehavior)[];
}

export interface PersonSelectedOption {
  user_id?: string;
  user_name?: string;
  avatar_url?: string;
}
