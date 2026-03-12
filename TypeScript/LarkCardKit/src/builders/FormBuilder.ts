import type { Form, Element, FormAction } from '../models/elements';
import { InputBuilder } from './InputBuilder';
import { ButtonBuilder } from './ButtonBuilder';
import { SelectBuilder } from './SelectBuilder';
import { DatePickerBuilder } from './DatePickerBuilder';
import { CheckboxBuilder } from './CheckboxBuilder';
import { PickerTimeBuilder } from './PickerTimeBuilder';
import { PickerDatetimeBuilder } from './PickerDatetimeBuilder';
import { SelectPersonBuilder, MultiSelectPersonBuilder } from './SelectPersonBuilder';
import { CheckerBuilder } from './CheckerBuilder';
import { TextDivBuilder } from './TextDivBuilder';

export class FormBuilder {
  private _form: Form = { tag: 'form', name: '', elements: [] };

  name(name: string): this {
    this._form.name = name;
    return this;
  }

  addElement(element: Element): this {
    this._form.elements.push(element);
    return this;
  }

  input(configure: (builder: InputBuilder) => void): this {
    const builder = new InputBuilder();
    configure(builder);
    this._form.elements.push(builder.build());
    return this;
  }

  button(configure: (builder: ButtonBuilder) => void): this {
    const builder = new ButtonBuilder();
    configure(builder);
    this._form.elements.push(builder.build());
    return this;
  }

  select(configure: (builder: SelectBuilder) => void): this {
    const builder = new SelectBuilder();
    configure(builder);
    this._form.elements.push(builder.build());
    return this;
  }

  datePicker(configure: (builder: DatePickerBuilder) => void): this {
    const builder = new DatePickerBuilder();
    configure(builder);
    this._form.elements.push(builder.build());
    return this;
  }

  checkbox(configure: (builder: CheckboxBuilder) => void): this {
    const builder = new CheckboxBuilder();
    configure(builder);
    this._form.elements.push(builder.build());
    return this;
  }

  pickerTime(configure: (builder: PickerTimeBuilder) => void): this {
    const builder = new PickerTimeBuilder();
    configure(builder);
    this._form.elements.push(builder.build());
    return this;
  }

  pickerDatetime(configure: (builder: PickerDatetimeBuilder) => void): this {
    const builder = new PickerDatetimeBuilder();
    configure(builder);
    this._form.elements.push(builder.build());
    return this;
  }

  selectPerson(configure: (builder: SelectPersonBuilder) => void): this {
    const builder = new SelectPersonBuilder();
    configure(builder);
    this._form.elements.push(builder.build());
    return this;
  }

  multiSelectPerson(configure: (builder: MultiSelectPersonBuilder) => void): this {
    const builder = new MultiSelectPersonBuilder();
    configure(builder);
    this._form.elements.push(builder.build());
    return this;
  }

  checker(configure: (builder: CheckerBuilder) => void): this {
    const builder = new CheckerBuilder();
    configure(builder);
    this._form.elements.push(builder.build());
    return this;
  }

  div(configure: (builder: TextDivBuilder) => void): this {
    const builder = new TextDivBuilder();
    configure(builder);
    this._form.elements.push(builder.build());
    return this;
  }

  action(url: string, method?: string): this {
    const action: FormAction = { url };
    if (method) action.method = method;
    this._form.action = action;
    return this;
  }

  elementId(id: string): this {
    this._form.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._form.margin = margin;
    return this;
  }

  build(): Form {
    return this._form;
  }
}
