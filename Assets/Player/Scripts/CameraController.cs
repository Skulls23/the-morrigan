using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    Animator anim;
    RotatePlayer rP;
    CharacterMovement CM;
    public DetectionConeController DDC;
    public LockLooker LL;

    [SerializeField]
    private bool lockInput;
    public GameObject lockedEnemy;
    public Transform cameraFocus;
    public Transform LockStartPoint;
    public GameObject CharacterCam;
    public GameObject LockOnCamera;
    public GameObject LockOnCamera2;
    public MeshCollider LockZone; // TO DO

    public Transform DotTransform;
    public Image DotImage;

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
        if (CM.isLockedOn && lockedEnemy!=null)
        {
            DotTransform.position = Camera.main.WorldToScreenPoint(lockedEnemy.GetComponent<Enemy>().LockPoint.transform.position);
        }
    }

    private void FixedUpdate()
    {
        if (CM.isLockedOn)
        {
            CharacterCam.GetComponent<Cinemachine.CinemachineFreeLook>().UpdateCameraState(Vector3.up, Time.fixedDeltaTime);
        }
    }

    public void OnLock(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (lockedEnemy == null)
            {
                Debug.Log("Lock");

                //Assignation of enemy variable
                lockedEnemy = DDC.SelectTarget();
                if (lockedEnemy)
                {
                    Debug.Log(lockedEnemy.name);
                    LL.SetEnemy(lockedEnemy);

                    //Enable isLocked On on all scripts
                    lockInput = !lockInput;
                    CM.isLockedOn = lockInput;
                    anim.SetBool(HashTable.isLockOn, CM.isLockedOn);
                    rP.LockedOn = CM.isLockedOn;
                    LockLogic(CM.isLockedOn);

                    //Enable lock FX
                    DotImage.enabled = true;
                }  
            }
            else
            {
                DeLock();
            }
        }
    }

    public void DeLock()
    {
        Debug.Log("De-Lock");

        //Reset locked Enemy Variable
        lockedEnemy = null;
        LL.SetEnemy(null);

        //Disable isLocked On on all scripts
        lockInput = !lockInput;
        CM.isLockedOn = false;
        anim.SetBool(HashTable.isLockOn, CM.isLockedOn);
        rP.LockedOn = CM.isLockedOn;
        LockLogic(CM.isLockedOn);

        //Disable lock FX
        DotImage.enabled = false;
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
                        lockedEnemy = tempEnemy;
                        LL.SetEnemy(lockedEnemy);
                        LockOnCamera2.SetActive(!LockOnCamera2.activeInHierarchy);
                    }
                }
            }

            if (!canSwapEnemy && (Mathf.Abs(joysticValue.x) < 0.2f && Mathf.Abs(joysticValue.y) < 0.2f))
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
