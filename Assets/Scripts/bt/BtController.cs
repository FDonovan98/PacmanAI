using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BtController : MonoBehaviour
{
    private BtNode m_root;
    private Blackboard m_blackboard;

    // method to create the tree, sorry - no GUI for this we need to build it by hand
    abstract protected BtNode createTree();

    // Start is called before the first frame update
    virtual protected void Start()
    {
        if (m_root == null)
        {
            m_root = createTree();
            m_blackboard = new Blackboard();
            m_blackboard.owner = gameObject;
        }
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        NodeState result = m_root.evaluate(m_blackboard);
        if (result != NodeState.RUNNING)
        {
            Debug.Log("ResetState");
            m_root.reset();
        }
    }
}