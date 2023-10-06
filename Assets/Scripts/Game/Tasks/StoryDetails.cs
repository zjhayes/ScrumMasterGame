using UnityEngine;

[CreateAssetMenu(fileName = "New Story", menuName = "Game/Story Details")]
public class StoryDetails : ScriptableObject
{
    [SerializeField]
    private string summary;
    [SerializeField]
    [TextArea(3, 10)]
    private string description;
    [SerializeField]
    private StoryType type;
    [SerializeField]
    private StoryRequirements requirements;

    public string Summary
    {
        get { return summary; }
    }

    public string Description
    {
        get { return description; }
    }

    public StoryType Type
    {
        get { return type; }
    }

    public StoryRequirements Requirements
    {
        get { return requirements; }
    }
}
