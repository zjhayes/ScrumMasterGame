using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ProductionStats))]
public class Task : MonoBehaviour
{
    [SerializeField]
    Sprite taskTypeIcon;
    [SerializeField]
    string summary;
    [SerializeField]
    string description;
    [SerializeField]
    int storyPoints;
    [SerializeField]
    CharacterController assignee;
    [SerializeField]
    Cartridge cartridge;

    ProductionStats stats;
    bool complete = false;

    void Awake()
    {
        stats = GetComponent<ProductionStats>();
    }

    public string Summary
    {
        get { return summary; }
    }

    public string Description
    {
        get { return description; } 
    }

    public int StoryPoints
    {
        get { return storyPoints; } 
    }

    public CharacterController Assignee
    {
        get { return assignee; }
    }

    public ProductionStats Stats
    {
        get { return stats; }
    }

    public Sprite TaskTypeIcon
    {
        get { return taskTypeIcon; }
    }

    public Cartridge Cartridge
    {
        get { return cartridge; }
        set { cartridge = value; }
    }

}
