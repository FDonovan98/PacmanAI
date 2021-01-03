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

using System.Collections.Generic;
using UnityEngine;

public abstract class BtController : MonoBehaviour
{
    private BtNode m_root;
    public Blackboard m_blackboard;

    // method to create the tree, sorry - no GUI for this we need to build it by hand
    abstract protected BtNode createTree();

    // Start is called before the first frame update
    protected void Start()
    {
        if (m_root == null)
        {
            m_root = createTree();
        }

        InitialiseKnownItems();
    }

    // Designed to be overridden in inherited classes if needed.
    virtual protected void InitialiseKnownItems()
    {

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

    virtual protected BtNode AwayFromPlayer(float fleeDistance, bool checkIfPowered = true)
    {
        if (checkIfPowered)
        {
            return new Sequence(new TargetPlayer(MemoryType.Player), new IsBeingMovedTo(), new AwayFromTarget(fleeDistance));
        }

        return new Sequence(new TargetPlayer(MemoryType.Player), new IsBeingMovedTo(), new AwayFromTarget(fleeDistance));
    }

    virtual protected BtNode MoveToPlayer(float huntRange)
    {
        return new Sequence(new IsClose(huntRange, MemoryType.Player), new TargetPlayer(MemoryType.Player), new TowardsTarget());
    }
}