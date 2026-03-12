import type { PickerTime } from '../models/elements';

export class PickerTimeBuilder {
  private _picker: PickerTime = { tag: 'picker_time', name: '' };

  name(name: string): this {
    this._picker.name = name;
    return this;
  }

  placeholder(placeholder: string): this {
    this._picker.placeholder = { tag: 'plain_text', content: placeholder };
    return this;
  }

  initialTime(time: string): this {
    this._picker.initial_time = time;
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

  build(): PickerTime {
    return this._picker;
  }
}
