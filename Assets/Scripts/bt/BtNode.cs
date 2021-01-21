// Title: BtNode.cs
// Author: Joseph Walton-Rivers
// Collaborators:
// Date Last Edited: 06/11/2020
// Last Edited By: Joseph Walton-Rivers
// References: 
// File Source: Assets\Scripts\bt\BtNode.cs
// Dependencies: Assets\Scripts\bt\Blackboard.cs
// Description: Abstract class for individual behaviour tree nodes to be based upon.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Behaviour tree implementation adapted from: https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/

public enum NodeState
{
    SUCCESS,
    FAILURE,
    RUNNING
}

public abstract class BtNode
{

    public delegate NodeState nodeReturn();
    protected NodeState m_nodeState;

    public NodeState nodeState
    {
        get
        {
            return m_nodeState;
        }
    }

    public BtNode()
    {}

    public virtual void reset()
    {
        m_nodeState = NodeState.RUNNING;
    }

    public virtual IEnumerable<BtNode> children()
    {
        return new List<BtNode>();
    }

    public abstract NodeState Evaluate(Blackboard blackboard);
    public abstract string getName();
}