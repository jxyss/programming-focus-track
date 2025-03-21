using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[System.Serializable]
public class ListOfGameObjects
{
    public List<GameObject> gameObjects;
}

//code for making buttons reveal certain things, used in this project for showing other buttons
public class UnlockSkills : MonoBehaviour
{
    //each skillbutton that can reveal things it is sequential, so the first button needs to be pressed first, second second, etc etc
    [SerializeField] List<SkillButton> skillButtons;
    //a list with lists of gameobjects to turn on, so one button can turn on any number of things
    [SerializeField] List<ListOfGameObjects> thingsToTurnOn;

    //at what button it currently is
    int unlockedIndex;

    void Start()
    {
        //first turn off all things to later tun on
        foreach (ListOfGameObjects list in thingsToTurnOn)
        {
            turnOff(list);
        }

        //make the first button run the ClickNext function
        skillButtons[0].onPress += ClickNext;
    }

    public void ClickNext()
    {
        //remove the button from this function
        skillButtons[unlockedIndex].onPress -= ClickNext;

        //if there are enough lists of things to turn on
        if (thingsToTurnOn.Count > unlockedIndex)
        {
            //turn on all gameobjects in the next list
            turnOn(thingsToTurnOn[unlockedIndex]);
        }
        //go to the next button
        unlockedIndex++;
        if (unlockedIndex < skillButtons.Count)
        {
            //make the next button run the clicknext function
            skillButtons[unlockedIndex].onPress += ClickNext;
            
        }
    }

    //functions for turning all gameobjects in a listOfGameObjects on and off
    private void turnOn(ListOfGameObjects objects)
    {
        //just loop over all of them,
        foreach (GameObject obj in objects.gameObjects)
        {
            //and turn them all on
            obj.SetActive(true);
        }
    }
    private void turnOff(ListOfGameObjects objects)
    {
        foreach (GameObject obj in objects.gameObjects)
        {
            //turn all of them off here
            obj.SetActive(false);
        }
    }


}
