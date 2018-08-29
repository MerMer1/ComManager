using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComManager
{
    class ConcreteObservers
    {
        
    }
    class A : Observer
    {
        private ComEntity comEntity;
        public byte[] msg;
        private string _name = "A";
        private comManager comManager;
        private string p;

        public A(ComEntity subject, byte[] _msg)
        {
            this.comEntity = subject;
            this.msg = _msg;
        }

        public override void Update(byte[] data)
        {
            msg = data;
            Console.WriteLine("Observer {0}'s new state is {1}", _name, Encoding.UTF8.GetString(msg));
        }

    }
    class B : Observer
    {
        private ComEntity comEntity;
        public byte[] msg;
        private string _name = "B";
        private comManager comManager;
        private string p;

        public B(ComEntity subject, byte[] _msg)
        {
            this.comEntity = subject;
            this.msg = _msg;
        }

        public override void Update(byte[] data)
        {
            msg = data;
            Console.WriteLine("Observer {0}'s new state is {1}", _name, Encoding.UTF8.GetString(msg));
        }
    }


    //class C : Observer
    //{

    //}
}
