using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    private AudioSource audioSource;
    [Header("Music")]
    [SerializeField] private AudioClip defaultBGM;
    [SerializeField] private AudioClip menuBGM;
    [SerializeField] private AudioClip level1BGM;
    [SerializeField] private AudioClip level2BGM;
    [SerializeField] private AudioClip level3BGM;
    [SerializeField] private AudioClip level4BGM;
    [SerializeField] private AudioClip level5BGM;
    [SerializeField] private AudioClip level6BGM;
    [SerializeField] private AudioClip modifierBGM;

    [Header("SFX")]
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
        var manager = FindObjectOfType<CentralGameManager>();
        if (manager != null) manager.UpdateMusic();
    }

    public void PlayDefaultBGM()
    {
        if (audioSource.clip != defaultBGM)
        {
            audioSource.clip = defaultBGM;
            audioSource.Play();
        }
    }

    public void PlayModifierBGM()
    {
        if (audioSource.clip != modifierBGM)
        {
            audioSource.clip = modifierBGM;
            audioSource.Play();
        }
    }

    public void PlayMenuBGM()
    {
        if (audioSource.clip != menuBGM)
        {
            audioSource.clip = menuBGM;
            audioSource.Play();
        }
    }

    public void PlayLevel1BGM()
    {
        if (audioSource.clip != level1BGM)
        {
            audioSource.clip = level1BGM;
            audioSource.Play();
        }
    }

    public void PlayLevel2BGM()
    {
        if (audioSource.clip != level2BGM)
        {
            audioSource.clip = level2BGM;
            audioSource.Play();
        }
    }

    public void PlayLevel3BGM()
    {
        if (audioSource.clip != level3BGM)
        {
            audioSource.clip = level3BGM;
            audioSource.Play();
        }
    }

    public void PlayLevel4BGM()
    {
        if (audioSource.clip != level4BGM)
        {
            audioSource.clip = level4BGM;
            audioSource.Play();
        }
    }

    public void PlayLevel5BGM()
    {
        if (audioSource.clip != level5BGM)
        {
            audioSource.clip = level5BGM;
            audioSource.Play();
        }
    }

    public void PlayLevel6BGM()
    {
        if (audioSource.clip != level6BGM)
        {
            audioSource.clip = level6BGM;
            audioSource.Play();
        }
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
