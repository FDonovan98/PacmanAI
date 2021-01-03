using UnityEngine;

public class OuijaBoard : MonoBehaviour
{
    public delegate void ItemFound(MemoryType memoryType, GameObject item);
    public static ItemFound itemFound;
}
