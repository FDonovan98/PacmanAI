// Title: BtController.cs
// Author: Joseph Walton-Rivers
// Collaborators: Harry Donovan
// Date Last Edited: 28/12/2020
// Last Edited By: Harry Donovan
// References:
// File Source: Assets\Scripts\bt\Behaviours\BtController.cs
// Dependencies: Assets\Scripts\bt\Blackboard.cs
//               Assets\Scripts\bt\BtNode.cs
// Description: Abstract class used as a base to build different ai behaviour scripts.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BtController : MonoBehaviour
{
    private BtNode m_root;
    private Blackboard m_blackboard;
    public GameObject itemPool;

    // method to create the tree, sorry - no GUI for this we need to build it by hand
    abstract protected BtNode createTree();

    // Start is called before the first frame update
    virtual protected void Start()
    {
        if (m_root == null)
        {
            m_root = createTree();
            // Initialised with int array. 
            // Each element corresponds to it's Enum MemoryType element.
            m_blackboard = new Blackboard(gameObject, itemPool, new int[]
            {
                5,
                1
            });
        }
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        NodeState result = m_root.evaluate(m_blackboard);
        if (result != NodeState.RUNNING)
        {
            m_root.reset();
        }
    }
}