﻿using System.Windows.Forms;

namespace CLCore
{
	public static class Constants
	{
		//The folder name which contains the plugin DLLs
		public const string PluginsFolderName = "Plugins";
		public static string LicenseKey = null;
		public static string ClientPath = null;
		public static System.ComponentModel.BackgroundWorker MainWorker = null;
		public static bool CloseOnFinish = false;
		public static bool HideInTrayOnFinish = false;
		public static int MinVersionUseServerDat = 5717;
		public static int MaxVersionUseServerDat = 6736;
		public static string LockConfigurationKey = "CONQUERLOADERDFX";
		public static int MinVersionUseDX8DX9Folders = 6371;
		public static bool EnableCLServerConnections = true; // manual de momento
    }
	public static class CLTheme
	{
        public static Control.ControlCollection MainControls = null;
    }
	public enum PluginType
	{
		FREE,
		PREMIUM
	}
}

