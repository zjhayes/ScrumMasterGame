using System.Collections;
using UnityEngine;

public interface IController
{
    GameObject gameObject { get; }
    Transform transform { get; }
}