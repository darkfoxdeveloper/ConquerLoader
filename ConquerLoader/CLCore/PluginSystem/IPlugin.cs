using System.Collections.Generic;

namespace ConquerLoader.CLCore
{
	public interface IPlugin
	{
		string Name { get; }
		string Explanation { get; }
		void Run();
	}
}
