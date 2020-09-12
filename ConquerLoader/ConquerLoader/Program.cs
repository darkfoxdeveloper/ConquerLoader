using System;
using System.Windows.Forms;

namespace ConquerLoader
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Core.LoadAvailablePlugins();
            Application.Run(new Main());
        }
    }
}
