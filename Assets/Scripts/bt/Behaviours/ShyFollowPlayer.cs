using UnityEngine;

[RequireComponent(typeof(AgentDetection))]
public class ShyFollowPlayer : BtController
{
    GameObject m_player = null;
    // Sets up the behaviour tree and provides an initial location for the player.
    protected override BtNode createTree()
    {
        return new Selector(AwayFromPlayer(7.0f, false), MoveToItem(MemoryType.Player, playerAttackRange));
    }

    protected override void Update()
    {
        base.Update();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPos = player.transform.position;

        player.transform.position = new Vector3(playerPos.x, 0.0f, playerPos.z);

        m_blackboard.UpdateRememberedItems(MemoryType.Player, player);

        player.transform.position = playerPos;

        if (m_player != null)
        {
            OuijaBoard.ItemFound(MemoryType.Player, m_player);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_player = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_player = null;
        }
    }
}