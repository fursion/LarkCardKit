import type { ElementBase } from './Element';
import type { PlainText } from './PlainText';

export interface Loop extends ElementBase {
  tag: 'loop';
  data_source: LoopDataSource;
  template: ElementBase;
}

export interface LoopDataSource {
  list?: any[];
  [key: string]: any;
}
