using UnityEngine;

public class FollowPlayer : BtController
{
    protected override BtNode createTree()
    {
        return MoveToItem(MemoryType.Player);
    }

    protected override void Update()
    {
        base.Update();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPos = player.transform.position;

        player.transform.position = new Vector3(playerPos.x, 0.0f, playerPos.z);

        m_blackboard.UpdateRememberedItems(MemoryType.Player, player);

        player.transform.position = playerPos;
    }
}