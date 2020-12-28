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
    public GameObject owner;
    public GameObject target;
    GameObject[][] rememberedItems;

    public Blackboard(GameObject owner, GameObject itemPool, int[] maxRememberedItems)
    {
        this.owner = owner;

        string[] enumNames = Enum.GetNames(typeof(MemoryType));
        int numOfMemTypes = Enum.GetNames(typeof(MemoryType)).Length;
        rememberedItems = new GameObject[numOfMemTypes][];

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
            rememberedItems[i] = new GameObject[maxRememberedItems[i]];
        }

        GameObject spawnObject = new GameObject();
        GameObject agentSubPool = GameObject.Instantiate(spawnObject, Vector3.zero, new Quaternion(), itemPool.transform);
        agentSubPool.name = owner.name + "ItemPool";

        GameObject[] typeSubPools = new GameObject[loopLength];

        for (int i = 0; i < loopLength; i++)
        {
            typeSubPools[i] = GameObject.Instantiate(spawnObject, Vector3.zero, new Quaternion(), agentSubPool.transform);
            typeSubPools[i].name = enumNames[i] + "Pool";

            for (int j = 0; j < rememberedItems[i].Length; j++)
            {
                rememberedItems[i][j] = GameObject.Instantiate(spawnObject, Vector3.zero, new Quaternion(), typeSubPools[i].transform);
                rememberedItems[i][j].name = "RememberedItem" + j;
                AIRememberedItem aIRememberedItem = rememberedItems[i][j].AddComponent<AIRememberedItem>();
                aIRememberedItem.Initialise(this, (MemoryType)i, Vector3.positiveInfinity);
            }
        }

        GameObject.Destroy(spawnObject);
    }
}