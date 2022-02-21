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

    private float _timeElapsedInGame = 0f;
    public bool _IsTimeElapsing = false;

    public float _nb_hit = 0f;
    public float _nbUnusedLiquor;
    public bool _IsHotelPossesed;

    public void Update()
    {
        if (_IsTimeElapsing)
        {
            _timeElapsedInGame += Time.deltaTime;
        }
    }

    public float ComputePlayerScore()
    {
        float _timeScore = Math.Max(0f, _TimeBP - _TimeLP * _timeElapsedInGame);
        float _hitScore = Math.Max(0f, _HitTakenBP - _HitTakenLP * _nb_hit);
        float _liquorScore = _nbUnusedLiquor * _UnusedLiquorsPoints;
        
        return _timeScore + _hitScore + _liquorScore + _mongfindScore(_IsHotelPossesed);
    }

    public float _mongfindScore(bool _IsHotelPossesed) => _IsHotelPossesed ? _GiftedPoints : 0f;
}
