import * as lark from '@larksuiteoapi/node-sdk';
import { CardBuilder, ButtonType, toJson } from '../../LarkCardKit/dist/esm/index.js';

const client = new lark.Client({
  appId: process.env.APP_ID!,
  appSecret: process.env.APP_SECRET!
});

async function runAllTests() {
  console.log('========================================');
  console.log('  LarkCardKit TypeScript 测试套件');
  console.log('========================================\n');

  // 测试 1: 基础卡片构建
  console.log('📋 测试 1: 基础卡片构建');
  const basicCard = CardBuilder.create()
    .header(h => h.title('基础卡片').template('blue'))
    .body(b => b
      .plainText('这是一条测试消息')
      .button(btn => btn.text('点击').type(ButtonType.Primary)))
    .build();
  console.log('✅ 基础卡片构建成功');
  console.log('JSON:', toJson(basicCard, true), '\n');

  // 测试 2: 表单卡片构建
  console.log('📋 测试 2: 表单卡片构建');
  const formCard = CardBuilder.create()
    .header(h => h.title('表单卡片'))
    .body(b => b
      .form(f => f
        .name('testForm')
        .input(i => i.name('name').label('姓名').required())
        .select(s => s.name('city').placeholder('选择城市').option('北京', 'bj').option('上海', 'sh'))
        .button(btn => btn.text('提交').submit())))
    .build();
  console.log('✅ 表单卡片构建成功');
  console.log('JSON:', toJson(formCard, true), '\n');

  // 测试 3: 表格卡片构建
  console.log('📋 测试 3: 表格卡片构建');
  const tableCard = CardBuilder.create()
    .header(h => h.title('表格卡片'))
    .body(b => b
      .table(t => t
        .column(col => col.name('name').displayName('名称'))
        .column(col => col.name('value').displayName('值'))
        .row(r => r.textCell('name', '测试1').textCell('value', '100'))
        .row(r => r.textCell('name', '测试2').textCell('value', '200'))))
    .build();
  console.log('✅ 表格卡片构建成功');
  console.log('JSON:', toJson(tableCard, true), '\n');

  // 测试 4: 模板参数
  console.log('📋 测试 4: 模板参数填充');
  const templateCard = CardBuilder.create()
    .header(h => h.title('${title}'))
    .body(b => b.plainText('Hello, ${userName}!'))
    .setParameters({ title: '欢迎', userName: '世界' })
    .build();
  console.log('✅ 模板参数填充成功');
  console.log('JSON:', toJson(templateCard, true), '\n');

  // 测试 5: 元素操作
  console.log('📋 测试 5: 元素操作');
  const builder = CardBuilder.create()
    .body(b => b
      .plainText('文本1')
      .button(btn => btn.elementId('btn1').text('按钮1')));

  const foundElement = builder.findElementById('btn1');
  console.log('查找元素:', foundElement ? '✅ 找到' : '❌ 未找到');

  const modified = builder.modifyElement('btn1', el => {
    if (el.tag === 'button') {
      (el as any).text = { tag: 'plain_text', content: '修改后的按钮' };
    }
  });
  console.log('修改元素:', modified ? '✅ 成功' : '❌ 失败');

  console.log('\n========================================');
  console.log('  所有测试完成！');
  console.log('========================================\n');

  // 发送测试消息（如果配置了环境变量）
  if (process.env.RECEIVE_ID) {
    console.log('📤 发送测试消息到飞书...');
    try {
      const res = await client.im.message.create({
        params: { receive_id_type: (process.env.RECEIVE_ID_TYPE || 'open_id') as any },
        data: {
          receive_id: process.env.RECEIVE_ID!,
          msg_type: 'interactive',
          content: toJson(basicCard)
        }
      });
      console.log('✅ 消息发送成功！消息 ID:', res.data?.message_id);
    } catch (error: any) {
      console.error('❌ 消息发送失败:', error.message);
    }
  } else {
    console.log('💡 提示: 配置 .env 文件中的 RECEIVE_ID 可发送测试消息到飞书');
  }
}

runAllTests();
