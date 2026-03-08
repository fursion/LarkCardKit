using LarkCardKit.Models;

namespace LarkCardKit.Builders;

public class CardLinkBuilder
{
    private readonly CardLink _cardLink = new();
    
    public CardLinkBuilder Url(string url)
    {
        _cardLink.Url = url;
        return this;
    }
    
    public CardLinkBuilder PcUrl(string pcUrl)
    {
        _cardLink.PcUrl = pcUrl;
        return this;
    }
    
    public CardLinkBuilder IosUrl(string iosUrl)
    {
        _cardLink.IosUrl = iosUrl;
        return this;
    }
    
    public CardLinkBuilder AndroidUrl(string androidUrl)
    {
        _cardLink.AndroidUrl = androidUrl;
        return this;
    }
    
    public CardLink Build() => _cardLink;
}
