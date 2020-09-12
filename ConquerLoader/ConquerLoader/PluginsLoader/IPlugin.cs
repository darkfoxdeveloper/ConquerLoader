namespace ConquerLoader.PluginsLoader
{
	public interface IPlugin
	{
		string Name { get; }
		string Explanation { get; }
		void Go(string parameters);
	}
}

