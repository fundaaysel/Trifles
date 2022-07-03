using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collectible>())
        {
            Destroy(other.gameObject);
            var collectible = other.GetComponent<Collectible>();
            EventHandler.onCollect?.Invoke(collectible.type);
        }

        if (other.CompareTag("RandomGate"))
        {
            EventHandler.onRandomGate?.Invoke();
        }

        if (other.CompareTag("OrderGate"))
        {
            EventHandler.onOrderGate?.Invoke();
        }

        if (other.CompareTag("Jump"))
        {
            other.GetComponent<Jump>().CharacterJump(gameObject);
        }

        if (other.CompareTag("Speed"))
        {
            StartCoroutine(SpeedPlace());
        }
    }

    IEnumerator SpeedPlace()
    {
        CharacterController.ins.speedZ = 10f;
        yield return new WaitForSeconds(4f);
        CharacterController.ins.speedZ = 5f;
    }
}