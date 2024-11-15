using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Breakable : MonoBehaviour
{
    public delegate void DamageTakenEvent();
    public event DamageTakenEvent OnDamageTaken;

    [SerializeField] private string hitboxTag; // immune to hitboxes of the same tag
    [SerializeField] private float health;
    [SerializeField] private GameObject acornDrops;
    [SerializeField] private int numAcorns;
    private float maxHealth;
    protected Blink blink = null;
    protected HealthBar healthBar;

    private bool isDead = false;

    protected void Awake()
    {
        maxHealth = health;
        blink = GetComponent<Blink>();
        if (blink == null ) blink = gameObject.AddComponent<Blink>();
        healthBar = GetComponent<HealthBar>();
        if (healthBar == null ) healthBar = gameObject.AddComponent<HealthBar>();
    }

    public float Health
    {
        get { return health; }
        private set 
        { 
            health = value;
            CheckHealth();
        }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {       
        var hitbox = collision.gameObject.GetComponent<Hitbox>();
        
        if (hitbox != null && hitbox.HitboxTag != hitboxTag)
        {
            //SFXManager.instance.PlayHit();
            TakeDamage(hitbox);     
        }      
    }

    virtual protected void TakeDamage(Hitbox _hitbox)
    {
        Health -= _hitbox.Damage;
        blink.BlinkAmount(1);
        if (OnDamageTaken != null)
            OnDamageTaken();
    }

    private void CheckHealth()
    {
        if (isDead) return;
        if (health <= 0) 
        {
            isDead = true;
            for (int i = 0; i < numAcorns; i++)
            {
                Instantiate(acornDrops).transform.position = this.transform.position;
            }

            Destroy(this.gameObject); 
        }
    }    
}
