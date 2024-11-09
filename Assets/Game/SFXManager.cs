using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip BGM;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip fire;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip kill;
    [SerializeField] private AudioClip collect;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this.gameObject); }

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = BGM;
        audioSource.Play();
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(jump);
    }

    public void PlayFire()
    {
        audioSource.PlayOneShot(fire);
    }

    public void PlayDeath()
    {
        audioSource.PlayOneShot(death);
    }

    public void PlayKill()
    {
        audioSource.PlayOneShot(kill);
    }

    public void PlayCollect()
    {
        audioSource.PlayOneShot(collect);
    }
}
