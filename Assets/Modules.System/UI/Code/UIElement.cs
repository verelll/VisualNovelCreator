using System.Collections.Generic;
using Architecture.Behaviours;
using Architecture.IOC;
using Architecture.Manager;
using Architecture.Model;

namespace Game.UI
{
    public abstract class UIElement : BaseMonoBehaviour, IModelBehaviour
    {
        public virtual void Init() { }

        public virtual void SetModel(IModel model) { }
        
        protected T GetManager<T>() where T : IManager => GlobalContainer.Main.Get<T>();

        protected List<T> GetAll<T>(List<T> objects = null) => GlobalContainer.Main.GetAll<T>(objects);
    }
}
