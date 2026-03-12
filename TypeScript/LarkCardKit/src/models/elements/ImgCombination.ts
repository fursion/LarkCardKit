import type { ElementBase } from './Element';

export interface ImgCombination extends ElementBase {
  tag: 'img_combination';
  img_key: string;
  mode?: string;
  combination_id?: string;
}
