import { CardBuilder, ButtonType } from 'lark-card-kit';

// 测试通过 ID 查找和修改组件
console.log('=== 测试通过 ID 查找和修改组件 ===\n');

// 创建卡片
const builder = CardBuilder.create();
builder.header(h => h.title('Test Card'));
builder.body(b => {
  b.plainText('Hello World');
  b.button(btn => btn.text('Click Me').type(ButtonType.Primary).elementId('btn1'));
  b.button(btn => btn.text('Cancel').type(ButtonType.Default).elementId('btn2'));
  b.form(f => {
    f.name('testForm');
    f.input(i => i.name('username').placeholder('Enter username').elementId('input1'));
  });
});

// 测试查找组件
console.log('1. 查找组件:');
const btn1 = builder.findElementById('btn1');
console.log(`   找到 btn1: ${btn1 ? '✓' : '✗'}`);
console.log(`   btn1 tag: ${btn1?.tag}`);

// 测试修改组件
console.log('\n2. 修改组件:');
const modified = builder.modifyElement('btn1', (el: any) => {
  el.disabled = true;
});
console.log(`   修改 btn1 成功: ${modified ? '✓' : '✗'}`);

// 验证修改
const btn1AfterModify = builder.findElementById('btn1') as any;
console.log(`   btn1.disabled: ${btn1AfterModify?.disabled}`);

// 测试类型化修改
console.log('\n3. 类型化修改:');
const modifiedTyped = builder.modifyElementAs<any>('btn2', 'button', (btn) => {
  btn.disabled = true;
});
console.log(`   类型化修改 btn2 成功: ${modifiedTyped ? '✓' : '✗'}`);

// 测试查找所有按钮
console.log('\n4. 查找所有按钮:');
const buttons = builder.findAllElementsByTag('button');
console.log(`   找到 ${buttons.length} 个按钮`);

// 测试批量修改
console.log('\n5. 批量修改:');
const count = builder.updateAllByTag('button', (el: any) => {
  el.width = '100px';
});
console.log(`   修改了 ${count} 个按钮`);

// 测试替换组件
console.log('\n6. 替换组件:');
const replaced = builder.replaceElement('input1', {
  tag: 'input',
  name: 'replacedInput',
  placeholder: { tag: 'plain_text', content: 'Replaced input' }
} as any);
console.log(`   替换 input1 成功: ${replaced ? '✓' : '✗'}`);

// 构建并输出 JSON
const card = builder.build();
console.log('\n7. 最终卡片 JSON:');
console.log(JSON.stringify(card, null, 2));

console.log('\n=== 测试完成 ===');