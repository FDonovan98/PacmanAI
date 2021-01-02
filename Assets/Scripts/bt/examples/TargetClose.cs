// Original Author: Joseph Walton-Rivers
// Collaborators: Harry Donovan
// References: 
// Dependencies: Assets\Scripts\AIRememberedItem.cs, 
//               Assets\Scripts\bt\BtNode.cs
// Description: Finds the closest remembered object based on a provided MemoryType.

using UnityEngine;

public class TargetPlayer : BtNode
{
    private MemoryType m_memoryType;

    public TargetPlayer(MemoryType memoryType)
    {
        this.m_memoryType = memoryType;
    }

    public override NodeState evaluate(Blackboard blackboard)
    {
        AIRememberedItem closest = null;
        float closestDistance = float.MaxValue;

        foreach (AIRememberedItem gObject in blackboard.rememberedItems[(int)m_memoryType])
        {
            if (gObject.trackingRealObject)
            {
                float distance = Vector3.Distance(blackboard.owner.transform.position, gObject.position);
                if (distance < closestDistance)
                {
                    closest = gObject;
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
        return "TargetPlayer";
    }
}