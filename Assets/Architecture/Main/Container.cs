using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.Manager;

namespace Architecture.IOC
{
    public class Container
    {
		private  Dictionary<Type, IManager> _managers = new Dictionary<Type, IManager>();

		public List<IManager>   ManagerObjects => _managers.Values.ToList();

		#region Add

		public T Add<T>() where T : IManager, new()
		{
			var type = typeof(T);
			var manager = new T();
			_managers.Add(type, manager);
			return manager;
		}
        
		#endregion

		
		#region Init
		
		public void Init()
		{
			foreach (var pair in _managers)
				pair.Value.Init();
		}
		
		#endregion
		
		
		#region Dispose
		
		public void Dispose()
		{
			foreach (var pair in _managers)
				pair.Value.Dispose();
		}
		
		#endregion
		
		
		#region Get

		public T Get<T>() where T : IManager
		{
			var type = typeof(T);
			if (!_managers.TryGetValue(type, out var manager))
				throw new Exception($"[Game]. Manager [{type}] not found");
               
			return (T) manager;
		}

		public List<A> GetAll<A>(List<A> objects = null)
		{
			Type target = typeof(A);
            
			if (objects == null)
				objects = new List<A>();
            
			foreach (var manager in _managers.Values)
			{
				if (manager.GetType().GetInterfaces().Contains(target))
				{
					objects.Add((A) manager);
				}
			}

			return objects;
		}

		#endregion
    }
}
