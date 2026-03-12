import type { SelectImg, SelectImgOption, ConfirmConfig } from '../models/elements/SelectImg';
import type { PlainText } from '../models/elements/PlainText';

export class SelectImgBuilder {
  private selectImg: SelectImg = {} as SelectImg;

  constructor() {
    this.selectImg.tag = 'select_img';
  }

  /**
   * 设置名称
   */
  name(name: string): SelectImgBuilder {
    this.selectImg.name = name;
    return this;
  }

  /**
   * 设置是否必填
   */
  required(required: boolean): SelectImgBuilder {
    this.selectImg.required = required;
    return this;
  }

  /**
   * 设置占位文本
   */
  placeholder(content: string): SelectImgBuilder {
    this.selectImg.placeholder = { tag: 'plain_text', content } as PlainText;
    return this;
  }

  /**
   * 设置标签
   */
  label(content: string): SelectImgBuilder {
    this.selectImg.label = { tag: 'plain_text', content } as PlainText;
    return this;
  }

  /**
   * 设置选择模式
   */
  selectMode(mode: 'single' | 'multi'): SelectImgBuilder {
    this.selectImg.select_mode = mode;
    return this;
  }

  /**
   * 设置单选模式
   */
  single(): SelectImgBuilder {
    this.selectImg.select_mode = 'single';
    return this;
  }

  /**
   * 设置多选模式
   */
  multi(): SelectImgBuilder {
    this.selectImg.select_mode = 'multi';
    return this;
  }

  /**
   * 设置初始选中选项
   */
  initialOptions(options: string[]): SelectImgBuilder {
    this.selectImg.initial_options = options;
    return this;
  }

  /**
   * 添加选项
   */
  option(imgKey: string, text?: string): SelectImgBuilder {
    if (!this.selectImg.options) {
      this.selectImg.options = [];
    }
    const option: SelectImgOption = { value: imgKey };
    if (text) {
      option.text = { tag: 'plain_text', content: text } as PlainText;
    }
    this.selectImg.options.push(option);
    return this;
  }

  /**
   * 设置宽度
   */
  width(width: string): SelectImgBuilder {
    this.selectImg.width = width;
    return this;
  }

  /**
   * 设置禁用
   */
  disabled(disabled: boolean): SelectImgBuilder {
    this.selectImg.disabled = disabled;
    return this;
  }

  /**
   * 设置二次确认
   */
  confirm(title: string, text: string): SelectImgBuilder {
    this.selectImg.confirm = {
      title: { tag: 'plain_text', content: title } as PlainText,
      text: { tag: 'plain_text', content: text } as PlainText
    } as ConfirmConfig;
    return this;
  }

  /**
   * 设置元素 ID
   */
  elementId(id: string): SelectImgBuilder {
    this.selectImg.element_id = id;
    return this;
  }

  /**
   * 构建 SelectImg 对象
   */
  build(): SelectImg {
    return this.selectImg;
  }
}
