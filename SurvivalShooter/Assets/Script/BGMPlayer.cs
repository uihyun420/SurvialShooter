using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public AudioClip bgmClip;
    public AudioSource audioSource;

    private void Awake()
    {  
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.Play();
    }
}
