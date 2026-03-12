import type { Card, CardConfig, CardHeader, CardBody, CardLink, Fallback } from '../models';
import type { Element } from '../models/elements';
import { CardConfigBuilder } from './CardConfigBuilder';
import { CardHeaderBuilder } from './CardHeaderBuilder';
import { CardBodyBuilder } from './CardBodyBuilder';
import { CardLinkBuilder } from './CardLinkBuilder';
import { FallbackBuilder } from './FallbackBuilder';
import { TemplateParameterFiller } from '../utils/TemplateParameterFiller';
import { fillTemplate } from '../utils/TemplateHelper';
import { ElementFinder } from '../utils/ElementFinder';
import { toJson } from '../utils/JsonSerializer';

/**
 * 卡片构建器
 *
 * 提供流畅 API 构建飞书卡片，支持通过 ID 查找和修改组件。
 *
 * @example
 * ```typescript
 * // 构建卡片
 * const card = CardBuilder.create()
 *   .header(h => h.title('Welcome'))
 *   .body(b => b
 *     .plainText('Hello')
 *     .button(btn => btn.text('Click').elementId('btn1')))
 *   .build();
 *
 * // 通过 ID 修改组件
 * const builder = CardBuilder.create();
 * builder.body(b => b.button(btn => btn.text('Click').elementId('btn1')));
 * builder.modifyElement('btn1', el => { el.disabled = true; });
 * const card = builder.build();
 * ```
 */
export class CardBuilder {
  private _card: Card = {
    schema: '2.0',
    body: { elements: [] }
  };
  private _filler = new TemplateParameterFiller();
  private _finder?: ElementFinder;
  private _templateApplied = false;

  config(configure: (builder: CardConfigBuilder) => void): this {
    const builder = new CardConfigBuilder();
    configure(builder);
    this._card.config = builder.build();
    return this;
  }

  header(configure: (builder: CardHeaderBuilder) => void): this {
    const builder = new CardHeaderBuilder();
    configure(builder);
    this._card.header = builder.build();
    return this;
  }

  body(configure: (builder: CardBodyBuilder) => void): this {
    const builder = new CardBodyBuilder(this._card.body);
    configure(builder);
    return this;
  }

  cardLink(configure: (builder: CardLinkBuilder) => void): this {
    const builder = new CardLinkBuilder();
    configure(builder);
    this._card.card_link = builder.build();
    return this;
  }

  cardLinkUrl(url: string): this {
    this._card.card_link = { url };
    return this;
  }

  fallback(configure: (builder: FallbackBuilder) => void): this {
    const builder = new FallbackBuilder();
    configure(builder);
    this._card.fallback = builder.build();
    return this;
  }

  fallbackText(title: string, content: string): this {
    this._card.fallback = {
      title: { tag: 'plain_text', content: title },
      content: { tag: 'plain_text', content: content }
    };
    return this;
  }

  setParameter(key: string, value: unknown): this {
    this._filler.setParameter(key, value);
    this._templateApplied = false;
    return this;
  }

  setParameters(params: Record<string, unknown>): this {
    this._filler.setParameters(params);
    this._templateApplied = false;
    return this;
  }

  private applyTemplateParameters(): void {
    if (this._templateApplied) return;
    if (this._filler.hasParameters()) {
      fillTemplate(this._card, this._filler);
    }
    this._templateApplied = true;
  }

  private getFinder(): ElementFinder {
    this._finder ??= new ElementFinder(this._card);
    return this._finder;
  }

  /**
   * 通过 ID 查找组件
   * @param elementId 组件 ID
   * @returns 找到的组件，如果未找到则返回 undefined
   *
   * @example
   * ```typescript
   * const button = builder.findElementById('btn1');
   * if (button) {
   *   button.disabled = true;
   * }
   * ```
   */
  findElementById(elementId: string): Element | undefined {
    return this.getFinder().findElementById(elementId);
  }

  /**
   * 通过 ID 查找组件并验证类型
   * @param elementId 组件 ID
   * @param expectedTag 期望的组件 tag
   * @returns 找到的组件，如果未找到或类型不匹配则返回 undefined
   *
   * @example
   * ```typescript
   * const button = builder.findElementByIdAs<Button>('btn1', 'button');
   * ```
   */
  findElementByIdAs<T extends Element>(elementId: string, expectedTag: string): T | undefined {
    return this.getFinder().findElementById<T>(elementId, expectedTag);
  }

  /**
   * 通过 ID 修改组件
   * @param elementId 组件 ID
   * @param modifier 修改函数
   * @returns 是否成功修改
   *
   * @example
   * ```typescript
   * builder.modifyElement('btn1', (el) => {
   *   el.disabled = true;
   * });
   * ```
   */
  modifyElement(elementId: string, modifier: (element: Element) => void): boolean {
    return this.getFinder().modifyElementById(elementId, modifier);
  }

  /**
   * 通过 ID 修改组件（带类型验证）
   * @param elementId 组件 ID
   * @param expectedTag 期望的组件 tag
   * @param modifier 修改函数
   * @returns 是否成功修改
   *
   * @example
   * ```typescript
   * builder.modifyElementAs<Button>('btn1', 'button', (btn) => {
   *   btn.disabled = true;
   * });
   * ```
   */
  modifyElementAs<T extends Element>(
    elementId: string,
    expectedTag: string,
    modifier: (element: T) => void
  ): boolean {
    return this.getFinder().modifyElementByIdAs(elementId, expectedTag, modifier);
  }

  /**
   * 替换指定 ID 的组件
   * @param elementId 要替换的组件 ID
   * @param newElement 新组件
   * @returns 是否成功替换
   *
   * @example
   * ```typescript
   * builder.replaceElement('text1', { tag: 'div', text: { tag: 'plain_text', content: 'New text' } });
   * ```
   */
  replaceElement(elementId: string, newElement: Element): boolean {
    return this.replaceElementRecursive(this._card.body.elements, elementId, newElement);
  }

  private replaceElementRecursive(elements: Element[], elementId: string, newElement: Element): boolean {
    for (let i = 0; i < elements.length; i++) {
      if (elements[i].element_id === elementId) {
        elements[i] = newElement;
        return true;
      }
      // Check nested elements (Div, Form, ColumnSet, etc.)
      const elem = elements[i] as Element & { elements?: Element[] };
      if ('elements' in elem && Array.isArray(elem.elements)) {
        if (this.replaceElementRecursive(elem.elements!, elementId, newElement)) {
          return true;
        }
      }
    }
    return false;
  }

  /**
   * 通过 tag 查找所有组件
   * @param tag 组件 tag
   * @returns 找到的组件列表
   */
  findAllElementsByTag(tag: string): Element[] {
    return this.getFinder().findAllElementsByTag(tag);
  }

  /**
   * 批量修改所有指定 tag 的组件
   * @param tag 组件 tag
   * @param modifier 修改函数
   * @returns 修改的组件数量
   */
  updateAllByTag(tag: string, modifier: (element: Element) => void): number {
    return this.getFinder().updateAllByTag(tag, modifier);
  }

  build(): Card {
    this.applyTemplateParameters();
    return this._card;
  }

  toJson(): string {
    this.applyTemplateParameters();
    return toJson(this._card);
  }

  toJsonIndented(): string {
    this.applyTemplateParameters();
    return toJson(this._card, true);
  }

  static create(): CardBuilder {
    return new CardBuilder();
  }
}