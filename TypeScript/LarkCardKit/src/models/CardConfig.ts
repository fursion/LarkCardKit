import type { StreamingConfig, StreamingSummary, CardStyle } from './CardStreaming';

export interface CardConfig {
  update_multi?: boolean;
  streaming_mode?: boolean;
  streaming_config?: StreamingConfig;
  summary?: StreamingSummary;
  locales?: string[];
  enable_forward?: boolean;
  width_mode?: string;
  use_custom_translation?: boolean;
  enable_forward_interaction?: boolean;
  style?: CardStyle;
}

export type { StreamingConfig, StreamingSummary, CardStyle } from './CardStreaming';
