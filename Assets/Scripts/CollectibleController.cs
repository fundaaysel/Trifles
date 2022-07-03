using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CollectibleController : MonoSingleton<CollectibleController>
{
    [SerializeField] private GameObject blueCube;
    [SerializeField] private GameObject yellowCube;
    [SerializeField] private GameObject orangeCube;

    [SerializeField] private Vector3 firstCubePos;

    private void Awake()
    {
        EventHandler.onCollect += AddToList;
    }

    private void OnDestroy()
    {
        EventHandler.onCollect -= AddToList;
    }

    private void AddToList(int type)
    {
        switch (type)
        {
            case 1:
                SpawnCube(blueCube, 1);
                break;
            case 2:
                SpawnCube(yellowCube, 2);
                break;
            case 3:
                SpawnCube(orangeCube, 3);
                break;
        }
    }


    private void SpawnCube(GameObject cube, int type)
    {
        EventHandler.onCollectNewCube?.Invoke();
        var tempCube = Instantiate(cube, transform);
        tempCube.transform.localPosition = firstCubePos;
        tempCube.transform.DOScale(new Vector3(1.25f, 1f, 1.25f), 0.1f)
            .OnComplete(() => tempCube.transform.DOScale(new Vector3(1, 1, 1), 0.1f));

        tempCube.AddComponent<SlotHand>();
        var tempSlot = tempCube.GetComponent<SlotHand>();
        tempSlot.obj = tempCube;
        tempSlot.type = type;
        ListController.ins.slots.Add(tempSlot);
        EventHandler.onControlList?.Invoke();
    }
}