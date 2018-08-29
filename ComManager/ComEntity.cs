using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ComManager
{
    abstract class ComEntity
    {
        public int ComId;
        public abstract bool init();
        public abstract bool Send(int comId, byte[] msg);
        //public abstract bool Receive(int comId, string msg);

        private List<Observer> _observers = new List<Observer>();

        public void Attach(Observer observer)
        {
            _observers.Add(observer);
        }

        public void Detach(Observer observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(byte[] msg)
        {
            foreach (Observer o in _observers)
            {
                o.Update(msg);
            }
        }
        
    }
}
