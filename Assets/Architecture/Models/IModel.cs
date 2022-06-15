using System;

namespace Architecture.Model
{
    public interface IModel
    {
        event Action OnChanged;
		
        void InvokeChanged();
    }
	
    public abstract class BaseModel : IModel
    {
        public event Action OnChanged;
		
        public void InvokeChanged() => OnChanged?.Invoke();
    }
}
