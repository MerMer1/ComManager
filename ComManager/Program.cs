using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComManager
{
    class Program
    {
        static void Main(string[] args)
        {

            comManager myComManager = new comManager();
            myComManager.init();
            
            byte[] testArray = Encoding.UTF8.GetBytes("XXXXXXXXXXXX");
            A observerA = new A(myComManager.comEntityDict[2], testArray);
            B observerB = new B(myComManager.comEntityDict[3], testArray);

            myComManager.Attach(2, observerA);
            myComManager.Attach(3, observerB);

            byte[] msg1 = Encoding.UTF8.GetBytes("HELLO1");
            byte[] msg2 = Encoding.UTF8.GetBytes("HELLO2");

            myComManager.Send(2, msg1);
            myComManager.Send(3,msg2);



            Console.WriteLine("the new msg in A observer is ==>  " + Encoding.UTF8.GetString(observerA.msg));
            Console.WriteLine("the new msg in B observer is ==>  " + Encoding.UTF8.GetString(observerB.msg));
            
            //Thread.Sleep(1000);
            Console.ReadLine();

            
        }
    }
}
