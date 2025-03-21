using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.FPS;
using TMPro;


//this works with both ui buttons and toggles looking back, i probably could and should have made them sepperate
public class SkillButton : MonoBehaviour
{ 
    [SerializeField] private int skillCost;
    [SerializeField] private skill skillToChange;
    [SerializeField] private float newValue;
    //if you want it to multiply the current skill level or set it manuall you could accidentilly lower a skill with this set false, so i have to take care
    [SerializeField] private bool multiplier = true;
    //do you want to turn the button off after use
    [SerializeField] private bool turnButtonOff = true;
    //only relevant if you don't turn off the button after use, this makes it so the next time you use the button it gets more expensive
    [SerializeField] private float costMultiplier = 1;

    private Toggle Toggle;
    private Button Button;
    private TMP_Text skillCostText;

    //signal other scripts that the button has been pressed
    public Action onPress;
    private bool buttonActivated;

    private void Start()
    {
        //try to get either the button or toggle component
        if (GetComponent<Button>())
        {
            Button = GetComponent<Button>();
            //make the button run the ButtonPressed function
            Button.onClick.AddListener(ButtonPressed);
        }

        if (GetComponent<Toggle>())
        {
            Toggle = GetComponent<Toggle>();
            //make the toggle run the TogglePressed function
            Toggle.onValueChanged.AddListener(TogglePressed);
        }
        //turnOff();

        //find the skillcost text component
        skillCostText = GetComponentInChildren<TMP_Text>();
        skillCostText.text = skillCost.ToString();
    }

    public void ButtonPressed()
    {
        //if you have enough skillpoints
        if (SkillData.Instance.skillPoints >= skillCost)
        {
            //subtract the needed skillpoints
            SkillData.Instance.AddSkillPoints(-skillCost);
            //increase the future cost
            float temp = skillCost * costMultiplier;
            skillCost = (int)temp;
            //update the text on the button
            skillCostText.text = skillCost.ToString();
            //update the skill that is getting leveled up
            SkillData.Instance.SetSkill(skillToChange, newValue, multiplier);
            if (turnButtonOff)
            {
                //turn off the button component if it's only supposed to be able to be used once
                setActivated();
            }

            //run things in whatever script might be listening to this button
            onPress?.Invoke();
        }   
    }
    
    //basically the same as the button, but the toggle component makes it so that when you press it, it shows a  checkmark 
    public void TogglePressed(bool turnedOn)
    {
        if (SkillData.Instance.skillPoints >= skillCost)
        {
            SkillData.Instance.AddSkillPoints(-skillCost);
            float temp = skillCost * costMultiplier;
            skillCost = (int)temp;
            skillCostText.text = skillCost.ToString();
            SkillData.Instance.SetSkill(skillToChange, newValue, multiplier);
            if (turnButtonOff)
            {
                setActivated();
            }
            onPress?.Invoke();
        } else
        {
            Toggle.isOn = false;
        }
    }




    //setActivated and unActivated are for removing the button functionality completely while turnOff and On are for still showing the button, but have it be grayed out
    private void setActivated()
    {
        buttonActivated = true;
        if (Toggle)
        {
            Toggle.enabled = false;
        }

        if (Button)
        {
            Button.enabled = false;
        }

        skillCostText.gameObject.SetActive(false);
    }
    private void setUnActivated()
    {
        buttonActivated = false;
        if (Toggle)
        {
            Toggle.enabled = true;
        }

        if (Button)
        {
            Button.enabled = true;
        }

        skillCostText.gameObject.SetActive(true);
    }

    public void turnOff()
    {
        if (Toggle)
        {
            Toggle.interactable = false;
        }

        if (Button)
        {
            Button.interactable = false;
        }
    }

    public void turnOn()
    {
        if (Toggle)
        {
            Toggle.interactable = true;
        }

        if (Button)
        {
            Button.interactable = true;
        }
    }
}
