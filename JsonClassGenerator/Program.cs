using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Xamasoft.JsonClassGenerator.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmCSharpClassGeneration());
        }





#if APPSERVICES
        public static antiufo.ApplicationServices.ApplicationServices appServices;


        public static void InitAppServices()
        {
            if (appServices != null) return;
            appServices = new antiufo.ApplicationServices.ApplicationServices("JsonClassGen", "JSON C# Class Generator", true, true);
            appServices.DefaultEndpoint = "http://antiufo.altervista.org/services/index.php";
            appServices.ProducerUrl = "http://antiufo.altervista.org/";
            appServices.EndpointTag = "<!--Endpoint2 ";
            appServices.UpdateChecker.AsyncAutomaticUpdatesCheck();
        }

#else
        public static void InitAppServices()
        {

        }

#endif 
        




    }
}
