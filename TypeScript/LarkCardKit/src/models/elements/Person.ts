import type { ElementBase } from './Element';

export interface Person extends ElementBase {
  tag: 'person';
  user_id?: string;
  open_id?: string;
  union_id?: string;
  style?: string;
  show_name?: boolean;
  show_avatar?: boolean;
}

export interface PersonList extends ElementBase {
  tag: 'person_list';
  user_ids?: string[];
  open_ids?: string[];
  union_ids?: string[];
  style?: string;
}
