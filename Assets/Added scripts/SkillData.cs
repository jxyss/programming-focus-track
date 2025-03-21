using System;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using Unity.FPS.Gameplay;

//defining which skills can be changed:
public enum skill
{
    jumpHeigth,
    moveSpeed,
    health,
    jetpack,
    globalDamage,
    globalFireRate,
    //there could maybe have been a more efficent way to handle the different types of guns but i couldnt think of one rn
    hGDamage,
    hGFireRate,
    lDamage,
    lFireRate,
    sGDamage,
    sGFireRate,
    everything
}


public class SkillData : MonoBehaviour
{
    //these are needed for unlocking the jetpack and the guns
    [SerializeField] Jetpack jetpackScript;
    [SerializeField] PlayerWeaponsManager weaponsManager;

    //all the prefabs of all possible unlockable guns
    [SerializeField] WeaponController handGunPrefab;
    [SerializeField] WeaponController launcherPrefab;
    [SerializeField] WeaponController shotgunPrefab;

    //setting up the skillpoints
    [Space(10)]
    [SerializeField] private int startingSkillPoints;

    //this is for the editor
    [NonSerialized]
    public int skillPoints;


    //all the skill multipliers:
    [Space(15)]
    [Header("skill levels:")]
    [Space(5)]
    public float jumpHeight = 1;
    public float moveSpeed = 1;
    public float health = 1;
    public float jetpack = 1;
    [Space(10)]
    [Header("gun values:")]
    [Space(5)]
    public float globalDamage = 1;
    public float globalFireRate = 1;
    [Space(15)]
    [Header("specific gun values:")]
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


    //an action that gets invoked any time a skill gets changed
    public static Action<skill, float> skillChange;
    //an action specifically for the health i had to do it this way as there didnt seem like a better way to do it without running a function in the health script
    public static Action healthSkillChanged;

