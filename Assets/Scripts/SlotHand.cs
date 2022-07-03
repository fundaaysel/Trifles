using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHand : MonoBehaviour
{
    public GameObject obj;
    public int type; // blue =1 ; yellow=2 ; orange=3 ;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (!ListController.ins.feverMod)
            {
                var ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.transform.gameObject.CompareTag("Obstacle"))
                    {
                        transform.parent = null;
                        ListController.ins.slots.Remove(this);
                    }
                }
            }
        }
    }
}