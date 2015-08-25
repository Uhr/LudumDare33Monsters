using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{

    static MusicPlayer instance;

    public AudioSource audioSource;

    // Use this for initialization
    void Start()
    {

        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
            audioSource.volume = 0.2f;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 0 || level == 1)
        {
            audioSource.volume = 0.2f;
        }
        else
        {
            audioSource.volume = 1;
        }
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }




}
