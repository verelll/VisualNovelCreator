using Architecture.Behaviours;
using Architecture.Model;

namespace Game.UI
{
    public abstract class UIElement : BaseMonoBehaviour, IModelBehaviour
    {
        public virtual void Init() { }

        public virtual void SetModel(IModel model) { }
    }
}
