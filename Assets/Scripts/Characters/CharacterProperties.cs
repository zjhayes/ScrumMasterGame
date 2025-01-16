using UnityEngine;

public class CharacterProperties : MonoBehaviour
{
    [SerializeField]
    private int numberOfPrioritiesConsidered = 3; // Increasing this number makes the character's actions more random.
    [SerializeField]
    private OverheadElement idleBubble;
    [SerializeField]
    private float paceSpeed = 0.5f;
    [SerializeField]
    private float minWaitTime = 2.0f;
    [SerializeField]
    private float maxWaitTime = 10.0f;

    public int NumberOfPrioritiesConsidered
    {
        get { return numberOfPrioritiesConsidered; }
        set { numberOfPrioritiesConsidered = value; }
    }

    public OverheadElement IdleBubble
    {
        get { return idleBubble; }
        set { idleBubble = value; }
    }

    public float PaceSpeed
    {
        get { return paceSpeed; }
        set { paceSpeed = value; }
    }

    public float MinWaitTime
    {
        get { return minWaitTime; }
        set { minWaitTime = value; }
    }

    public float MaxWaitTime
    {
        get { return maxWaitTime; }
        set { maxWaitTime = value; }
    }
}