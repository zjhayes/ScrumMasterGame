using UnityEngine;

public static class ServiceInjector 
{
    public static void Resolve<S, B>(S dependency)
            where S : IService
            where B : Component, IBehaviour<S>
    {
        B[] dependents = Resources.FindObjectsOfTypeAll<B>();
        // Inject service into each dependent behaviour.
        foreach(B behaviour in dependents)
        {
            behaviour.Inject(dependency);
        }
    }
}
