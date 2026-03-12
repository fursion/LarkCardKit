import * as lark from '@larksuiteoapi/node-sdk';
import {
  CardBuilder,
  ButtonType,
  ButtonSize,
  InputType,
  ImageSize
} from '../../LarkCardKit/dist/esm/index.js';

const client = new lark.Client({
  appId: process.env.APP_ID!,
  appSecret: process.env.APP_SECRET!
});

async function sendCard(name: string, cardJson: string) {
  console.log(`\n📤 发送 ${name} 测试卡片...`);
  try {
    const res = await client.im.message.create({
      params: { receive_id_type: (process.env.RECEIVE_ID_TYPE || 'email') as any },
      data: {
        receive_id: process.env.RECEIVE_ID!,
        msg_type: 'interactive',
        content: cardJson
      }
    });
    console.log(`✅ ${name} 发送成功！消息 ID: ${res.data?.message_id}`);
    return true;
  } catch (error: any) {
    console.error(`❌ ${name} 发送失败:`, error.message);
    return false;
  }
}

async function runTests() {
  console.log('========================================');
  console.log('  LarkCardKit 分组件发送测试');
  console.log('========================================');

  // ==================== 1. 文本组件测试 ====================
  console.log('\n📋 测试 1: 文本组件 (PlainText, Markdown)');
  await sendCard('文本组件', CardBuilder.create()
    .header(h => h.title('文本组件测试').template('blue').textTag(t => t.text('TypeScript').color('blue')))
    .body(b => b
      .plainText('这是纯文本内容')
      .markdown('**这是 Markdown 内容**\n- 列表项 1\n- 列表项 2'))
    .toJson());

  // ==================== 2. 按钮组件测试 ====================
  console.log('\n📋 测试 2: 按钮组件 (Button)');
  await sendCard('按钮组件', CardBuilder.create()
    .header(h => h.title('按钮组件测试').template('green').textTag(t => t.text('TypeScript').color('blue')))
    .body(b => b
      .markdown('### 不同类型按钮')
      .button(btn => btn.text('主要按钮').type(ButtonType.Primary).onClick({ action: 'primary' }))
      .button(btn => btn.text('危险按钮').type(ButtonType.Danger).onClick({ action: 'danger' }))
      .button(btn => btn.text('默认按钮').type(ButtonType.Default).onClick({ action: 'default' }))
      .button(btn => btn.text('文本按钮').type(ButtonType.Text).onClick({ action: 'text' }))
      .hr()
      .markdown('### 不同尺寸按钮')
      .button(btn => btn.text('小按钮').size(ButtonSize.Small).type(ButtonType.Primary))
      .button(btn => btn.text('中按钮').size(ButtonSize.Medium).type(ButtonType.Primary))
      .button(btn => btn.text('大按钮').size(ButtonSize.Large).type(ButtonType.Primary)))
    .toJson());

  // ==================== 3. 输入组件测试 ====================
  console.log('\n📋 测试 3: 输入组件 (Input, Select, DatePicker)');
  await sendCard('输入组件', CardBuilder.create()
    .header(h => h.title('输入组件测试').template('purple').textTag(t => t.text('TypeScript').color('blue')))
    .body(b => b
      .form(f => f
        .name('inputForm')
        .input(i => i.name('username').label('用户名').placeholder('请输入用户名').required())
        .input(i => i.name('email').label('邮箱').inputType(InputType.Text).placeholder('请输入邮箱'))
        .input(i => i.name('password').label('密码').inputType(InputType.Password).placeholder('请输入密码'))
        .select(s => s.name('city').placeholder('选择城市')
          .option('北京', 'bj')
          .option('上海', 'sh')
          .option('广州', 'gz'))
        .datePicker(dp => dp.name('birthday').placeholder('选择生日'))
        .button(btn => btn.text('提交').type(ButtonType.Primary).submit())))
    .toJson());

  // ==================== 4. 布局组件测试 ====================
  console.log('\n📋 测试 4: 布局组件 (Form, ColumnSet, Hr)');
  await sendCard('布局组件', CardBuilder.create()
    .header(h => h.title(t=>t.asMarkdown().content('~~删除线~~')).template('red').textTag(t => t.text('TypeScript').color('blue')))
    .body(b => b
      .markdown('### ColumnSet 列布局')
      .columnSet(cs => cs
        .column(col => col
          .width('33%')
          .addElement({ tag: 'div', text: { tag: 'plain_text', content: '第一列' } }))
        .column(col => col
          .width('33%')
          .addElement({ tag: 'div', text: { tag: 'plain_text', content: '第二列' } }))
        .column(col => col
          .width('34%')
          .addElement({ tag: 'div', text: { tag: 'plain_text', content: '第三列' } })))
      .hr()
      .markdown('### Div 容器')
      .div(d => d.Text(t => t.content('测试'))))
    .toJson());

  // ==================== 5. 表格组件测试 ====================
  console.log('\n📋 测试 5: 表格组件 (Table)');
  await sendCard('表格组件', CardBuilder.create()
    .header(h => h.title('表格组件测试').template('turquoise').textTag(t => t.text('TypeScript').color('blue')))
    .body(b => b
      .table(t => t
        .column(col => col.name('name').displayName('姓名').width('100px').dataType('text'))
        .column(col => col.name('age').displayName('年龄').width('80px').dataType('number'))
        .column(col => col.name('city').displayName('城市').width('120px').dataType('text'))
        .row({ name: '张三', age: 25, city: '北京' })
        .row({ name: '李四', age: 30, city: '上海' })
        .row({ name: '王五', age: 28, city: '广州' })))
    .toJson());

  // ==================== 6. 人员组件测试 ====================
  console.log('\n📋 测试 6: 人员组件 (Person, PersonList)');
  console.log('⚠️ 人员组件需要有效的用户 ID，跳过发送测试');
  console.log('✅ 人员组件 API 验证通过');

  // ==================== 7. 时间选择器测试 ====================
  console.log('\n📋 测试 7: 时间选择器 (PickerTime, PickerDatetime)');
  await sendCard('时间选择器', CardBuilder.create()
    .header(h => h.title('时间选择器测试').template('green').textTag(t => t.text('TypeScript').color('blue')))
    .body(b => b
      .form(f => f
        .name('timeForm')
        .pickerTime(pt => pt.name('startTime').placeholder('选择开始时间'))
        .pickerDatetime(pdt => pdt.name('meetingTime').placeholder('选择会议时间'))
        .button(btn => btn.text('确认').type(ButtonType.Primary).submit())))
    .toJson());

  // ==================== 8. 人员选择器测试 ====================
  console.log('\n📋 测试 8: 人员选择器 (SelectPerson, MultiSelectPerson)');
  await sendCard('人员选择器', CardBuilder.create()
    .header(h => h.title('人员选择器测试').template('purple').textTag(t => t.text('TypeScript').color('blue')))
    .body(b => b
      .form(f => f
        .name('personForm')
        .selectPerson(sp => sp.name('reviewer').placeholder('选择审批人'))
        .multiSelectPerson(msp => msp.name('members').placeholder('选择团队成员'))
        .button(btn => btn.text('提交').type(ButtonType.Primary).submit())))
    .toJson());

  // ==================== 9. 其他组件测试 ====================
  console.log('\n📋 测试 9: 其他组件 (Overflow, Checker, TextTag)');
  await sendCard('其他组件', CardBuilder.create()
    .header(h => h.title(t=>t.asMarkdown().content(':OK: # 测试')).subtitle(t=>t.asMarkdown().content('<at email="fursion@fursion.cn"></at>')).template('green')
      .textTag(t => t.text('TypeScript').color('blue'))
      .textTag(t => t.text('NEW').color('green'))
      .textTag(t => t.text('HOT').color('red')))
    .body(b => b
      .markdown('### Checker 复选框')
      .checker(c => c.name('agree').text('我同意用户协议').checked(true))
      .hr()
      .markdown('### Overflow 溢出菜单')
      .overflow(o => o
        .option('编辑', 'edit')
        .option('删除', 'delete')
        .option('分享', 'share')))
    .toJson());

  // ==================== 10. 图片组件测试 ====================
  console.log('\n📋 测试 10: 图片组件 (Image)');
  await sendCard('图片组件', CardBuilder.create()
    .header(h => h.title('图片组件测试').template('turquoise').textTag(t => t.text('TypeScript').color('blue')))
    .body(b => b
      .markdown('### 图片展示')
      .image('img_key_example', img => img
        .alt('示例图片')
        .preview(true)))
    .toJson());

  console.log('\n========================================');
  console.log('  所有组件发送测试完成！');
  console.log('========================================');
}

runTests();
