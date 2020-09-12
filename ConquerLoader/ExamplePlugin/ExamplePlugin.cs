using ConquerLoader.PluginsLoader;
using System.IO;

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

		public void Go(string parameters)
		{
			File.WriteAllText("ExamplePlugin.log", parameters);
		}
	}
}
