using System;
using System.IO;

namespace SleepData
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = "data.txt";

            // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();
            
            if (resp == "1")
            {
                // create data file

                 // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                int weeks = int.Parse(Console.ReadLine());
                
                // determine start and end date
                DateTime today = DateTime.Now;
                // we want full weeks sunday - saturday
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                // subtract # of weeks from endDate to get startDate
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
                Console.WriteLine(dataDate);

                // random number generator
                Random rnd = new Random();

                // create file
                StreamWriter sw = new StreamWriter(file);

                // loop for the desired # of weeks
                while (dataDate < dataEndDate)
                {
                    // 7 days in a week
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++)
                    {
                        // generate random number of hours slept between 4-12 (inclusive)
                        hours[i] = rnd.Next(4, 13);
                    }
                    // M/d/yyyy,#|#|#|#|#|#|#
                    //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                    // add 1 week to date
                    dataDate = dataDate.AddDays(7);
                }
                sw.Close();
            }
            else if (resp == "2")
            {
                // TODO: parse data file
                if (File.Exists(file))
                {
                    StreamReader sr = new StreamReader(file);
                    while (!sr.EndOfStream){
                        // Splits up data from file
                        string line = sr.ReadLine();
                        DateTime date = DateTime.Parse(line.Split(',')[0]);

                        string nights = line.Split(',')[1];
                        String[] hours = nights.Split('|');

                        int total = 0;
                        for(int i = 0; i < 7; i++){
                            total += int.Parse(nights.Split('|')[i]);
                        }
                        double average = total / 7;

                        Console.WriteLine($"Week of {line.Split(',')[0]:MMM, dd, yyyy}");
                        Console.WriteLine("Mo Tu We Th Fr Sa Su Tot Avg");
                        Console.WriteLine("-- -- -- -- -- -- -- --- ---");
                        Console.WriteLine($"{hours[0],2}{hours[1],3}{hours[2],3}{hours[3],3}{hours[4],3}{hours[5],3}{hours[6],3}{total,4}{average,4}");
                        Console.WriteLine(" ");
                    }
                    sr.Close();
                }
            }
        }
    }
}
