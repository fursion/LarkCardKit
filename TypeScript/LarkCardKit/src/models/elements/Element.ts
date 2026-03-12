export interface ElementBase {
  tag: string;
  element_id?: string;
  margin?: string;
}

/**
 * TextObject 类型：支持 plain_text 和 lark_md 两种文本模式
 */
export type TextObject = import('./PlainText').PlainText | import('./MarkdownText').MarkdownText;

export type Element =
  | import('./TextDiv').TextDiv
  | import('./Button').Button
  | import('./Input').Input
  | import('./Select').Select
  | import('./DatePicker').DatePicker
  | import('./Checkbox').Checkbox
  | import('./Image').Image
  | import('./Form').Form
  | import('./ColumnSet').ColumnSet
  | import('./Hr').Hr
  | import('./Person').Person
  | import('./Person').PersonList
  | import('./PickerTime').PickerTime
  | import('./PickerDatetime').PickerDatetime
  | import('./SelectPerson').SelectPerson
  | import('./SelectPerson').MultiSelectPerson
  | import('./Overflow').Overflow
  | import('./Checker').Checker
  | import('./ImgCombination').ImgCombination
  | import('./InteractiveContainer').InteractiveContainer
  | import('./Table').Table
  | import('./Markdown').Markdown
  | import('./PlainText').PlainText
  | import('./MarkdownText').MarkdownText
  | import('./TextTag').TextTag
  | { tag: string; [key: string]: unknown };
