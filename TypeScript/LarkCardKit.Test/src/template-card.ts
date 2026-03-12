import * as lark from '@larksuiteoapi/node-sdk';
import { CardBuilder, ButtonType } from '../../LarkCardKit/dist/esm/index.js';

const client = new lark.Client({
  appId: process.env.APP_ID!,
  appSecret: process.env.APP_SECRET!
});

async function sendTemplateCard() {
  console.log('=== 模板参数测试 ===');
  
  const cardJson = CardBuilder.create()
    .header(h => h
      .title('${title}')
      .template('${theme}'))
    .body(b => b
      .markdown('**${greeting}**\n\n亲爱的 ${userName}，欢迎加入 ${companyName}！')
      .hr()
      .div(d => d
        .plainText('您的工号是: ${employeeId}')
        .plainText('入职日期: ${joinDate}'))
      .hr()
      .div(d => d
        .horizontal()
        .button(btn => btn
          .text('查看详情')
          .type(ButtonType.Primary)
          .onClickUrl('${detailUrl}'))
        .button(btn => btn
          .text('联系 HR')
          .type(ButtonType.Default)
          .onClick({ action: 'contact_hr', userId: '${employeeId}' }))))
    .setParameters({
      title: '入职欢迎通知',
      theme: 'blue',
      greeting: '🎉 欢迎加入团队！',
      userName: '张三',
      companyName: '飞书科技有限公司',
      employeeId: 'EMP-2024-001',
      joinDate: '2024-01-15',
      detailUrl: 'https://feishu.cn/welcome'
    })
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

sendTemplateCard();
