import type { SelectPerson, MultiSelectPerson, PersonSelectedOption } from '../models/elements';

export class SelectPersonBuilder {
  private _select: SelectPerson = { tag: 'select_person', name: '' };

  name(name: string): this {
    this._select.name = name;
    return this;
  }

  placeholder(placeholder: string): this {
    this._select.placeholder = { tag: 'plain_text', content: placeholder };
    return this;
  }

  selectedOption(userId: string, userName?: string, avatarUrl?: string): this {
    this._select.selected_options ??= [];
    const opt: PersonSelectedOption = { user_id: userId };
    if (userName) opt.user_name = userName;
    if (avatarUrl) opt.avatar_url = avatarUrl;
    this._select.selected_options.push(opt);
    return this;
  }

  multiple(multiple: boolean = true): this {
    this._select.multiple = multiple;
    return this;
  }

  disabled(disabled: boolean = true): this {
    this._select.disabled = disabled;
    return this;
  }

  allowedUserIds(userIds: string[]): this {
    this._select.allowed_user_ids = userIds;
    return this;
  }

  elementId(id: string): this {
    this._select.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._select.margin = margin;
    return this;
  }

  build(): SelectPerson {
    return this._select;
  }
}

export class MultiSelectPersonBuilder {
  private _select: MultiSelectPerson = { tag: 'multi_select_person', name: '' };

  name(name: string): this {
    this._select.name = name;
    return this;
  }

  placeholder(placeholder: string): this {
    this._select.placeholder = { tag: 'plain_text', content: placeholder };
    return this;
  }

  selectedOption(userId: string, userName?: string, avatarUrl?: string): this {
    this._select.selected_options ??= [];
    const opt: PersonSelectedOption = { user_id: userId };
    if (userName) opt.user_name = userName;
    if (avatarUrl) opt.avatar_url = avatarUrl;
    this._select.selected_options.push(opt);
    return this;
  }

  disabled(disabled: boolean = true): this {
    this._select.disabled = disabled;
    return this;
  }

  allowedUserIds(userIds: string[]): this {
    this._select.allowed_user_ids = userIds;
    return this;
  }

  elementId(id: string): this {
    this._select.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._select.margin = margin;
    return this;
  }

  build(): MultiSelectPerson {
    return this._select;
  }
}
