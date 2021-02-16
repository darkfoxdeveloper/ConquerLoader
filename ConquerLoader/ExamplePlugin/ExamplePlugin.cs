using CLCore;
using ConquerLoader.CLCore;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ConquerLoader.Plugins
{
    public class ExamplePlugin : IPlugin
	{
		public string Explanation
		{
			get
			{
				return "This is a example plugin. Develop your own plugin if you need.";
			}
		}

		public string Name
		{
			get
			{
				return "ExamplePlugin";
			}
		}

		public PluginType PluginType { get; } = PluginType.FREE;

		public void Init()
		{
			LoaderEvents.LauncherLoaded += LoaderEvents_LauncherLoaded;
			LoaderEvents.ConquerLaunched += LoaderEvents_ConquerLaunched;
			LoaderEvents.LauncherExit += LoaderEvents_LauncherExit;
		}

		public void Configure()
		{
			MessageBox.Show($"Its a example plugin, not require configuration.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void LoaderEvents_LauncherLoaded()
		{
			File.WriteAllText("ExamplePlugin_LauncherLoaded.log", "This is a example plugin. This text is writted in the file by Run Method of the plugin. => Writted in LauncherLoaded Event!");
		}

		private void LoaderEvents_ConquerLaunched(List<Parameter> parameters)
		{
			string ParametersTxt = "";
			foreach (Parameter par in parameters)
            {
				ParametersTxt += $"Id={par.Id};Value={par.Value}";
            }
			File.WriteAllText("ExamplePlugin_ConquerLaunched.log", "This is a example plugin. This text is writted in the file by Run Method of the plugin. => Writted in ConquerLaunched Event! Parameters: " + ParametersTxt);
		}

		private void LoaderEvents_LauncherExit(List<Parameter> parameters)
		{
			string ParametersTxt = "";
			foreach (Parameter par in parameters)
			{
				ParametersTxt += $"Id={par.Id};Value={par.Value}";
			}
			File.WriteAllText("ExamplePlugin_LauncherExit.log", "This is a example plugin. This text is writted in the file by Run Method of the plugin. => Writted in LauncherExit Event! Parameters: " + ParametersTxt);
		}
	}
}
