using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    Animator anim;
    RotatePlayer rP;
    CharacterMovement CM;
    public DetectionConeController DDC;

    [SerializeField]
    private bool lockInput;
    public GameObject lockedEnemy;
    public Transform cameraFocus;
    public Transform LockStartPoint;
    public GameObject CharacterCam;
    public GameObject LockOnCamera;
    public GameObject LockOnCamera2;
    public MeshCollider LockZone; // TO DO

    public bool canSwapEnemy = true;

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

    private void FixedUpdate()
    {
        if (CM.isLockedOn)
        {
            CharacterCam.GetComponent<Cinemachine.CinemachineFreeLook>().UpdateCameraState(Vector3.up, Time.fixedDeltaTime);
            /*if (LockOnCamera2.activeInHierarchy)
            {
                LockOnCamera2.GetComponent<Cinemachine.CinemachineFreeLook>().ForceCameraPosition(CharacterCam.transform.position, CharacterCam.transform.rotation);
            }
            else
            {
                LockOnCamera.GetComponent<Cinemachine.CinemachineFreeLook>().ForceCameraPosition(CharacterCam.transform.position, CharacterCam.transform.rotation);
            }*/
        }
    }

    public void OnLock(InputAction.CallbackContext context)
    {
        if (lockedEnemy == null)
        {
            lockedEnemy = DDC.SelectTarget(context.ReadValue<Vector2>());
            lockedEnemy.GetComponent<Enemy>().LockPoint.SetActive(true);
            LockOnCamera.GetComponent<Cinemachine.CinemachineFreeLook>().LookAt = lockedEnemy.transform;
            lockInput = !lockInput;
            CM.isLockedOn = lockInput;
            anim.SetBool(HashTable.isLockOn, CM.isLockedOn);
            rP.LockedOn = CM.isLockedOn;
            LockLogic(CM.isLockedOn);
        }
        else
        {
            lockedEnemy.GetComponent<Enemy>().LockPoint.SetActive(false);
            lockedEnemy = null;
        }     
    }

    public void OnChangeLock(InputAction.CallbackContext context)
    {
        if (CM.isLockedOn)
        {
            Vector2 joysticValue = context.ReadValue<Vector2>();
            if ((Mathf.Abs(joysticValue.x) > 0.2f || Mathf.Abs(joysticValue.y) > 0.2f) && canSwapEnemy)
            {
                canSwapEnemy = false;
                if (context.performed && CM.isLockedOn)
                {
                    GameObject tempEnemy = DDC.SelectTarget(context.ReadValue<Vector2>(), lockedEnemy);
                    if (tempEnemy != null && lockedEnemy != tempEnemy)
                    {
                        lockedEnemy.GetComponent<Enemy>().LockPoint.SetActive(false);
                        lockedEnemy = tempEnemy;

                        if (LockOnCamera2.activeInHierarchy)
                        {
                            LockOnCamera.GetComponent<Cinemachine.CinemachineFreeLook>().LookAt = lockedEnemy.transform;
                        }
                        else
                        {
                            LockOnCamera2.GetComponent<Cinemachine.CinemachineFreeLook>().LookAt = lockedEnemy.transform;
                        }

                        lockedEnemy.GetComponent<Enemy>().LockPoint.SetActive(true);
                        LockOnCamera2.SetActive(!LockOnCamera2.activeInHierarchy);
                    }
                }
            }

            if (!canSwapEnemy && (Mathf.Abs(joysticValue.x) < 0.1f && Mathf.Abs(joysticValue.y) < 0.1f))
            {
                canSwapEnemy = true;
            }
        }
    }

    private void LockLogic(bool isLockedOn)
    {
        if (isLockedOn)
        {
            LockOnCamera.SetActive(true);
        }

        else
        {
            LockOnCamera.SetActive(false);
            LockOnCamera2.SetActive(false);
        }
    }
}
