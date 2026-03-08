namespace LarkCardKit.Models;

public class CheckboxOptionInfo
{
    public string Value { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    
    public CheckboxOptionInfo() { }
    
    public CheckboxOptionInfo(string value, string text)
    {
        Value = value;
        Text = text;
    }
    
    public static implicit operator CheckboxOptionInfo((string value, string text) tuple) 
        => new(tuple.value, tuple.text);
}
