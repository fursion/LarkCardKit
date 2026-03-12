import type { PlainText } from './elements/PlainText';
import type { MarkdownText } from './elements/MarkdownText';
import type { TextTag } from './elements/TextTag';

export interface HeaderIcon {
  tag: 'standard_icon' | 'custom_icon';
  token?: string;
  color?: string;
  img_key?: string;
}

export interface CardHeader {
  title?: PlainText | MarkdownText;
  subtitle?: PlainText | MarkdownText;
  template?: string;
  icon?: HeaderIcon;
  text_tag_list?: TextTag[];
  padding?: string;
}
