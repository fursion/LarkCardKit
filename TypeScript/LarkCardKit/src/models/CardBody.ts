import type { Element } from './elements/Element';

export interface CardBody {
  elements: Element[];
  vertical_spacing?: string;
  horizontal_spacing?: string;
  padding?: string;
  direction?: string;
  horizontal_align?: string;
  vertical_align?: string;
}
