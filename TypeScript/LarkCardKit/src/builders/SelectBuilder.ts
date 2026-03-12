import type { Select, SelectOption } from '../models/elements';

export class SelectBuilder {
  private _select: Select = { tag: 'select_static', name: '' };

  name(name: string): this {
    this._select.name = name;
    return this;
  }

  placeholder(placeholder: string): this {
    this._select.placeholder = { tag: 'plain_text', content: placeholder };
    return this;
  }

  option(text: string, value: string, description?: string): this {
    this._select.options ??= [];
    const opt: SelectOption = { text: { tag: 'plain_text', content: text }, value };
    if (description) opt.description = { tag: 'plain_text', content: description };
    this._select.options.push(opt);
    return this;
  }

  options(options: Array<{ text: string; value: string; description?: string }>): this {
    this._select.options = options.map(opt => ({
      text: { tag: 'plain_text' as const, content: opt.text },
      value: opt.value,
      ...(opt.description && { description: { tag: 'plain_text' as const, content: opt.description } })
    }));
    return this;
  }

  initialOption(value: string): this {
    this._select.initial_option = value;
    return this;
  }

  multiple(multiple: boolean = true): this {
    this._select.multiple = multiple;
    return this;
  }

  disabled(disabled: boolean = true): this {
    this._select.disabled = disabled;
    return this;
  }

  elementId(id: string): this {
    this._select.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._select.margin = margin;
    return this;
  }

  build(): Select {
    return this._select;
  }
}
