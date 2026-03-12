export interface OpenUrlBehavior {
  type: 'open_url';
  default_url?: string;
  pc_url?: string;
  ios_url?: string;
  android_url?: string;
}

export interface CallbackBehavior {
  type: 'callback';
  value?: unknown;
}
