// Title: Blackboard.cs
// Author: Joseph Walton-Rivers
// Collaborators: Harry Donovan
// Date Last Edited: 23/12/2020
// Last Edited By: Harry Donovan
// References: 
// File Source: Assets\Scripts\bt\Blackboard.cs
// Dependencies: Assets\Scripts\AIRememberedItem.cs
// Description: Blackboard used for each ai agent. Expanded to allow each ai agent to have a pool of items it remembers the locations of. These pools of gameobjects are generated at initilisation and then updated throughout runtime.

using System;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard
{
    float rememberedObjectDisplacementTolerance;
    public GameObject owner;
    public AIRememberedItem target;

    public AIRememberedItem[][] rememberedItems
    {
        get;
    }

    public Blackboard(GameObject owner, int[] maxRememberedItems, float rememberedObjectDisplacementTolerance)
    {
        this.owner = owner;
        this.rememberedObjectDisplacementTolerance = rememberedObjectDisplacementTolerance;

        string[] enumNames = Enum.GetNames(typeof(MemoryType));
        int numOfMemTypes = Enum.GetNames(typeof(MemoryType)).Length;
        rememberedItems = new AIRememberedItem[numOfMemTypes][];

        int loopLength;
        if (numOfMemTypes > maxRememberedItems.Length)
        {
            loopLength = maxRememberedItems.Length;
        }
        else
        {
            loopLength = numOfMemTypes;
        }

        for (int i = 0; i < loopLength; i++)
        {
            rememberedItems[i] = new AIRememberedItem[maxRememberedItems[i]];
            for (int j = 0; j < maxRememberedItems[i]; j++)
            {
                rememberedItems[i][j] = new AIRememberedItem(this, (MemoryType)i);
            }
        }
    }

    // Replaces the oldest remembered item with the new itemToAdd.
    public void UpdateRememberedItems(MemoryType memoryType, GameObject itemToAdd)
    {
        float oldestTime = float.MaxValue;

        int i = 0;
        int replacementIndex = int.MaxValue;
        foreach (AIRememberedItem element in rememberedItems[(int)memoryType])
        {
            // If the gameobject is already being remembered then it is not added.
            // Assumes any object close enough is the same object which would cause problems if multiple of the same MemoryType of object could be next to each other.
            // However for this application that can't happen so this implementation is suitable.
            if (Vector3.Distance(itemToAdd.transform.position, element.position) < rememberedObjectDisplacementTolerance)
            {
                replacementIndex = int.MaxValue;
                break;
            }

            float newTime = element.timeUpdated;
            if (newTime < oldestTime)
            {
                oldestTime = newTime;
                replacementIndex = i;
            }

            i++;
        }

        if (replacementIndex != int.MaxValue)
        {
            rememberedItems[(int)memoryType][replacementIndex].UpdateLocation(itemToAdd.transform.position);
        }
    }
}