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
    public OuijaBoard ouijaBoard = null;
    public Blackboard m_blackboard;
    public float playerAttackRange;
    protected float m_minApproachRange;
    // method to create the tree, sorry - no GUI for this we need to build it by hand
    abstract protected BtNode createTree();

    // Start is called before the first frame update
    protected void Start()
    {
        m_minApproachRange = GetComponent<CapsuleCollider>().bounds.extents.x * 1.5f;

        if (m_root == null)
        {
            m_root = createTree();
        }

        InitialiseKnownItems();

        // if (ouijaBoard = null)
        // {
        //     ouijaBoard = GameObject.Find("OuijaBoard").GetComponent<OuijaBoard>();
        // }

        OuijaBoard.itemFoundDelegate += m_blackboard.UpdateRememberedItems;
    }

    // Designed to be overridden in inherited classes if needed.
    virtual protected void InitialiseKnownItems()
    {

    }

    // Update is called once per frame
    virtual protected void Update()
    {
        NodeState result = m_root.Evaluate(m_blackboard);
        if (result != NodeState.RUNNING)
        {
            m_root.reset();
        }
    }

    virtual protected BtNode AwayFromPlayer(float fleeDistance, bool checkIfPowered = true)
    {
        if (checkIfPowered)
        {
            return new Sequence(new TargetClose(MemoryType.Player), new IsBeingMovedTo(), new AwayFromTarget(fleeDistance));
        }

        return new Sequence(new TargetClose(MemoryType.Player), new IsBeingMovedTo(), new AwayFromTarget(fleeDistance));
    }

    virtual protected BtNode MoveToPlayer(float huntRange)
    {
        return new Sequence(new HasActiveMemoryInRange(MemoryType.Player, playerAttackRange), new TargetClose(MemoryType.Player), new TowardsTarget(m_minApproachRange));
    }
    
    virtual protected BtNode MoveToItem(MemoryType memoryType)
    {
        return new Sequence(new TargetNewestItem(memoryType), new TowardsTarget(m_minApproachRange));
    }
    virtual protected BtNode MoveToItem(MemoryType memoryType, float range)
    {
        return new Sequence(new Inverter(new IsClose(0.1f, memoryType)), new TargetNewestItem(memoryType, range), new TowardsTarget(m_minApproachRange));
    }
}