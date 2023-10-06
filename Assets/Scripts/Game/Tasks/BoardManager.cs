using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private List<Story> stories;

    public event Events.GameEvent OnBoardUpdated;

    public StorySearchService Stories
    {
        get { return StorySearchService.Search(stories); }
    }

    public void ImportStoryDetails(List<StoryDetails> storyDetails)
    {
        stories = new List<Story>();

        foreach (StoryDetails storyDetail in storyDetails)
        {
            Story story = new Story(storyDetail, StoryStatus.BACKLOG);
            stories.Add(story);
        }
    }

    public void RemoveStoriesWithStatus(StoryStatus status)
    {
        List<Story> storiesToRemove = Stories.WithStatus(status).Get();
        stories.RemoveAll(story => storiesToRemove.Contains(story));
    }
}
