using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

public class PersonBuilder
{
    private readonly Person _person = new();
    
    public PersonBuilder UserId(string userId)
    {
        _person.UserId = userId;
        return this;
    }
    
    public PersonBuilder OpenId(string openId)
    {
        _person.OpenId = openId;
        return this;
    }
    
    public PersonBuilder UnionId(string unionId)
    {
        _person.UnionId = unionId;
        return this;
    }
    
    public PersonBuilder Style(string style)
    {
        _person.Style = style;
        return this;
    }
    
    public PersonBuilder ShowName(bool show = true)
    {
        _person.ShowName = show;
        return this;
    }
    
    public PersonBuilder ShowAvatar(bool show = true)
    {
        _person.ShowAvatar = show;
        return this;
    }
    
    public PersonBuilder ElementId(string id)
    {
        _person.ElementId = id;
        return this;
    }
    
    public PersonBuilder Margin(string margin)
    {
        _person.Margin = margin;
        return this;
    }
    
    public Person Build() => _person;
}
