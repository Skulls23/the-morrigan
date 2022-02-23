using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Management")]
    [Tooltip("This is the Time Base Points")]
    public float _TimeBP = 100000f;
    [Tooltip("Amount of points lost every second - substracted to _TimeBP")]
    public float _TimeLP = 100f;
    [Tooltip("This is the Hit Taken Base Points")]
    public float _HitTakenBP = 100000f;
    [Tooltip("Amount of points lost every hit taken by the player - substracted to _HitTakenBP")]
    public float _HitTakenLP = 2000f;
    [Tooltip("Amount of points granted to the player if Mongfind's Gift")]
    public float _GiftedPoints = 100000f;
    [Tooltip("Amount of points granted to the player for each liquor which was not used")]
    public float _UnusedLiquorsPoints = 20000f;

    public float _timeElapsedInGame = 0f;
    public bool _IsTimeElapsing = false;

    public float _nb_hit = 0f;
    public float _nbUnusedLiquor;
    public bool _IsHotelPossesed;

    public float _timeScore;
    public float _hitScore;
    public float _liquorScore;

    private int _bronze = 50000;
    private int _silver = 100000;
    private int _gold = 150000;
    private int _platinum = 300000;
    private int _diamond = 500000;

    private float score = 0f;

    // Texts and labels
    [SerializeField]
    private Text _timeElapsedString;
    [SerializeField]
    private Text _timeScoreString;
    [SerializeField]
    private Text _nbHitsString;
    [SerializeField]
    private Text _nbHitsScoreString;
    [SerializeField]
    private Text _nbUnusedLiquorString;
    [SerializeField]
    private Text _nbUnusedLiquorcoreString;
    [SerializeField]
    private Text _hotelLabelString;
    [SerializeField]
    private Text _hotelResultString;
    [SerializeField]
    private Text _hotelScoreString;
    [SerializeField]
    private Text _totalScoreString;
    [SerializeField]
    private Text _rankString;
    [SerializeField]
    private Text _pointsToNextRankString;
    [SerializeField]
    private Image _badgeSlot;

    [SerializeField]
    private Sprite[] rankSprites;

    public void Start()
    {
        Debug.Log(ThousandsSeparator(66666666.4f));
    }

    public void Update()
    {
        if (_IsTimeElapsing)
        {
            _timeElapsedInGame += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            DisplayScore();
        }
    }

    public string ComputePlayerScore()
    {
        _timeScore = Math.Max(0f, _TimeBP - _TimeLP * _timeElapsedInGame);
        _hitScore = Math.Max(0f, _HitTakenBP - _HitTakenLP * _nb_hit);
        _liquorScore = _nbUnusedLiquor * _UnusedLiquorsPoints;

        score = _timeScore + _hitScore + _liquorScore + _mongfindScore(_IsHotelPossesed);
        return ThousandsSeparator(_timeScore + _hitScore + _liquorScore + _mongfindScore(_IsHotelPossesed));
    }

    public string ComputePlayerRank(float score)
    {
        if(score < _bronze)
        {
            _badgeSlot.sprite = rankSprites[0];
            return "Aucun pallier atteint";
        }
        else if(score >= _bronze && score < _silver)
        {
            _badgeSlot.sprite = rankSprites[1];
            return "Bronze";
        }
        else if(score >= _silver && score < _gold)
        {
            _badgeSlot.sprite = rankSprites[2];
            return "Argent";
        }
        else if(score >= _gold && score < _platinum)
        {
            _badgeSlot.sprite = rankSprites[3];
            return "Or";
        }
        else if (score >= _platinum && score < _diamond)
        {
            _badgeSlot.sprite = rankSprites[4];
            return "Platine";
        }
        else if(score >= _diamond)
        {
            _badgeSlot.sprite = rankSprites[5];
            return "Diamant";
        }

        return "player rank could'nt be retrieved";
    }

    public string PointsToNextRank(float score)
    {
        string rank = ComputePlayerRank(score);
        switch (rank)
        {
            case "Aucun pallier atteint":
                return ThousandsSeparator(_bronze - score);
            case "Bronze":
                return ThousandsSeparator(_silver - score);
            case "Argent":
                return ThousandsSeparator(_gold - score);
            case "Or":
                return ThousandsSeparator(_platinum - score);
            case "Platine":
                return ThousandsSeparator(_diamond - score);
            case "Diamant":
                return "Vous avez atteint le dernier rang !";
            default:
                return "either score or rank could'nt be retrieved";
        }
    }

    public string TimeScore()
    {
        return ThousandsSeparator(_timeScore);
    }

    // return time elapsed in game following FormatTime
    public string TimeElapsed()
    {
        return FormatTime(_timeElapsedInGame);
    }

    // misc
    public string FormatTime(float _time)
    {
        int minutes = (int)_time / 60;
        int seconds = (int)_time - 60 * minutes;
        int hundredths = (int) (1000 * (_timeElapsedInGame - minutes * 60 - seconds));

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }

    public string ThousandsSeparator(float n)
    {
        return string.Format("{0:n0}", n);
    }

    public float _mongfindScore(bool _IsHotelPossesed) => _IsHotelPossesed ? _GiftedPoints : 0f;
    public string _mongfindLabel(bool _isHotelPossesed) => _IsHotelPossesed ? "Présent de Mongfind" : "??????";
    public string _mongfindResult(bool _isHotelPossesed) => _IsHotelPossesed ? "Oui" : "-";


    void DisplayScore()
    {
        _totalScoreString.text = ComputePlayerScore();
        _timeElapsedString.text = TimeElapsed();
        _timeScoreString.text = TimeScore();
        _nbHitsString.text = _nb_hit.ToString();
        _nbHitsScoreString.text = ThousandsSeparator(_hitScore);
        _nbUnusedLiquorString.text = _nbUnusedLiquor.ToString();
        _nbUnusedLiquorcoreString.text = ThousandsSeparator(_liquorScore);
        _hotelLabelString.text = _mongfindLabel(_IsHotelPossesed);
        _hotelResultString.text = _mongfindResult(_IsHotelPossesed);
        _hotelScoreString.text = ThousandsSeparator(_mongfindScore(_IsHotelPossesed));
        _rankString.text = ComputePlayerRank(score);
        _pointsToNextRankString.text = PointsToNextRank(score);
    }
}
