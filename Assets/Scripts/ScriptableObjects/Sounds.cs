using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Sounds", menuName = "We/Sounds", order = 0)]
public class Sounds : ScriptableObject
{
    [SerializeField] public List<Sound> listSounds;

#if UNITY_EDITOR
    [Button("Get Sound")]
    public void GetSound()
    {
        listSounds = new List<Sound>();

        AudioClip[] themes = Resources.LoadAll<AudioClip>("Sounds/BGM/");

        for (int i = 0; i < themes.Length; i++)
        {
            Sound sound = new Sound();
            sound.nameSound = themes[i].name;
            sound.clip = themes[i];
            sound.playOnAwake = true;
            sound.loop = true;
            sound.volume = 0.5f;
            sound.typeSound = TypeSound.Theme;

            listSounds.Add(sound);
        }

        themes = Resources.LoadAll<AudioClip>("Sounds/Sfx/");

        for (int i = 0; i < themes.Length; i++)
        {
            Sound sound = new Sound();
            sound.nameSound = themes[i].name;
            sound.clip = themes[i];
            sound.playOnAwake = true;
            sound.loop = false;
            sound.volume = 1;
            sound.typeSound = TypeSound.Sfx;

            listSounds.Add(sound);
        }
    }
#endif
}
