import type { ElementBase, Element } from './Element';

export interface Form extends ElementBase {
  tag: 'form';
  name: string;
  elements: Element[];
  action?: FormAction;
}

export interface FormAction {
  url?: string;
  method?: string;
}
