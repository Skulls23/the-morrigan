using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Optimised way to communicate with animator by hashing string values to int
public static class HashTable
{
    public static int moveV { get; set; }
    public static int moveH { get; set; }

    public static void Init()
    {
        moveV = Animator.StringToHash("forward");
        moveH = Animator.StringToHash("sideways");
    }
}
