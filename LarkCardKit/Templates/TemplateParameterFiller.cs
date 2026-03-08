using System.Collections;
using System.Text.RegularExpressions;

namespace LarkCardKit.Templates;

public class TemplateParameterFiller
{
    private readonly Dictionary<string, object?> _parameters = new();
    private readonly TemplateOptions _options;
    
    private static readonly Regex PlaceholderPattern = new(
        @"\$\{(?<key>[^}:]+)(?::(?<default>[^}]*))?\}",
        RegexOptions.Compiled);
    
    public TemplateParameterFiller(TemplateOptions? options = null)
    {
        _options = options ?? new TemplateOptions();
    }
    
    public TemplateParameterFiller SetParameter(string key, object? value)
    {
        _parameters[key] = value;
        return this;
    }
    
    public TemplateParameterFiller SetParameters(object parameters)
    {
        if (parameters == null) return this;
        
        var type = parameters.GetType();
        foreach (var prop in type.GetProperties())
        {
            _parameters[prop.Name] = prop.GetValue(parameters);
        }
        return this;
    }
    
    public TemplateParameterFiller SetParameters(Dictionary<string, object?> parameters)
    {
        foreach (var kvp in parameters)
        {
            _parameters[kvp.Key] = kvp.Value;
        }
        return this;
    }
    
    public object? GetValue(string key)
    {
        if (_parameters.TryGetValue(key, out var value))
            return value;
        return null;
    }
    
    public T? GetValue<T>(string key)
    {
        var value = GetValue(key);
        if (value == null) return default;
        
        if (value is T typedValue)
            return typedValue;
        
        return ConvertValue<T>(value);
    }
    
    public string FillString(string? input)
    {
        if (string.IsNullOrEmpty(input))
            return input ?? string.Empty;
        
        return PlaceholderPattern.Replace(input, match =>
        {
            var key = match.Groups["key"].Value;
            var defaultValue = match.Groups["default"].Success 
                ? match.Groups["default"].Value 
                : null;
            
            var value = GetNestedValue(key);
            
            if (value == null)
            {
                return defaultValue ?? (_options.KeepUnmatchedPlaceholders ? match.Value : "");
            }
            
            return value?.ToString() ?? defaultValue ?? "";
        });
    }
    
    private object? GetNestedValue(string path)
    {
        var parts = path.Split('.');
        object? current = null;
        
        if (!_parameters.TryGetValue(parts[0], out current))
            return null;
        
        for (int i = 1; i < parts.Length && current != null; i++)
        {
            var type = current.GetType();
            var prop = type.GetProperty(parts[i]);
            if (prop == null)
                return null;
            current = prop.GetValue(current);
        }
        
        return current;
    }
    
    private T? ConvertValue<T>(object value)
    {
        var targetType = typeof(T);
        
        if (value == null)
            return default;
        
        if (targetType == typeof(string))
            return (T?)(object?)value?.ToString();
        
        var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;
        
        if (underlyingType.IsEnum)
        {
            if (value is string strValue)
                return (T)Enum.Parse(underlyingType, strValue, true);
            return (T)value;
        }
        
        if (underlyingType == typeof(bool))
        {
            if (value is string boolStr)
                return (T)(object)bool.Parse(boolStr);
            return (T)value;
        }
        
        if (typeof(IEnumerable).IsAssignableFrom(targetType) && value is IEnumerable enumerable)
        {
            if (targetType.IsArray && value is Array array)
            {
                return (T)(object)array;
            }
            
            if (targetType.IsGenericType)
            {
                var genericDef = targetType.GetGenericTypeDefinition();
                var elementType = targetType.GetGenericArguments()[0];
                
                if (genericDef == typeof(IEnumerable<>) || 
                    genericDef == typeof(ICollection<>) ||
                    genericDef == typeof(List<>))
                {
                    var listType = typeof(List<>).MakeGenericType(elementType);
                    var list = (System.Collections.IList)Activator.CreateInstance(listType)!;
                    foreach (var item in enumerable)
                    {
                        list.Add(item);
                    }
                    return (T)list;
                }
            }
            
            if (targetType == typeof(IEnumerable) || targetType == typeof(ICollection))
            {
                return (T)value;
            }
        }
        
        return (T)Convert.ChangeType(value, underlyingType);
    }
    
    public bool HasParameter(string key) => _parameters.ContainsKey(key);
    
    public void Clear() => _parameters.Clear();
}

public class TemplateOptions
{
    public bool KeepUnmatchedPlaceholders { get; set; } = false;
}
