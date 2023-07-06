using Assets.Scripts.Commons;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Setting
{
    public class SettingPanel : BaseUI
    {
        [FoldoutGroup("Button")]
        public Button closeButton;
        [FoldoutGroup("Button")]
        public CommonSwitchButtonVisual soundButton;
        //[FoldoutGroup("Button")]
        //public CommonSwitchButtonVisual musicButton;
        [FoldoutGroup("Button")]
        public CommonSwitchButtonVisual vibrationButton;

        public override void LoadData()
        {
            transform.position = UIManager.Instance.right;
            closeButton.onClick.AddListener(delegate { SoundManager.Instance.Play("Button Click"); CloseButton(); });
            soundButton.SetStatus(DataManager.Instance.CurrentSoundEffectState == 1);
            //musicButton.SetStatus(DataManager.Instance.CurrentSoundThemeState == 1);
            vibrationButton.SetStatus(DataManager.Instance.CurrentVibrateState == 1);

            soundButton.OnClickDone += OnSoundChange;
            //musicButton.OnClickDone += OnMusicChange;
            vibrationButton.OnClickDone += OnVibrationChange;

        }

        private void OnVibrationChange()
        {
            SoundManager.Instance.Play("Button Click"); DataManager.Instance.SetVibrateState(vibrationButton.status ? 1 : 0);
        }

        //private void OnMusicChange()
        //{
        //    DataManager.Instance.SetSoundThemeState(musicButton.status ? 1 : 0);

        //    if (musicButton.status)
        //        SoundManager.Instance.Resume(TypeSound.Theme);
        //    else
        //        SoundManager.Instance.Pause(TypeSound.Theme);
        //}

        private void OnSoundChange()
        {
            SoundManager.Instance.Play("Button Click"); DataManager.Instance.SetSoundEffectState(soundButton.status ? 1 : 0);

            if (soundButton.status)
                SoundManager.Instance.Resume(TypeSound.Sfx);
            else
                SoundManager.Instance.Pause(TypeSound.Sfx);
        }

        private void CloseButton()
        {
            SoundManager.Instance.Play("Click");
            Close();
        }
        public override void Open()
        {
            base.Open();
            transform.DOMove(UIManager.Instance.center, 0.25f).SetUpdate(true);
        }
        public override void Close()
        {
            transform.DOMove(UIManager.Instance.right, 0.25f).SetUpdate(true);
            UIManager.Instance.homePanel.transform.DOMove(UIManager.Instance.center, 0.25f).SetUpdate(true);

        }
        public override void SaveData()
        {
            soundButton.OnClickDone -= OnSoundChange;
            //musicButton.OnClickDone -= OnMusicChange;
            vibrationButton.OnClickDone -= OnVibrationChange;
            closeButton.onClick.RemoveAllListeners();
        }

    }
}