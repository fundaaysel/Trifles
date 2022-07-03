using UnityEngine;

public class EventHandler : MonoBehaviour
{
   public delegate void CollectibleHandler(int type);

   public static CollectibleHandler onCollect;

   public delegate void ListHandler();

   public static ListHandler onControlList;
   public static ListHandler onCollectNewCube;
   public static ListHandler onRandomGate;
   public static ListHandler onOrderGate;
   
}
