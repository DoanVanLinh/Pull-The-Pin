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
using DG.Tweening;

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

        [FoldoutGroup("Camera"), SerializeField]
        private Camera mainCam;

        private List<GroupShopElement> ballGroups = new List<GroupShopElement>();
        private List<GroupShopElement> themeGroups = new List<GroupShopElement>();
        private List<GroupShopElement> pinGroups = new List<GroupShopElement>();
        private List<GroupShopElement> trailGroups = new List<GroupShopElement>();
        private List<GroupShopElement> wallGroups = new List<GroupShopElement>();

        private Vector3 left;
        private Vector3 right;
        private Vector3 center;

        private Transform currentTab;
        private int index;
        private float timeAni;
        private void Awake()
        {
            timeAni = 0.25f;
            LoadBall();
            LoadPin();
            LoadTheme();
            LoadTrail();
            LoadWall();

            center = luckyWheelParent.parent.parent.position;
            left = mainCam.ViewportToWorldPoint(new Vector3(-0.5f,0.5f, 0));
            left = new Vector3(left.x, center.y, center.z);
            right = mainCam.ViewportToWorldPoint(new Vector3(1.5f, 0.5f,center.z));
            right = new Vector3(right.x, center.y, center.z);
            index = 0;
            luckyWheelParent.parent.parent.position = center;
            ballParent.parent.parent.position = right;
            themeParent.parent.parent.position = right;
            pinParent.parent.parent.position = right;
            trailParent.parent.parent.position = right;
            wallParent.parent.parent.position = right;
        }

        public override void LoadData()
        {
            //CommonTabSwitchButton.OnSelectDone += OnTabSelected;
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
            currentTab = luckyWheelParent.parent.parent;
            //OnTabSelected();
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
            if (index == 5) return;
            currentTab.DOMove(index <5  ? left : right, timeAni);
            UpdateWall();
            wallParent.parent.parent.position = index < 5 ? right : left;
            wallParent.parent.parent.DOMove(center, timeAni);
            currentTab = wallParent.parent.parent;
            index = 5;
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
            if (index == 4) return;
            currentTab.DOMove(index < 4 ? left : right, timeAni);

            UpdateTrail();
            trailParent.parent.parent.position = index <4 ? right : left;
            trailParent.parent.parent.DOMove(center, timeAni);
            currentTab = trailParent.parent.parent;
            index = 4;

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
            if (index == 3) return;
            currentTab.DOMove(index < 3 ? left : right, timeAni);
            UpdatePin();
            pinParent.parent.parent.position = index > 3 ? left : right;
            pinParent.parent.parent.DOMove(center, timeAni);
            currentTab = pinParent.parent.parent;
            index = 3;

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
            if (index == 2) return;
            currentTab.DOMove(index < 2? left : right, timeAni);
            UpdateTheme();
            themeParent.parent.parent.position = index > 2 ? left : right;
            themeParent.parent.parent.DOMove(center, timeAni);
            currentTab = themeParent.parent.parent;
            index = 2;

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
            if (index == 1) return;
            currentTab.DOMove(index < 1 ? left : right, timeAni);
            UpdateBall();
            ballParent.parent.parent.position = index > 1 ? left : right;
            ballParent.parent.parent.DOMove(center, timeAni);
            currentTab = ballParent.parent.parent;
            index = 1;

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
            if (index == 0) return;
            currentTab.DOMove(index < 0 ? left : right, timeAni);
            
            luckyWheelParent.parent.parent.position = index > 0 ? left : right;
            luckyWheelParent.parent.parent.DOMove(center, timeAni);
            currentTab = luckyWheelParent.parent.parent;
            index = 0;

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