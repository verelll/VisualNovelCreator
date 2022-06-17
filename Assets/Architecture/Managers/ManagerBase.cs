using System.Collections.Generic;
using Architecture.Behaviours;
using Architecture.IOC;

namespace Architecture.Manager
{
    public class ManagerBase :  IManager
    {
        public virtual void Init(){ }
        public virtual void OnStart(){ }
        public virtual void Dispose(){ }
        
        protected T GetManager<T>() where T : IManager => GlobalContainer.Main.Get<T>();

        protected List<T> GetAll<T>(List<T> objects = null) => GlobalContainer.Main.GetAll<T>(objects);
    }
}
