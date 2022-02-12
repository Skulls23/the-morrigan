using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player_Stats_Manager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image[] _livesImage;

    //TESTING VARIABLES (NEEDS UPDATE)
    [SerializeField]
    private int _playerMaxLives;
    [SerializeField]
    private int _nbCorruptedLives;
    [SerializeField]
    private int _nbFullLives;
    [SerializeField]
    private int _nbEmptyLives;

    public void Start()
    {
        initPlayerLivesUI();
        _nbCorruptedLives = 0;
        _nbEmptyLives = 0;
        _nbFullLives = _playerMaxLives;
        UpdateLives();
    }

    private void initPlayerLivesUI()
    {
        for(int i = _playerMaxLives; i < _livesImage.Length; ++i)
        {
            _livesImage[i].enabled = false;
        }
    }

    public void UpdateLives()
    {
        FullLives();
        CorruptLives();
        EmptyLives();
    }

    private void FullLives()
    {
        for (int i = 0; i < _nbFullLives; ++i)
        {
            _livesImage[i].sprite = _liveSprites[0];
        }
    }

    private void EmptyLives()
    {
        for (int i = _nbFullLives + _nbCorruptedLives; i < _playerMaxLives; ++i)
        {
            _livesImage[i].sprite = _liveSprites[2];
        }
    }

    private void CorruptLives()
    {
        for (int i = _nbFullLives; i < _nbFullLives + _nbCorruptedLives; ++i)
        {
            _livesImage[i].sprite = _liveSprites[1];
        }
    }

    public void UpdatePlayerTest()
    {
        //player gets corrupted
        if (Input.GetKeyDown(KeyCode.U))
        {
            if(_nbFullLives > 0 && _nbCorruptedLives < _playerMaxLives - _nbEmptyLives - 1)
            {
                ++_nbCorruptedLives;
                --_nbFullLives;
                UpdateLives();
            }
        }

        //player hits weakpoint
        if (Input.GetKeyDown(KeyCode.Y))
        {
            _nbFullLives += _nbCorruptedLives;
            _nbCorruptedLives = 0;
            UpdateLives();
        }

        //player restores life
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(_nbFullLives <= _playerMaxLives && _nbCorruptedLives > 0)
            {
                ++_nbFullLives;
                --_nbCorruptedLives;
            }
            else if(_nbFullLives < _playerMaxLives)
            {
                ++_nbFullLives;
                --_nbEmptyLives;
            }

            UpdateLives();
        }

        //players gets hit
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (_nbFullLives > 0 && _nbCorruptedLives < _playerMaxLives)
            {
                _nbEmptyLives += _nbCorruptedLives + 1;
                _nbCorruptedLives = 0;
                --_nbFullLives;
                UpdateLives();
            }
        }
    }

    private void Update()
    {
        UpdatePlayerTest();
    }
}