using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtController : MonoBehaviour
{
    private BtNode m_root;
    private Blackboard m_blackboard;

    // method to create the tree, sorry - no GUI for this we need to build it by hand
    protected BtNode createTree()
    {
        BtNode stickyTarget = new Selector(new Sequence(new IsTargeting("pill"), new Inverter(new IsClose(1))), new TargetRandom("pill"));
        BtNode wonderToPill = new Sequence(stickyTarget, new TowardsTarget());
        // return wonderToPill;

        BtNode chasePlayer = new Sequence(new IsClose(3, "Player"), new TargetPlayer("Player"), new TowardsTarget());
        return new Selector(chasePlayer, wonderToPill);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (m_root == null)
        {
            m_root = createTree();
            m_blackboard = new Blackboard();
            m_blackboard.owner = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        NodeState result = m_root.evaluate(m_blackboard);
        if (result != NodeState.RUNNING)
        {
            Debug.Log("ResetState");
            m_root.reset();
        }
    }
}