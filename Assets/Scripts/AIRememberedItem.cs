// Title: AIRememberedItem.cs
// Author: Harry Donovan
// Collaborators:
// Date Last Edited: 23/12/2020
// Last Edited By: Harry Donovan
// References: 
// File Source: Assets\Scripts\AIRememberedItem.cs
// Dependencies: Assets\Scripts\bt\Blackboard.cs
// Description: Script used by ai agents to mark and track gameobjects as remembered objects. Designed to be added to a blank GameObject which is then used as the remembered object.

using UnityEngine;

[RequireComponent(typeof(Transform))]
public class AIRememberedItem : MonoBehaviour
{
    float timeUpdated;
    MemoryType memoryType;
    Blackboard blackboard;
    Vector3 pos;

    public void Initialise(Blackboard blackboard, MemoryType memoryType, Vector3 pos)
    {
        this.memoryType = memoryType;
        this.blackboard = blackboard;
        UpdateLocation(pos);
    }

    public void UpdateLocation(Vector3 pos)
    {
        this.pos = pos;
        // Uses Vector3.Distance as bool checks are inaccurate w/ v. large values.
        if (Vector3.Distance(pos, Vector3.positiveInfinity) < 1)
        {
            this.transform.position = pos;
            timeUpdated = Time.timeSinceLevelLoad;
        }
    }
}

public enum MemoryType
{
    Pill,
    Player
}