using UnityEngine;

public class OuijaBoard
{
    public delegate void ItemFoundDelegate(MemoryType memoryType, GameObject item);
    public static ItemFoundDelegate itemFoundDelegate;

    public static void ItemFound(MemoryType memoryType, GameObject item)
    {
        itemFoundDelegate(memoryType, item);
    }
}
