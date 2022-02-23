using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnboardingManager : MonoBehaviour
{
    [SerializeField]
    private float _timeBeforeMovePopup = 5f;

    [SerializeField]
    private float _waitBeforeReceiveInput = 2f;

    [SerializeField]
    private GameObject _MovePopupSprite;

    private bool _movePopup = false;
    public bool hasMove = false;
    private bool _onboardMovePopup = true;

    public void Update()
    {
        if (_onboardMovePopup)
        {
            if (_timeBeforeMovePopup > 0f)
            {
                _timeBeforeMovePopup -= Time.deltaTime;
            }

            if (_timeBeforeMovePopup < 0f && !_movePopup && !hasMove)
            {
                DisplayMovePopup(true);
                _movePopup = true;
            }

            if (_movePopup)
            {
                _waitBeforeReceiveInput -= Time.deltaTime;
            }

            if (_movePopup && hasMove && _waitBeforeReceiveInput < 0f)
            {
                DisplayMovePopup(false);
                _onboardMovePopup = false;
            }
        }
    }

    public void DisplayMovePopup(bool set)
    {
        _MovePopupSprite.SetActive(set);
    }

    public void OnMove()
    {
        hasMove = true;
    }
}
