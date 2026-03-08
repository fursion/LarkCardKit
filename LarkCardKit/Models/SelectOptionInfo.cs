namespace LarkCardKit.Models;

public class SelectOptionInfo
{
    public string Value { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string? Description { get; set; }
    
    public SelectOptionInfo() { }
    
    public SelectOptionInfo(string value, string text, string? description = null)
    {
        Value = value;
        Text = text;
        Description = description;
    }
    
    public static implicit operator SelectOptionInfo((string value, string text) tuple) 
        => new(tuple.value, tuple.text);
    
    public static implicit operator SelectOptionInfo((string value, string text, string description) tuple) 
        => new(tuple.value, tuple.text, tuple.description);
}
