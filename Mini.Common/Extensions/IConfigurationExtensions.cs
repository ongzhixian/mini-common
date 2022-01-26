using Microsoft.Extensions.Configuration;

namespace Mini.Common.Extensions;

public static class IConfigurationExtensions
{
    public static T GetSectionAs<T>(this IConfiguration configuration, string section)
    {
        var configurationSection = GetConfigurationSection(configuration, section);

        return configurationSection.Get<T>();
    }

    public static IEnumerable<IConfigurationSection> GetSectionChildren(this IConfiguration configuration, string section)
    {
        var configurationSection = GetConfigurationSection(configuration, section);

        return configurationSection.GetChildren();
    }

    public static IConfigurationSection GetConfigurationSection(this IConfiguration configuration, string section)
    {
        var configurationSection = configuration.GetSection(section);

        if (!configurationSection.Exists())
            throw new InvalidOperationException($"Configuration section {section} does not exists.");

        // CONSIDER: Implementing an Exception class for such an occurrence.
        // throw new NotExistsException("Configuration section does not exists.", "Jwt:Conso2")
        // throw new MissingConfigurationSectionException("Jwt:Conso2")

        return configurationSection;
    }

}