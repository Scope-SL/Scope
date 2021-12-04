// -----------------------------------------------------------------------
// <copyright file="RoundStart.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Features.Packets
{
    using Scope.Client.API.Enums;

    /// <summary>
    /// The RoundStart packet.
    /// </summary>
    public static class RoundStart
    {
        /// <summary>
        /// Gets the packet's <see cref="TransmissionNetworkObjectType"/>.
        /// </summary>
        public static TransmissionNetworkObjectType Type => TransmissionNetworkObjectType.RoundStart;
    }
}
