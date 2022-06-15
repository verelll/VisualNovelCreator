using Architecture.Behaviours;

namespace Architecture.Manager
{
    public class ManagerBase : BaseMonoBehaviour, IManager
    {
        public virtual void Init(){ }
        public virtual void OnStart(){ }
    }
}
