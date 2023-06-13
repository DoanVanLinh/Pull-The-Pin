using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Commons;
using System;

public class SoundManager : MonoBehaviour
{

    #region Singleton
    public static SoundManager Instance { get; set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        currentTheme = "InGame 1";
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public Sounds sounds;
    public List<CommonSound> currentSounds = new List<CommonSound>();
    public string currentTheme;
    private float cacheFrame;

    public void Play(string name, float duration = 0)
    {
        Sound sound = sounds.listSounds.Where(s => s.nameSound == name).FirstOrDefault();
        if (sound == null)
            return;

        if (ObjectPooler.Instance.poolingsCanDispose[name].Count != 0&&cacheFrame == Time.unscaledDeltaTime) return;

        CommonSound commonSound = Helper.SpawnSound(name, duration);
        if (commonSound == null)
            return;

        commonSound.OnDispose += OnSoundDispose;
        currentSounds.Add(commonSound);

        if (commonSound.typeSound == TypeSound.Theme && DataManager.Instance.CurrentSoundThemeState == 0)
        {
            commonSound.Pause();
        }

        if (commonSound.typeSound == TypeSound.Sfx && DataManager.Instance.CurrentSoundEffectState == 0)
        {
            commonSound.Pause();
        }
        cacheFrame = Time.unscaledDeltaTime;
    }

    private void OnSoundDispose(PoolingObject obj)
    {
        CommonSound commonSound = (CommonSound)obj;
        commonSound.OnDispose -= OnSoundDispose;
        currentSounds.Remove(commonSound);
    }

    public void Stop(string name)
    {
        ObjectPooler.Instance.ForceDispose(name);
    }

    public void Pause(string name)
    {
        for (int i = 0; i < currentSounds.Count; i++)
        {
            if (currentSounds[i].nameObj != name)
                continue;

            currentSounds[i].Pause();
        }
    }

    /// <summary>
    /// Pause All Current Sounds
    /// </summary>
    public void Pause()
    {
        for (int i = 0; i < currentSounds.Count; i++)
        {
            currentSounds[i].Pause();
        }
    }
    public void Pause(TypeSound type)
    {
        for (int i = 0; i < currentSounds.Count; i++)
        {
            if (currentSounds[i].typeSound == type)
                currentSounds[i].Pause();
        }
    }

    public void Resume(string name)
    {
        for (int i = 0; i < currentSounds.Count; i++)
        {
            if (currentSounds[i].nameObj != name)
                continue;

            currentSounds[i].Resume();
        }
    }
    /// <summary>
    /// Resume All Current Sounds
    /// </summary>
    /// <param name="name"></param>
    public void Resume()
    {
        for (int i = 0; i < currentSounds.Count; i++)
        {
            if (currentSounds[i].typeSound == TypeSound.Sfx && DataManager.Instance.CurrentSoundEffectState == 1)
                currentSounds[i].Resume();

            if (currentSounds[i].typeSound == TypeSound.Theme && DataManager.Instance.CurrentSoundThemeState == 1)
                currentSounds[i].Resume();
        }
    }

    public void Resume(TypeSound type)
    {
        for (int i = 0; i < currentSounds.Count; i++)
        {
            if (currentSounds[i].typeSound == type)
                currentSounds[i].Resume();
        }
    }
}

[System.Serializable]
public class Sound
{
    public string nameSound;
    public AudioClip clip;
    public bool playOnAwake;
    public bool loop;
    [Range(0f, 1f)]
    public float volume;
    public TypeSound typeSound;
}
