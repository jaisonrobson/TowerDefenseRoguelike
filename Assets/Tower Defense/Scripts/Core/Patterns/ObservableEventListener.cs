using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Core.Patterns
{
    [System.Serializable]
    public class UnityAnyObjectEvent<T> : UnityEvent<T> { }

    [HideMonoScript]
    public class ObservableEventListener : MonoBehaviour
    {
        public ObservableEvent oEvent;
        public UnityAnyObjectEvent<GameObject> response = new UnityAnyObjectEvent<GameObject>();

        protected virtual void OnEnable()
        {
            oEvent.Register(this);
        }

        protected virtual void OnDisable()
        {
            oEvent.Unregister(this);
        }

        public virtual void OnEventOccurs(GameObject gameObject)
        {
            response.Invoke(gameObject);
        }
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////