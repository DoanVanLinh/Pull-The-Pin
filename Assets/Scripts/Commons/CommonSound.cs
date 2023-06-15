using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Commons
{
    [RequireComponent(typeof(AudioSource))]
    public class CommonSound : PoolingObject
    {
        public AudioSource sourceAudio;
        public TypeSound typeSound;
        public bool autoDispose => timeDispose == 0;

        public float timeDispose;
        public override void Init()
        {
            
        }

        public void AutoDisPose()
        {
            if (autoDispose)
            {
                if (!sourceAudio.loop)
                    Invoke("Dispose", sourceAudio.clip.length);
            }
            else
                Invoke("Dispose", timeDispose);
        }

        public void LoadSound(Sound sound)
        {
            sourceAudio.clip = sound.clip;
            sourceAudio.volume = sound.volume;
            sourceAudio.playOnAwake = sound.playOnAwake;
            sourceAudio.loop = sound.loop;
            typeSound = sound.typeSound;
        }

        public void Pause()
        {
            sourceAudio.Pause();
        }
        public void Resume()
        {
            sourceAudio.UnPause();
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

#if UNITY_EDITOR
        [Button("Get Data")]
        private void GetData()
        {
            Sounds sounds = Resources.LoadAll<Sounds>("ScriptableObject/SoundsData/")[0];

            gameObject.name = nameObj;
            for (int i = 0; i < sounds.listSounds.Count; i++)
            {
                if (sounds.listSounds[i].nameSound.Equals(nameObj))
                    LoadSound(sounds.listSounds[i]);
            }
        }
#endif
    }
}