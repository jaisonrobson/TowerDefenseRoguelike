using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Core.Patterns
{
	[Serializable]
	public struct PoolableType
	{
		public PoolableTypeEnum type;
		[SceneObjectsOnly]
		[Required]
		public Transform transform;

		public PoolableType(PoolableTypeEnum pType, Transform pTransform)
		{
			type = pType;
			transform = pTransform;
		}
	}
	/// <summary>
	/// Managers a dictionary of pools, getting and returning 
	/// </summary>
	public class PoolManager : Singleton<PoolManager>
	{
		/// <summary>
		/// Every prefab spawned which does not have an specific space will be stored in this transform
		/// </summary>
		[Title("Areas")]
		[PropertyOrder(1)]
		[OnInspectorInit("MaintainPoolableTypes")]
		[ValidateInput("Validate_NotRepeated_PoolableType_Transform", "Poolable Type transform cannot be repeated.")]
		public List<PoolableType> poolableTypes;

		[BoxGroup("Pool Manager", Order = 2)]
		/// <summary>
		/// List of poolables that will be used to initialize corresponding pools
		/// </summary>
		public List<Poolable> poolables;

		/// <summary>
		/// Dictionary of pools, key is the prefab
		/// </summary>
		[ShowInInspector]
		[HideInEditorMode]
		[ReadOnly]
		[BoxGroup("Pool Manager")]
		protected Dictionary<Poolable, AutoComponentPrefabPool<Poolable>> m_Pools;

		public Transform GetPoolableTransformArea(PoolableTypeEnum pte) => poolableTypes.Find(pt => pt.type == pte).transform;

		/// <summary>
		/// Gets a poolable component from the corresponding pool
		/// </summary>
		/// <param name="poolablePrefab"></param>
		/// <returns></returns>
		public Poolable GetPoolable(Poolable poolablePrefab) { return GetPoolable(poolablePrefab, null); }
		public Poolable GetPoolable(Poolable poolablePrefab, Action<Poolable> immediateRetrievalAction)
		{
			if (!m_Pools.ContainsKey(poolablePrefab))
			{
				Action<Poolable> initializationAction = Initialize;

				Action<Poolable> poolRetrievalAction = PoolRetrieval;

				Action<Poolable> poolInsertionAction = PoolInsertion;

				m_Pools.Add(poolablePrefab, new AutoComponentPrefabPool<Poolable>(poolablePrefab, initializationAction, poolRetrievalAction, poolInsertionAction, poolablePrefab.initialPoolCapacity));
			}

			AutoComponentPrefabPool<Poolable> pool = m_Pools[poolablePrefab];

			Poolable spawnedInstance;

			immediateRetrievalAction += PoolRetrieval;

			if (immediateRetrievalAction != null)
				spawnedInstance = pool.Get(immediateRetrievalAction);
			else
				spawnedInstance = pool.Get();

			spawnedInstance.pool = pool;
			return spawnedInstance;
		}

		/// <summary>
		/// Returns the poolable component to its component pool
		/// </summary>
		/// <param name="poolable"></param>
		public void ReturnPoolable(Poolable poolable)
		{
			poolable.pool.Return(poolable);
		}

		/// <summary>
		/// Initializes the dicionary of pools
		/// </summary>
		protected void OnEnable()
		{
			if (m_Pools == null)
				m_Pools = new Dictionary<Poolable, AutoComponentPrefabPool<Poolable>>();

			foreach (var poolable in poolables)
			{
				if (poolable == null)
				{
					continue;
				}

				GetPoolable(poolable);
			}
		}

		void PoolInsertion(Component component)
		{
			Poolable cp = (Poolable)component;

			component.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

			cp.poolInsertionActions.Invoke(cp);
		}

		void PoolRetrieval(Component component)
		{
			Poolable cp = (Poolable)component;

			cp.poolRetrievalActions.Invoke(cp);
		}

		void Initialize(Component component)
		{
			Poolable cp = (Poolable)component;

			component.transform.SetParent(cp.GetParent(), true);

			cp.prefabInitializationActions.Invoke(cp);
		}


		// (Validation) Methods [START]
		private void MaintainPoolableTypes()
		{
			if (poolableTypes == null)
				poolableTypes = new List<PoolableType>();

			PoolableTypeEnum[] values = (PoolableTypeEnum[])Enum.GetValues(typeof(PoolableTypeEnum));

			values.ToList().ForEach(pte =>
			{
				if (!poolableTypes.Any(pt => pt.type == pte))
                {
					Transform poolableTransformArea = GameObject.FindGameObjectWithTag("PA_" + Enum.GetName(typeof(PoolableTypeEnum), pte))?.transform;

					if (poolableTransformArea == null)
						throw new Exception("Did not found tag in any object: "+ "PA_" + Enum.GetName(typeof(PoolableTypeEnum), pte));
					else
						poolableTypes.Add(new PoolableType(pte, poolableTransformArea));
                }
			});
		}
		private bool Validate_NotRepeated_PoolableType_Transform()
		{
			MaintainPoolableTypes();

			if (poolableTypes != null && poolableTypes.Count > 0)
            {
				List<PoolableType> duplicates = (List<PoolableType>)poolableTypes.GroupBy(pt => pt.type).Where(group => group.Count() > 1).SelectMany(group => group).ToList();

				if (duplicates.Count > 0)
					return false;
			}

			return true;
		}
		// (Validation) Methods [END]
	}
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////