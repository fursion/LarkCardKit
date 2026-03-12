import type { CardBody } from '../models';
import type { Element } from '../models/elements';
import { TextDivBuilder } from './TextDivBuilder';
import { ButtonBuilder } from './ButtonBuilder';
import { InputBuilder } from './InputBuilder';
import { SelectBuilder } from './SelectBuilder';
import { DatePickerBuilder } from './DatePickerBuilder';
import { CheckboxBuilder } from './CheckboxBuilder';
import { FormBuilder } from './FormBuilder';
import { ColumnSetBuilder } from './ColumnSetBuilder';
import { ImageBuilder } from './ImageBuilder';
import { HrBuilder } from './HrBuilder';
import { PickerTimeBuilder } from './PickerTimeBuilder';
import { PickerDatetimeBuilder } from './PickerDatetimeBuilder';
import { PersonBuilder, PersonListBuilder } from './PersonBuilder';
import { SelectPersonBuilder, MultiSelectPersonBuilder } from './SelectPersonBuilder';
import { OverflowBuilder } from './OverflowBuilder';
import { CheckerBuilder } from './CheckerBuilder';
import { ImgCombinationBuilder } from './ImgCombinationBuilder';
import { InteractiveContainerBuilder } from './InteractiveContainerBuilder';
import { TableBuilder } from './TableBuilder';
import { PlainTextBuilder } from './PlainTextBuilder';
import { MarkdownBuilder } from './MarkdownBuilder';

export class CardBodyBuilder {
  constructor(private _body: CardBody) {}

  verticalSpacing(spacing: string): this {
    this._body.vertical_spacing = spacing;
    return this;
  }

  horizontalSpacing(spacing: string): this {
    this._body.horizontal_spacing = spacing;
    return this;
  }

  padding(padding: string): this {
    this._body.padding = padding;
    return this;
  }

  addElement(element: Element): this {
    this._body.elements.push(element);
    return this;
  }

  div(configure: (builder: TextDivBuilder) => void): this {
    const builder = new TextDivBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  button(configure: (builder: ButtonBuilder) => void): this {
    const builder = new ButtonBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  input(configure: (builder: InputBuilder) => void): this {
    const builder = new InputBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  select(configure: (builder: SelectBuilder) => void): this {
    const builder = new SelectBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  datePicker(configure: (builder: DatePickerBuilder) => void): this {
    const builder = new DatePickerBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  checkbox(configure: (builder: CheckboxBuilder) => void): this {
    const builder = new CheckboxBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  form(configure: (builder: FormBuilder) => void): this {
    const builder = new FormBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  columnSet(configure: (builder: ColumnSetBuilder) => void): this {
    const builder = new ColumnSetBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  image(imgKey: string, configure?: (builder: ImageBuilder) => void): this {
    const builder = new ImageBuilder(imgKey);
    configure?.(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  hr(configure?: (builder: HrBuilder) => void): this {
    const builder = new HrBuilder();
    configure?.(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  pickerTime(configure: (builder: PickerTimeBuilder) => void): this {
    const builder = new PickerTimeBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  pickerDatetime(configure: (builder: PickerDatetimeBuilder) => void): this {
    const builder = new PickerDatetimeBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  person(configure: (builder: PersonBuilder) => void): this {
    const builder = new PersonBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  personList(configure: (builder: PersonListBuilder) => void): this {
    const builder = new PersonListBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  selectPerson(configure: (builder: SelectPersonBuilder) => void): this {
    const builder = new SelectPersonBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  multiSelectPerson(configure: (builder: MultiSelectPersonBuilder) => void): this {
    const builder = new MultiSelectPersonBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  overflow(configure: (builder: OverflowBuilder) => void): this {
    const builder = new OverflowBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  checker(configure: (builder: CheckerBuilder) => void): this {
    const builder = new CheckerBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  imgCombination(configure: (builder: ImgCombinationBuilder) => void): this {
    const builder = new ImgCombinationBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  interactiveContainer(configure: (builder: InteractiveContainerBuilder) => void): this {
    const builder = new InteractiveContainerBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  table(configure: (builder: TableBuilder) => void): this {
    const builder = new TableBuilder();
    configure(builder);
    this._body.elements.push(builder.build());
    return this;
  }

  plainText(content: string, configure?: (builder: PlainTextBuilder) => void): this {
    const builder = new PlainTextBuilder().content(content);
    configure?.(builder);
    this._body.elements.push({
      tag: 'div',
      text: builder.build()
    });
    return this;
  }

  markdown(content: string, configure?: (builder: MarkdownBuilder) => void): this {
    const builder = new MarkdownBuilder().content(content);
    configure?.(builder);
    this._body.elements.push(builder.build());
    return this;
  }
}
