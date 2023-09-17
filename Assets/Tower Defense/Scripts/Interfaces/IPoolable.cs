using UnityEngine;
using Core.Patterns;

public interface IPoolable
{
    public abstract void PoolRetrievalAction(Poolable poolable);
    public abstract void PoolInsertionAction(Poolable poolable);
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////