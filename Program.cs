using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeRest2020
{
    class Program
    {
        static void Main(string[] args)
        {
            //Worker worker = new Worker();
            //worker.Start();
            Customer customer=new Customer();
            customer.Start();
            Console.ReadLine();
        }
    }
}
