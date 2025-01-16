using UnityEngine;

public class CharacterProperties : MonoBehaviour
{
    [SerializeField]
    private int numberOfPrioritiesConsidered = 3; // Increasing this number makes the character's actions more random.
    [SerializeField]
    private OverheadElement idleBubble;
    [SerializeField]
    private OverheadElement frustrationBubble;
    [SerializeField]
    private float paceSpeed = 0.5f;
    [SerializeField]
    private float minWaitTime = 2.0f;
    [SerializeField]
    private float maxWaitTime = 10.0f;
    [SerializeField]
    private float emoteTime = 2.0f;

    public int NumberOfPrioritiesConsidered
    {
        get { return numberOfPrioritiesConsidered; }
        set { numberOfPrioritiesConsidered = value; }
    }

    public OverheadElement IdleBubble
    {
        get { return idleBubble; }
    }

    public OverheadElement FrustrationBubble
    {
        get { return frustrationBubble; }
    }

    public float PaceSpeed
    {
        get { return paceSpeed; }
    }

    public float MinWaitTime
    {
        get { return minWaitTime; }
    }

    public float MaxWaitTime
    {
        get { return maxWaitTime; }
    }

    public float EmoteTime
    {
        get { return emoteTime; }
    }
}