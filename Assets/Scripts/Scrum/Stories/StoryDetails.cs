using System.Collections.Generic;
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
    [Range(1, 10)]
    private int storyPoints;
    [SerializeField]
    private StoryRequirements requirements;
    [SerializeField]
    private List<StoryDetails> causes; // Stories this will add to backlog.
    [SerializeField]
    private StoryDetails risk; // Potential defect.

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

    public int StoryPoints
    {
        get { return storyPoints; }
    }

    public StoryRequirements Requirements
    {
        get { return requirements; }
    }

    public List<StoryDetails> Causes
    {
        get { return causes; }
    }

    public StoryDetails Risk
    {
        get { return risk; }
    }
}
