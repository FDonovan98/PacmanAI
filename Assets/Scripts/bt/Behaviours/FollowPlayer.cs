public class FollowPlayer : BtController
{
    protected override BtNode createTree()
    {
        return MoveToPlayer(100.0f);
    }
}