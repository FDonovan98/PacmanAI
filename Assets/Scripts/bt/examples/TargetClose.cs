// Original Author: Joseph Walton-Rivers
// Collaborators: Harry Donovan
// References: 
// Dependencies: Assets\Scripts\AIRememberedItem.cs, 
//               Assets\Scripts\bt\BtNode.cs
// Description: Finds the closest remembered object based on a provided MemoryType.

using UnityEngine;

public class TargetClose : BtNode
{
    private MemoryType m_memoryType;
    bool m_requireActiveMemory;

    public TargetClose(MemoryType memoryType, bool requireActiveMemory = true)
    {
        m_memoryType = memoryType;
        m_requireActiveMemory = requireActiveMemory;
    }

    public override NodeState Evaluate(Blackboard blackboard)
    {
        AIRememberedItem closest = null;
        float closestDistance = float.MaxValue;

        foreach (AIRememberedItem item in blackboard.rememberedItems[(int)m_memoryType])
        {
            if (!(m_requireActiveMemory && !item.activeMemory))
            {
                float distance = Vector3.Distance(blackboard.owner.transform.position, item.position);
                if (distance < closestDistance)
                {
                    closest = item;
                    closestDistance = distance;
                }
            }
        }

        if (closest != null)
        {
            blackboard.target = closest;
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }

    public override string getName()
    {
        return "TargetClose";
    }
}