using System.Collections.Generic;

namespace ConquerLoader.CLCore
{
	public interface IPlugin
	{
		string Name { get; }
		string Explanation { get; }
		LoadType LoadType { get; }
		void Run();

		List<Parameter> Parameters { get; set; }
	}

	public enum LoadType
	{
		LOADER_EXECUTION,
		ON_FORM_LOAD,
		ON_GAME_START
	}
}
