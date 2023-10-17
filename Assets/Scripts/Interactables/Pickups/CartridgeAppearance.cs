using UnityEngine;

[RequireComponent(typeof(Cartridge))]
public class CartridgeAppearance : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer storyIcon;
    [SerializeField]
    private Renderer cartridgeRenderer;

    [SerializeField]
    private Color taskColor;
    [SerializeField]
    private Sprite taskIcon;
    [SerializeField]
    private Color bugColor;
    [SerializeField]
    private Sprite bugIcon;
    [SerializeField]
    private Color requestColor;
    [SerializeField]
    private Sprite requestIcon;

    private Cartridge cartridge;

    private void Awake()
    {
        cartridge = GetComponent<Cartridge>();
        cartridge.OnStoryUpdated += UpdateAppearance;
    }

    private void UpdateAppearance()
    {
        SetFor(cartridge.Story.Details.Type);
    }

    private void SetFor(StoryType type)
    {
        if(type == StoryType.TASK)
        {
            Set(taskColor, taskIcon);
        }
        else if(type == StoryType.BUG)
        {
            Set(bugColor, bugIcon);
        }
        else if(type == StoryType.REQUEST)
        {
            Set(requestColor, requestIcon);
        }
    }

    private void Set(Color color, Sprite icon)
    {
        Material targetMaterial = cartridgeRenderer.materials[0];
        targetMaterial.color = color;

        storyIcon.sprite = icon;
    }
}
