import type { ColumnSet, Column, Element } from '../models/elements';

export class ColumnSetBuilder {
  private _columnSet: ColumnSet = { tag: 'column_set', columns: [] };

  column(configure: (builder: ColumnBuilder) => void): this {
    const builder = new ColumnBuilder();
    configure(builder);
    this._columnSet.columns.push(builder.build());
    return this;
  }

  horizontalSpacing(spacing: string): this {
    this._columnSet.horizontal_spacing = spacing;
    return this;
  }

  flexMode(mode: string): this {
    this._columnSet.flex_mode = mode;
    return this;
  }

  backgroundStyle(style: string): this {
    this._columnSet.background_style = style;
    return this;
  }

  elementId(id: string): this {
    this._columnSet.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._columnSet.margin = margin;
    return this;
  }

  build(): ColumnSet {
    return this._columnSet;
  }
}

export class ColumnBuilder {
  private _column: Column = { tag: 'column', elements: [] };

  addElement(element: Element): this {
    this._column.elements.push(element);
    return this;
  }

  width(width: string): this {
    this._column.width = width;
    return this;
  }

  weight(weight: number): this {
    this._column.weight = weight;
    return this;
  }

  verticalAlign(align: string): this {
    this._column.vertical_align = align;
    return this;
  }

  horizontalAlign(align: string): this {
    this._column.horizontal_align = align;
    return this;
  }

  backgroundStyle(style: string): this {
    this._column.background_style = style;
    return this;
  }

  padding(padding: string): this {
    this._column.padding = padding;
    return this;
  }

  elementId(id: string): this {
    this._column.element_id = id;
    return this;
  }

  build(): Column {
    return this._column;
  }
}
