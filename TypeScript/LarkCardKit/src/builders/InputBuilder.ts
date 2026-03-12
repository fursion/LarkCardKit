import type { Input } from '../models/elements';
import { InputType } from '../enums';

export class InputBuilder {
  private _input: Input = { tag: 'input', name: '' };

  name(name: string): this {
    this._input.name = name;
    return this;
  }

  inputType(type: InputType | string): this {
    this._input.input_type = typeof type === 'string' ? type : type;
    return this;
  }

  required(required: boolean = true): this {
    this._input.required = required;
    return this;
  }

  placeholder(placeholder: string): this {
    this._input.placeholder = { tag: 'plain_text', content: placeholder };
    return this;
  }

  label(label: string): this {
    this._input.label = { tag: 'plain_text', content: label };
    return this;
  }

  defaultValue(value: string): this {
    this._input.default_value = value;
    return this;
  }

  disabled(disabled: boolean = true): this {
    this._input.disabled = disabled;
    return this;
  }

  disabledTips(tips: string): this {
    this._input.disabled_tips = { tag: 'plain_text', content: tips };
    return this;
  }

  confirm(title: string, text: string): this {
    this._input.confirm = {
      title: { tag: 'plain_text', content: title },
      text: { tag: 'plain_text', content: text }
    };
    return this;
  }

  maxLength(length: number): this {
    this._input.max_length = length;
    return this;
  }

  maxLines(lines: number): this {
    this._input.max_lines = lines;
    return this;
  }

  elementId(id: string): this {
    this._input.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._input.margin = margin;
    return this;
  }

  build(): Input {
    return this._input;
  }
}
