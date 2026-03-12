import type { ElementBase } from './Element';
import type { PlainText } from './PlainText';

export interface SelectImgOption {
  value: string;
  text?: PlainText;
}

export interface SelectImg extends ElementBase {
  tag: 'select_img';
  name?: string;
  required?: boolean;
  placeholder?: PlainText;
  label?: PlainText;
  select_mode?: 'single' | 'multi';
  initial_options?: string[];
  options?: SelectImgOption[];
  width?: string;
  disabled?: boolean;
  confirm?: ConfirmConfig;
  behaviors?: any[];
}

export interface ConfirmConfig {
  title?: PlainText;
  text?: PlainText;
}
