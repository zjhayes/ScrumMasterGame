namespace StateMachine
{
    using UnityEngine;

    public class StateFactory : MonoBehaviour
    {
        StateMachine context;

        public StateFactory(StateMachine _context)
        {
            context = _context;
        }
    }

}