using System.Collections.Generic;
using UnityEngine;

namespace Core.Patterns
{
    [CreateAssetMenu(fileName = "New Observable Event", menuName = "Observables/Observable Event", order = 11)]
    public class ObservableEvent : ScriptableObject
    {
        private List<ObservableEventListener> elisteners = new List<ObservableEventListener>();

        public void Register(ObservableEventListener listener)
        {
            elisteners.Add(listener);
        }

        public void Unregister(ObservableEventListener listener)
        {
            elisteners.Remove(listener);
        }

        public void Occurred(GameObject gameObject)
        {
            for (int i = 0; i < elisteners.Count; i++)
            {
                elisteners[i].OnEventOccurs(gameObject);
            }
        }
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////