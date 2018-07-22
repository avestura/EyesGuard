using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyesGuard.Configurations
{
    public partial class Configuration
    {
        /// <summary>
        /// Name of <see cref="Configuration"/> file
        /// </summary>
        private const string fileName = "App.Config.Xml";

        /// <summary>
        /// Directory to store <see cref="Configuration"/> in it
        /// </summary>
        private static string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "//Aryan Software//Eyes Guard//";

        /// <summary>
        /// Full path of <see cref="Configuration"/> file
        /// </summary>
        private static string path = directory + fileName;

    }
}
