using Assets.Scripts.UI.Play;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

    public class Wall : MonoBehaviour
    {
        public static Action OnUpdateVisual;
        public SpriteShape sprShape;
        public Material roadMaterial;
        public Material roadUndersideMaterial;

        private void OnEnable()
        {
            UpdateVisual();
            OnUpdateVisual += UpdateVisual;
        }

        private void UpdateVisual()
        {
            sprShape.angleRanges[0].sprites = new List<Sprite>() { GameManager.Instance.currentWall.wallTexture };
            roadMaterial.color = GameManager.Instance.currentTheme.wallColor;
            roadUndersideMaterial.color = GameManager.Instance.currentTheme.wallColor;
            ((PlayPanel)UIManager.Instance.gamePlayPanel).SetBG(GameManager.Instance.currentTheme.themeBG);
        }

        private void OnDestroy()
        {
            OnUpdateVisual -= UpdateVisual;
        }
    }
