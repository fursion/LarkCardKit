import type { Card } from '../models';
import type { Element } from '../models/elements';

/**
 * Element finder for locating and updating elements within a card.
 *
 * 提供通过 ID 或 tag 查找和修改组件的能力。
 *
 * @example
 * ```typescript
 * const finder = new ElementFinder(card);
 *
 * // 查找组件
 * const button = finder.findElementById('btn1');
 *
 * // 修改组件
 * finder.modifyElementById('btn1', (el) => {
 *   el.disabled = true;
 * });
 *
 * // 批量修改
 * finder.updateAllByTag('button', (el) => {
 *   el.disabled = true;
 * });
 * ```
 */
export class ElementFinder {
  constructor(private _card: Card) {}

  /**
   * 通过 ID 查找组件
   * @param elementId 组件 ID
   * @returns 找到的组件，如果未找到则返回 undefined
   */
  findElementById(elementId: string): Element | undefined;

  /**
   * 通过 ID 查找组件并验证类型
   * @param elementId 组件 ID
   * @param expectedTag 期望的组件 tag
   * @returns 找到的组件，如果未找到或类型不匹配则返回 undefined
   */
  findElementById<T extends Element>(elementId: string, expectedTag: string): T | undefined;

  findElementById(elementId: string, expectedTag?: string): Element | undefined {
    return this.findElementInElements(this._card.body.elements, elementId, expectedTag);
  }

  /**
   * 通过 ID 修改组件
   * @param elementId 组件 ID
   * @param modifier 修改函数
   * @returns 是否成功修改（组件存在时返回 true）
   */
  modifyElementById(elementId: string, modifier: (element: Element) => void): boolean {
    const element = this.findElementById(elementId);
    if (!element) return false;
    modifier(element);
    return true;
  }

  /**
   * 通过 ID 修改组件（带类型验证）
   * @param elementId 组件 ID
   * @param expectedTag 期望的组件 tag
   * @param modifier 修改函数
   * @returns 是否成功修改（组件存在且类型匹配时返回 true）
   */
  modifyElementByIdAs<T extends Element>(
    elementId: string,
    expectedTag: string,
    modifier: (element: T) => void
  ): boolean {
    const element = this.findElementById<T>(elementId, expectedTag);
    if (!element) return false;
    modifier(element);
    return true;
  }

  /**
   * 通过 tag 查找所有组件
   * @param tag 组件 tag
   * @returns 找到的组件列表
   */
  findAllElementsByTag(tag: string): Element[] {
    const results: Element[] = [];
    this.findElementsByTagInElements(this._card.body.elements, tag, results);
    return results;
  }

  /**
   * 批量修改所有指定 tag 的组件
   * @param tag 组件 tag
   * @param modifier 修改函数
   * @returns 修改的组件数量
   */
  updateAllByTag(tag: string, modifier: (element: Element) => void): number {
    const elements = this.findAllElementsByTag(tag);
    for (const element of elements) {
      modifier(element);
    }
    return elements.length;
  }

  private findElementInElements(elements: Element[], elementId: string, expectedTag?: string): Element | undefined {
    for (const element of elements) {
      if (element.element_id === elementId) {
        if (!expectedTag || element.tag === expectedTag) {
          return element;
        }
      }

      const elem = element as Record<string, unknown>;
      if ('elements' in elem && Array.isArray(elem.elements)) {
        const found = this.findElementInElements(elem.elements as Element[], elementId, expectedTag);
        if (found) return found;
      }

      if ('columns' in elem && Array.isArray(elem.columns)) {
        for (const column of elem.columns as Array<Record<string, unknown>>) {
          if (column.elements && Array.isArray(column.elements)) {
            const found = this.findElementInElements(column.elements as Element[], elementId, expectedTag);
            if (found) return found;
          }
        }
      }
    }
    return undefined;
  }

  private findElementsByTagInElements(elements: Element[], tag: string, results: Element[]): void {
    for (const element of elements) {
      if (element.tag === tag) {
        results.push(element);
      }

      const elem = element as Record<string, unknown>;
      if ('elements' in elem && Array.isArray(elem.elements)) {
        this.findElementsByTagInElements(elem.elements as Element[], tag, results);
      }

      if ('columns' in elem && Array.isArray(elem.columns)) {
        for (const column of elem.columns as Array<Record<string, unknown>>) {
          if (column.elements && Array.isArray(column.elements)) {
            this.findElementsByTagInElements(column.elements as Element[], tag, results);
          }
        }
      }
    }
  }
}