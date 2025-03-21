using System;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;


//it's anoying that it has te be done this way, but the
//scripts of the fps microgame are in a different code asembly,
//and i can't just add a using tag to make it work for some
//reason, and i couldn't find a way to work around that elegantly
public class SkillSocket : MonoBehaviour
{
    //all the skill variables again
    [Space(15)]
    [Header("skill levels:")]
    [Space(5)]
    public float jumpHeight = 1;
    public float moveSpeed = 1;
    public float health = 1;
    public float jetpack = 1;
    [Space(10)]
    public float globalDamage = 1;
    public float globalFireRate = 1;
    [Space(15)]
    [Header("gun specific values:")]
    [Space(5)]
    [Header("handgun")]
    public float hGDamage = 1;
    public float hGFireRate = 1;
    [Space(10)]
    [Header("launcher")]
    public float lDamage = 1;
    public float lFireRate = 1;
    [Space(10)]
    [Header("shotgun")]
    public float sGDamage = 1;
    public float sGFireRate = 1;

    //for when the health value gets changed
    public Action healthSkillChanged;

    public Action<int> skillPointsAwarded;

    //i got this from a tutorial about singletons, i edited their example to get to this
    //https://gamedevbeginner.com/singletons-in-unity-the-right-way/#game_manager_singletons
    public static SkillSocket Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }
}
