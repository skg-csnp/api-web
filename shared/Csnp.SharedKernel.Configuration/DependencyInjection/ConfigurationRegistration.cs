using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Csnp.SharedKernel.Configuration.DependencyInjection;

/// <summary>
/// Provides extension methods for overriding configuration options with environment variable values.
/// </summary>
public static class ConfigurationRegistration
{
    /// <summary>
    /// Overrides strongly-typed configuration options with values from environment variables.
    /// Useful for injecting runtime environment values (e.g., from .env files or system variables) into configuration.
    /// </summary>
    /// <typeparam name="T">The type of options to override. Must be a class with a parameterless constructor.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
    /// <param name="sectionName">The configuration section name. Used to identify the environment variable key prefix.</param>
    /// <param name="envPrefix">The prefix used for environment variable keys (e.g., "LOC_NOTIFICATION").</param>
    /// <param name="mergeFunc">
    /// A function that takes the original configuration object and the environment-derived object,
    /// and returns a merged result.
    /// </param>
    /// <returns>The modified <see cref="IServiceCollection"/> to support method chaining.</returns>
    public static IServiceCollection OverrideWithEnv<T>(
        this IServiceCollection services,
        string sectionName,
        string envPrefix,
        Func<T, T, T> mergeFunc) where T : class, new()
    {
        services.PostConfigure<T>(options =>
        {
            T envValues = sectionName.ConvertEnvironmentVariable<T>(envPrefix);
            T result = mergeFunc(options, envValues);

            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                object? value = prop.GetValue(result);
                prop.SetValue(options, value);
            }
        });

        return services;
    }
}
