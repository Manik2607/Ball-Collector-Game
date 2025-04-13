using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string name;       // Name of the sound
        public AudioClip clip;    // Audio clip to play
        public float volume = 1f; // Volume of the sound
    }

    public Sound[] sounds;
    private Dictionary<string, Sound> soundDict;
    private AudioSource audioSource;

    void Awake()
    {
        // Singleton setup to ensure only one instance of AudioManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //keeps AudioManager across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate AudioManager instances
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component
        soundDict = new Dictionary<string, Sound>();

        foreach (Sound s in sounds)
        {
            soundDict[s.name] = s; // Populate the dictionary with sound data
        }
    }

    public void Play(string name)
    {
        if (soundDict.TryGetValue(name, out Sound sound))
        {
            audioSource.PlayOneShot(sound.clip, sound.volume); // Play the sound clip
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
        }
    }
}
