// -----------------------------------------------------------------------
// <copyright file="TransmissionNetworkObjectType.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Enums
{
    /// <summary>
    /// Unique identifier for the different types of packets.
    /// </summary>
    public enum TransmissionNetworkObjectType
    {
        /// <summary>
        /// The Welcome packet.
        /// </summary>
        Welcome = 0x00,

        /// <summary>
        /// The Message packet.
        /// </summary>
        Message = 0x01,

        /// <summary>
        /// The Object Spawn packet.
        /// </summary>
        ObjectSpawn = 0x0A,

        /// <summary>
        /// The Object Destroy packet.
        /// </summary>
        ObjectDestroy = 0x0B,

        /// <summary>
        /// The Object Location packet.
        /// </summary>
        ObjectLocation = 0x0C,

        /// <summary>
        /// The Client Redirect packet.
        /// </summary>
        ClientRedirect = 0x14,

        /// <summary>
        /// The Client PlaySound packet.
        /// </summary>
        ClientPlaySound = 0x15,

        /// <summary>
        /// Round packes.
        /// </summary>
        RoundStart = 0x1E,
    }
}
