import type { Table, TableColumn, TableHeaderStyle, TableNumberFormat } from '../models/elements';

export class TableBuilder {
  private _table: Table = { tag: 'table', columns: [], rows: [] };

  pageSize(size: number): this {
    this._table.page_size = size;
    return this;
  }

  rowHeight(height: string): this {
    (this._table as any).row_height = height;
    return this;
  }

  column(configure: (builder: TableColumnBuilder) => void): this {
    const builder = new TableColumnBuilder();
    configure(builder);
    this._table.columns ??= [];
    this._table.columns.push(builder.build());
    return this;
  }

  row(values: Record<string, any>): this {
    this._table.rows ??= [];
    this._table.rows.push(values);
    return this;
  }

  headerStyle(configure: (builder: TableHeaderStyleBuilder) => void): this {
    const builder = new TableHeaderStyleBuilder();
    configure(builder);
    this._table.header_style = builder.build();
    return this;
  }

  elementId(id: string): this {
    this._table.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._table.margin = margin;
    return this;
  }

  build(): Table {
    return this._table;
  }
}

export class TableColumnBuilder {
  private _column: TableColumn = { name: '', data_type: 'text' };

  name(name: string): this {
    this._column.name = name;
    return this;
  }

  displayName(name: string): this {
    this._column.display_name = name;
    return this;
  }

  dataType(type: string): this {
    this._column.data_type = type;
    return this;
  }

  width(width: string): this {
    this._column.width = width;
    return this;
  }

  horizontalAlign(align: string): this {
    this._column.horizontal_align = align;
    return this;
  }

  verticalAlign(align: string): this {
    this._column.vertical_align = align;
    return this;
  }

  format(configure: (builder: TableNumberFormatBuilder) => void): this {
    const builder = new TableNumberFormatBuilder();
    configure(builder);
    this._column.format = builder.build();
    return this;
  }

  build(): TableColumn {
    return this._column;
  }
}

export class TableHeaderStyleBuilder {
  private _style: TableHeaderStyle = {};

  backgroundStyle(style: string): this {
    this._style.background_style = style;
    return this;
  }

  textAlign(align: string): this {
    this._style.text_align = align;
    return this;
  }

  textSize(size: string): this {
    this._style.text_size = size;
    return this;
  }

  textColor(color: string): this {
    this._style.text_color = color;
    return this;
  }

  bold(bold: boolean = true): this {
    this._style.bold = bold;
    return this;
  }

  build(): TableHeaderStyle {
    return this._style;
  }
}

export class TableNumberFormatBuilder {
  private _format: TableNumberFormat = {};

  decimalPlaces(places: number): this {
    this._format.decimal_places = places;
    return this;
  }

  symbol(symbol: string): this {
    this._format.symbol = symbol;
    return this;
  }

  formattingType(type: string): this {
    this._format.formatting_type = type;
    return this;
  }

  build(): TableNumberFormat {
    return this._format;
  }
}
