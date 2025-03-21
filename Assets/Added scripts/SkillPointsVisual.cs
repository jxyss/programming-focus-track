using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillPointsVisual : MonoBehaviour
{
    private TMP_Text skillPointsTextBox;

    //i got this from a tutorial about singletons, i edited their example to get to this that can be found in references
    //i modified this one so i could have a list of SkillPointsVisual scripts so i could change the value in multiple positions at once
    public static List<SkillPointsVisual> Instances { get; private set; }

    private void Awake()
    {
        if (Instances != null)
        {
            if (Instances.Contains(this) == false)
            {
                Instances.Add(this);
            }
        } else
        {
            Instances = new List<SkillPointsVisual>();
            Instances.Add(this);
        }

        //get a reference to the textbox
        skillPointsTextBox = GetComponent<TMP_Text>();
    }

    //if the skillpoints got changed:
    public void UpdateSkillPoints(int skillpoints)
    {
        //update the textbox
        skillPointsTextBox.text = skillpoints.ToString();
    }
}
