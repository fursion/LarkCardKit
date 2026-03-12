import type { Checker } from '../models/elements';

export class CheckerBuilder {
  private _checker: Checker = { tag: 'checker', name: '' };

  name(name: string): this {
    this._checker.name = name;
    return this;
  }

  text(text: string): this {
    this._checker.text = { tag: 'plain_text', content: text };
    return this;
  }

  checked(checked: boolean = true): this {
    this._checker.checked = checked;
    return this;
  }

  disabled(disabled: boolean = true): this {
    this._checker.disabled = disabled;
    return this;
  }

  elementId(id: string): this {
    this._checker.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._checker.margin = margin;
    return this;
  }

  build(): Checker {
    return this._checker;
  }
}
