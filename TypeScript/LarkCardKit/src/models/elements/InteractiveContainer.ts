import type { ElementBase, Element } from './Element';

export interface InteractiveContainer extends ElementBase {
  tag: 'interactive_container';
  elements: Element[];
  actions?: InteractiveAction[];
}

export interface InteractiveAction {
  tag: string;
  element_id?: string;
  value?: Record<string, unknown>;
}
