using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField]
    List<CharacterController> characters;

    public List<CharacterController> Characters
    {
        get { return characters; }
    }
}
