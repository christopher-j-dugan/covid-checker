namespace CovidMonitor
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    internal class Program
    {
        private static readonly int _pingIntervalInSecs = 5;

        private static readonly List<IStore> _stores = new()
        {
            new RiteAid()
            //new CVS()
        };

        private static void Main(string[] args)
        {
            while (true)
            {
                var availableAppts = CheckAllStores();

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("--------------------");
                Console.WriteLine($"Time: {DateTime.Now}");

                if (availableAppts.Count > 0)
                    for (var i = 0; i < 20; i++)
                    {
                        // Purposefully annoying beep to alert you of an appt
                        Thread.Sleep(100);
                        Console.Beep();
                    }
                else
                    Console.WriteLine("** No available appointments **");

                Console.WriteLine("--------------------");
                Console.WriteLine(Environment.NewLine);

                Thread.Sleep(_pingIntervalInSecs * 1000);
            }
        }

        private static List<string> CheckAllStores()
        {
            var availableAppts = new List<string>();

            foreach (var store in _stores)
            {
                var apptsForThisStore = store.CheckAllLocations();
                if (apptsForThisStore == null) continue;

                foreach (var appt in apptsForThisStore) availableAppts.Add($"{store.Name} - {appt}");
            }

            availableAppts.ForEach(appt => Console.WriteLine(appt));
            return availableAppts;
        }
    }
}