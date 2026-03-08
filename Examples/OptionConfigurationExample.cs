using LarkCardKit.Builders;
using LarkCardKit.Enums;
using LarkCardKit.Models;

namespace LarkCardKit.Examples;

public static class OptionConfigurationExample
{
    public static void Run()
    {
        Console.WriteLine("=== 对象参数配置选项示例 ===\n");

        Example1_BasicOptions();
        Example2_SelectOptionInfoObject();
        Example3_TupleImplicitConversion();
        Example4_GenericCollectionConversion();
        Example5_ComplexScenario();
    }

    private static void Example1_BasicOptions()
    {
        Console.WriteLine("--- 示例 1: 基本选项配置 ---");

        var card = CardBuilder.Create()
            .Header(h => h.Title("基本选项配置"))
            .Body(b => b
                .Select(s => s
                    .Name("city")
                    .Placeholder("请选择城市")
                    .AddOption("beijing", "北京")
                    .AddOption("shanghai", "上海")
                    .AddOption("guangzhou", "广州")))
            .ToJson(indented: true);

        Console.WriteLine(card);
        Console.WriteLine();
    }

    private static void Example2_SelectOptionInfoObject()
    {
        Console.WriteLine("--- 示例 2: 使用 SelectOptionInfo 对象 ---");

        var options = new List<SelectOptionInfo>
        {
            new SelectOptionInfo("dev", "开发部", "负责产品研发"),
            new SelectOptionInfo("qa", "测试部", "负责质量保证"),
            new SelectOptionInfo("ops", "运维部", "负责系统运维")
        };

        var card = CardBuilder.Create()
            .Header(h => h.Title("使用 SelectOptionInfo 对象"))
            .Body(b => b
                .Select(s => s
                    .Name("department")
                    .Placeholder("请选择部门")
                    .AddOptions(options)))
            .ToJson(indented: true);

        Console.WriteLine(card);
        Console.WriteLine();
    }

    private static void Example3_TupleImplicitConversion()
    {
        Console.WriteLine("--- 示例 3: 使用元组隐式转换 ---");

        SelectOptionInfo option1 = ("high", "高优先级");
        SelectOptionInfo option2 = ("medium", "中优先级");
        SelectOptionInfo option3 = ("low", "低优先级");

        var card = CardBuilder.Create()
            .Header(h => h.Title("使用元组隐式转换"))
            .Body(b => b
                .Select(s => s
                    .Name("priority")
                    .Placeholder("请选择优先级")
                    .AddOption(option1)
                    .AddOption(option2)
                    .AddOption(option3)))
            .ToJson(indented: true);

        Console.WriteLine(card);
        Console.WriteLine();
    }

    private static void Example4_GenericCollectionConversion()
    {
        Console.WriteLine("--- 示例 4: 使用泛型集合转换 ---");

        var users = new List<UserInfo>
        {
            new UserInfo { Id = "u001", Name = "张三", Email = "zhangsan@example.com" },
            new UserInfo { Id = "u002", Name = "李四", Email = "lisi@example.com" },
            new UserInfo { Id = "u003", Name = "王五", Email = "wangwu@example.com" }
        };

        var card = CardBuilder.Create()
            .Header(h => h.Title("使用泛型集合转换"))
            .Body(b => b
                .Select(s => s
                    .Name("assignee")
                    .Placeholder("请选择负责人")
                    .Options(users, u => new SelectOptionInfo(u.Id, u.Name))))
            .ToJson(indented: true);

        Console.WriteLine(card);
        Console.WriteLine();

        var card2 = CardBuilder.Create()
            .Header(h => h.Title("使用值/文本选择器"))
            .Body(b => b
                .Select(s => s
                    .Name("reviewer")
                    .Placeholder("请选择审核人")
                    .Options(users, u => u.Id, u => $"{u.Name} ({u.Email})")))
            .ToJson(indented: true);

        Console.WriteLine(card2);
        Console.WriteLine();
    }

    private static void Example5_ComplexScenario()
    {
        Console.WriteLine("--- 示例 5: 复杂场景 - 多选组件 ---");

        var skills = new List<SkillInfo>
        {
            new SkillInfo { Code = "csharp", Name = "C#", Category = "后端" },
            new SkillInfo { Code = "typescript", Name = "TypeScript", Category = "前端" },
            new SkillInfo { Code = "python", Name = "Python", Category = "后端" },
            new SkillInfo { Code = "react", Name = "React", Category = "前端" }
        };

        var card = CardBuilder.Create()
            .Header(h => h.Title("技能选择（多选）"))
            .Body(b => b
                .Checkbox(c => c
                    .Name("skills")
                    .Placeholder("请选择技能")
                    .MultiSelect()
                    .Options(skills, s => new CheckboxOptionInfo(s.Code, $"{s.Name} ({s.Category})"))))
            .ToJson(indented: true);

        Console.WriteLine(card);
        Console.WriteLine();
    }

    private class UserInfo
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    private class SkillInfo
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}
