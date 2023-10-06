using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sprint", menuName = "Game/Sprint Details")]
public class SprintDetails : ScriptableObject
{
    [SerializeField]
    private List<StoryDetails> stories;

    public List<StoryDetails> Stories
    {
        get { return stories; }
    }
}
