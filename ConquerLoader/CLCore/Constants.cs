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
	}
	public enum PluginType
	{
		FREE,
		PREMIUM
	}
}

