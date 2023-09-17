using UnityEngine;
using Sirenix.OdinInspector;

namespace Core.Patterns
{
	/// <summary>
	/// Class that is to be pooled
	/// </summary>
	[HideMonoScript]
	[DisallowMultipleComponent]
	public class Poolable : MonoBehaviour
	{
		[PropertySpace(3f)]
		/// <summary>
		/// Number of poolables the pool will initialize
		/// </summary>
		public int initialPoolCapacity = 10;

		public PoolableTypeEnum poolType;

		[PropertySpace(5f)]
		[Title("Actions")]

		[DisableMultieditSupport]
		public PoolableEvent<Poolable> prefabInitializationActions;

		[DisableMultieditSupport]
		public PoolableEvent<Poolable> poolRetrievalActions;

		[DisableMultieditSupport]
		public PoolableEvent<Poolable> poolInsertionActions;

		/// <summary>
		/// Pool that this poolable belongs to
		/// </summary>
		public Pool<Poolable> pool;

		/// <summary>
		/// Repool this instance, and move us under the poolmanager
		/// </summary>
		protected virtual void Repool()
		{
			transform.SetParent(GetParent(), true);
			pool.Return(this);
		}

		/// <summary>gameObject
		/// Pool the object if possible, otherwise destroy it
		/// </summary>
		/// <param name="gameObject">GameObject attempting to pool</param>
		public static void TryPool(GameObject gameObject)
		{
			var poolable = gameObject.GetComponent<Poolable>();
			if (poolable != null && poolable.pool != null && PoolManager.instanceExists)
			{
				poolable.Repool();
			}
			else
			{
				Destroy(gameObject);
			}
		}

		/// <summary>
		/// If the prefab is poolable returns a pooled object otherwise instantiates a new object
		/// </summary>
		/// <param name="prefab">Prefab of object required</param>
		/// <typeparam name="T">Component type</typeparam>
		/// <returns>The pooled or instantiated component</returns>
		public static T TryGetPoolable<T>(GameObject prefab) where T : Component
		{
			var poolable = prefab.GetComponent<Poolable>();
			T instance = poolable != null && PoolManager.instanceExists ? 
				PoolManager.instance.GetPoolable(poolable).GetComponent<T>() : Instantiate(prefab).GetComponent<T>();
			return instance;
		}

		/// <summary>
		/// If the prefab is poolable returns a pooled object otherwise instantiates a new object
		/// </summary>
		/// <param name="prefab">Prefab of object required</param>
		/// <returns>The pooled or instantiated gameObject</returns>
		public static GameObject TryGetPoolable(GameObject prefab) { return TryGetPoolable(prefab, null); }
		public static GameObject TryGetPoolable(GameObject prefab, System.Action<Poolable> immediateRetrievalAction)
		{
			var poolable = prefab.GetComponent<Poolable>();
			GameObject instance = poolable != null && PoolManager.instanceExists ? 
				PoolManager.instance.GetPoolable(poolable, immediateRetrievalAction).gameObject : Instantiate(prefab);
			return instance;
		}

		public static Transform GetParentByType(PoolableTypeEnum type) =>
			PoolManager.instance.GetPoolableTransformArea(type);

		public Transform GetParent() { return GetParentByType(poolType); }
	}
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////