﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using Assets.Scripts.Commons;
using System;
using Assets.Scripts.Data;

namespace Assets.Scripts.UI.Shop
{
    public class ShopElement : MonoBehaviour
    {
        public static ShopElement currentItemElement;
        public static Action OnSelected;
        public static int id;

        [FoldoutGroup("Image"), SerializeField]
        private Image lockImg;
        [FoldoutGroup("Image"), SerializeField]
        private Image icon;

        [FoldoutGroup("Button")]
        public Button choseButton;

        [FoldoutGroup("Component")]
        public GameObject hightLight;
        [FoldoutGroup("Component")]
        public GameObject notification;

        public string itemId;
        private Item itemData;
        private bool isLock;
        private void OnEnable()
        {
            OnSelected += Chose;
            choseButton.onClick.AddListener(delegate { SoundManager.Instance.Play("Button Click"); ChoseButton(); });
        }

        public void LoadElement(string id)
        {
            this.itemId = id;
            transform.localPosition = Vector3.zero;

            UpdateElement();
        }
        public void UpdateElement()
        {
            itemData = GameManager.Instance.itemsData[this.itemId];
            icon.sprite = itemData.icon;
            //icon.SetNativeSize();
            isLock = !DataManager.Instance.GetData().HasItem(this.itemId);
            lockImg.gameObject.SetActive(isLock);

            notification.SetActive(!isLock&&!DataManager.Instance.HasKey(itemId));

            if (itemId == DataManager.Instance.GetCurrentItemByType(itemData.itemType))
            {
                ShopElement.id = gameObject.GetInstanceID();
                OnSelected?.Invoke();
            }
        }

        private void ChoseButton()
        {
            if (!isLock)
            {
                SoundManager.Instance.Play("Click");
                id = gameObject.GetInstanceID();
                DataManager.Instance.SetCurrentItemByType(itemData.itemType, itemId);
                DataManager.Instance.SetInt(itemId, 1);
                notification.SetActive(false);
                OnSelected?.Invoke();
            }
        }

        public void Chose()
        {
            hightLight.SetActive(id == gameObject.GetInstanceID());
        }
        private void OnDisable()
        {
            OnSelected -= Chose;
            id = 0;
            currentItemElement = null;
            hightLight.SetActive(false);
            choseButton.onClick.RemoveAllListeners();
        }
    }
}