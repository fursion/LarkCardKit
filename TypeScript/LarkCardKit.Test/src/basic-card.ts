import * as lark from '@larksuiteoapi/node-sdk';
import { CardBuilder, ButtonType } from '../../LarkCardKit/dist/esm/index.js';

const client = new lark.Client({
  appId: process.env.APP_ID!,
  appSecret: process.env.APP_SECRET!
});

async function sendBasicCard() {
  console.log('=== 基础卡片测试 ===');
  
  const cardJson = CardBuilder.create()
    .header(h => h
      .title('Hello from LarkCardKit TypeScript')
      .template('blue')
      .subtitle('TypeScript 版本测试'))
    .body(b => b
      .markdown('**测试成功！**\n\n这是 TypeScript 版本的 LarkCardKit，使用 Rslib 构建。')
      .hr()
      .div(d => d
        .horizontal()
        .horizontalSpacing('12px')
        .button(btn => btn
          .text('主要按钮')
          .type(ButtonType.Primary)
          .onClick({ action: 'primary_click' }))
        .button(btn => btn
          .text('默认按钮')
          .type(ButtonType.Default)
          .onClick({ action: 'default_click' }))))
    .toJson();

  console.log('卡片 JSON:');
  console.log(JSON.stringify(JSON.parse(cardJson), null, 2));

  if (!process.env.RECEIVE_ID) {
    console.log('\n请配置 .env 文件中的 RECEIVE_ID 以发送消息');
    return;
  }

  try {
    const res = await client.im.message.create({
      params: { receive_id_type: (process.env.RECEIVE_ID_TYPE || 'open_id') as any },
      data: {
        receive_id: process.env.RECEIVE_ID!,
        msg_type: 'interactive',
        content: cardJson
      }
    });
    console.log('\n消息发送成功！');
    console.log('消息 ID:', res.data?.message_id);
  } catch (error: any) {
    console.error('\n消息发送失败:', error.message);
  }
}

sendBasicCard();
