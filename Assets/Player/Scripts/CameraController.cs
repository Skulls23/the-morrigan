using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    Animator anim;
    RotatePlayer rP;
    CharacterMovement CM;

    [SerializeField]
    private bool lockInput;
    public Transform cameraFocus;
    public Transform LockStartPoint;
    public GameObject CharacterCam;
    public GameObject LockOnCamera;
    public GameObject LockOnCamera2;
    public GameObject VFXProto;
    public GameObject VFXProto2;
    public MeshCollider LockZone; // TO DO

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rP = GetComponentInChildren<RotatePlayer>();
        CM = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLock(InputAction.CallbackContext context)
    {
        if (rP.lockPoint)
        {
            lockInput = !lockInput;
            CM.isLockedOn = lockInput;
            anim.SetBool(HashTable.isLockOn, CM.isLockedOn);
            rP.LockedOn = CM.isLockedOn;
            LockLogic(CM.isLockedOn);
        }
    }

    public void OnChangeLock(InputAction.CallbackContext context)
    {
        if (context.performed && CM.isLockedOn)
        {
            LockOnCamera2.SetActive(!LockOnCamera2.activeInHierarchy);
            rP.ennemy2 = !rP.ennemy2;
            VFXProto.SetActive(!rP.ennemy2);
            VFXProto2.SetActive(rP.ennemy2);
        }
    }

    private void LockLogic(bool isLockedOn)
    {
        if (isLockedOn)
        {
            LockOnCamera.SetActive(true);
            VFXProto.SetActive(true);
        }

        else
        {
            LockOnCamera.SetActive(false);
            LockOnCamera2.SetActive(false);
            VFXProto.SetActive(false);
            VFXProto2.SetActive(false);
        }
    }
}
