import type { ElementBase } from './Element';

export interface Chart extends ElementBase {
  tag: 'chart';
  chart_type: string;
  data: any;
  width?: string;
}
