using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using Assets.Scripts.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data;

namespace Assets.Scripts.UI.Shop
{
    public class ShopPanel : BaseUI
    {
        [FoldoutGroup("Button"), SerializeField]
        private CommonTabSwitchButton luckyWheelBtn;
        [FoldoutGroup("Button"), SerializeField]
        private CommonTabSwitchButton ballBtn;
        [FoldoutGroup("Button"), SerializeField]
        private CommonTabSwitchButton themeBtn;
        [FoldoutGroup("Button"), SerializeField]
        private CommonTabSwitchButton pinBtn;
        [FoldoutGroup("Button"), SerializeField]
        private CommonTabSwitchButton trailBtn;
        [FoldoutGroup("Button"), SerializeField]
        private CommonTabSwitchButton wallBtn;

        [FoldoutGroup("Parent"), SerializeField]
        private Transform luckyWheelParent;
        [FoldoutGroup("Parent"), SerializeField]
        private Transform ballParent;
        [FoldoutGroup("Parent"), SerializeField]
        private Transform themeParent;
        [FoldoutGroup("Parent"), SerializeField]
        private Transform pinParent;
        [FoldoutGroup("Parent"), SerializeField]
        private Transform trailParent;
        [FoldoutGroup("Parent"), SerializeField]
        private Transform wallParent;

        [FoldoutGroup("Prefabs"), SerializeField]
        private GroupShopElement groupElement;

        private List<GroupShopElement> ballGroups = new List<GroupShopElement>();
        private List<GroupShopElement> themeGroups = new List<GroupShopElement>();
        private List<GroupShopElement> pinGroups = new List<GroupShopElement>();
        private List<GroupShopElement> trailGroups = new List<GroupShopElement>();
        private List<GroupShopElement> wallGroups = new List<GroupShopElement>();

        private void Awake()
        {
            LoadBall();
            LoadPin();
            LoadTheme();
            LoadTrail();
            LoadWall();

        }

        public override void LoadData()
        {
            CommonTabSwitchButton.OnSelectDone += OnTabSelected;
            luckyWheelBtn.OnClickDone += LuckyWhellButton;
            ballBtn.OnClickDone += BallButton;
            themeBtn.OnClickDone += ThemeButton;
            pinBtn.OnClickDone += PinButton;
            trailBtn.OnClickDone += TrailButton;
            wallBtn.OnClickDone += WallButton;

            luckyWheelBtn.SetStatus(true);
            ballBtn.SetStatus(false);
            themeBtn.SetStatus(false);
            pinBtn.SetStatus(false);
            trailBtn.SetStatus(false);
            wallBtn.SetStatus(false);
            OnTabSelected();
        }

        private void OnTabSelected()
        {
            luckyWheelParent.gameObject.SetActive(luckyWheelBtn.status);
            ballParent.gameObject.SetActive(ballBtn.status);
            themeParent.gameObject.SetActive(themeBtn.status);
            pinParent.gameObject.SetActive(pinBtn.status);
            trailParent.gameObject.SetActive(trailBtn.status);
            wallParent.gameObject.SetActive(wallBtn.status);
        }

        private void WallButton()
        {
            UpdateWall();
        }
        private void LoadWall()
        {
            List<List<Item>> groupBy = GameManager.Instance.wallData.Values
                                                    .GroupBy(o => o.itemUnlockType)
                                                    .Select(g => g.ToList())
                                                    .ToList();

            int length = groupBy.Count;
            for (int i = 0; i < length; i++)
            {
                wallGroups.Add(Instantiate(groupElement, Vector3.zero, Quaternion.identity, wallParent));
                wallGroups[i].LoadGroup();
            }
        }
        private void UpdateWall()
        {
            int length = wallGroups.Count;

            for (int i = 0; i < length; i++)
            {
                wallGroups[i].UpdateGroup();
            }
        }
        private void TrailButton()
        {
            UpdateTrail();

        }
        private void LoadTrail()
        {
            List<List<Item>> groupBy = GameManager.Instance.trailData.Values
                                                    .GroupBy(o => o.itemUnlockType)
                                                    .Select(g => g.ToList())
                                                    .ToList();

            int length = groupBy.Count;
            for (int i = 0; i < length; i++)
            {
                trailGroups.Add(Instantiate(groupElement, Vector3.zero, Quaternion.identity, trailParent));
                trailGroups[i].AddItems(groupBy[i]);
                trailGroups[i].LoadGroup();
            }
        }
        private void UpdateTrail()
        {
            int length = trailGroups.Count;

            for (int i = 0; i < length; i++)
            {
                trailGroups[i].UpdateGroup();
            }
        }
        private void PinButton()
        {
            UpdatePin();

        }
        private void LoadPin()
        {
            List<List<Item>> groupBy = GameManager.Instance.pinData.Values
                                                    .GroupBy(o => o.itemUnlockType)
                                                    .Select(g => g.ToList())
                                                    .ToList();

            int length = groupBy.Count;
            for (int i = 0; i < length; i++)
            {
                pinGroups.Add(Instantiate(groupElement, Vector3.zero, Quaternion.identity, pinParent));
                pinGroups[i].AddItems(groupBy[i]);
                pinGroups[i].LoadGroup();
            }
        }
        private void UpdatePin()
        {
            int length = pinGroups.Count;

            for (int i = 0; i < length; i++)
            {
                pinGroups[i].UpdateGroup();
            }
        }
        private void ThemeButton()
        {
            UpdateTheme();

        }
        private void LoadTheme()
        {
            List<List<Item>> groupBy = GameManager.Instance.themeData.Values
                                                    .GroupBy(o => o.itemUnlockType)
                                                    .Select(g => g.ToList())
                                                    .ToList();

            int length = groupBy.Count;
            for (int i = 0; i < length; i++)
            {
                themeGroups.Add(Instantiate(groupElement, Vector3.zero, Quaternion.identity, themeParent));
                themeGroups[i].AddItems(groupBy[i]);
                themeGroups[i].LoadGroup();
            }

        }
        private void UpdateTheme()
        {
            int length = themeGroups.Count;

            for (int i = 0; i < length; i++)
            {
                themeGroups[i].UpdateGroup();
            }
        }
        private void BallButton()
        {
            UpdateBall();
        }
        private void LoadBall()
        {

            List<List<Item>> groupBy = GameManager.Instance.ballData.Values
                                                                .GroupBy(o => o.itemUnlockType)
                                                                .Select(g => g.ToList())
                                                                .ToList();

            int length = groupBy.Count;
            for (int i = 0; i < length; i++)
            {
                ballGroups.Add(Instantiate(groupElement, Vector3.zero, Quaternion.identity, ballParent));
                ballGroups[i].AddItems(groupBy[i]);
                ballGroups[i].LoadGroup();
            }
        }
        private void UpdateBall()
        {
            int length = ballGroups.Count;

            for (int i = 0; i < length; i++)
            {
                ballGroups[i].UpdateGroup();
            }
        }

        private void LuckyWhellButton()
        {
        }

        public override void SaveData()
        {
            CommonTabSwitchButton.OnSelectDone -= OnTabSelected;
            luckyWheelBtn.OnClickDone -= LuckyWhellButton;
            ballBtn.OnClickDone -= BallButton;
            themeBtn.OnClickDone -= ThemeButton;
            pinBtn.OnClickDone -= PinButton;
            trailBtn.OnClickDone -= TrailButton;
            wallBtn.OnClickDone -= WallButton;
        }
    }
}