// Title: AIRememberedItem.cs
// Author: Harry Donovan
// Collaborators:
// Date Last Edited: 23/12/2020
// Last Edited By: Harry Donovan
// References: 
// File Source: Assets\Scripts\AIRememberedItem.cs
// Dependencies: Assets\Scripts\bt\Blackboard.cs
// Description: Script used by ai agents to mark and track gameobjects as remembered objects. Designed to be added to a blank GameObject which is then used as the remembered object.
// Should likely be changed to contain a position and then just use an array rather then actual game objects.

using UnityEngine;

public class AIRememberedItem
{
    public float timeUpdated;
    public MemoryType memoryType;
    Blackboard blackboard;
    public bool trackingRealObject;
    public Vector3 position;

    public AIRememberedItem(Blackboard blackboard, MemoryType memoryType) : this(blackboard, memoryType, new Vector3())
    {
        trackingRealObject = false;
    }

    public AIRememberedItem(Blackboard blackboard, MemoryType memoryType, Vector3 pos)
    {
        this.memoryType = memoryType;
        this.blackboard = blackboard;
        UpdateLocation(pos);
    }

    public void UpdateLocation(Vector3 pos)
    {
        this.position = pos;
        timeUpdated = Time.timeSinceLevelLoad;
        trackingRealObject = true;
    }
}

public enum MemoryType
{
    Player,
    Pill,
    PowerPill
}