import type { Overflow, OverflowOption } from '../models/elements';

export class OverflowBuilder {
  private _overflow: Overflow = { tag: 'overflow', options: [] };

  option(text: string, value: string): this {
    this._overflow.options ??= [];
    this._overflow.options.push({
      text: { tag: 'plain_text', content: text },
      value
    });
    return this;
  }

  options(options: Array<{ text: string; value: string }>): this {
    this._overflow.options = options.map(opt => ({
      text: { tag: 'plain_text' as const, content: opt.text },
      value: opt.value
    }));
    return this;
  }

  elementId(id: string): this {
    this._overflow.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._overflow.margin = margin;
    return this;
  }

  build(): Overflow {
    return this._overflow;
  }
}
