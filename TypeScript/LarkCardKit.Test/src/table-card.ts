import * as lark from '@larksuiteoapi/node-sdk';
import { CardBuilder } from '../../LarkCardKit/dist/esm/index.js';

const client = new lark.Client({
  appId: process.env.APP_ID!,
  appSecret: process.env.APP_SECRET!
});

async function sendTableCard() {
  console.log('=== 表格卡片测试 ===');
  
  const cardJson = CardBuilder.create()
    .header(h => h
      .title('销售数据报表')
      .template('green'))
    .body(b => b
      .markdown('**2024年第一季度销售数据**')
      .table(t => t
        .pageSize(5)
        .column(col => col
          .name('product')
          .displayName('产品名称')
          .dataType('text'))
        .column(col => col
          .name('quantity')
          .displayName('销售数量')
          .dataType('number'))
        .column(col => col
          .name('amount')
          .displayName('销售金额')
          .dataType('number')
          .format(fmt => fmt.symbol('¥')))
        .row(row => row
          .textCell('product', '飞书会员')
          .numberCell('quantity', 150)
          .numberCell('amount', 15000))
        .row(row => row
          .textCell('product', '企业版订阅')
          .numberCell('quantity', 80)
          .numberCell('amount', 40000))
        .row(row => row
          .textCell('product', '高级功能包')
          .numberCell('quantity', 200)
          .numberCell('amount', 20000))))
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

sendTableCard();
