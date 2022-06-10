using System.Collections.Generic;
using Architecture.Behaviours;
using Architecture.Starter;
using UnityEngine;

namespace Architecture.Manager
{
    public class ManagerBase : BaseMonoBehaviour, IManager
    {
        public virtual void Init(){ }

        public virtual void Dispose(){ }
    }
}
