using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSkillsSequential : MonoBehaviour
{
    //list of buttons to turn on sequentially 
    [SerializeField] List<SkillButton> skillButtons;
    //this is to turn all the other buttons of at the start
    bool turningOff = true;
    //at what button it currently is
    int unlockedIndex;


    void Start()
    {
        //make the first button run the ClickNext function
        skillButtons[0].onPress += ClickNext;
    }
    private void Update()
    {
        //i had to do this in update because it didn't work in start probably because there are other scripts turning the parent objects of the buttons off in start, that overrode these turning the buttons off. 
        if (turningOff)
        {
            int index = 0;
            foreach (SkillButton button in skillButtons)
            {
                //don't turn the first button off
                if (index > 0)
                {
                    button.turnOff();
                }
                index++;
            }
            turningOff = false;
        }
    }


    public void ClickNext()
    {
        //remove the button from this function
        skillButtons[unlockedIndex].onPress -= ClickNext;
        
        //go to the next button
        unlockedIndex++;
        if (unlockedIndex < skillButtons.Count)
        {
            //make it's click run this function instead
            skillButtons[unlockedIndex].onPress += ClickNext;
            //set it active, this means they don't have to be active from the start,
            //and you can make it so it turns them on outright
            skillButtons[unlockedIndex].gameObject.SetActive(true);
            //turn the button on so you can actually press it
            skillButtons[unlockedIndex].turnOn();
        }
    }
}
