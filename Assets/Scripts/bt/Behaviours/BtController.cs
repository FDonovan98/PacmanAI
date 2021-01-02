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
    private Blackboard m_blackboard;
    private SphereCollider sphereCollider;
    private List<GameObject> itemsInRange = new List<GameObject>();
    public GameObject itemPool;
    public float detectionRange = 7.0f;
    public float rememberedObjectDisplacementTolerance = 1.0f;

    // method to create the tree, sorry - no GUI for this we need to build it by hand
    abstract protected BtNode createTree();

    // Start is called before the first frame update
    virtual protected void Start()
    {
        if (m_root == null)
        {
            m_root = createTree();
            // Initialised with int array. 
            // Each element corresponds to it's Enum MemoryType element.
            m_blackboard = new Blackboard(gameObject, itemPool, new int[]
            {
                5,
                1
            });
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
            if (element.tag == "Player")
            {
                UpdateRememberedItems(MemoryType.Player, element.gameObject);
            }

            if (element.tag == "pill")
            {
                UpdateRememberedItems(MemoryType.Pill, element.gameObject);
            }

        }
    }

    // Add collider to list itemsInRange.
    // Check if item is visible.
    // If a remembered object check there is an actual object within rememberedObjectDisplacementTolerance.
    // If not flag object as having been moved/ destroyed.
    // If object check for remembered object that matches within rememberedObjectDisplacementTolerance.
    // If none then update remembered objects, removing farthest object if no free space. 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        itemsInRange.Add(other.gameObject);

        if (other.tag == "Player")
        {
            UpdateRememberedItems(MemoryType.Player, other.gameObject);
        }

        if (other.tag == "pill")
        {
            UpdateRememberedItems(MemoryType.Pill, other.gameObject);
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

    void UpdateRememberedItems(MemoryType memoryType, GameObject other)
    {
        if (CheckForLOS(other))
        {
            Debug.Log("LOS " + other.name);
            m_blackboard.UpdateRememberedItems(memoryType, other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        itemsInRange.Remove(other.gameObject);
    }

    virtual protected BtNode AwayFromPlayer(float fleeDistance, bool checkIfPowered = true)
    {
        if (checkIfPowered)
        {
            return new Sequence(new TargetPlayer("Player"), new IsBeingMovedTo(), new AwayFromTarget(fleeDistance));
        }

        return new Sequence(new TargetPlayer("Player"), new IsBeingMovedTo(), new AwayFromTarget(fleeDistance));
    }

    virtual protected BtNode MoveToPlayer(float huntRange)
    {
        return new Sequence(new IsClose(huntRange, "Player"), new TargetPlayer("Player"), new TowardsTarget());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}