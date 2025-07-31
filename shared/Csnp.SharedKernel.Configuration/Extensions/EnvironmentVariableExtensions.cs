using System.Collections;
using System.Text.Json;

namespace Csnp.SharedKernel.Configuration;

/// <summary>
/// Extensions for environment variable mapping
/// </summary>
public static class EnvironmentVariableExtensions
{
    #region -- Methods --

    /// <summary>
    /// Convert environment variables to a strongly typed object
    /// </summary>
    /// <typeparam name="T">Target type</typeparam>
    /// <param name="projectPrefix">Module name (e.g., NOTIFICATION)</param>
    /// <param name="commonPrefix">Global prefix (e.g., LOC, DEV, PRO)</param>
    /// <param name="splitter">Splitter between levels (default: "__")</param>
    /// <returns>Object of type T</returns>
    public static T ConvertEnvironmentVariable<T>(this string projectPrefix, string commonPrefix, string splitter = "__") where T : new()
    {
        if (string.IsNullOrWhiteSpace(projectPrefix) || string.IsNullOrWhiteSpace(commonPrefix))
        {
            return new T();
        }

        var resultLines = new List<string>();
        IDictionary variables = Environment.GetEnvironmentVariables();

        string fullPrefix = $"{commonPrefix}_{projectPrefix}_"; // e.g., LOC_NOTIFICATION_

        foreach (DictionaryEntry entry in variables)
        {
            string? key = entry.Key?.ToString();
            if (key is null || !key.StartsWith(fullPrefix, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            // Strip prefix and any leading underscore
            string strippedKey = key.Substring(fullPrefix.Length).TrimStart('_');
            string value = entry.Value?.ToString() ?? string.Empty;
            resultLines.Add($"{strippedKey}={value}");
        }

        Dictionary<string, object> dictionary = resultLines.ToArray().ConvertIniToDictionary(splitter);
        string json = JsonSerializer.Serialize(dictionary);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        T? result = JsonSerializer.Deserialize<T>(json, options);
        return result ?? new T();
    }

    /// <summary>
    /// Convert flat INI-style lines to a nested dictionary using a splitter (default: "__")
    /// </summary>
    /// <param name="lines">Key-value pairs in INI format</param>
    /// <param name="splitter">Splitter for nested keys (e.g., "__")</param>
    /// <param name="equality">Character that separates key and value (default '=')</param>
    /// <returns>Nested dictionary representing JSON structure</returns>
    private static Dictionary<string, object> ConvertIniToDictionary(this string[] lines, string splitter = "__", char equality = '=')
    {
        var result = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        foreach (string line in lines)
        {
            string trimmed = line.Trim();
            if (string.IsNullOrEmpty(trimmed) || !trimmed.Contains(equality, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            string[] parts = trimmed.Split(equality, 2);
            string[] keys = parts[0].Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            string rawValue = parts[1];

            object typedValue = TryConvertValue(rawValue);

            Dictionary<string, object> current = result;
            for (int i = 0; i < keys.Length - 1; i++)
            {
                string key = keys[i];
                if (!current.TryGetValue(key, out object? child) || child is not Dictionary<string, object> childDict)
                {
                    childDict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    current[key] = childDict;
                }

                current = childDict;
            }

            current[keys[^1]] = typedValue;
        }

        return result;
    }

    /// <summary>
    /// Attempt to convert string to bool, int, double, or leave as string
    /// </summary>
    private static object TryConvertValue(string value)
    {
        if (bool.TryParse(value, out bool boolResult))
        {
            return boolResult;
        }

        if (int.TryParse(value, out int intResult))
        {
            return intResult;
        }

        if (double.TryParse(value, out double doubleResult))
        {
            return doubleResult;
        }

        return value;
    }

    #endregion
}
