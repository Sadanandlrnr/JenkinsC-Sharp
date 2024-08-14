using System.Configuration;

namespace Single_Click
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            // Fetch the custom configuration section
            
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}