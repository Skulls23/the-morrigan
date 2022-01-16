using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Optimised way to communicate with animator by hashing string values to int
public static class HashTable
{
    public static int moveV { get; set; }
    public static int moveH { get; set; }
    public static int dirX { get; set; }
    public static int dirZ { get; set; }
    public static int isLockOn { get; set; }

    public static int dodged { get; set; }
    public static int attacked { get; set; }

    public static void Init()
    {
        moveV = Animator.StringToHash("forward");
        moveH = Animator.StringToHash("sideways");
        dirX = Animator.StringToHash("dirX");
        dirZ = Animator.StringToHash("dirZ");
        isLockOn = Animator.StringToHash("isLockOn");
        dodged = Animator.StringToHash("dodged");
        attacked = Animator.StringToHash("attacked");
    }
}
