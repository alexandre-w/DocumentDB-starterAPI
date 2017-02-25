using log4net;
using log4net.Config;
using System;
using System.Diagnostics;
using System.IO;

namespace DocumentDB_starterAPI.Common
{
    public class App
    {

        /// <summary>
        /// The current type
        /// </summary>
        private static Type currentType;

        /// <summary>
        /// Initialise les membres statiques de la classe <see cref="App"/>
        /// </summary>
        static App()
        {
            string dossier = AppDomain.CurrentDomain.BaseDirectory;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log4net.config")));
        }


        /// <summary>
        /// Obtient ou définit le type de l'objet courant
        /// </summary>
        /// <value>Type de l'objet courant</value>
        public static Type CurrentType
        {
            get { return App.currentType; }
            set { App.currentType = value; }
        }

        /// <summary>
        /// Logs the specified p_o type.
        /// </summary>
        /// <param name="p_oType">Type of the p_o.</param>
        /// <returns>Current logger</returns>
        public static ILog Log(Type p_oType)
        {
            App.currentType = p_oType;
            return LogManager.GetLogger(p_oType);
        }

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        /// <returns>The current method name</returns>
        public static string GetMethodName()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }

    }
}