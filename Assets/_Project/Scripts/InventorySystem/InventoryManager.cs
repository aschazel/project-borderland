using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using ProjectBorderland.Core;
using ProjectBorderland.Core.FreeRoam;

namespace ProjectBorderland.InventorySystem
{
    /// <summary>
    /// Handles player inventory.
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        //==============================================================================
        // Variables
        //==============================================================================
        #region singletonDDOL
        private static InventoryManager instance;
        public static InventoryManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<InventoryManager>();

                    if (instance == null)
                    {
                        GameObject newGameObject = new GameObject();
                        newGameObject.name = typeof(InventoryManager).Name;
                        instance = newGameObject.AddComponent<InventoryManager>();
                    }
                }

                return instance;
            }
        }
        #endregion

        public static Action OnInventoryChanged;
        public static Action OnEquippedChanged;
        private static int slotIndex = 0;
        public static int SlotIndex { get { return slotIndex; } }
        private static List<ItemSO> items = new List<ItemSO>();
        public static List<ItemSO> Items { get { return items; } }

        [Header("Object References")]
        [SerializeField] public PlayerItemHolder PlayerItemHolder;
        
        [Header("Attribute Configurations")]
        [SerializeField] private int maxCapacity = 8;
        


        //==============================================================================
        // Functions
        //==============================================================================
        #region MonoBehaviour methods
        private void Awake()
        {
            #region singletonDDOL
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            #endregion

            items = Enumerable.Repeat(CreateNullItem(), maxCapacity).ToList();
        }



        private void Update()
        {
            GetInput();
        }
        #endregion



        #region ProjectBorderland methods
        /// <summary>
        /// Gets input from Unity Input Manager.
        /// </summary>
        private void GetInput()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                IncrementEquippedIndex();
            }

            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                DecrementEquippedIndex();
            }

            if (Input.GetKeyDown(InputController.Instance.Slot1))
            {
                slotIndex = 0;
                NotifyOnEquippedChanged();
            }

            if (Input.GetKeyDown(InputController.Instance.Slot2))
            {
                slotIndex = 1;
                NotifyOnEquippedChanged();
            }

            if (Input.GetKeyDown(InputController.Instance.Slot3))
            {
                slotIndex = 2;
                NotifyOnEquippedChanged();
            }

            if (Input.GetKeyDown(InputController.Instance.Slot4))
            {
                slotIndex = 3;
                NotifyOnEquippedChanged();
            }

            if (Input.GetKeyDown(InputController.Instance.Slot5))
            {
                slotIndex = 4;
                NotifyOnEquippedChanged();
            }

            if (Input.GetKeyDown(InputController.Instance.Slot6))
            {
                slotIndex = 5;
                NotifyOnEquippedChanged();
            }

            if (Input.GetKeyDown(InputController.Instance.Slot7))
            {
                slotIndex = 6;
                NotifyOnEquippedChanged();
            }

            if (Input.GetKeyDown(InputController.Instance.Slot8))
            {
                slotIndex = 7;
                NotifyOnEquippedChanged();
            }
        }



        /// <summary>
        /// Increments the equipped item slot index.
        /// </summary>
        private void IncrementEquippedIndex()
        {
            if (slotIndex < maxCapacity - 1)
            {
                slotIndex++;
                NotifyOnEquippedChanged();
            }
        }



        /// <summary>
        /// Decrements the equipped item slot index.
        /// </summary>
        private void DecrementEquippedIndex()
        {
            if (slotIndex > 0)
            {
                slotIndex--;
                NotifyOnEquippedChanged();
            }
        }



        /// <summary>
        /// Creates null item for empty item reference.
        /// </summary>
        private ItemSO CreateNullItem()
        {
            ItemSO nullItem = ScriptableObject.CreateInstance<ItemSO>();
            nullItem.IsNullItem = true;

            return nullItem;
        }



        /// <summary>
        /// Adds an item to inventory by index and returns true if success.
        /// </summary>
        /// <param name="item"></param>
        public static bool Add(ItemSO item, int index)
        {
            if (InventoryManager.Items[index].IsNullItem)
            {
                items[index] = item;
                NotifyOnInventoryChanged();
                return true;
            }

            else if (GetEmptySlot() != -1)
            {
                items[GetEmptySlot()] = item;
                NotifyOnInventoryChanged();
                return true;
            }

            else
            {
                return false;
            }
        }



        /// <summary>
        /// Remove an item from inventory by index.
        /// </summary>
        /// <param name="item"></param>
        public static void Remove(int index)
        {
            ItemSO nullItem = instance.CreateNullItem();
            items[index] = nullItem;
            NotifyOnInventoryChanged();
        }



        /// <summary>
        /// Get currently equipped item.
        /// </summary>
        public static ItemSO GetCurrentIndex()
        {
            return items[slotIndex];
        }



        /// <summary>
        /// Shorthand for Add() to current slot index.
        /// </summary>
        /// <param name="item"></param>
        public static bool AddCurrentIndex(ItemSO item)
        {
            return Add(item, slotIndex);
        }



        /// <summary>
        /// Shorthand for Remove()) to current slot index.
        /// </summary>
        /// <param name="item"></param>
        public static void RemoveCurrentIndex()
        {
            Remove(slotIndex);
        }



        /// <summary>
        /// Gets first empty item slot index and returns -1 if inventory is full.
        /// </summary>
        public static int GetEmptySlot()
        {
            int index = 0;
            foreach (ItemSO item in items)
            {
                if (item.IsNullItem)
                {
                    return index;
                }

                index++;
            }

            return -1;
        }



        /// <summary>
        /// Get sprite from item.
        /// </summary>
        /// <param name="index"></param>
        public static Sprite GetItemSprite(int index)
        {
            if (!items[index].IsNullItem)
            {
                return items[index].Sprite;
            }

            else
            {
                return null;
            }
        }



        /// <summary>
        /// Get item prefab from item.
        /// </summary>
        /// <param name="index"></param>
        public static GameObject GetItemPrefab(int index)
        {
            if (!items[index].IsNullItem)
            {
                return items[index].Prefab;
            }

            else
            {
                return null;
            }
        }



        #region observer
        /// <summary>
        /// Notifies when inventory changed.
        /// </summary>
        private static void NotifyOnInventoryChanged()
        {
            OnInventoryChanged?.Invoke();
            NotifyOnEquippedChanged();
        }



        /// <summary>
        /// Notifies when item equipped changed.
        /// </summary>
        private static void NotifyOnEquippedChanged()
        {
            OnEquippedChanged?.Invoke();
        }
        #endregion
        #endregion
    }
}