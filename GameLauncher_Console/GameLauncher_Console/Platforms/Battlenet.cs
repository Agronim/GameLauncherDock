﻿using GameCollector.StoreHandlers.BattleNet;
using GameFinder.Common;
using GameFinder.RegistryUtils;
using Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Versioning;
using static GameLauncher_Console.CGameData;
using FileSystem = NexusMods.Paths.FileSystem;

namespace GameLauncher_Console
{
    // Battle.net (Blizzard)
    // [installed games only]
    public class PlatformBattlenet : IPlatform
    {
        public const GamePlatform ENUM			= GamePlatform.Battlenet;
        public const string PROTOCOL			= "battlenet://";  // "blizzard://" works too [TODO: is one more compatible with older versions?]
        public const string UNINST_GAME			= @"Battle.net\Agent\Blizzard Uninstaller.exe"; // ProgramData
        private static readonly string _name = Enum.GetName(typeof(GamePlatform), ENUM);

        GamePlatform IPlatform.Enum => ENUM;

        string IPlatform.Name => _name;

        string IPlatform.Description => GetPlatformString(ENUM);

        public static void Launch()
        {
            if (OperatingSystem.IsWindows())
                _ = CDock.StartShellExecute(PROTOCOL);
            else
                _ = Process.Start(PROTOCOL);
        }

        // return value
        // -1 = not implemented
        // 0 = failure
        // 1 = success
        public static int InstallGame(CGame game)
        {
            //CDock.DeleteCustomImage(game.Title, justBackups: false);
            Launch();
            return -1;
        }

        public static void StartGame(CGame game)
        {
            CLogger.LogInfo($"Launch: {game.Launch}");
            if (OperatingSystem.IsWindows())
                _ = CDock.StartShellExecute(game.Launch);
            else
                _ = Process.Start(game.Launch);
        }

        [SupportedOSPlatform("windows")]
        public void GetGames(List<ImportGameData> gameDataList, Settings settings, bool expensiveIcons = false)
        {
            string strPlatform = GetPlatformString(ENUM);

            BattleNetHandler handler = new(FileSystem.Shared, WindowsRegistry.Shared);
            foreach (var game in handler.FindAllGames(settings))
            {
                if (game.IsT0)
                {
                    CLogger.LogDebug("* " + game.AsT0.GameName);
                    gameDataList.Add(new ImportGameData(strPlatform, game.AsT0));
                }
                else
                    CLogger.LogWarn(game.AsT1.Message);
            }

            CLogger.LogDebug("--------------------------");
        }

        public static string GetIconUrl(CGame _) => throw new NotImplementedException();

        /// <summary>
        /// Scan the key name and extract the Battlenet game id
        /// </summary>
        /// <param name="key">The game string</param>
        /// <returns>Battlenet game ID as string</returns>
        public static string GetGameID(string key)
        {
            if (key.StartsWith("battlenet_"))
                return key[10..];
            return key;
        }
    }
}