// -----------------------------------------------------------------------
// <copyright file="Database.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Server.Database
{
    using System;
    using System.IO;
    using LiteDB;
    using Scope.Client.API.Features;

    /// <summary>
    /// Database system based on LiteDB.
    /// </summary>
    internal static class Database
    {
        /// <summary>
        /// Gets or sets an instance of Database.
        /// </summary>
        internal static LiteDatabase LiteDatabase { get; set; }

        /// <summary>
        /// Gets or sets the clients collection.
        /// </summary>
        internal static ILiteCollection<Collections.ClientCollection> ClientCollection { get; set; }

        /// <summary>
        /// Gets the folder path.
        /// </summary>
        internal static string Folder => Environment.CurrentDirectory;

        /// <summary>
        /// Gets the database path.
        /// </summary>
        internal static string FullPath => Path.Combine(Folder, "database.db");

        /// <summary>
        /// Opens the database.
        /// </summary>
        internal static void OpenDatabase()
        {
            try
            {
                if (!Directory.Exists(Folder))
                {
                    Directory.CreateDirectory(Folder);
                }

                LiteDatabase = new LiteDatabase(FullPath);
                ClientCollection = LiteDatabase.GetCollection<Collections.ClientCollection>();

                ClientCollection.EnsureIndex(p => p.RawId, true);
                ClientCollection.EnsureIndex(p => p.HWID, true);

                Log.Info("Database loaded");
            } catch (Exception e)
            {
                Log.Error($"Error whilst trying to load the database:\n {e.StackTrace}\n {e.Message}");
            }
        }

        /// <summary>
        /// Closes the database.
        /// </summary>
        internal static void CloseDatabase()
        {
            try
            {
                LiteDatabase.Checkpoint();
                LiteDatabase.Dispose();
                LiteDatabase = null;

                Log.Info("Database closed");
            } catch (Exception e)
            {
                Log.Error($"Error whilst trying to close the database:\n {e.StackTrace}\n {e.Message}");
            }
        }
    }
}
