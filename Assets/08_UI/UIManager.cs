using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _playerInput;
    [SerializeField]
    private BlessingManager _blessingManager;

    public enum _navDirs
    {
        _RIGHT = 0,
        _LEFT = 1,
        _UP = 2,
        _DOWN = 3,
        _DEFAULT = 4
    }

    public bool _UIActive = false;
    public Vector2 _navDir;
    
    public void Update()
    {
        if (_UIActive) _playerInput.SwitchCurrentActionMap("UI");
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _navDir = context.ReadValue<Vector2>();
            Debug.Log("perform : " + context);
            performActionOnNavigate();
        }
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            performActionOnSubmit();
        }
    }

    public void performActionOnSubmit()
    {
        if (_blessingManager._blessingSelecting)
        {
            _blessingManager.MoveIntoAssignement();
        }
        else if (_blessingManager._blessingAssigning)
        {
            _blessingManager.ValidateAssignation();
        }
    }

    public void performActionOnNavigate()
    {
        if (_blessingManager._blessingSelecting)
        {
            if(_navDir == Vector2.left)
            {
                if(_blessingManager.indexOfActiveCollection == 0)
                {
                    _blessingManager.indexOfActiveCollection = 1;
                }
                else
                {
                    _blessingManager.indexOfActiveCollection = --_blessingManager.indexOfActiveCollection % _blessingManager.activeCollection.Length;
                }
                _blessingManager.ActivateBlessingCard(_blessingManager.activeCollection, 
                    _blessingManager.indexOfActiveCollection);
            }
            else if(_navDir == Vector2.right)
            {
                _blessingManager.indexOfActiveCollection = ++_blessingManager.indexOfActiveCollection % _blessingManager.activeCollection.Length;
                _blessingManager.ActivateBlessingCard(_blessingManager.activeCollection,
                    _blessingManager.indexOfActiveCollection);
            }
        }
        else if (_blessingManager._blessingAssigning)
        {
            if (_navDir == Vector2.left)
            {
                if (_blessingManager.indexOfActiveCollection == 0)
                {
                    _blessingManager.indexOfActiveCollection = 1;
                }
                else
                {
                    _blessingManager.indexOfActiveCollection = --_blessingManager.indexOfActiveCollection % _blessingManager.activeCollection.Length;
                }
                _blessingManager.ActivateBlessingSlot(_blessingManager.activeCollection,
                    _blessingManager.indexOfActiveCollection, _blessingManager.activeBlessingToAssign);
            }
            else if (_navDir == Vector2.right)
            {
                _blessingManager.indexOfActiveCollection = ++_blessingManager.indexOfActiveCollection % _blessingManager.activeCollection.Length;
                _blessingManager.ActivateBlessingSlot(_blessingManager.activeCollection,
                    _blessingManager.indexOfActiveCollection, _blessingManager.activeBlessingToAssign);
            }
        }
    }
}
