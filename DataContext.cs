using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace threads
{
    class DataContext
    {
        ReaderWriterLock rwl = new ReaderWriterLock();
        public void myRead(object threadName)
        {
            Thread.Sleep(120);

            //Accquire Reader Lock.
            rwl.AcquireReaderLock(Timeout.Infinite);
            string line;
            using (StreamReader reader = new StreamReader("important.txt"))
            {
                line = reader.ReadToEnd();
            }
            Console.WriteLine("myRead: {0} thread: {1}",line, threadName);
            rwl.ReleaseReaderLock();
            //Release Lock
        }
        public void myWrite()
        {
            rwl.AcquireWriterLock(Timeout.Infinite);
            Console.WriteLine("\nWriter start");

            Thread.Sleep(300);

            var prr = new PRR
            {
                Email = "testemail@email.com",
                DateReceived = DateTime.Now
            };
            
            using (StreamWriter file = File.CreateText("important.txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, prr);
            }

            Console.WriteLine("Writer End");

            rwl.ReleaseWriterLock();
        }
    }   
}