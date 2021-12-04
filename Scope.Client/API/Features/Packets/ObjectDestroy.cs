// -----------------------------------------------------------------------
// <copyright file="ObjectDestroy.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Features.Packets
{
    using Scope.Client.API.Enums;

    /// <summary>
    /// The Object Spawn packet.
    /// </summary>
    public static class ObjectDestroy
    {
        /// <summary>
        /// Gets the packet's <see cref="TransmissionNetworkObjectType"/>.
        /// </summary>
        public static TransmissionNetworkObjectType Type => TransmissionNetworkObjectType.ObjectDestroy;

        /// <summary>
        /// Encodes the packet.
        /// </summary>
        /// <param name="blueprint">The <see cref="string"/> blueprint.</param>
        /// <param name="value">The <see cref="string"/> name.</param>
        /// <returns>The encoded <see cref="TransmissionNetworkObject"/>.</returns>
        public static TransmissionNetworkObject Encode(string blueprint, string value) =>
            TransmissionNetworkObject.GetDecodedObject((ushort)Type, new Packet(blueprint, value));

        /// <summary>
        /// Decodes the packet.
        /// </summary>
        /// <param name="transmissionNetworkObject">The encoded <see cref="TransmissionNetworkObject"/>.</param>
        /// <param name="value">The decoded <see cref="string"/> name.</param>
        /// <param name="blueprint">The decoded <see cref="string"/> blueprint.</param>
        public static void Decode(TransmissionNetworkObject transmissionNetworkObject, out string value, out string blueprint)
        {
            Packet packet = transmissionNetworkObject.Cast<Packet>();
            value = packet.Name;
            blueprint = packet.Blueprint;
        }

        private class Packet
        {
            public Packet(string blueprint, string value)
            {
                Blueprint = blueprint;
                Name = value;
            }

            public string Blueprint { get; set; }

            public string Name { get; set; }
        }
    }
}
