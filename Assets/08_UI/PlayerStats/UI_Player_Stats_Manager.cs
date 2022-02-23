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
    [SerializeField]
    private Text _liquorNumberText;

    //TESTING VARIABLES (NEEDS UPDATE)
    [SerializeField]
    private int _playerMaxLives;
    [SerializeField]
    private int _nbCorruptedLives;
    [SerializeField]
    private int _nbFullLives;
    [SerializeField]
    private int _nbEmptyLives;
    [SerializeField]
    private int _nbRemainingLiquors;

    public void Start()
    {
        initPlayerLivesUI();
        _nbCorruptedLives = 0;
        _nbEmptyLives = 0;
        _nbFullLives = _playerMaxLives;
        UpdateLives();
        UpdateLiquorVisual();
    }

    private void initPlayerLivesUI()
    {
        for(int i = _playerMaxLives; i < _livesImage.Length; ++i)
        {
            _livesImage[i].enabled = false;
        }
    }

    private void UpdateLifeBar()
    {
        for (int i = 0; i < _playerMaxLives; ++i)
        {
            _livesImage[i].enabled = true;
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

    //player restores life
    public void PlayerHeal(int value = 0)
    {
        for(int i = 0; i < value; i++)
        {
            if (_nbFullLives <= _playerMaxLives && _nbCorruptedLives > 0)
            {
                ++_nbFullLives;
                --_nbCorruptedLives;
            }
            else if (_nbFullLives < _playerMaxLives)
            {
                ++_nbFullLives;
                if(_nbEmptyLives > 0)
                    --_nbEmptyLives;
            }
        } 
        UpdateLives();
    }


    //players gets hit
    public bool PlayerGetHit()
    {
        if (_nbFullLives > 0 && _nbCorruptedLives < _playerMaxLives)
        {
            if (_nbCorruptedLives == 0)
            {
                _nbEmptyLives += 1;
                --_nbFullLives;
                UpdateLives();
            }
            else if (_nbCorruptedLives != 0)
            {
                _nbEmptyLives += 2;
                --_nbCorruptedLives;
                --_nbFullLives;
                UpdateLives();
            }
            if (_nbFullLives <= 0)
            {
                _nbCorruptedLives = 0;
                EmptyLives();
                return true;
            }
        }
        else if(_nbFullLives == 0)
        {
            return true;
        }
        return false;
    }


    //player gets corrupted
    public void PlayerGetCorrupted()
    {
        if (_nbFullLives > 0 && _nbCorruptedLives<_playerMaxLives - _nbEmptyLives - 1)
        {
            ++_nbCorruptedLives;
            --_nbFullLives;
            UpdateLives();
        }
    }

    //player hits weakpoint
    public void PlayerHitsWeakpoint()
    {
        _nbFullLives += _nbCorruptedLives;
        _nbCorruptedLives = 0;
        UpdateLives();
    }

    public void ChangeMaxLives(int value)
    {
        Debug.Log(value);
        _playerMaxLives = value;
        PlayerHeal(value);
        initPlayerLivesUI();
        UpdateLifeBar();
    }

    public void ChangeLiquorNumber(int value)
    {
        if(_nbRemainingLiquors-value < 0)
        {
            _nbRemainingLiquors = 0;
        }
        else
        {
            _nbRemainingLiquors += value;
        }
        UpdateLiquorVisual();
    }

    public bool TryHealing()
    {
        if(_nbRemainingLiquors > 0)
        {
            ChangeLiquorNumber(-1);
            return true;
        }
        return false;
    }

    private void UpdateLiquorVisual()
    {
        _liquorNumberText.text = _nbRemainingLiquors.ToString();
    }

    private void Update()
    {
        //UpdatePlayerTest();
    }
}