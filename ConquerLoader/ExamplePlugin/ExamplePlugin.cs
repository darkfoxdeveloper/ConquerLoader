using ConquerLoader.CLCore;
using System.Collections.Generic;
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

		public LoadType LoadType
		{
			get
			{
				return LoadType.LOADER_EXECUTION;
			}
		}
		public List<Parameter> Parameters { get; set; }

		public void Run()
		{
			File.WriteAllText("ExamplePlugin.log", "This is a example plugin. This text is writted in the file by Run Method of the plugin.");
		}
	}
}
