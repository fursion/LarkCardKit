export interface StreamingConfig {
  print_frequency_ms?: number | string;
  print_step?: number | string;
  print_strategy?: string;
}

export interface StreamingSummary {
  content?: string;
  i18n_content?: Record<string, string>;
}

export interface CardStyle {
  text_size?: string | number;
  color?: string;
}
