using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Patterns
{
    [Serializable]
    public class PoolableEvent<T> : UnityEvent<T> {}
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////