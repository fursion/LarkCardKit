import type { CardConfig } from './CardConfig';
import type { CardHeader } from './CardHeader';
import type { CardBody } from './CardBody';
import type { CardLink } from './CardLink';
import type { Fallback } from './Fallback';

export interface Card {
  schema: string;
  config?: CardConfig;
  card_link?: CardLink;
  header?: CardHeader;
  body: CardBody;
  fallback?: Fallback;
}
