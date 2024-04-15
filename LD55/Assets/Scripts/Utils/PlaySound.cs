using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;
    public float soundProbability = 0.01f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Random.Range(0f, 1f) < soundProbability)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
