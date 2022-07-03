using UnityEngine;

public class PlayerCapsule : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("fail");
        }
    }
}