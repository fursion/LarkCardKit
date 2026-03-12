import type { ElementBase } from './Element';
import type { PlainText } from './PlainText';
import type { ConfirmConfig } from './Button';
import type { OpenUrlBehavior, CallbackBehavior } from './Behaviors';

export interface PickerDatetime extends ElementBase {
  tag: 'picker_datetime';
  name: string;
  placeholder?: PlainText;
  label?: PlainText;
  initial_datetime?: string;
  disabled?: boolean;
  confirm?: ConfirmConfig;
  behaviors?: (OpenUrlBehavior | CallbackBehavior)[];
}
