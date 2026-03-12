import type { Card } from '../models/Card';
import type { Element, ElementBase, TextObject } from '../models/elements/Element';
import type { PlainText } from '../models/elements/PlainText';
import type { MarkdownText } from '../models/elements/MarkdownText';
import type { Markdown } from '../models/elements/Markdown';
import type { ColumnSet } from '../models/elements/ColumnSet';
import type { Form } from '../models/elements/Form';
import type { CollapsiblePanel } from '../models/elements/CollapsiblePanel';
import type { Loop } from '../models/elements/Loop';
import type { TextTag } from '../models/elements/TextTag';
import type { TextDiv } from '../models/elements/TextDiv';
import { TemplateParameterFiller } from './TemplateParameterFiller';

/**
 * 模板参数填充工具
 * 支持在卡片文本内容中使用 ${变量名} 格式的模板变量
 */

/**
 * 使用参数字典填充卡片中的模板参数
 */
export function fillTemplate(card: Card, params: Record<string, unknown>): void;

/**
 * 使用 TemplateParameterFiller 填充卡片中的模板参数
 */
export function fillTemplate(card: Card, filler: TemplateParameterFiller): void;

export function fillTemplate(card: Card, fillerOrParams: TemplateParameterFiller | Record<string, unknown>): void {
  const filler = fillerOrParams instanceof TemplateParameterFiller
    ? fillerOrParams
    : createFillerFromParams(fillerOrParams);

  if (card.header) {
    fillTemplateInTextObject(card.header.title, filler);
    fillTemplateInTextObject(card.header.subtitle, filler);
    if (card.header.text_tag_list) {
      for (const textTag of card.header.text_tag_list) {
        fillTemplateInTextObject(textTag.text, filler);
      }
    }
  }

  fillTemplateInElements(card.body.elements, filler);
}

/**
 * 填充字符串中的模板参数
 */
export function fillTemplateString(content: string, filler: TemplateParameterFiller): string;

export function fillTemplateString(content: string, params: Record<string, unknown>): string;

export function fillTemplateString(content: string, fillerOrParams: TemplateParameterFiller | Record<string, unknown>): string {
  const filler = fillerOrParams instanceof TemplateParameterFiller
    ? fillerOrParams
    : createFillerFromParams(fillerOrParams);
  return filler.fillString(content);
}

// ============ 私有辅助函数 ============

function createFillerFromParams(params: Record<string, unknown>): TemplateParameterFiller {
  const filler = new TemplateParameterFiller();
  filler.setParameters(params);
  return filler;
}

function fillTemplateInTextObject(textObj: TextObject | undefined, filler: TemplateParameterFiller): void {
  if (!textObj) return;

  if (textObj.tag === 'plain_text' || textObj.tag === 'lark_md') {
    const text = textObj as PlainText | MarkdownText;
    if (text.content) {
      text.content = filler.fillString(text.content);
    }
  }
}

function fillTemplateInElement(element: Element | ElementBase, filler: TemplateParameterFiller): void {
  if (!element) return;

  const tag = element.tag;

  switch (tag) {
    case 'plain_text':
    case 'lark_md': {
      const text = element as PlainText | MarkdownText;
      if (text.content) {
        text.content = filler.fillString(text.content);
      }
      break;
    }
    case 'markdown': {
      const md = element as Markdown;
      if (md.content) {
        md.content = filler.fillString(md.content);
      }
      break;
    }
    case 'div': {
      const div = element as TextDiv;
      if (div.text) {
        // TextDiv.text 是 PlainText 类型
        const text = div.text as PlainText;
        if (text.content) {
          text.content = filler.fillString(text.content);
        }
      }
      break;
    }
    case 'text_tag': {
      const textTag = element as TextTag;
      fillTemplateInTextObject(textTag.text, filler);
      break;
    }
    case 'column_set': {
      const columnSet = element as ColumnSet;
      if (columnSet.columns) {
        for (const column of columnSet.columns) {
          if (column.elements) {
            fillTemplateInElements(column.elements, filler);
          }
        }
      }
      break;
    }
    case 'form': {
      const form = element as Form;
      if (form.elements) {
        fillTemplateInElements(form.elements, filler);
      }
      break;
    }
    case 'collapsible_panel': {
      const panel = element as CollapsiblePanel;
      if (panel.header?.title) {
        fillTemplateInTextObject(panel.header.title, filler);
      }
      if (panel.elements) {
        fillTemplateInElements(panel.elements, filler);
      }
      break;
    }
    case 'loop': {
      const loop = element as Loop;
      if (loop.template) {
        fillTemplateInElement(loop.template, filler);
      }
      break;
    }
    case 'interactive_container': {
      const container = element as Element & { elements?: Element[] };
      if (container.elements) {
        fillTemplateInElements(container.elements, filler);
      }
      break;
    }
  }
}

function fillTemplateInElements(elements: (Element | ElementBase)[] | undefined, filler: TemplateParameterFiller): void {
  if (!elements) return;

  for (const element of elements) {
    fillTemplateInElement(element, filler);
  }
}