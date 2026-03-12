import type { CardConfig, StreamingConfig, StreamingSummary, CardStyle } from '../models';

export class CardConfigBuilder {
  private _config: CardConfig = {};

  updateMulti(value: boolean): this {
    this._config.update_multi = value;
    return this;
  }

  streamingMode(value: boolean): this {
    this._config.streaming_mode = value;
    return this;
  }

  streamingConfig(config: StreamingConfig): this {
    this._config.streaming_config = config;
    return this;
  }

  summary(content: string, i18nContent?: Record<string, string>): this {
    const summary: StreamingSummary = { content };
    if (i18nContent) summary.i18n_content = i18nContent;
    this._config.summary = summary;
    return this;
  }

  locales(...locales: string[]): this {
    this._config.locales = locales;
    return this;
  }

  enableForward(value: boolean): this {
    this._config.enable_forward = value;
    return this;
  }

  widthMode(mode: string): this {
    this._config.width_mode = mode;
    return this;
  }

  useCustomTranslation(value: boolean): this {
    this._config.use_custom_translation = value;
    return this;
  }

  enableForwardInteraction(value: boolean): this {
    this._config.enable_forward_interaction = value;
    return this;
  }

  style(style: CardStyle): this {
    this._config.style = style;
    return this;
  }

  build(): CardConfig {
    return this._config;
  }
}
