using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRandom : BtNode
{
    private MemoryType m_memoryType;
    private bool m_weightedRandom;

    public TargetRandom(MemoryType memoryType, bool weightedRandom = false)
    {
        m_memoryType = memoryType;
        m_weightedRandom = weightedRandom;
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

        if (!m_weightedRandom)
        {
            blackboard.target = possibleItems[Random.Range(0, possibleItems.Count)];;
        }
        else
        {
            float[] targetAngles = new float[possibleItems.Count];
            int i = 0;
            foreach (AIRememberedItem element in blackboard.rememberedItems[(int)m_memoryType)
            {
                Vector3 dir = (element.position - blackboard.owner.transform.position).normalized;
                targetAngles[i] = Vector3.Angle(blackboard.owner.transform.forward, dir);
                i++;
            }

            List<AIRememberedItem> temp = new List<AIRememberedItem>();

            for (i = 0; i < targetAngles.Length; i++)
            {
                
            }
        }
        m_nodeState = NodeState.SUCCESS;
        return m_nodeState;
    }

    public override string getName()
    {
        return "TargetRandom";
    }
}