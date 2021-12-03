using System.IO;
using Il2CppSystem.Reflection;

namespace Scope.Client.Loader
{
    public class Paths
    {
        public static string ModsDirectory { get; set; }
        public static string ModsDependenciesDirectory { get; set; }
 
        internal static void LoadPaths()
        {
            ModsDirectory = Assembly.GetCallingAssembly().Location + "\\..\\..\\..\\Mods";
            ModsDependenciesDirectory = Path.Combine(ModsDirectory, "dependencies");
        } 
    }
}