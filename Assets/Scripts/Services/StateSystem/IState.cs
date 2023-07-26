using UnityEngine;

public interface IState<T> where T : IController
{
    void Handle(T controller);
    void Exit();
}