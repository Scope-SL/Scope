// -----------------------------------------------------------------------
// <copyright file="ClientCollection.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Server.Database.Collections
{
    using System;
    using LiteDB;

    /// <summary>
    /// A tool to handle clients information for database solutions.
    /// </summary>
    internal class ClientCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientCollection"/> class.
        /// </summary>
        /// <param name="rawId">Client's raw id.</param>
        /// <param name="auth">Client's auth.</param>
        /// <param name="username">Client's username.</param>
        /// <param name="ipAddress">Client's IP address.</param>
        /// <param name="hwid">Client's HWID.</param>
        /// <param name="firstJoin">Client's first join.</param>
        /// <param name="lastJoin">Client's last join.</param>
        [BsonCtor]
        internal ClientCollection(string rawId, string auth, string username, string ipAddress, string hwid, DateTime firstJoin, DateTime lastJoin)
        {
            RawId = rawId;
            Auth = auth;
            Username = username;
            IPAddress = ipAddress;
            HWID = hwid;
            FirstJoin = firstJoin;
            LastJoin = lastJoin;
        }

        /// <summary>
        /// Gets the client's raw id.
        /// </summary>
        internal string RawId { get; }

        /// <summary>
        /// Gets the client's auth.
        /// </summary>
        internal string Auth { get; }

        /// <summary>
        /// Gets or sets the client's username.
        /// </summary>
        internal string Username { get; set; }

        /// <summary>
        /// Gets or sets the client's IP address.
        /// </summary>
        internal string IPAddress { get; set; }

        /// <summary>
        /// Gets the client's HWID.
        /// </summary>
        internal string HWID { get; }

        /// <summary>
        /// Gets the client's first join.
        /// </summary>
        internal DateTime FirstJoin { get; }

        /// <summary>
        /// Gets or sets the client's last join.
        /// </summary>
        internal DateTime LastJoin { get; set; }
    }
}
