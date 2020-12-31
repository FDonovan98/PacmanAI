public class ShyFollowPlayer : BtController
{
    protected override BtNode createTree()
    {
        return new Selector(AwayFromPlayer(7.0f), MoveToPlayer(100.0f));
    }
}