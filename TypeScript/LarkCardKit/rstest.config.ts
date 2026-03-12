import { defineConfig } from '@rstest/core';
import { withRslibConfig } from '@rstest/adapter-rslib';

export default defineConfig({
    extends: withRslibConfig(),
    // 额外的 rstest 特定配置
});