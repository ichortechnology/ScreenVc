using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoadInvestorIndustries
{
    /// <summary>
    /// Parses command line parameters.
    /// </summary>
    public class CommandLine
    {
        public static bool TryParse(string[] args, out int start, out int end)
        {
            start = 1;
            end = int.MaxValue;

            if (args.Length == 0)
            {
            }
            else if (args.Length == 1)
            {
                if (!Int32.TryParse(args[0], out start))
                {
                    ShowUsage();
                    return false;
                }
            }
            if (args.Length == 2)
            {
                if (!Int32.TryParse(args[0], out start))
                {
                    ShowUsage();
                    return false;
                }
                if (!Int32.TryParse(args[1], out end))
                {
                    ShowUsage();
                    return false;
                }
            }
            else if (args.Length > 2)
            {
                ShowUsage();
                return false;
            }
            return true;
        }

        static void ShowUsage()
        {
            Console.WriteLine(string.Format(Properties.Settings.Default.Usage, Environment.NewLine));
        }
    }
}
