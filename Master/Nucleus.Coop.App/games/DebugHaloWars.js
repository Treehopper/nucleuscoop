Game.AddOption("Keyboard Player",
    "The player that will be playing on keyboard and mouse (if any)",
    Nucleus.KeyboardPlayer.NoKeyboardPlayer,
    "KeyboardPlayer");
//Game.ExecutableContext = [
//    "binkw32.dll"
//];
//Game.KillMutex = [
//    "SR3"
//];

Game.Debug = true;
Game.SymlinkExe = false;
Game.SymlinkGame = true;
Game.SupportsKeyboard = true;
Game.ExecutableName = "xgamefinal.exe";
Game.SteamID = "459220";
Game.GUID = "459220";
Game.GameName = "Halo Wars";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.BinariesFolder = "";
Game.NeedsSteamEmulation = true;
Game.LauncherTitle = "";
Game.SaveType = Nucleus.SaveType.None;
Game.SupportsPositioning = true;
Game.HideTaskbar = false;
Game.CustomXinput = true;
Game.StartArguments = "";
//Game.HookNeeded = true;
//Game.HookGameWindowName = "Halo Wars: Definitive Edition";
Game.SupportsXInput = true;

Game.Play = function () {
    //Context.ModifySave = [
    //   new Nucleus.IniSaveInfo("", "ResolutionWidth", Context.Width),
    //   new Nucleus.IniSaveInfo("", "ResolutionHeight", Context.Height),
    //   new Nucleus.IniSaveInfo("", "Fullscreen", false),
    //   new Nucleus.IniSaveInfo("", "VerifyResolution", false),
    //   new Nucleus.IniSaveInfo("", "SkipIntroVideo", true),
    //];
    //Context.SavePath = Context.GetFolder(Nucleus.Folder.InstancedGameFolder) + "\\display.ini";
}