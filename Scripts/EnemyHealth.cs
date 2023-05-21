using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int maxHealth;
    private int current_Health;
    public GameObject bloodEffect;
    private Animator anim;
    private Rigidbody2D rb;

    public float speed;
    private float timeBtwShots;
    public float startTimeBtwShots;
    private Transform player;
    public GameObject projectile;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        if (this.gameObject.CompareTag("Level1"))
        {
            maxHealth = 20;
        }
        else if (this.gameObject.CompareTag("Level2"))
        {
            maxHelath = 30;
        }
        else if (this.gameObject.CompareTag("Level3"))
        {
            maxHealth = 50;
        }
        current_Health = maxHealth;
    }


    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        transform.position = Vector2.MoveTowards(transform.position,
                player.position, speed * Time.deltaTime);
        if (current_Health <= 0)
        {
            Destroy(this.gameObject);

            GameObject effect =
                         Instantiate(bloodEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            Score.score += 1;
            Audio.KillBugSoundPlay();
        }

        if (this.gameObject.CompareTag("Level3"))
        {
            if (timeBtwShots <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }

        if (PlayerHealth.currentHealth <= 0)
        {
            Destroy(this.gameObject);
            GameObject effect =
                         Instantiate(bloodEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            takeDamage(10);
            anim.SetTrigger("hitting");
            Shake.instance.StartShake(Random.Range(.05f, .1f), Random.Range(.5f, 1f));
        }
    }

    void takeDamage(int damage)
    {
        current_Health -= damage;
    }
}