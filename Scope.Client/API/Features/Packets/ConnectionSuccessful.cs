// -----------------------------------------------------------------------
// <copyright file="ConnectionSuccessful.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Features.Packets
{
    using Scope.Client.API.Enums;

    /// <summary>
    /// The ConnectionSuccessful packet.
    /// </summary>
    public static class ConnectionSuccessful
    {
        /// <summary>
        /// Gets the packet's <see cref="TransmissionNetworkObjectType"/>.
        /// </summary>
        public static TransmissionNetworkObjectType Type => TransmissionNetworkObjectType.Welcome;

        /// <summary>
        /// Encodes the packet.
        /// </summary>
        /// <param name="data">The client mods.</param>
        /// <returns>The encoded <see cref="TransmissionNetworkObject"/>.</returns>
        public static TransmissionNetworkObject Encode(string[] data) => TransmissionNetworkObject.GetDecodedObject((ushort)Type, new Packet(data));

        /// <summary>
        /// Decodes the packet.
        /// </summary>
        /// <param name="transmissionNetworkObject">The packet to decode.</param>
        /// <param name="data">The client mods.</param>
        public static void Decode(TransmissionNetworkObject transmissionNetworkObject, out string[] data) =>
            data = transmissionNetworkObject.Cast<Packet>().ClientMods;

        private class Packet
        {
            public Packet(string[] clientMods) => ClientMods = clientMods;

            public string[] ClientMods { get; }
        }
    }
}
