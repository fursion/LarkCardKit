import type { Hr } from '../models/elements';

export class HrBuilder {
  private _hr: Hr = { tag: 'hr' };

  elementId(id: string): this {
    this._hr.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._hr.margin = margin;
    return this;
  }

  build(): Hr {
    return this._hr;
  }
}
