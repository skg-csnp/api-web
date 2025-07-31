using System.Reflection;

namespace Csnp.SharedKernel.Configuration.Extensions;

/// <summary>
/// Provides extension methods for merging configuration objects.
/// </summary>
public static class ConfigurationMergeExtensions
{
    /// <summary>
    /// Merges non-null and non-default values from a source object into the target object.
    /// This is typically used to apply overrides from environment variables or other external sources.
    /// </summary>
    /// <typeparam name="T">The type of the configuration object. Must be a reference type with a parameterless constructor.</typeparam>
    /// <param name="target">The target object to receive the merged values.</param>
    /// <param name="source">The source object containing the override values.</param>
    /// <returns>The updated target object with non-default values merged in.</returns>
    /// <remarks>
    /// - For reference types, only non-null values from <paramref name="source"/> are merged.
    /// - For value types, only values that differ from the default are merged.
    /// </remarks>
    public static T MergeNonDefaultValues<T>(this T target, T source) where T : class, new()
    {
        foreach (PropertyInfo prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            object? sourceValue = prop.GetValue(source);
            object? defaultValue = prop.PropertyType.IsValueType ? Activator.CreateInstance(prop.PropertyType) : null;

            if (sourceValue is not null && !sourceValue.Equals(defaultValue))
            {
                prop.SetValue(target, sourceValue);
            }
        }

        return target;
    }
}
