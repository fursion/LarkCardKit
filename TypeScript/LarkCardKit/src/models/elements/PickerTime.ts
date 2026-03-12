import type { ElementBase } from './Element';
import type { PlainText } from './PlainText';
import type { ConfirmConfig } from './Button';
import type { OpenUrlBehavior, CallbackBehavior } from './Behaviors';

export interface PickerTime extends ElementBase {
  tag: 'picker_time';
  name: string;
  placeholder?: PlainText;
  label?: PlainText;
  initial_time?: string;
  disabled?: boolean;
  confirm?: ConfirmConfig;
  behaviors?: (OpenUrlBehavior | CallbackBehavior)[];
}
