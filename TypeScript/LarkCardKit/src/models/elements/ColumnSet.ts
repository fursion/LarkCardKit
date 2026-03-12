import type { ElementBase, Element } from './Element';

export interface ColumnSet extends ElementBase {
  tag: 'column_set';
  columns: Column[];
  horizontal_spacing?: string;
  flex_mode?: string;
  background_style?: string;
}

export interface Column extends ElementBase {
  tag: 'column';
  elements: Element[];
  width?: string;
  weight?: number;
  vertical_align?: string;
  horizontal_align?: string;
  background_style?: string;
  padding?: string;
}
