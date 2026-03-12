import type { Checkbox, CheckboxOption } from '../models/elements';

export class CheckboxBuilder {
  private _checkbox: Checkbox = { tag: 'check_box', name: '' };

  name(name: string): this {
    this._checkbox.name = name;
    return this;
  }

  placeholder(placeholder: string): this {
    this._checkbox.placeholder = { tag: 'plain_text', content: placeholder };
    return this;
  }

  option(text: string, value: string, isChecked: boolean = false): this {
    this._checkbox.options ??= [];
    this._checkbox.options.push({
      text: { tag: 'plain_text', content: text },
      value,
      is_checked: isChecked
    });
    return this;
  }

  options(options: Array<{ text: string; value: string; isChecked?: boolean }>): this {
    this._checkbox.options = options.map(opt => ({
      text: { tag: 'plain_text' as const, content: opt.text },
      value: opt.value,
      ...(opt.isChecked && { is_checked: opt.isChecked })
    }));
    return this;
  }

  disabled(disabled: boolean = true): this {
    this._checkbox.disabled = disabled;
    return this;
  }

  elementId(id: string): this {
    this._checkbox.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._checkbox.margin = margin;
    return this;
  }

  build(): Checkbox {
    return this._checkbox;
  }
}
