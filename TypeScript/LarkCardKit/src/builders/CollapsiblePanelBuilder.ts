import type { CollapsiblePanel, CollapsiblePanelHeader, PanelIcon } from '../models/elements/CollapsiblePanel';
import type { ElementBase } from '../models/elements/Element';
import type { PlainText } from '../models/elements/PlainText';
import type { MarkdownText } from '../models/elements/MarkdownText';

export class CollapsiblePanelBuilder {
  private panel: CollapsiblePanel = {} as CollapsiblePanel;

  constructor() {
    this.panel.tag = 'collapsible_panel';
    this.panel.elements = [];
  }

  /**
   * 设置是否展开
   */
  expanded(expanded: boolean): CollapsiblePanelBuilder {
    this.panel.expanded = expanded;
    return this;
  }

  /**
   * 设置头部标题（纯文本）
   */
  title(content: string): CollapsiblePanelBuilder {
    if (!this.panel.header) {
      this.panel.header = {} as CollapsiblePanelHeader;
    }
    this.panel.header.title = { tag: 'plain_text', content } as PlainText;
    return this;
  }

  /**
   * 设置头部标题（Markdown 格式）
   */
  titleWithMarkdown(content: string): CollapsiblePanelBuilder {
    if (!this.panel.header) {
      this.panel.header = {} as CollapsiblePanelHeader;
    }
    this.panel.header.title = { tag: 'lark_md', content } as MarkdownText;
    return this;
  }

  /**
   * 设置头部图标
   */
  headerIcon(icon: PanelIcon): CollapsiblePanelBuilder {
    if (!this.panel.header) {
      this.panel.header = {} as CollapsiblePanelHeader;
    }
    this.panel.header.icon = icon;
    return this;
  }

  /**
   * 设置展开时的图标
   */
  expandedIcon(icon: PanelIcon): CollapsiblePanelBuilder {
    if (!this.panel.header) {
      this.panel.header = {} as CollapsiblePanelHeader;
    }
    this.panel.header.expanded_icon = icon;
    return this;
  }

  /**
   * 添加元素
   */
  addElement(element: ElementBase): CollapsiblePanelBuilder {
    this.panel.elements.push(element);
    return this;
  }

  /**
   * 设置元素 ID
   */
  elementId(id: string): CollapsiblePanelBuilder {
    this.panel.element_id = id;
    return this;
  }

  /**
   * 设置外边距
   */
  margin(margin: string): CollapsiblePanelBuilder {
    this.panel.margin = margin;
    return this;
  }

  /**
   * 构建 CollapsiblePanel 对象
   */
  build(): CollapsiblePanel {
    return this.panel;
  }
}
