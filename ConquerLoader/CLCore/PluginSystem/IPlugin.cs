using CLCore;

namespace ConquerLoader.CLCore
{
    public interface IPlugin
	{
		string Name { get; }
		string Explanation { get; }
		PluginType PaymentPlugin { get; set; }
		void Run();
		void Configure();
	}
}
