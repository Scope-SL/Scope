// -----------------------------------------------------------------------
// <copyright file="ObjectSpawn.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Features.Packets
{
    using Scope.Client.API.Enums;
    using UnityEngine;

    /// <summary>
    /// The Object Spawn packet.
    /// </summary>
    public static class ObjectSpawn
    {
        /// <summary>
        /// Gets the packet's <see cref="TransmissionNetworkObjectType"/>.
        /// </summary>
        public static TransmissionNetworkObjectType Type => TransmissionNetworkObjectType.ObjectSpawn;

        /// <summary>
        /// Encodes the packet.
        /// </summary>
        /// <param name="position">The <see cref="Vector3"/> position.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> rotation.</param>
        /// <param name="value">The <see cref="string"/> name.</param>
        /// <param name="blueprint">The <see cref="string"/> blueprint.</param>
        /// <returns>The encoded <see cref="TransmissionNetworkObject"/>.</returns>
        public static TransmissionNetworkObject Encode(Vector3 position, Quaternion rotation, string value, string blueprint) =>
            TransmissionNetworkObject.GetDecodedObject((ushort)Type, new Packet
            {
                Blueprint = blueprint,
                Name = value,
                X = position.x,
                Y = position.y,
                Z = position.z,
                Rx = rotation.x,
                Ry = rotation.y,
                Rz = rotation.z,
                Rw = rotation.w,
            });

        /// <summary>
        /// Decodes the packet.
        /// </summary>
        /// <param name="transmissionNetworkObject">The encoded <see cref="TransmissionNetworkObject"/>.</param>
        /// <param name="position">The decoded <see cref="Vector3"/> position.</param>
        /// <param name="rotation">The decoded <see cref="Quaternion"/> rotation.</param>
        /// <param name="value">The decoded <see cref="string"/> name.</param>
        /// <param name="blueprint">The decoded <see cref="string"/> blueprint.</param>
        public static void Decode(
            TransmissionNetworkObject transmissionNetworkObject,
            out Vector3 position,
            out Quaternion rotation,
            out string value,
            out string blueprint)
        {
            Packet packet = transmissionNetworkObject.Cast<Packet>();
            position = new(packet.X, packet.Y, packet.Z);
            rotation = new(packet.Rx, packet.Ry, packet.Rx, packet.Rw);
            value = packet.Name;
            blueprint = packet.Blueprint;
        }

        private class Packet
        {
            public string Blueprint { get; set; }

            public string Name { get; set; }

            [Newtonsoft.Json.JsonProperty("x")]
            public float X { get; set; }

            [Newtonsoft.Json.JsonProperty("y")]
            public float Y { get; set; }

            [Newtonsoft.Json.JsonProperty("z")]
            public float Z { get; set; }

            [Newtonsoft.Json.JsonProperty("rx")]
            public float Rx { get; set; }

            [Newtonsoft.Json.JsonProperty("ry")]
            public float Ry { get; set; }

            [Newtonsoft.Json.JsonProperty("rz")]
            public float Rz { get; set; }

            [Newtonsoft.Json.JsonProperty("rw")]
            public float Rw { get; set; }
        }
    }
}
