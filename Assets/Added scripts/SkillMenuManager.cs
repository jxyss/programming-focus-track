using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//i just adapted the code the original game used for its ingame menu. the main thing i did was just remove a lot of unnecissairy things specfic to opening that menu, but the core functionality is the same.
//the script i used was called InGameMenuManager
namespace Unity.FPS.UI
{
    public class SkillMenuManager : MonoBehaviour
    {
        [Tooltip("Root GameObject of the menu used to toggle its activation")]
        public GameObject MenuRoot;

        [Tooltip("Master volume when menu is open")]
        [Range(0.001f, 1f)]
        public float VolumeWhenMenuOpen = 0.5f;


        void Start()
        {
            MenuRoot.SetActive(false);
        }

        void Update()
        {
            if (!menuData.menuActive && Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (Input.GetButtonDown(GameConstants.k_ButtonNameSkillMenu) || (MenuRoot.activeSelf && Input.GetButtonDown(GameConstants.k_ButtonNameCancel)))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                SetSkillMenuActivation(!MenuRoot.activeSelf);
            }
        }

        public void CloseSkillMenu()
        {
            SetSkillMenuActivation(false);
        }

        void SetSkillMenuActivation(bool active)
        {
            MenuRoot.SetActive(active);
            menuData.menuActive = active;

            if (MenuRoot.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
                AudioUtility.SetMasterVolume(VolumeWhenMenuOpen);

                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
                AudioUtility.SetMasterVolume(1);
            }

        }
    }
}
