using System.Collections.Generic;
using UnityEngine;

public class StoryService : MonoBehaviour
{
    public static int CountStoryPoints(List<Story> stories)
    {
        int total = 0;
        foreach (Story story in stories)
        {
            total += story.Details.StoryPoints;
        }
        return total;
    }
}
