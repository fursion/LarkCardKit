using LarkCardKit.Enums;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

public class ImageBuilder
{
    private readonly Image _image;
    
    public ImageBuilder(string imgKey)
    {
        _image = new Image { ImgKey = imgKey };
    }
    
    public ImageBuilder Title(string title)
    {
        _image.Title = new PlainText { Content = title };
        return this;
    }
    
    public ImageBuilder ScaleType(string scaleType)
    {
        _image.ScaleType = scaleType;
        return this;
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
    
    public ImageBuilder Size(string size)
    {
        _image.Size = size;
        return this;
    }
    
    public ImageBuilder CornerRadius(string cornerRadius)
    {
        _image.CornerRadius = cornerRadius;
        return this;
    }
    
    public ImageBuilder Transparent(bool transparent = true)
    {
        _image.Transparent = transparent;
        return this;
    }
    
    public ImageBuilder Preview(bool preview = true)
    {
        _image.Preview = preview;
        return this;
    }
    
    public ImageBuilder Alt(string alt)
    {
        _image.Alt = new PlainText { Content = alt };
        return this;
    }
    
    public ImageBuilder Width(string width)
    {
        _image.Width = width;
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
