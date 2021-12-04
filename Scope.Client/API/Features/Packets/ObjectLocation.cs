// -----------------------------------------------------------------------
// <copyright file="ObjectLocation.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Features.Packets
{
    using System;
    using System.Text;
    using Scope.Client.API.Enums;
    using UnityEngine;

    /// <summary>
    /// The Object Location packet.
    /// </summary>
    public static class ObjectLocation
    {
        /// <summary>
        /// Gets the packet's <see cref="TransmissionNetworkObjectType"/>.
        /// </summary>
        public static TransmissionNetworkObjectType Type => TransmissionNetworkObjectType.ObjectLocation;

        /// <summary>
        /// Encodes the packet.
        /// </summary>
        /// <param name="position">The <see cref="Vector3"/> position.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> rotation.</param>
        /// <param name="value">The <see cref="string"/> name.</param>
        /// <returns>The encoded <see cref="TransmissionNetworkObject"/>.</returns>
        public static TransmissionNetworkObject Encode(Vector3 position, Quaternion rotation, string value)
        {
            byte[] bytes = new byte[(4 * 3) + (4 * 4) + 4 + (4 + value.Length)];
            byte[] xb = BitConverter.GetBytes(position.x);
            byte[] yb = BitConverter.GetBytes(position.y);
            byte[] zb = BitConverter.GetBytes(position.z);
            byte[] rwb = BitConverter.GetBytes(rotation.w);
            byte[] rxb = BitConverter.GetBytes(rotation.x);
            byte[] ryb = BitConverter.GetBytes(rotation.y);
            byte[] rzb = BitConverter.GetBytes(rotation.z);
            byte[] nlb = BitConverter.GetBytes(value.Length);
            byte[] nb = Encoding.UTF8.GetBytes(value);

            for (int i = 0; i < 4; i++)
                bytes[i] = xb[i];

            for (int i = 0; i < 4; i++)
                bytes[i + (4 * 1)] = yb[i];

            for (int i = 0; i < 4; i++)
                bytes[i + (4 * 2)] = zb[i];

            for (int i = 0; i < 4; i++)
                bytes[i + (4 * 3)] = rwb[i];

            for (int i = 0; i < 4; i++)
                bytes[i + (4 * 4)] = rxb[i];

            for (int i = 0; i < 4; i++)
                bytes[i + (4 * 5)] = ryb[i];

            for (int i = 0; i < 4; i++)
                bytes[i + (4 * 6)] = rzb[i];

            for (int i = 0; i < 4; i++)
                bytes[i + (4 * 7)] = nlb[i];

            for (int i = 0; i < value.Length; i++)
                bytes[i + (4 * 8)] = nb[i];

            return TransmissionNetworkObject.GetSource((ushort)Type, bytes);
        }

        /// <summary>
        /// Decodes the packet.
        /// </summary>
        /// <param name="transmissionNetworkObject">The encoded <see cref="TransmissionNetworkObject"/>.</param>
        /// <param name="position">The decoded <see cref="Vector3"/> position.</param>
        /// <param name="rotation">The decoded <see cref="Quaternion"/> rotation.</param>
        /// <param name="value">The decoded <see cref="string"/> name.</param>
        public static void Decode(
            TransmissionNetworkObject transmissionNetworkObject,
            out Vector3 position,
            out Quaternion rotation,
            out string value)
        {
            byte[] data = transmissionNetworkObject.Data;
            float x = BitConverter.ToSingle(data, 4 * 0);
            float y = BitConverter.ToSingle(data, 4 * 1);
            float z = BitConverter.ToSingle(data, 4 * 2);
            float rw = BitConverter.ToSingle(data, 4 * 3);
            float rx = BitConverter.ToSingle(data, 4 * 4);
            float ry = BitConverter.ToSingle(data, 4 * 5);
            float rz = BitConverter.ToSingle(data, 4 * 6);
            int nl = BitConverter.ToInt32(data, 4 * 7);
            string n = Encoding.UTF8.GetString(data, 4 * 8, nl);
            position = new Vector3(x, y, z);
            rotation = new Quaternion(rx, ry, rz, rw);
            value = n;
        }
    }
}
