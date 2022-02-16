using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using UnityEngine.UI;
using System;

public class BlessingManager : MonoBehaviour
{
    [SerializeField]
    private List<BlessingNS> Blessings = new List<BlessingNS>();

    [SerializeField]
    private BlessingNS Hotel;

    [SerializeField]
    private BlessingWindow _blessingWindow001;
    [SerializeField]
    private BlessingWindow _blessingWindow002;

    private Blessing _equippedSlot01;
    private Blessing _equippedSlot02;

    public bool isActive = true;

    public bool _blessingSelecting = false;
    public bool _blessingReplacement = false;

    public GameObject[] activeCollection;
    public int indexOfActiveCollection = 0;

    public void Start()
    {
        StartChunk001();
    }

    public BlessingNS BlessingRandomPicker()
    {
        // init
        int _totalWeight = 0;

        foreach(BlessingNS b in Blessings)
        {
            _totalWeight += b.weight;
        }

        int rnd = UnityEngine.Random.Range(1, _totalWeight + 1);
        int pos = 0;

        for(int i = 0; i < Blessings.Count; ++i)
        {
            if (rnd <= Blessings[i].weight + pos)
            {
                return Blessings[i];
            }
            pos += Blessings[i].weight;
        }

        return null;
    }

    //Chunk Management
    public void StartChunk001()
    {
        // The first blessing is always 7
        _blessingWindow002._blessingHandlers[0].transform.GetChild(2).GetComponent<Image>().sprite = Hotel.blessing.Sprite;
        _blessingWindow002._blessingHandlers[0].transform.GetChild(3).GetComponent<Text>().text = Hotel.blessing.Name;
        _blessingWindow002._blessingHandlers[0].transform.GetChild(4).GetComponent<Text>().text = Hotel.blessing.Effect;

        // the second blessing is a random between the rest ones
        BlessingNS b = BlessingRandomPicker();
        _blessingWindow002._blessingHandlers[1].transform.GetChild(2).GetComponent<Image>().sprite = b.blessing.Sprite;
        _blessingWindow002._blessingHandlers[1].transform.GetChild(3).GetComponent<Text>().text = b.blessing.Name;
        _blessingWindow002._blessingHandlers[1].transform.GetChild(4).GetComponent<Text>().text = b.blessing.Effect;

        activeCollection = _blessingWindow002._blessingHandlers;
        _blessingSelecting = true;
        ActivateBlessingCard(activeCollection, indexOfActiveCollection);
    }

    public void ActivateBlessingCard(GameObject[] bh, int activeIndex)
    {
        for(int i = 0; i < bh.Length; ++i)
        {
            if(i == activeIndex)
            {
                bh[i].transform.localScale = new Vector3(1.2f, 1.2f, 1f);
                bh[i].transform.GetChild(5).GetComponent<Image>().enabled = true;
            }
            else
            {
                bh[i].transform.localScale = new Vector3(1f, 1f, 1f);
                bh[i].transform.GetChild(5).GetComponent<Image>().enabled = false;
            }
        }
    }
}