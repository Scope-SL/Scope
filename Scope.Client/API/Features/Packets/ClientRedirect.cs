// -----------------------------------------------------------------------
// <copyright file="ClientRedirect.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Features.Packets
{
    using Scope.Client.API.Enums;

    /// <summary>
    /// The Client Redirect packet.
    /// </summary>
    public static class ClientRedirect
    {
        /// <summary>
        /// Gets the packet's <see cref="TransmissionNetworkObjectType"/>.
        /// </summary>
        public static TransmissionNetworkObjectType Type => TransmissionNetworkObjectType.ClientRedirect;

        /// <summary>
        /// Encodes the packet.
        /// </summary>
        /// <param name="client">The <see cref="string"/> client.</param>
        /// <returns>The encoded <see cref="TransmissionNetworkObject"/>.</returns>
        public static TransmissionNetworkObject Encode(string client) => TransmissionNetworkObject.GetDecodedObject((ushort)Type, new Packet(client));

        /// <summary>
        /// Decodes the packet.
        /// </summary>
        /// <param name="transmissionNetworkObject">The encoded <see cref="TransmissionNetworkObject"/>.</param>
        /// <param name="value">The decoded <see cref="string"/> name.</param>
        public static void Decode(TransmissionNetworkObject transmissionNetworkObject, out string value) => value = transmissionNetworkObject.Cast<Packet>().Client;

        private class Packet
        {
            public Packet(string client) => Client = client;

            public string Client { get; set; }
        }
    }
}
