using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

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
    [SerializeField]
    private BlessingWindow _blessingWindowAssigning;

    private Blessing _equippedSlot01;
    private Blessing _equippedSlot02;

    public BlessingNS activeBlessingToAssign;

    public bool isActive = true;

    public bool _blessingSelecting = false;
    public bool _blessingAssigning = false;

    public GameObject[] activeCollection;
    public int indexOfActiveCollection = 0;

    public Image[] blessingSlotsStats;

    public UIManager UIManager;
    public PlayerBlessingsHandler PBS;

    public void Start()
    {
        //StartChunk001();
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
        _blessingWindow002.gameObject.SetActive(true);
        UIManager._UIActive = true;
        // The first blessing is always Hotel
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

    public void StartChunkGeneric()
    {
        _blessingWindow001.gameObject.SetActive(true);
        UIManager._UIActive = true;

        // the first blessing is a random between the rest ones
        BlessingNS b = BlessingRandomPicker();
        _blessingWindow001._blessingHandlers[0].transform.GetChild(2).GetComponent<Image>().sprite = b.blessing.Sprite;
        _blessingWindow001._blessingHandlers[0].transform.GetChild(3).GetComponent<Text>().text = b.blessing.Name;
        _blessingWindow001._blessingHandlers[0].transform.GetChild(4).GetComponent<Text>().text = b.blessing.Effect;

        // the second blessing is a random between the rest ones
        BlessingNS b2 = BlessingRandomPicker();
        while(b2 == b)
        {
            b2 = BlessingRandomPicker();
        }
        _blessingWindow001._blessingHandlers[1].transform.GetChild(2).GetComponent<Image>().sprite = b2.blessing.Sprite;
        _blessingWindow001._blessingHandlers[1].transform.GetChild(3).GetComponent<Text>().text = b2.blessing.Name;
        _blessingWindow001._blessingHandlers[1].transform.GetChild(4).GetComponent<Text>().text = b2.blessing.Effect;

        // the second blessing is a random between the rest ones
        BlessingNS b3 = BlessingRandomPicker();
        while (b3 == b || b3 == b2)
        {
            b3 = BlessingRandomPicker();
        }
        _blessingWindow001._blessingHandlers[2].transform.GetChild(2).GetComponent<Image>().sprite = b3.blessing.Sprite;
        _blessingWindow001._blessingHandlers[2].transform.GetChild(3).GetComponent<Text>().text = b3.blessing.Name;
        _blessingWindow001._blessingHandlers[2].transform.GetChild(4).GetComponent<Text>().text = b3.blessing.Effect;

        activeCollection = _blessingWindow001._blessingHandlers;
        _blessingSelecting = true;
        ActivateBlessingCard(activeCollection, indexOfActiveCollection);
    }

    public void MoveIntoAssignement()
    {
        activeBlessingToAssign = GetActiveBlessingForSelection(activeCollection);

        _blessingSelecting = false;
        _blessingAssigning = true;

        _blessingWindow002.gameObject.SetActive(false);
        _blessingWindow001.gameObject.SetActive(false);
        _blessingWindowAssigning.gameObject.SetActive(true);


        _blessingWindowAssigning.gameObject.transform.GetChild(3).GetChild(2).GetComponent<Image>().sprite = activeBlessingToAssign.blessing.Sprite;
        _blessingWindowAssigning.gameObject.transform.GetChild(3).GetChild(3).GetComponent<Text>().text = activeBlessingToAssign.blessing.Name;
        _blessingWindowAssigning.gameObject.transform.GetChild(3).GetChild(4).GetComponent<Text>().text = activeBlessingToAssign.blessing.Effect;

        activeCollection = _blessingWindowAssigning._blessingHandlers;

        indexOfActiveCollection = 0;

        activeCollection[indexOfActiveCollection].transform.GetChild(2).GetComponent<Image>().enabled = true;
        activeCollection[indexOfActiveCollection].transform.GetChild(1).GetComponent<Image>().enabled = true;
        activeCollection[indexOfActiveCollection].transform.GetChild(1).GetComponent<Image>().sprite = activeBlessingToAssign.blessing.Sprite;
    }

    public BlessingNS GetActiveBlessingForSelection(GameObject[] go)
    {
        if (go[indexOfActiveCollection].gameObject.transform.GetChild(3).GetComponent<Text>().text == "Présent de Mongfind") return Hotel;

        for(int i = 0; i < Blessings.Count; ++i)
        {
            if (Blessings[i].blessing.Name == go[indexOfActiveCollection].gameObject.transform.GetChild(3).GetComponent<Text>().text)
            {
                return Blessings[i];
            }
        }

        return null;
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

    public void ActivateBlessingSlot(GameObject[] bh, int activeIndex, BlessingNS b)
    {
        for (int i = 0; i < bh.Length; ++i)
        {
            if (i == activeIndex)
            {
                bh[i].transform.GetChild(2).GetComponent<Image>().enabled = true;
                bh[i].transform.GetChild(1).GetComponent<Image>().enabled = true;
                bh[i].transform.GetChild(1).GetComponent<Image>().sprite = b.blessing.Sprite;
            }
            else
            {
                bh[i].transform.GetChild(2).GetComponent<Image>().enabled = false;
                bh[i].transform.GetChild(1).GetComponent<Image>().enabled = false;
                bh[i].transform.GetChild(1).GetComponent<Image>().sprite = null;
            }
        }
    }

    public void ValidateAssignation()
    {
        if(indexOfActiveCollection == 0)
        {
            if (_equippedSlot01)
            {
                RemoveBenediction(_equippedSlot01.Name);
            }
            _equippedSlot01 = activeBlessingToAssign.blessing;
            blessingSlotsStats[0].enabled = true;
            blessingSlotsStats[0].sprite = activeBlessingToAssign.blessing.Sprite;
        }
        else if(indexOfActiveCollection == 1)
        {
            if (_equippedSlot02)
            {
                RemoveBenediction(_equippedSlot02.Name);
            }
            _equippedSlot02 = activeBlessingToAssign.blessing;
            blessingSlotsStats[1].enabled = true;
            blessingSlotsStats[1].sprite = activeBlessingToAssign.blessing.Sprite;
        }

        _blessingWindowAssigning.gameObject.SetActive(false);
        UIManager._UIActive = false;
        UIManager._playerInput.SwitchCurrentActionMap("Player");
        Debug.Log(activeBlessingToAssign.blessing.Name);
        ActivateBenediction(activeBlessingToAssign.blessing.Name);

        for(int i = 0; i< Blessings.Count; i++)
        {
            if (Blessings[i].blessing.Name == activeBlessingToAssign.blessing.Name)
            {
                Blessings.RemoveAt(i);
                return;
            }
        }
    }

    private void ActivateBenediction(string name)
    {
        PBS.ApplyEffect(name);
    }

    private void RemoveBenediction(string name)
    {
        PBS.RemoveEffect(name);
    }
}