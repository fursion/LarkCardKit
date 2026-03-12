import type { Button, PlainText, ConfirmConfig, OpenUrlBehavior, CallbackBehavior } from '../models/elements';
import { ButtonType } from '../enums';
import { ButtonSize } from '../enums';

export class ButtonBuilder {
  private _button: Button = { tag: 'button' };

  text(text: string): this {
    this._button.text = { tag: 'plain_text', content: text };
    return this;
  }

  type(type: ButtonType | string): this {
    this._button.type = typeof type === 'string' ? type : type;
    return this;
  }

  size(size: ButtonSize | string): this {
    this._button.size = typeof size === 'string' ? size : size;
    return this;
  }

  width(width: string): this {
    this._button.width = width;
    return this;
  }

  elementId(id: string): this {
    this._button.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._button.margin = margin;
    return this;
  }

  disabled(disabled: boolean = true): this {
    this._button.disabled = disabled;
    return this;
  }

  disabledTips(tips: string): this {
    this._button.disabled_tips = { tag: 'plain_text', content: tips };
    return this;
  }

  hoverTips(tips: string): this {
    this._button.hover_tips = { tag: 'plain_text', content: tips };
    return this;
  }

  confirm(title: string, text: string): this {
    this._button.confirm = {
      title: { tag: 'plain_text', content: title },
      text: { tag: 'plain_text', content: text }
    };
    return this;
  }

  onClick(callbackData: unknown): this {
    this._button.behaviors ??= [];
    this._button.behaviors.push({ type: 'callback', value: callbackData } as CallbackBehavior);
    return this;
  }

  onClickUrl(url: string, pcUrl?: string, iosUrl?: string, androidUrl?: string): this {
    this._button.behaviors ??= [];
    const behavior: OpenUrlBehavior = { type: 'open_url', default_url: url };
    if (pcUrl) behavior.pc_url = pcUrl;
    if (iosUrl) behavior.ios_url = iosUrl;
    if (androidUrl) behavior.android_url = androidUrl;
    this._button.behaviors.push(behavior);
    return this;
  }

  onClickUrlV2(url: string): this {
    this._button.onclick = { url };
    return this;
  }

  formAction(actionType: string): this {
    this._button.form_action_type = actionType;
    return this;
  }

  submit(): this {
    this._button.form_action_type = 'submit';
    return this;
  }

  reset(): this {
    this._button.form_action_type = 'reset';
    return this;
  }

  name(name: string): this {
    this._button.name = name;
    return this;
  }

  build(): Button {
    return this._button;
  }
}
