using UnityEngine;

namespace _Scripts.Audio
{
    public class BackgroundSoundManager : MonoBehaviour
    {
        public AudioClip backMusicNormal;
        public float musicVolume = 0.06f;

        public static AudioSource AudioSource;
    
        // Start is called before the first frame update
        void Start()
        {
            AudioSource = gameObject.AddComponent<AudioSource>();
            AudioSource.loop = true;
            AudioSource.volume = musicVolume * PlayerPrefs.GetFloat("GlobalVolume");
            AudioSource.clip = backMusicNormal;
            AudioSource.Play();
        }

        private void Update()
        {
            AudioSource.volume = musicVolume * PlayerPrefs.GetFloat("GlobalVolume");
        }
    }
}
