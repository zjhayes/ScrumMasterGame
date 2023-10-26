namespace StateMachine
{
    using UnityEngine;

    public class StateMachine : MonoBehaviour
    {
        private BaseState currentState;

        private void Update()
        {
            currentState.UpdateStates();
        }

        public BaseState CurrentState 
        { 
            get { return currentState; }
            set { currentState = value; }
        }
    }
}