using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Patterns
{
    public class FiniteStateMachine
    {
        public AgentStateEnum name;
        protected FSMEventEnum stage;
        protected FiniteStateMachine nextState;

        public FiniteStateMachine()
        {
            stage = FSMEventEnum.ENTER;
        }

        public virtual void Enter() { stage = FSMEventEnum.UPDATE; }
        public virtual void Update() { stage = FSMEventEnum.UPDATE; }
        public virtual void Exit() { stage = FSMEventEnum.EXIT; }

        public FiniteStateMachine Process()
        {
            if (stage == FSMEventEnum.ENTER) Enter();
            if (stage == FSMEventEnum.UPDATE) Update();
            if (stage == FSMEventEnum.EXIT)
            {
                Exit();

                return nextState;
            }

            return this;
        }
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////