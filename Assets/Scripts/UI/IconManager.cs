using System.Collections.Generic;
using UnityEngine;

public class IconManager : MonoBehaviour
{
    [SerializeField]
    private Sprite task;
    [SerializeField]
    private Sprite bug;
    [SerializeField]
    private Sprite request;

    private Dictionary<StoryType, Sprite> storyIcons;

    private void Awake()
    {
        // Cache relationship between story types and icons.
        storyIcons = new Dictionary<StoryType, Sprite>
        {
            { StoryType.TASK, task },
            { StoryType.BUG, bug },
            { StoryType.REQUEST, request }
        };
    }

    public Sprite GetIconForStoryType(StoryType type)
    {
        return storyIcons.TryGetValue(type, out var icon) ? icon : null;
    }
}
