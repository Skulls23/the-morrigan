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
    private bool hasMove = false;

    public void Update()
    {
        if(_timeBeforeMovePopup > 0f)
        {
            _timeBeforeMovePopup -= Time.deltaTime;
        }

        if(_timeBeforeMovePopup < 0f && !_movePopup && !hasMove)
        {
            DisplayMovePopup(true);
            _movePopup = true;
        }
    }

    public void DisplayMovePopup(bool set)
    {
        _MovePopupSprite.SetActive(set);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        hasMove = true;
    }
}
