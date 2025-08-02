using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Csnp.SharedKernel.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for configuring EF Core to use snake_case naming convention.
/// </summary>
public static class ModelBuilderExtensions
{
    #region -- Methods --

    /// <summary>
    /// Applies snake_case naming convention to tables, columns, keys, foreign keys, and indexes.
    /// </summary>
    /// <param name="modelBuilder">The EF Core model builder.</param>
    public static void UseSnakeCaseNames(this ModelBuilder modelBuilder)
    {
        foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(ToSnakeCase(entity.GetTableName()!));

            foreach (IMutableProperty property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.Name));
            }

            foreach (IMutableKey key in entity.GetKeys())
            {
                key.SetName(ToSnakeCase(key.GetName()!));
            }

            foreach (IMutableForeignKey foreignKey in entity.GetForeignKeys())
            {
                foreignKey.SetConstraintName(ToSnakeCase(foreignKey.GetConstraintName()!));
            }

            foreach (IMutableIndex index in entity.GetIndexes())
            {
                index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()!));
            }
        }
    }

    /// <summary>
    /// Converts a given string to snake_case.
    /// </summary>
    /// <param name="name">The input string in PascalCase or camelCase.</param>
    /// <returns>The string converted to snake_case.</returns>
    private static string ToSnakeCase(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return name;
        }

        StringBuilder stringBuilder = new StringBuilder();
        UnicodeCategory? previousCategory = null;

        for (int i = 0; i < name.Length; i++)
        {
            char c = name[i];
            UnicodeCategory currentCategory = char.GetUnicodeCategory(c);

            if (currentCategory == UnicodeCategory.UppercaseLetter ||
                currentCategory == UnicodeCategory.TitlecaseLetter)
            {
                if (i > 0 && previousCategory != UnicodeCategory.SpaceSeparator)
                {
                    stringBuilder.Append('_');
                }

                stringBuilder.Append(char.ToLowerInvariant(c));
            }
            else
            {
                stringBuilder.Append(c);
            }

            previousCategory = currentCategory;
        }

        return stringBuilder.ToString();
    }

    #endregion
}
