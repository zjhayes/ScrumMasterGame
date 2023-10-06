using System.Collections.Generic;
using System.Linq;

public class StorySearchService
{
    List<Story> stories;

    private StorySearchService(List<Story> stories)
    {
        this.stories = stories;
    }

    public static StorySearchService Search(List<Story> stories)
    {
        return new StorySearchService(stories);
    }

    public List<Story> Get()
    {
        return stories;
    }

    public bool TryGetFirst(out Story outStory)
    {
        outStory = stories.FirstOrDefault();
        return outStory != null;
    }

    public StorySearchService OfType(StoryType type)
    {
        stories = stories.Where(story => story.Details.Type == type).ToList();
        return this;
    }

    public StorySearchService WithStatus(params StoryStatus[] statuses)
    {
        stories = stories.Where(story => statuses.Contains(story.Status)).ToList();
        return this;
    }

    public StorySearchService AssignedTo(ICharacterController assignee)
    {
        stories = stories.Where(story => story.Assignee == assignee).ToList();
        return this;
    }
}
