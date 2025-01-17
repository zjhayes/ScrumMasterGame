namespace HierarchicalStateMachine
{
    using System.Collections.Generic;
    using System;

    public abstract class StateMachineContext<TState, TStateEnum> : IStateMachine<TState>
        where TState : BaseState<TState>
        where TStateEnum : Enum
    {
        private Dictionary<TStateEnum, TState> states;
        private TStateEnum defaultState;
        private TState currentState;

        public delegate void StateMachineEvent();
        public event StateMachineEvent OnTransition;

        protected StateMachineContext(Dictionary<TStateEnum, TState> initialStateDictionary, TStateEnum defaultState)
        {
            states = initialStateDictionary;
            this.defaultState = defaultState;
        }

        public void Start()
        {
            TransitionTo(defaultState); // Enter default state.
        }

        public TState CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        public TState GetState(TStateEnum stateEnum)
        {
            if (TryGetState(stateEnum, out TState state))
            {
                return state;
            }
            else
            {
                throw new StateNotFoundException();
            }
        }

        public bool TryGetState(TStateEnum stateEnum, out TState state)
        {
            return states.TryGetValue(stateEnum, out state);
        }

        public void TransitionTo(TStateEnum nextStateEnum)
        {
            if (TryGetState(nextStateEnum, out TState nextState))
            {
                SwitchState(nextState);
                OnTransition?.Invoke();
            }
            else
            {
                throw new StateNotFoundException();
            }
        }
        
        private void SwitchState(TState newState)
        {
            CurrentState?.Exit(); // Exit current state.
            if (CurrentState == null || CurrentState.SuperState == null)
            {
                CurrentState = newState;
            }
            else
            {
                CurrentState.SuperState.SetSubState(newState);
            }
            newState.Enter();
        }
    }
}