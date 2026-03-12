import * as lark from '@larksuiteoapi/node-sdk';
import { CardBuilder, ButtonType } from '../../LarkCardKit/dist/esm/index.js';

const client = new lark.Client({
  appId: process.env.APP_ID!,
  appSecret: process.env.APP_SECRET!
});

async function sendFormCard() {
  console.log('=== 表单卡片测试 ===');
  
  const cardJson = CardBuilder.create()
    .header(h => h
      .title('用户信息收集')
      .template('turquoise'))
    .body(b => b
      .form(f => f
        .name('userForm')
        .input(i => i
          .name('username')
          .label('用户名')
          .placeholder('请输入用户名')
          .required())
        .input(i => i
          .name('email')
          .label('邮箱')
          .placeholder('请输入邮箱地址')
          .inputType('email'))
        .select(s => s
          .name('department')
          .placeholder('请选择部门')
          .option('技术部', 'tech')
          .option('产品部', 'product')
          .option('运营部', 'operation')
          .option('市场部', 'marketing'))
        .checkbox(c => c
          .name('skills')
          .placeholder('请选择技能')
          .option('TypeScript', 'typescript', true)
          .option('JavaScript', 'javascript')
          .option('Node.js', 'nodejs')
          .option('React', 'react'))
        .div(d => d
          .horizontal()
          .button(btn => btn
            .text('提交')
            .type(ButtonType.Primary)
            .submit())
          .button(btn => btn
            .text('重置')
            .type(ButtonType.Default)
            .reset()))))
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

sendFormCard();