    //i got this from a tutorial about singletons i edited their example to get to this that can be found in the referneces 
    public static SkillData Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }


    private void Start()
    {
        //i have to siphon all values and actions to the skillSocket script, because it is in two different asemblys, and i couldn't reference this script from the inbuilt fpsgame scripts idk why ;;
        healthSkillChanged += SkillSocket.Instance.healthSkillChanged;

        //setting up the skillpoints
        skillPoints = 0;
        AddSkillPoints(startingSkillPoints);
        SkillSocket.Instance.skillPointsAwarded += AddSkillPoints;
    }

    public void SetSkill(skill skillToChange, float newValue, bool mult = true)
    {
        //nothing would normally set a skill back to 1, so i thought this would be a good way to unlock things
        if (newValue == 1 && !mult)
        {
            UnlockSkill(skillToChange);
            return;
        }

        //do you want it to multiply the current value, or just set it outright
        if (mult)
        {
            //check which skill is being changed
            if (skillToChange == skill.jumpHeigth)
            {
                //multiply the current value by the newValue
                jumpHeight *= newValue;
                //set the corrosponding skill on the skillSocket to the correct value
                SkillSocket.Instance.jumpHeight = jumpHeight;
            }
            else if (skillToChange == skill.moveSpeed)
            {
                //do this for all others
                moveSpeed *= newValue;
                SkillSocket.Instance.moveSpeed = moveSpeed;
            }
            else if (skillToChange == skill.health)
            {
                health *= newValue;
                SkillSocket.Instance.health = health;
                //except for health, as that also needs to invoke a function
                healthSkillChanged.Invoke();
            }
            else if (skillToChange == skill.jetpack)
            {
                jetpack *= newValue;
                SkillSocket.Instance.jetpack = jetpack;
            }
            else if (skillToChange == skill.hGDamage)
            {
                hGDamage *= newValue;
                SkillSocket.Instance.hGDamage = hGDamage;
            }
            else if (skillToChange == skill.hGFireRate)
            {
                hGFireRate *= newValue;
                SkillSocket.Instance.hGFireRate = hGFireRate;
            }
            else if (skillToChange == skill.lDamage)
            {
                lDamage *= newValue;
                SkillSocket.Instance.lDamage = lDamage;
            }
            else if (skillToChange == skill.lFireRate)
            {
                lFireRate *= newValue;
                SkillSocket.Instance.lFireRate = lFireRate;
            }
            else if (skillToChange == skill.sGDamage)
            {
                sGDamage *= newValue;
                SkillSocket.Instance.sGDamage = sGDamage;
            }
            else if (skillToChange == skill.sGFireRate)
            {
                sGFireRate *= newValue;
                SkillSocket.Instance.sGFireRate = sGFireRate;
            }
            else if (skillToChange == skill.everything)
            {
                jumpHeight *= newValue;
                moveSpeed *= newValue;
                health *= newValue;
                jetpack *= newValue;
                hGDamage *= newValue;
                hGFireRate *= newValue;
                lDamage *= newValue;
                lFireRate *= newValue;
                sGDamage *= newValue;
                sGFireRate *= newValue;

                SkillSocket.Instance.jumpHeight = jumpHeight;
                SkillSocket.Instance.moveSpeed = moveSpeed;
                SkillSocket.Instance.health = health;
                SkillSocket.Instance.jetpack = jetpack;
                SkillSocket.Instance.hGDamage = hGDamage;
                SkillSocket.Instance.hGFireRate = hGFireRate;
                SkillSocket.Instance.lDamage = lDamage;
                SkillSocket.Instance.lFireRate = lFireRate;
                SkillSocket.Instance.sGDamage = sGDamage;
                SkillSocket.Instance.sGFireRate = sGFireRate;

                healthSkillChanged.Invoke();
            }
            else
            {
                //if the skill is in the skill enum, but not in this if chain
                Debug.LogWarning("skill not implemented yet");
            }
        }
        else
        {
            //if you want to manually set the skills value instead of multiplying

            if (skillToChange == skill.jumpHeigth)
            {
                //just set the correct skill to the new value
                jumpHeight = newValue;
                //do the same for the skillSocket
                SkillSocket.Instance.jumpHeight = jumpHeight;
            }
            else if (skillToChange == skill.moveSpeed)
            {
                moveSpeed = newValue;
                SkillSocket.Instance.moveSpeed = moveSpeed;
            }
            else if (skillToChange == skill.health)
            {
                health = newValue;
                SkillSocket.Instance.health = health;
                //invoke the health action
                healthSkillChanged.Invoke();
            }
            else if (skillToChange == skill.jetpack)
            {
                jetpack = newValue;
                SkillSocket.Instance.jetpack = jetpack;
            }
            else if (skillToChange == skill.hGDamage)
            {
                hGDamage = newValue;
                SkillSocket.Instance.hGDamage = hGDamage;
            }
            else if (skillToChange == skill.hGFireRate)
            {
                hGFireRate = newValue;
                SkillSocket.Instance.hGFireRate = hGFireRate;
            }
            else if (skillToChange == skill.lDamage)
            {
                lDamage = newValue;
                SkillSocket.Instance.lDamage = lDamage;
            }
            else if (skillToChange == skill.lFireRate)
            {
                lFireRate = newValue;
                SkillSocket.Instance.lFireRate = lFireRate;
            }
            else if (skillToChange == skill.sGDamage)
            {
                sGDamage = newValue;
                SkillSocket.Instance.sGDamage = sGDamage;
            }
            else if (skillToChange == skill.sGFireRate)
            {
                sGFireRate = newValue;
                SkillSocket.Instance.sGFireRate = sGFireRate;
            }
            else if (skillToChange == skill.everything)
            {
                jumpHeight = newValue;
                moveSpeed = newValue;
                health = newValue;
                jetpack = newValue;
                hGDamage = newValue;
                hGFireRate = newValue;
                lDamage = newValue;
                lFireRate = newValue;
                sGDamage = newValue;
                sGFireRate = newValue;

                SkillSocket.Instance.jumpHeight = jumpHeight;
                SkillSocket.Instance.moveSpeed = moveSpeed;
                SkillSocket.Instance.health = health;
                SkillSocket.Instance.jetpack = jetpack;
                SkillSocket.Instance.hGDamage = hGDamage;
                SkillSocket.Instance.hGFireRate = hGFireRate;
                SkillSocket.Instance.lDamage = lDamage;
                SkillSocket.Instance.lFireRate = lFireRate;
                SkillSocket.Instance.sGDamage = sGDamage;
                SkillSocket.Instance.sGFireRate = sGFireRate;

                healthSkillChanged.Invoke();
            }
            else
            {
                //added this to know if the skill is in the skill enum, but not in this if chain
                Debug.LogWarning("skill not implemented yet");
            }
        }

     


        //invoke the action for a skill getting changed
        skillChange?.Invoke(skillToChange, newValue);
    }

    //to add skillpoints, this would normally run after an enemy dies:
    public void AddSkillPoints(int Amount)
    {
        skillPoints += Amount;

        foreach (SkillPointsVisual skillPointsVisual in SkillPointsVisual.Instances)
        {
            skillPointsVisual.UpdateSkillPoints(skillPoints);
        }
        
    }

    //for unlocking things
    public void UnlockSkill(skill skillToUnlock)
    {
        //for unlocking the jetpack
        if (skillToUnlock == skill.jetpack)
        {
            //TryUnlock does what it says, it tries to unlock the jetpack, returns false if it's already unlocked
            jetpackScript.TryUnlock();
        }
        // for unlocking the handgun
        else if (skillToUnlock == skill.hGDamage)
        {
            //try to add the handgun, if it's already unlocked, this will return false
            if (weaponsManager.AddWeapon(handGunPrefab))
            {
                //if there is no weapon selected, select the handgun
                if (weaponsManager.GetActiveWeapon() == null)
                {
                    weaponsManager.SwitchWeapon(true);
                }
            }
        }
        //for unlocking the launcher
        else if (skillToUnlock == skill.lDamage)
        {
            //same as the handgun
            if (weaponsManager.AddWeapon(launcherPrefab))
            {
                if (weaponsManager.GetActiveWeapon() == null)
                {
                    weaponsManager.SwitchWeapon(true);
                }
            }
        }
        //for unlocking the shotgun
        else if (skillToUnlock == skill.sGDamage)
        {
            if (weaponsManager.AddWeapon(shotgunPrefab))
            {
                if (weaponsManager.GetActiveWeapon() == null)
                {
                    weaponsManager.SwitchWeapon(true);
                }
            }
        }
    }
}

