using UnityEngine;

public class PitchSoundUpgrades : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip upgradeSound;
    [Range(0.8f, 1.5f)]
    public float minPitch = 0.9f;

    [Range(0.8f, 1.5f)]
    public float maxPitch = 1.1f;


    public void PlayUpgradeSound()
    {
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(upgradeSound);
    }
}
