using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rb;
    private Animator anim;
    private RotatePlayer rP;
    
    [Header("Values")]
    [SerializeField]
    private Vector2 dir;
    [SerializeField]
    Vector2 dirAbs;

    [Header("Game Design")]
    [SerializeField]
    private float speed;
    [SerializeField, Range(0,1)]
    private float startWalkingValue;
    [SerializeField, Range(0, 1)]
    private float startJogingValue;


    // Start is called before the first frame update
    void Start()
    {
        HashTable.Init();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        rP = GetComponentInChildren<RotatePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    //Event getting the values on controller left joystic, keyboard arrows and WASD
    public void OnMove(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<Vector2>();
        rP.dir = dir;
        dirAbs = RoundValues();
    }

    //Updates the animator movement layer and the player velocity
    private void ApplyMovement()
    {
        anim.SetFloat(HashTable.moveH, dirAbs.x <= dirAbs.y ? dirAbs.y : dirAbs.x, 0.2f, Time.fixedDeltaTime);
        rb.velocity = new Vector3(dir.x * speed, rb.velocity.y, dir.y * speed);
    }

    //Round values for game design purpose
    private Vector2 RoundValues()
    {
        Vector2 tempDirAbs = new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y));

        if (tempDirAbs.x > startWalkingValue)
            if (tempDirAbs.x > startJogingValue) tempDirAbs.x = 1;
            else tempDirAbs.x = 0.5f;
        else tempDirAbs.x = 0;

        if (tempDirAbs.y > startWalkingValue)
            if (tempDirAbs.y > startJogingValue) tempDirAbs.y = 1;
            else tempDirAbs.y = 0.5f;
        else tempDirAbs.y = 0;

        return tempDirAbs;
    }
}
