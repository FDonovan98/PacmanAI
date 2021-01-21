using UnityEngine;

public class TargetNewestItem : BtNode
{
    private MemoryType m_memoryType;
    private float m_range;

    public TargetNewestItem(MemoryType memoryType, float range = float.MaxValue)
    {
        m_memoryType = memoryType;
        m_range = range;
    }

    public override NodeState Evaluate(Blackboard blackboard)
    {
        AIRememberedItem newest = null;
        float newestTime = float.MinValue;

        foreach (AIRememberedItem element in blackboard.rememberedItems[(int)m_memoryType])
        {
            if (element.activeMemory)
            {
                float distance = Vector3.Distance(blackboard.owner.transform.position, element.position);
                if (distance < m_range && element.timeUpdated > newestTime)
                {
                    newest = element;
                    newestTime = element.timeUpdated;
                }
            }
        }

        if (newest != null)
        {
            blackboard.target = newest;
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }

    public override string getName()
    {
        return "TargetItem";
    }
}