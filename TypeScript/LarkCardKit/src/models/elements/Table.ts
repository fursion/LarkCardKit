import type { ElementBase } from './Element';

export interface Table extends ElementBase {
  tag: 'table';
  page_size?: number;
  row_height?: string;
  row_max_height?: string;
  freeze_first_column?: boolean;
  header_style?: TableHeaderStyle;
  columns: TableColumn[];
  rows: Record<string, any>[];
}

export interface TableColumn {
  name: string;
  display_name?: string;
  width?: string;
  data_type: string;
  vertical_align?: string;
  horizontal_align?: string;
  format?: TableNumberFormat;
  date_format?: string;
}

export interface TableHeaderStyle {
  text_align?: string;
  text_size?: string;
  background_style?: string;
  text_color?: string;
  bold?: boolean;
  lines?: number;
}

export interface TableNumberFormat {
  decimal_places?: number;
  symbol?: string;
  formatting_type?: string;
}
