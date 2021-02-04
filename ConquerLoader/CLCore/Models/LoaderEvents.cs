using ConquerLoader.CLCore;
using System.Collections.Generic;

namespace CLCore
{
    public class LoaderEvents
    {
        public delegate void OnLauncherLoaded();
        public delegate void OnLauncherExit(List<Parameter> parameters);
        public delegate void OnConquerLaunched(List<Parameter> parameters);
        public static event OnLauncherLoaded LauncherLoaded;
        public static event OnConquerLaunched ConquerLaunched;
        public static event OnLauncherExit LauncherExit;

        public static void LauncherLoadedStartEvent()
        {
            LauncherLoaded?.Invoke();
        }

        public static void ConquerLaunchedStartEvent(List<Parameter> parameters)
        {
            ConquerLaunched?.Invoke(parameters);
        }

        public static void LauncherExitStartEvent(List<Parameter> parameters)
        {
            LauncherExit?.Invoke(parameters);
        }
    }
}
