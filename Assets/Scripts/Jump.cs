using DG.Tweening;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private Transform endPoint;


    public void CharacterJump(GameObject go)
    {
        go.transform.DOJump(endPoint.position, 10f, 1, 3f, false);
    }
}