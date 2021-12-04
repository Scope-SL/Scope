// -----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A set of extensions for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a <see cref="string"/> to snake_case convention.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <param name="shouldReplaceSpecialChars">Indicates whether special chars has to be replaced or not.</param>
        /// <returns>A snake_case <see cref="string"/>.</returns>
        public static string ToSnakeCase(this string str, bool shouldReplaceSpecialChars = true)
        {
            string snakeCaseString = string.Concat(str.Select((ch, i) => i > 0 && char.IsUpper(ch) ? "_" + ch.ToString() : ch.ToString())).ToLower();

            return shouldReplaceSpecialChars ? Regex.Replace(snakeCaseString, @"[^0-9a-zA-Z_]+", string.Empty) : snakeCaseString;
        }

        /// <summary>
        /// Converts an <see cref="IEnumerable{T}"/> into <see cref="string"/>.
        /// </summary>
        /// <typeparam name="T">The type of the IEnumerable.</typeparam>
        /// <param name="enumerable">The instance.</param>
        /// <param name="showIndex">Indicates whether the enumerator index should be shown or not.</param>
        /// <returns>A <see cref="string"/> which represents the converted <see cref="IEnumerable{T}"/>.</returns>
        public static string ToString<T>(this IEnumerable<T> enumerable, bool showIndex = true)
        {
            StringBuilder stringBuilder = new();
            int index = 0;

            stringBuilder.AppendLine(string.Empty);

            foreach (T enumerator in enumerable)
            {
                if (showIndex)
                {
                    stringBuilder.Append(index++);
                    stringBuilder.Append(' ');
                }

                stringBuilder.AppendLine(enumerator.ToString());
            }

            string result = stringBuilder.ToString();

            return result;
        }
    }
}
