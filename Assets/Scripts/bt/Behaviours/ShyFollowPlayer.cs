using UnityEngine;

public class ShyFollowPlayer : BtController
{
    // Sets up the behaviour tree and provides an initial location for the player.
    protected override BtNode createTree()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPos = player.transform.position;

        player.transform.position = new Vector3(playerPos.x, 0.0f, playerPos.z);

        m_blackboard.UpdateRememberedItems(MemoryType.Player, player);

        player.transform.position = playerPos;

        return new Selector(AwayFromPlayer(7.0f, false), MoveToPlayer(100.0f));
    }
}