using Microsoft.Extensions.Configuration;

static class ConfigurationExtension
{
    public static T GetMandatory<T>(this IConfiguration configuration, string key)
    {
        var value = configuration.GetValue<T>(key);
        if (value == null)
        {
            throw new ArgumentNullException(key);
        }

        return value;
    }
}