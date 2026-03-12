import type { PickerDatetime } from '../models/elements';

export class PickerDatetimeBuilder {
  private _picker: PickerDatetime = { tag: 'picker_datetime', name: '' };

  name(name: string): this {
    this._picker.name = name;
    return this;
  }

  placeholder(placeholder: string): this {
    this._picker.placeholder = { tag: 'plain_text', content: placeholder };
    return this;
  }

  initialDatetime(datetime: string): this {
    this._picker.initial_datetime = datetime;
    return this;
  }

  disabled(disabled: boolean = true): this {
    this._picker.disabled = disabled;
    return this;
  }

  confirm(title: string, text: string): this {
    this._picker.confirm = {
      title: { tag: 'plain_text', content: title },
      text: { tag: 'plain_text', content: text }
    };
    return this;
  }

  elementId(id: string): this {
    this._picker.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._picker.margin = margin;
    return this;
  }

  build(): PickerDatetime {
    return this._picker;
  }
}
