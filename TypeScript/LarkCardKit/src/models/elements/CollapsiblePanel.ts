import type { ElementBase, TextObject } from './Element';

export interface CollapsiblePanelHeader {
  title?: TextObject;
  icon?: PanelIcon;
  expanded_icon?: PanelIcon;
}

export interface PanelIcon {
  tag: 'standard_icon' | 'custom_icon';
  token?: string;
  color?: string;
  img_key?: string;
}

export interface CollapsiblePanel extends ElementBase {
  tag: 'collapsible_panel';
  expanded?: boolean;
  header: CollapsiblePanelHeader;
  elements: ElementBase[];
}
