using System.Collections;
using UnityEngine;

namespace ProjectBorderland.Core
{
    /// <summary>
    /// Handles game debug mode.
    /// </summary>
    public class InputController : MonoBehaviour
    {
        //==============================================================================
        // Variables
        //==============================================================================
        #region singletonDDOL
        private static InputController instance;
        public static InputController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<InputController>();

                    if (instance == null)
                    {
                        GameObject newGameObject = new GameObject();
                        newGameObject.name = typeof(InputController).Name;
                        instance = newGameObject.AddComponent<InputController>();
                    }
                }

                return instance;
            }
        }
        #endregion

        public KeyCode Forward;
        public KeyCode Backward;
        public KeyCode Right;
        public KeyCode Left;
        public KeyCode Sprint;
        public KeyCode Interact;
        public KeyCode Throw;
        public KeyCode Slot1;
        public KeyCode Slot2;
        public KeyCode Slot3;
        public KeyCode Slot4;
        public KeyCode Slot5;
        public KeyCode Slot6;
        public KeyCode Slot7;
        public KeyCode Slot8;



        //==============================================================================
        // Functions
        //==============================================================================
        #region MonoBehaviour methods
        private void Awake()
        {
            ParseKey();
        }
        #endregion



        #region ProjectBorderland methods
        /// <summary>
        /// Parse key inputs into Unity KeyCodes.
        /// </summary>
        private void ParseKey()
        {
            Interact = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("interactKey", "Mouse0"));
            Throw = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("throwKey", "G"));
            Sprint = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("sprintKey", "LeftShift"));
            Slot1 = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("slot1Key", "Alpha1"));
            Slot2 = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("slot2Key", "Alpha2"));
            Slot3 = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("slot3Key", "Alpha3"));
            Slot4 = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("slot4Key", "Alpha4"));
            Slot5 = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("slot5Key", "Alpha5"));
            Slot6 = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("slot6Key", "Alpha6"));
            Slot7 = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("slot7Key", "Alpha7"));
            Slot8 = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("slot8Key", "Alpha8"));
        }
        #endregion
    }
}