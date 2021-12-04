// -----------------------------------------------------------------------
// <copyright file="ClientPlaySound.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Features.Packets
{
    using Scope.Client.API.Enums;
    using UnityEngine;

    /// <summary>
    /// The Client PlaySound packet.
    /// </summary>
    public static class ClientPlaySound
    {
        /// <summary>
        /// Gets the packet's <see cref="TransmissionNetworkObjectType"/>.
        /// </summary>
        public static TransmissionNetworkObjectType Type => TransmissionNetworkObjectType.ClientPlaySound;

        /// <summary>
        /// Encodes the packet.
        /// </summary>
        /// <param name="value">The <see cref="string"/> name.</param>
        /// <param name="position">The <see cref="Vector3"/> position.</param>
        /// <returns>The encoded <see cref="TransmissionNetworkObject"/>.</returns>
        public static TransmissionNetworkObject Encode(string value, Vector3 position) =>
            TransmissionNetworkObject.GetDecodedObject((ushort)Type, new Packet(value, position.x, position.y, position.z));

        /// <summary>
        /// Decodes the packet.
        /// </summary>
        /// <param name="transmissionNetworkObject">The encoded <see cref="TransmissionNetworkObject"/>.</param>
        /// <param name="value">The decoded <see cref="string"/> name.</param>
        /// <param name="position">The decoded <see cref="Vector3"/> position.</param>
        public static void Decode(TransmissionNetworkObject transmissionNetworkObject, out string value, out Vector3 position)
        {
            Packet packet = transmissionNetworkObject.Cast<Packet>();
            value = packet.Name;
            position = new(packet.Z, packet.Y, packet.Z);
        }

        private class Packet
        {
            public Packet(string value, float x, float y, float z)
            {
                Name = value;
                X = x;
                Y = y;
                Z = z;
            }

            public string Name { get; set; }

            public float X { get; set; }

            public float Y { get; set; }

            public float Z { get; set; }
        }
    }
}
