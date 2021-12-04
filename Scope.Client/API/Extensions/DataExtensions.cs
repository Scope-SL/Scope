// -----------------------------------------------------------------------
// <copyright file="DataExtensions.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Scope.Client.API.Features;
    using static Scope.Client.API.Features.TransmissionNetworkObject;

    /// <summary>
    /// A set of extensions for data and network packets.
    /// </summary>
    public static class DataExtensions
    {
        /// <summary>
        /// Gets a value indicating whether or not the provided <see cref="byte"/>[] source is valid data.
        /// </summary>
        /// <param name="source">The <see cref="byte"/>[] source to check.</param>
        /// <returns><see langword="true"/> if the provided <see cref="byte"/>[] source is valid data; otherwise, <see langword="false"/>.</returns>
        public static bool IsData(this IEnumerable<byte> source)
        {
            if (source.Count() < 2)
                return false;
            return GetDataEminenceFromEncodedObject(source.ToArray());
        }
    }
}
