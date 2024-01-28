using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    [SerializeField] private GameObject endGameObject;
    [SerializeField] private AudioSource endAudioSource;
    [SerializeField] private AudioManager audioManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag(Duck.DUCK_TAG))
        {
            other.GetComponent<Duck>().Win();
            Victory();
        }
    }

    private void Victory()
    {
        endGameObject.SetActive(true);
        endAudioSource.volume = audioManager.SoundEffectsVolume;
        endAudioSource.Play();
    }
}
