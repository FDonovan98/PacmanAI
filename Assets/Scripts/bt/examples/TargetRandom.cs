using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRandom : BtNode
{
    private MemoryType m_memoryType;

    public TargetRandom(MemoryType memoryType)
    {
        m_memoryType = memoryType;
    }

    public override NodeState evaluate(Blackboard blackboard)
    {
        List<AIRememberedItem> possibleItems = new List<AIRememberedItem>();
        foreach (AIRememberedItem element in blackboard.rememberedItems[(int)m_memoryType])
        {
            if (element.trackingRealObject)
            {
                possibleItems.Add(element);
            }
        }

        if (possibleItems.Count == 0)
        {
            return NodeState.FAILURE;
        }

        blackboard.target = possibleItems[Random.Range(0, possibleItems.Count)];;
        m_nodeState = NodeState.SUCCESS;
        return m_nodeState;
    }

    public override string getName()
    {
        return "TargetRandom";
    }
}