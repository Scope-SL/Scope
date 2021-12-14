using System;
using System.Collections.Generic;
using System.IO;
using LiteDB;
using Scope.Client.API.Features;
using Scope.Server.Database.Collections;

namespace Scope.Server.Database
{
    public class Database
    {
        public static LiteDatabase LiteDatabase { get; private set; }
        public static ILiteCollection<Player> PlayerCollection { get; private set; }
        public static Dictionary<Client.API.Features.Client, Player> PlayerData { get; } = new Dictionary<Client.API.Features.Client, Player>();
        public static string Folder => "Server folder";
        public static string FullPath = Path.Combine(Folder, "database.db");
        
        public static void Open()
        {
            try
            {
                if (!Directory.Exists(Folder)) Directory.CreateDirectory(Folder);

                LiteDatabase = new LiteDatabase(FullPath);
                PlayerCollection = LiteDatabase.GetCollection<Player>();

                PlayerCollection.EnsureIndex(p => p.Id, true);
                PlayerCollection.EnsureIndex(p => p.Hwid);

                Log.Info("Database Loaded!");
            }
            catch (Exception e)
            {
                Log.Error($"Error when try to open database:\n {e}");
            }
        }

        public static void Close()
        {
            try
            {
                LiteDatabase.Checkpoint();
                LiteDatabase.Dispose();
                LiteDatabase = null;

                Log.Info("Database closed!");
            }
            catch (Exception e)
            {
                Log.Error($"Error when try to close database:\n {e}");
            }
        }
    }
}