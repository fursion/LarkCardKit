import type { ElementBase } from './Element';
import type { PlainText } from './PlainText';
import type { OpenUrlBehavior, CallbackBehavior } from './Behaviors';

export interface ConfirmConfig {
  title?: PlainText;
  text?: PlainText;
}

export interface ButtonIcon {
  tag: 'standard_icon' | 'custom_icon';
  token?: string;
  color?: string;
  img_key?: string;
}

export interface UrlClickAction {
  url: string;
}

export interface Button extends ElementBase {
  tag: 'button';
  type?: string;
  size?: string;
  width?: string;
  text?: PlainText;
  icon?: ButtonIcon;
  hover_tips?: PlainText;
  disabled?: boolean;
  disabled_tips?: PlainText;
  confirm?: ConfirmConfig;
  behaviors?: (OpenUrlBehavior | CallbackBehavior)[];
  name?: string;
  form_action_type?: string;
  onclick?: UrlClickAction;
}
