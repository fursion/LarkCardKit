import type { Person, PersonList } from '../models/elements';

export class PersonBuilder {
  private _person: Person = { tag: 'person' };

  userId(userId: string): this {
    this._person.user_id = userId;
    return this;
  }

  openId(openId: string): this {
    this._person.open_id = openId;
    return this;
  }

  unionId(unionId: string): this {
    this._person.union_id = unionId;
    return this;
  }

  elementId(id: string): this {
    this._person.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._person.margin = margin;
    return this;
  }

  build(): Person {
    return this._person;
  }
}

export class PersonListBuilder {
  private _personList: PersonList = { tag: 'person_list' };

  userIds(userIds: string[]): this {
    this._personList.user_ids = userIds;
    return this;
  }

  openIds(openIds: string[]): this {
    this._personList.open_ids = openIds;
    return this;
  }

  unionIds(unionIds: string[]): this {
    this._personList.union_ids = unionIds;
    return this;
  }

  style(style: string): this {
    this._personList.style = style;
    return this;
  }

  elementId(id: string): this {
    this._personList.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._personList.margin = margin;
    return this;
  }

  build(): PersonList {
    return this._personList;
  }
}
