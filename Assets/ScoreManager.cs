using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public float ComputePlayerScore()
    {
        _timeScore = Math.Max(0f, _TimeBP - _TimeLP * _timeElapsedInGame);
        _hitScore = Math.Max(0f, _HitTakenBP - _HitTakenLP * _nb_hit);
        _liquorScore = _nbUnusedLiquor * _UnusedLiquorsPoints;
        
        return _timeScore + _hitScore + _liquorScore + _mongfindScore(_IsHotelPossesed);
    }

    public string ComputePlayerRank(float score)
    {
        if(score < _bronze)
        {
            return "Aucun pallier atteint";
        }
        else if(score >= _bronze && score < _silver)
        {
            return "Bronze";
        }
        else if(score >= _silver && score < _gold)
        {
            return "Argent";
        }
        else if(score >= _gold && score < _platinum)
        {
            return "Platine";
        }
        else if(score >= _platinum)
        {
            return "Diamant";
        }

        return "player rank could'nt be retrieved";
    }

    public string PointsToNextRank(float score)
    {
        string rank = ComputePlayerRank(score);
        switch (rank)
        {
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
        return _timeScore.ToString();
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
    public string _mongfindLabel(bool _isHotelPossesed) => _IsHotelPossesed ? "Présent de Mongfind" : "???";
}
