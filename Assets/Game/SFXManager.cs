using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip BGM;
    [SerializeField] private AudioClip fire;
    [SerializeField] private AudioClip enemyFire;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip expCollect;
    [SerializeField] private AudioClip ammoCollect;
    [SerializeField] private AudioClip shroomieDeath;
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip open;
    [SerializeField] private AudioClip noAmmo;
    [SerializeField] private AudioClip zombieDeath;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this.gameObject); }

        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = BGM;
        audioSource.Play();
    }

    public void PlayNoAmmo()
    {
        audioSource.PlayOneShot(noAmmo);
    }

    public void PlayZombieDeath()
    {
        audioSource.PlayOneShot(zombieDeath);
    }

    public void PlayEnemyFire()
    {
        audioSource.PlayOneShot(enemyFire);
    }

    public void PlayOpen()
    {
        audioSource.PlayOneShot(open);
    }

    public void PlayShroomieDeath()
    {
        audioSource.PlayOneShot(shroomieDeath);
    }

    public void PlayWin()
    {
        audioSource.PlayOneShot(win);
    }

    public void PlayFire()
    {
        audioSource.PlayOneShot(fire);
    }

    public void PlayHit()
    {
        audioSource.PlayOneShot(hit);
    }

    public void PlayAmmoCollect()
    {
        audioSource.PlayOneShot(ammoCollect);
    }

    public void PlayEXPCollect()
    {
        audioSource.PlayOneShot(expCollect);
    }
}
