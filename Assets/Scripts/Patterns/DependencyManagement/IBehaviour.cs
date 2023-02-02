using UnityEngine;

public interface IBehaviour<T> where T : IService
{
    GameObject gameObject { get; }
    public void Inject(T service);
}
