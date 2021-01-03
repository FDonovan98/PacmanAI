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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class BtController : MonoBehaviour
{
    private BtNode m_root;
    protected Blackboard m_blackboard;
    private SphereCollider sphereCollider;
    private List<GameObject> itemsInRange = new List<GameObject>();
    public float detectionRange = 7.0f;
    public float rememberedObjectDisplacementTolerance = 1.0f;
    // Remembered player count (index 0) needs to be at least 2 for directional analysis to work.
    // See Assets\Editor\NamedArrayDrawer.cs for explanation of NamedArray attribute as it is NOT MY CODE.
    [NamedArrayAttribute(new string[] {"Player", "Pill", "PowerPill"})]
    public int[] rememberedItemCounts = new int[]
    {
        5,
        2
    };

    // method to create the tree, sorry - no GUI for this we need to build it by hand
    abstract protected BtNode createTree();

    // Start is called before the first frame update
    virtual protected void Start()
    {
        if (m_root == null)
        {
            // Initialised with int array. 
            // Each element corresponds to it's Enum MemoryType element.
            m_blackboard = new Blackboard(gameObject, rememberedItemCounts, rememberedObjectDisplacementTolerance);
            m_root = createTree();
        }

        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = detectionRange;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        NodeState result = m_root.evaluate(m_blackboard);
        if (result != NodeState.RUNNING)
        {
            m_root.reset();
        }

        foreach (GameObject element in itemsInRange)
        {
            CheckTagsToUpdateItems(element);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        itemsInRange.Add(other.gameObject);

        CheckTagsToUpdateItems(other.gameObject);

    }

    // Call local UpdateRememberedItems function dependant on other's tag.
    private void CheckTagsToUpdateItems(GameObject other)
    {
        if (other.tag == "Player")
        {
            UpdateRememberedItems(MemoryType.Player, other);
            return;
        }

        if (other.tag == "pill")
        {
            UpdateRememberedItems(MemoryType.Pill, other);
            return;
        }
    }

    // Checks if agent has line of sight to other.
    private bool CheckForLOS(GameObject other)
    {
        Ray ray = new Ray(transform.position, other.transform.position - transform.position);
        RaycastHit[] hits = Physics.RaycastAll(ray, Vector3.Distance(transform.position, other.transform.position));

        if (hits.Length > 2)
        {
            return false;
        }

        return true;
    }

    // Checks if agent has line of site to other and then calls the blackboard function to update remembered items.
    void UpdateRememberedItems(MemoryType memoryType, GameObject other)
    {
        if (CheckForLOS(other))
        {
            m_blackboard.UpdateRememberedItems(memoryType, other);
        }
    }

    // Remove items from itemsInRange when they are outside of detection range.
    private void OnTriggerExit(Collider other)
    {
        itemsInRange.Remove(other.gameObject);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        int i = 0;
        if (m_blackboard != null && m_blackboard.rememberedItems.Length > 0)
        {
            foreach (AIRememberedItem[] array in m_blackboard.rememberedItems)
            {
                switch (i)
                {
                    case (int)MemoryType.Player:
                        Gizmos.color = Color.red;
                        break;
                    case (int)MemoryType.Pill:
                        Gizmos.color = Color.green;
                        break;
                    case (int)MemoryType.PowerPill:
                        Gizmos.color = Color.yellow;
                        break;
                    default:
                        break;
                }

                if (array.Length > 0)
                {
                    foreach (AIRememberedItem element in array)
                    {
                        Gizmos.DrawCube(element.position, new Vector3(1.0f, 1.0f, 1.0f));
                    }
                }

                i++;
            }
        }
    }
}