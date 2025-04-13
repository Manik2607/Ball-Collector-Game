using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public float volume = 1f;
    }

    public Sound[] sounds;
    private Dictionary<string, Sound> soundDict;
    private AudioSource audioSource;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keeps AudioManager across scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        soundDict = new Dictionary<string, Sound>();

        foreach (Sound s in sounds)
        {
            soundDict[s.name] = s;
        }
    }

    public void Play(string name)
    {
        if (soundDict.TryGetValue(name, out Sound sound))
        {
            audioSource.PlayOneShot(sound.clip, sound.volume);
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
        }
    }
}
