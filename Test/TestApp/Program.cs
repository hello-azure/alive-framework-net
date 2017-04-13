using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TestApp
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
            Application.Run(new Form1());
            Func<string, string> aa = (x) => { return x; };
            List<string> list = new List<string>();

            Predicate<int> p = (c) => { return c > 1; };
           

        }
    }
}
