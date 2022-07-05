using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListController : MonoSingleton<ListController>
{
    public List<SlotHand> slots = new List<SlotHand>();

    private int type = 0;
    private int sameColorCount = 0;
    private List<GameObject> willDestroyObjs = new List<GameObject>();

    public bool match;
    public int matchCount;

    public float yIncValue;

    private float yValue;

    [SerializeField] private GameObject player;
    [HideInInspector] public bool feverMod;

    private void Awake()
    {
        EventHandler.onControlList += ControlListForExplosion;
        EventHandler.onCollectNewCube += AddedNewCube;
        EventHandler.onRandomGate += RandomizeList;
        EventHandler.onOrderGate += OrderList;
    }

    private void OnDestroy()
    {
        EventHandler.onControlList -= ControlListForExplosion;
        EventHandler.onCollectNewCube -= AddedNewCube;
        EventHandler.onRandomGate -= RandomizeList;
        EventHandler.onOrderGate -= OrderList;
    }

    private void ControlListForExplosion()
    {
        if (slots.Count > 2)
        {
            StartCoroutine(ControlListForExplosionCoroutine());
        }
    }

    IEnumerator ControlListForExplosionCoroutine()
    {
        for (int i = 0; i < slots.Count - 2; i++)
        {
            yield return new WaitForSeconds(0.5f);
            if (slots[i].type == slots[i + 1].type && slots[i + 1].type == slots[i + 2].type)
            {
                match = true;

                willDestroyObjs.Add(slots[i].obj);
                willDestroyObjs.Add(slots[i + 1].obj);
                willDestroyObjs.Add(slots[i + 2].obj);

                slots[i].obj = null;
                slots[i + 1].obj = null;
                slots[i + 2].obj = null;

                slots[i].type = 0;
                slots[i + 1].type = 0;
                slots[i + 2].type = 0;
            }
        }

        foreach (var slotHand in slots.ToArray())
        {
            if (slotHand.obj == null)
            {
                slots.Remove(slotHand);
            }
        }

        foreach (var willDestroyObj in willDestroyObjs)
        {
            Destroy(willDestroyObj, 0.1f);
        }

        if (match)
        {
            ControlListForExplosion();
            matchCount++;
        }
        else
        {
            matchCount = 0;
        }

        if (matchCount == 3)
        {
            StartCoroutine(FeverMod());
        }
    }

    IEnumerator FeverMod()
    {
        Debug.Log("fever");
        CharacterController.ins.speedZ = 8f;
        feverMod = true;
        yield return new WaitForSeconds(3f);
        CharacterController.ins.speedZ = 5f;
        feverMod = false;
    }


    private void RandomizeList()
    {
        for (var i = 0; i < slots.Count; ++i)
        {
            var r = UnityEngine.Random.Range(i, slots.Count);
            (slots[i], slots[r]) = (slots[r], slots[i]);
        }

        SortList();
    }

    private List<SlotHand> typeOne = new List<SlotHand>();
    private List<SlotHand> typeTwo = new List<SlotHand>();
    private List<SlotHand> typeThree = new List<SlotHand>();

    private void OrderList()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            var slotToUse = slots[i];
            if (slotToUse.type == 1)
            {
                typeOne.Add(slotToUse);
            }
            else if (slotToUse.type == 2)
            {
                typeTwo.Add(slotToUse);
            }
            else if (slotToUse.type == 3)
            {
                typeThree.Add(slotToUse);
            }
        }

        foreach (var slotHand in typeOne)
        {
            slots.Add(slotHand);
        }

        foreach (var slotHand in typeTwo)
        {
            slots.Add(slotHand);
        }

        foreach (var slotHand in typeThree)
        {
            slots.Add(slotHand);
        }

        SortList();
    }

    private void SortList()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            var objToUse = slots[i].obj;
            var pos = objToUse.transform.localPosition;
            pos.y = (slots.Count - i) * yIncValue;
            objToUse.transform.localPosition = pos;
        }

        var playerPos = player.transform.localPosition;
        playerPos.y = slots.Count * yIncValue;
        player.transform.localPosition = playerPos;
        ControlListForExplosion();
    }

    private void AddedNewCube()
    {
        var tempPos = player.transform.localPosition;
        tempPos.y += yIncValue;
        player.transform.localPosition = tempPos;

        for (int i = 0; i < slots.Count; i++)
        {
            var slotToUse = slots[i];
            var cubePos = slotToUse.transform.localPosition;
            cubePos.y += yIncValue;
            slotToUse.transform.localPosition = cubePos;
        }
    }
}