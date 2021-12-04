// -----------------------------------------------------------------------
// <copyright file="Data.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Events.Handlers
{
    using Scope.Client.Events.EventArgs;
    using Scope.Client.Events.Extensions;
    using static Scope.Client.Events.Events;

    /// <summary>
    /// Data related events.
    /// </summary>
    public static class Data
    {
        /// <summary>
        /// Invoked before receiving data.
        /// </summary>
        public static event CustomEventHandler<ReceivingDataEventArgs> ReceivingData;

        /// <summary>
        /// Invoked before sending data.
        /// </summary>
        public static event CustomEventHandler<SendingDataEventArgs> SendingData;

        /// <summary>
        /// Called before receiving data.
        /// </summary>
        /// <param name="ev">The <see cref="ReceivingDataEventArgs"/> event.</param>
        public static void OnReceivingData(ReceivingDataEventArgs ev) => ReceivingData.InvokeSafely(ev);

        /// <summary>
        /// Called before sending data.
        /// </summary>
        /// <param name="ev">The <see cref="SendingDataEventArgs"/> event.</param>
        public static void OnSendingData(SendingDataEventArgs ev) => SendingData.InvokeSafely(ev);
    }
}
