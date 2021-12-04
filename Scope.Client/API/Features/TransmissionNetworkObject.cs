// -----------------------------------------------------------------------
// <copyright file="TransmissionNetworkObject.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Features
{
    using System;
    using System.Text;
    using Newtonsoft.Json;
    using Org.BouncyCastle.Utilities.Encoders;
    using Scope.Client.API.Enums;
    using Scope.Client.API.Extensions;
    using Scope.Client.Events.EventArgs;
    using Scope.Client.Events.Patches;

    /// <summary>
    /// A tool to manage packets.
    /// </summary>
    public class TransmissionNetworkObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransmissionNetworkObject"/> class.
        /// </summary>
        public TransmissionNetworkObject()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransmissionNetworkObject"/> class.
        /// </summary>
        /// <param name="id">The packet <see cref="ushort">id</see>.</param>
        /// <param name="data">The packet <see cref="byte">data</see>.</param>
        public TransmissionNetworkObject(ushort id, byte[] data)
        {
            Id = id;
            Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransmissionNetworkObject"/> class.
        /// </summary>
        /// <param name="id">The packet <see cref="ushort">id</see>.</param>
        /// <param name="data">The packet <see cref="byte">data</see>.</param>
        /// <param name="transmissionEminence">The stream state.</param>
        public TransmissionNetworkObject(ushort id, byte[] data, byte transmissionEminence)
            : this(id, data) => TrasmissionEminence = transmissionEminence;

        /// <summary>
        /// Gets or sets the TransmissionNetworkObject's packet id.
        /// </summary>
        public ushort Id
        {
            get => (ushort)Type;
            set => Type = (TransmissionNetworkObjectType)value;
        }

        /// <summary>
        /// Gets or sets TransmissionNetworkObject's packet data.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Gets the <see cref="string"/> value of <see cref="Data"/>.
        /// </summary>
        public string Source => Encoding.UTF8.GetString(Data);

        /// <summary>
        /// Gets the readable encoded <see cref="Source"/>.
        /// </summary>
        public string ReadableEncodedSource => Base64.ToBase64String(GetEncodedObject(this));

        /// <summary>
        /// Gets or sets the stream state of the TransmissionNetworkObject.
        /// </summary>
        public byte TrasmissionEminence { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="TransmissionNetworkObjectType"/> of the TransmissionNetworkObject.
        /// </summary>
        public TransmissionNetworkObjectType Type { get; set; }

        /// <summary>
        /// Gets the <see cref="TransmissionNetworkObject"/> from the provided <see cref="byte"/>[] data.
        /// </summary>
        /// <param name="id">The specified <see cref="ushort"/> id.</param>
        /// <param name="data">The <see cref="byte"/>[] data.</param>
        /// <returns>A new <see cref="TransmissionNetworkObject"/>.</returns>
        public static TransmissionNetworkObject GetSource(ushort id, byte[] data) => new(id, data);

        /// <summary>
        /// Gets the <see cref="TransmissionNetworkObject"/> from the provided <see cref="string"/> data.
        /// </summary>
        /// <param name="id">The specified <see cref="ushort"/> id.</param>
        /// <param name="data">The <see cref="string"/> data.</param>
        /// <returns>A new <see cref="TransmissionNetworkObject"/>.</returns>
        public static TransmissionNetworkObject GetSource(ushort id, string data) => new(id, Encoding.UTF8.GetBytes(data));

        /// <summary>
        /// Gets the <see cref="TransmissionNetworkObject"/> from the provided deserialized <see cref="{T}"/> source.
        /// </summary>
        /// <typeparam name="T">The <see cref="{T}"/> source type.</typeparam>
        /// <param name="id">The specified <see cref="ushort"/> id.</param>
        /// <param name="data">The <see cref="{T}"/> source.</param>
        /// <returns>A new <see cref="TransmissionNetworkObject"/>.</returns>
        public static TransmissionNetworkObject GetDecodedObject<T>(ushort id, T data) => GetSource(id, JsonConvert.SerializeObject(data));

        /// <summary>
        /// Gets the encoded value of the provided <see cref="TransmissionNetworkObject"/>.
        /// </summary>
        /// <param name="transmissionNetworkObject">The specified <see cref="TransmissionNetworkObject"/> to encode.</param>
        /// <returns>A <see cref="byte"/>[] which represents the encoded value of the provided <see cref="TransmissionNetworkObject"/>.</returns>
        public static byte[] GetEncodedObject(TransmissionNetworkObject transmissionNetworkObject)
        {
            SetTargetDecodedObject(transmissionNetworkObject, out _, out byte[] id);
            return id;
        }

        /// <summary>
        /// Gets a value which determines if the <see cref="byte"/>[] source meets the standard data requirements.
        /// </summary>
        /// <param name="managedBuffer">The <see cref="byte"/>[] buffer value.</param>
        /// <returns><see langword="true"/> if the <see cref="byte"/>[] source meets the standard data requirements; otherwise, <see langword="false"/>.</returns>
        public static bool GetDataEminenceFromEncodedObject(byte[] managedBuffer) => managedBuffer[0] == byte.MinValue && managedBuffer[1] == byte.MaxValue;

        /// <summary>
        /// Sets the provided <see cref="TransmissionNetworkObject"/>'s values to make them usable for encoding stages.
        /// </summary>
        /// <param name="target">The specified <see cref="TransmissionNetworkObject"/>.</param>
        /// <param name="managedBuffer">The <see cref="byte"/>[] buffer result.</param>
        /// <param name="id">The <see cref="byte"/>[] <see cref="Id"/> result.</param>
        public static void SetTargetDecodedObject(TransmissionNetworkObject target, out byte[] managedBuffer, out byte[] id)
        {
            managedBuffer = new byte[target.Data.Length + 7];
            managedBuffer[0] = byte.MinValue;
            managedBuffer[1] = byte.MaxValue;
            managedBuffer[6] = target.TrasmissionEminence;
            id = BitConverter.GetBytes(target.Id);
            for (int i = 2; i < 6; i++)
                managedBuffer[i] = id[i - 2];
            for (int i = 0; i < target.Data.Length; i++)
                managedBuffer[i + 7] = target.Data[i];
        }

        /// <summary>
        /// Decodes the provided <see cref="byte"/>[] source.
        /// </summary>
        /// <param name="source">The <see cref="byte"/>[] source to decode.</param>
        /// <returns>A decoded <see cref="TransmissionNetworkObject"/>; otherwise, <see langword="null"/> if the provided source is not data.</returns>
        public static TransmissionNetworkObject DecodeTransmissionNetworkObject(byte[] source)
        {
            if (!source.IsData())
                return null;
            byte[] managedBuffer = new byte[source.Length - 7];
            for (int i = 0; i < managedBuffer.Length; i++)
                managedBuffer[i] = source[i + 7];
            return new TransmissionNetworkObject(BitConverter.ToUInt16(source, 2), managedBuffer, source[6]);
        }

        /// <summary>
        /// Handles the provided <see cref="TransmissionNetworkObject"/> to receive.
        /// </summary>
        /// <param name="data">The <see cref="TransmissionNetworkObject"/> to receive.</param>
        public static void ReceiveData(TransmissionNetworkObject data) => Log.Info($"TransmissionNetworkResponse: {data}");

        /// <summary>
        /// Handles the provided <see cref="TransmissionNetworkObject"/> to send.
        /// </summary>
        /// <param name="data">The <see cref="TransmissionNetworkObject"/> to send.</param>
        public static void SendData(TransmissionNetworkObject data)
        {
            SendingDataEventArgs ev = new(data);

            Events.Handlers.Data.OnSendingData(ev);

            if (!ev.IsAllowed)
                return;

            byte[] encoded = GetEncodedObject(data);
            Log.Info($"TransmissionNetworkRequest: {data.ReadableEncodedSource}");

            TransmissionNetwork.Transmission.CmdCommandToServer(encoded, false);
        }

        /// <summary>
        /// Casts the TransmissionNetworkObject to the specified type.
        /// </summary>
        /// <typeparam name="T">The <see cref="{T}"/> source type.</typeparam>
        /// <returns>A new <see cref="{T}"/> object.</returns>
        public T Cast<T>() => JsonConvert.DeserializeObject<T>(Source);
    }
}
