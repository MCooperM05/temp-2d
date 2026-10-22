using UnityEngine;

public class BubblePop : MonoBehaviour
{
    // This script is meant to be used for gameObjects that feature a bubble pop

    [SerializeField] private AudioSource audioSource;

    public void PlayPop()
    {
        audioSource.Play();
    }
}
