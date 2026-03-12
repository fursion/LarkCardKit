import type { InteractiveContainer, Element, InteractiveAction } from '../models/elements';

export class InteractiveContainerBuilder {
  private _container: InteractiveContainer = { tag: 'interactive_container', elements: [] };

  addElement(element: Element): this {
    this._container.elements.push(element);
    return this;
  }

  action(action: InteractiveAction): this {
    this._container.actions ??= [];
    this._container.actions.push(action);
    return this;
  }

  elementId(id: string): this {
    this._container.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._container.margin = margin;
    return this;
  }

  build(): InteractiveContainer {
    return this._container;
  }
}
