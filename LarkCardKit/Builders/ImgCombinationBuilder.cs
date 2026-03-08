using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

public class ImgCombinationBuilder
{
    private readonly ImgCombination _imgCombination = new();
    
    public ImgCombinationBuilder Mode(string mode)
    {
        _imgCombination.CombinationMode = mode;
        return this;
    }
    
    public ImgCombinationBuilder Double()
    {
        _imgCombination.CombinationMode = "double";
        return this;
    }
    
    public ImgCombinationBuilder Triple()
    {
        _imgCombination.CombinationMode = "triple";
        return this;
    }
    
    public ImgCombinationBuilder Quadruple()
    {
        _imgCombination.CombinationMode = "quadruple";
        return this;
    }
    
    public ImgCombinationBuilder GridSix()
    {
        _imgCombination.CombinationMode = "grid_six";
        return this;
    }
    
    public ImgCombinationBuilder GridNine()
    {
        _imgCombination.CombinationMode = "grid_nine";
        return this;
    }
    
    public ImgCombinationBuilder Transparent(bool transparent = true)
    {
        _imgCombination.CombinationTransparent = transparent;
        return this;
    }
    
    public ImgCombinationBuilder CornerRadius(string cornerRadius)
    {
        _imgCombination.CornerRadius = cornerRadius;
        return this;
    }
    
    public ImgCombinationBuilder AddImage(string imgKey, bool? transparent = null)
    {
        _imgCombination.ImgList ??= new List<ImgCombinationItem>();
        _imgCombination.ImgList.Add(new ImgCombinationItem
        {
            ImgKey = imgKey,
            Transparent = transparent
        });
        return this;
    }
    
    public ImgCombinationBuilder Images(params string[] imgKeys)
    {
        _imgCombination.ImgList ??= new List<ImgCombinationItem>();
        foreach (var imgKey in imgKeys)
        {
            _imgCombination.ImgList.Add(new ImgCombinationItem { ImgKey = imgKey });
        }
        return this;
    }
    
    public ImgCombinationBuilder ElementId(string id)
    {
        _imgCombination.ElementId = id;
        return this;
    }
    
    public ImgCombinationBuilder Margin(string margin)
    {
        _imgCombination.Margin = margin;
        return this;
    }
    
    public ImgCombination Build() => _imgCombination;
}
