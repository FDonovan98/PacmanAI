using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AgentDetection : MonoBehaviour
{
    private BtController m_btController;
    private SphereCollider sphereCollider;
    private List<GameObject> itemsInRange = new List<GameObject>();
    public float detectionRange = 7.0f;
    public float rememberedObjectDisplacementTolerance = 1.0f;
    // Remembered player count (index 0) needs to be at least 2 for directional analysis to work.
    // See Assets\Editor\NamedArrayDrawer.cs for explanation of NamedArray attribute as it is NOT MY CODE.
    [NamedArrayAttribute(new string[] {"Player", "Pill", "PowerPill"})]
    public int[] rememberedItemCounts = new int[]
    {
        2, 30, 4
    };
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            m_btController = GetComponent<BtController>();
            m_btController.m_blackboard = new Blackboard(gameObject, rememberedItemCounts, rememberedObjectDisplacementTolerance);
        }
        catch (System.Exception)
        {
            Debug.LogWarning(gameObject.name + " has no BtController attached");
        }

        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = detectionRange;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (other.tag == "powerpill")
        {
            UpdateRememberedItems(MemoryType.PowerPill, other);
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

    // Checks if agent has line of site to other and then calls the m_blackboard function to update remembered items.
    void UpdateRememberedItems(MemoryType memoryType, GameObject other)
    {
        if (CheckForLOS(other))
        {
            m_btController.m_blackboard.UpdateRememberedItems(memoryType, other);
        }
    }

    // Remove items from itemsInRange when they are outside of detection range.
    private void OnTriggerExit(Collider other)
    {
        itemsInRange.Remove(other.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw detection sphere
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw remembered items.
        int i = 0;
        if (m_btController != null && m_btController.m_blackboard != null && m_btController.m_blackboard.rememberedItems.Length > 0)
        {
            foreach (AIRememberedItem[] array in m_btController.m_blackboard.rememberedItems)
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
    
        // Draw current target.
        if (m_btController != null && m_btController.m_blackboard != null && m_btController.m_blackboard.target != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(m_btController.m_blackboard.target.position, 1.0f);
        }
    }
}
