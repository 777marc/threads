using System;

using System.Threading;

namespace threads
{
    class Program
    {
        
        static void Main(string[] args)
        {
            DataContext p = new DataContext();
            for (int i = 0; i < 5; i++)
            {
                Thread t1 = new Thread(p.myRead); //Reader Thread
                //Writer Thread
                Thread t2 = new Thread(new ThreadStart(p.myWrite));
                //Reader Again
                Thread t3 = new Thread(p.myRead);

                //Start all threads
                t1.Start("Thread 1");
                t2.Start();
                t3.Start("Thread 3");
                
                //Wait for them to finish execution
                t1.Join();
                t2.Join();
                t3.Join();
            }
            Console.WriteLine("Process Complete!");
            Console.ReadLine();  
        }
    }

    
}
