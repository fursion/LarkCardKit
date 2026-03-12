using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 多图选择构建器
/// </summary>
public class SelectImgBuilder
{
    private readonly SelectImg _selectImg = new();

    /// <summary>
    /// 设置名称
    /// </summary>
    public SelectImgBuilder Name(string name)
    {
        _selectImg.Name = name;
        return this;
    }

    /// <summary>
    /// 设置是否必填
    /// </summary>
    public SelectImgBuilder Required(bool required)
    {
        _selectImg.Required = required;
        return this;
    }

    /// <summary>
    /// 设置占位文本
    /// </summary>
    public SelectImgBuilder Placeholder(string content)
    {
        _selectImg.Placeholder = new PlainText { Content = content };
        return this;
    }

    /// <summary>
    /// 设置标签
    /// </summary>
    public SelectImgBuilder Label(string content)
    {
        _selectImg.Label = new PlainText { Content = content };
        return this;
    }

    /// <summary>
    /// 设置选择模式
    /// </summary>
    public SelectImgBuilder SelectMode(string mode)
    {
        _selectImg.SelectMode = mode;
        return this;
    }

    /// <summary>
    /// 设置单选模式
    /// </summary>
    public SelectImgBuilder Single()
    {
        _selectImg.SelectMode = "single";
        return this;
    }

    /// <summary>
    /// 设置多选模式
    /// </summary>
    public SelectImgBuilder Multi()
    {
        _selectImg.SelectMode = "multi";
        return this;
    }

    /// <summary>
    /// 设置初始选中选项
    /// </summary>
    public SelectImgBuilder InitialOptions(List<string> options)
    {
        _selectImg.InitialOptions = options;
        return this;
    }

    /// <summary>
    /// 添加选项
    /// </summary>
    public SelectImgBuilder Option(string imgKey, string? text = null)
    {
        if (_selectImg.Options == null)
        {
            _selectImg.Options = new List<SelectImgOption>();
        }
        
        var option = new SelectImgOption { Value = imgKey };
        if (!string.IsNullOrEmpty(text))
        {
            option.Text = new PlainText { Content = text };
        }
        
        _selectImg.Options.Add(option);
        return this;
    }

    /// <summary>
    /// 设置宽度
    /// </summary>
    public SelectImgBuilder Width(string width)
    {
        _selectImg.Width = width;
        return this;
    }

    /// <summary>
    /// 设置禁用
    /// </summary>
    public SelectImgBuilder Disabled(bool disabled)
    {
        _selectImg.Disabled = disabled;
        return this;
    }

    /// <summary>
    /// 设置二次确认
    /// </summary>
    public SelectImgBuilder Confirm(string title, string text)
    {
        _selectImg.Confirm = new ConfirmConfig
        {
            Title = new PlainText { Content = title },
            Text = new PlainText { Content = text }
        };
        return this;
    }

    /// <summary>
    /// 设置元素 ID
    /// </summary>
    public SelectImgBuilder ElementId(string id)
    {
        _selectImg.ElementId = id;
        return this;
    }

    /// <summary>
    /// 构建 SelectImg 对象
    /// </summary>
    public SelectImg Build()
    {
        return _selectImg;
    }
}
