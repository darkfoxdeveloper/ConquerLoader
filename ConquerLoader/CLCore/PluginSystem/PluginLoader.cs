using CLCore;
using CLCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace ConquerLoader.CLCore
{
    public class PluginLoader
	{
		public static List<IPlugin> Plugins { get; set; }

		public void LoadPlugins()
		{
			Plugins = new List<IPlugin>();

			//Load the DLLs from the Plugins directory
			if (Directory.Exists(Constants.PluginsFolderName))
			{
				string[] files = Directory.GetFiles(Constants.PluginsFolderName);
				foreach (string file in files)
				{
					if (file.EndsWith(".dll"))
					{
						Assembly.LoadFile(Path.GetFullPath(file));
					}
				}
			}

			Type interfaceType = typeof(IPlugin);
			//Fetch all types that implement the interface IPlugin and are a class
			Type[] types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(a => a.GetTypes())
				.Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass)
				.ToArray();
			foreach (Type type in types)
			{
				//Create a new instance of all found types
				Plugins.Add((IPlugin)Activator.CreateInstance(type));
			}
		}

		public int LoadPluginsFromAPI(LoaderConfig LoaderConfig)
		{
			int Loaded = 0;
			Plugins = new List<IPlugin>();

			HttpClient client = new HttpClient();
			HttpResponseMessage response = client.GetAsync("https://conquerloader.com/api/v1/167635d839c027b0c965cfa0f995dc43$/GetUserModules/" + LoaderConfig.LicenseKey).Result;
			if (response.IsSuccessStatusCode)
			{
				string result = response.Content.ReadAsStringAsync().Result;
				List<APIRemoteModule> apiRemoteModules = Newtonsoft.Json.JsonConvert.DeserializeObject<List<APIRemoteModule>>(result);
				foreach(APIRemoteModule m in apiRemoteModules)
				{
					Assembly.Load(m.Content);
					Loaded++;
				}
			}

			Type interfaceType = typeof(IPlugin);
			//Fetch all types that implement the interface IPlugin and are a class
			Type[] types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(a => a.GetTypes())
				.Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass)
				.ToArray();
			foreach (Type type in types)
			{
				//Create a new instance of all found types
				Plugins.Add((IPlugin)Activator.CreateInstance(type));
			}
			return Loaded;
		}

		protected class APIRemoteModule
		{
			public string Filename { get; set; }
			public byte[] Content { get; set; }
		}
	}
}

