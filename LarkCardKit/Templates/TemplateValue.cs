namespace LarkCardKit.Templates;

public class TemplateValue
{
    public string Key { get; }
    public string? DefaultValue { get; }
    
    public TemplateValue(string key, string? defaultValue = null)
    {
        Key = key;
        DefaultValue = defaultValue;
    }
    
    public static implicit operator TemplateValue(string key)
        => new(key);
    
    public static TemplateValue WithDefault(string key, string defaultValue)
        => new(key, defaultValue);
}
