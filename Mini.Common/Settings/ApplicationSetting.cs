namespace Mini.Common.Settings;


public class ApplicationSetting
{
    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"Name:{Name}, Version:{Version}";
    }
}