using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

public class PersonListBuilder
{
    private readonly PersonList _personList = new();
    
    public PersonListBuilder AddUserId(string userId)
    {
        _personList.UserIds ??= new List<string>();
        _personList.UserIds.Add(userId);
        return this;
    }
    
    public PersonListBuilder UserIds(params string[] userIds)
    {
        _personList.UserIds = userIds.ToList();
        return this;
    }
    
    public PersonListBuilder AddOpenId(string openId)
    {
        _personList.OpenIds ??= new List<string>();
        _personList.OpenIds.Add(openId);
        return this;
    }
    
    public PersonListBuilder OpenIds(params string[] openIds)
    {
        _personList.OpenIds = openIds.ToList();
        return this;
    }
    
    public PersonListBuilder AddUnionId(string unionId)
    {
        _personList.UnionIds ??= new List<string>();
        _personList.UnionIds.Add(unionId);
        return this;
    }
    
    public PersonListBuilder UnionIds(params string[] unionIds)
    {
        _personList.UnionIds = unionIds.ToList();
        return this;
    }
    
    public PersonListBuilder Style(string style)
    {
        _personList.Style = style;
        return this;
    }
    
    public PersonListBuilder ElementId(string id)
    {
        _personList.ElementId = id;
        return this;
    }
    
    public PersonListBuilder Margin(string margin)
    {
        _personList.Margin = margin;
        return this;
    }
    
    public PersonList Build() => _personList;
}
