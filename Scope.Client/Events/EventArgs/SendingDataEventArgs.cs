// -----------------------------------------------------------------------
// <copyright file="SendingDataEventArgs.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Events.EventArgs
{
    using Scope.Client.API.Features;

    /// <summary>
    /// Contains all informations before sending data.
    /// </summary>
    public class SendingDataEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendingDataEventArgs"/> class.
        /// </summary>
        /// <param name="data">The <see cref="TransmissionNetworkObject"/>.</param>
        /// <param name="isAllowed">Indicates wheter the event can be executed or not.</param>
        public SendingDataEventArgs(TransmissionNetworkObject data, bool isAllowed = true)
        {
            Data = data;
            IsAllowed = isAllowed;
        }

        /// <summary>
        /// Gets the <see cref="TransmissionNetworkObject"/>.
        /// </summary>
        public TransmissionNetworkObject Data { get; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the data can be sent.
        /// </summary>
        public bool IsAllowed { get; set; }
    }
}
