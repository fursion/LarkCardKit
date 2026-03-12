import type { Loop, LoopDataSource } from '../models/elements/Loop';
import type { ElementBase } from '../models/elements/Element';

export class LoopBuilder {
  private loop: Loop = {} as Loop;

  constructor() {
    this.loop.tag = 'loop';
  }

  /**
   * 设置数据源
   */
  dataSource(data: LoopDataSource): LoopBuilder {
    this.loop.data_source = data;
    return this;
  }

  /**
   * 设置列表数据
   */
  list(list: any[]): LoopBuilder {
    if (!this.loop.data_source) {
      this.loop.data_source = {};
    }
    this.loop.data_source.list = list;
    return this;
  }

  /**
   * 设置循环模板
   */
  template(template: ElementBase): LoopBuilder {
    this.loop.template = template;
    return this;
  }

  /**
   * 设置元素 ID
   */
  elementId(id: string): LoopBuilder {
    this.loop.element_id = id;
    return this;
  }

  /**
   * 构建 Loop 对象
   */
  build(): Loop {
    return this.loop;
  }
}
