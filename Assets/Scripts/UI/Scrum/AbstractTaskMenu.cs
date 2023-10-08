using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTaskMenu : MenuController
{
    [SerializeField]
    protected TaskDetailsPanel taskDetailsPanel;
    [SerializeField]
    protected GameObject taskPanelPrefab;

    protected Dictionary<Story, StoryPanel> storyPanelCache;

    protected abstract void HandleLoadingStoryPanel(Story story);

    public override void SetUp()
    {
        // Set up sub-panels.
        taskDetailsPanel.SetUp();
        taskDetailsPanel.Hide(); // Hidden by default.
        taskDetailsPanel.OnHide += OnHideStoryDetails;
    }

    public override void Show()
    {
        // Add tasks to board.
        LoadStoryPanels();
        base.Show();
    }

    public override void Hide()
    {
        if (taskDetailsPanel.IsShowing)
        {
            taskDetailsPanel.Hide();
        }
        ClearBoard();
        base.Hide();
    }

    public override void Escape()
    {
        if (taskDetailsPanel.IsShowing)
        {
            taskDetailsPanel.Escape();
        }
        else
        {
            base.Escape();
        }
    }

    protected void OnStoryPanelSelected(StoryPanel storyPanel)
    {
        ShowPreviouslySelectedStoryPanel(); // Show previously selected task, if any.
        // Replace task panel with task details panel.
        MoveStoryDetailsPanelToStoryPanel(storyPanel);
        storyPanel.Hide();
        taskDetailsPanel.Show(storyPanel.Story);
    }

    protected void OnHideStoryDetails(MenuController storyDetails)
    {
        // Show hidden task panel.
        ShowPreviouslySelectedStoryPanel();
    }

    protected void LoadStoryPanels()
    {
        // Clear existing task panels.
        ClearBoard();
        // Add tasks to board.
        storyPanelCache = new Dictionary<Story, StoryPanel>();
        foreach (Story story in gameManager.Board.Stories.Get())
        {
            HandleLoadingStoryPanel(story);
        }
    }

    protected StoryPanel CreateTaskPanel(Story story, Transform parent)
    {
        GameObject storyPanelGameObject = BehaviourBuilder.Create(taskPanelPrefab)
            .WithParent(parent)
            .WithPosition(parent.position)
            .WithRotation(parent.rotation)
            .Build<StoryPanel, IGameManager>(gameManager);
        StoryPanel storyPanel = storyPanelGameObject.GetComponent<StoryPanel>();
        storyPanel.Story = story;
        storyPanel.SetUp();
        
        return storyPanel;
    }

    protected void MoveStoryDetailsPanelToStoryPanel(StoryPanel storyPanel)
    {
        taskDetailsPanel.gameObject.transform.SetParent(storyPanel.gameObject.transform.parent);
        taskDetailsPanel.gameObject.transform.SetSiblingIndex(storyPanel.gameObject.transform.GetSiblingIndex());
    }

    protected void ShowPreviouslySelectedStoryPanel()
    {
        // Check if there's a selected task, then show it.
        if (taskDetailsPanel.Story != null && storyPanelCache.ContainsKey(taskDetailsPanel.Story))
        {
            // Get and show task panel for current task in task details panel.
            StoryPanel selectedTaskPanel = storyPanelCache[taskDetailsPanel.Story];
            selectedTaskPanel.Show();
        }
    }

    protected void ClearBoard()
    {
        if(storyPanelCache == null) { return; } // Nothing to clear.

        foreach (KeyValuePair<Story, StoryPanel> taskPanelPair in storyPanelCache)
        {
            // Destroy task panel.
            Destroy(taskPanelPair.Value.gameObject);
        }
        storyPanelCache = null;
    }
    
    protected StoryPanel GetPanelForStory(Story story)
    {
        if (storyPanelCache.TryGetValue(story, out StoryPanel storyPanel))
        {
            return storyPanel;
        }
        return null;
    }
}
