using LarkCardKit.Enums;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 图片构建器
/// </summary>
public class ImageBuilder
{
    private readonly Image _image;
    
    public ImageBuilder(string imgKey)
    {
        _image = new Image { ImgKey = imgKey };
    }
    
    public ImageBuilder Size(ImageSize size)
    {
        _image.Size = size switch
        {
            ImageSize.CropCenter => "crop_center",
            ImageSize.CropTop => "crop_top",
            ImageSize.CropBottom => "crop_bottom",
            ImageSize.Origin => "origin",
            _ => "crop_center"
        };
        return this;
    }
    
    public ImageBuilder Width(string width)
    {
        _image.Width = width;
        return this;
    }
    
    public ImageBuilder Alt(string alt)
    {
        _image.Alt = new PlainText { Content = alt };
        return this;
    }
    
    public ImageBuilder ElementId(string id)
    {
        _image.ElementId = id;
        return this;
    }
    
    public ImageBuilder Margin(string margin)
    {
        _image.Margin = margin;
        return this;
    }
    
    public Image Build() => _image;
}
