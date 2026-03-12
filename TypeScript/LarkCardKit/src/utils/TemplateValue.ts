export class TemplateValue {
  constructor(
    public readonly key: string,
    public readonly defaultValue?: unknown
  ) {}

  static create(key: string, defaultValue?: unknown): TemplateValue {
    return new TemplateValue(key, defaultValue);
  }

  get placeholder(): string {
    if (this.defaultValue !== undefined) {
      return `\${${this.key}:${this.defaultValue}}`;
    }
    return `\${${this.key}}`;
  }

  toString(): string {
    return this.placeholder;
  }
}

export function template(key: string, defaultValue?: unknown): TemplateValue {
  return TemplateValue.create(key, defaultValue);
}
