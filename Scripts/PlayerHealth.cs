using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public static int currentHealth;
    public GameObject bloodEffect;

    public GameManager gameManager;

    private Animator anim;

    public int health;  // set as 10 in inspector, current health
    public int numOfHearts; // maximum health

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            GameObject effect = Instantiate(bloodEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);

            gameManager.gameOver();
            Destroy(this.gameObject);
        }

        // for hearts image
        // if health is over the number of maximum hearts then set as it
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            // until the current health set the full heart images
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            // if enable hearts image until the number of maximum hearts 
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            takeDamage(10);
            anim.SetTrigger("takeDamage");
            Audio.getAttackSoundPlay();
        }
        else if (collision.gameObject.tag == "bullet of enemy")
        {
            takeDamage(10);
            anim.SetTrigger("takeDamage");
            Audio.getAttackSoundPlay();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bullet of enemy"))
        {
            takeDamage(10);
            anim.SetTrigger("takeDamage");
        }
    }


    void takeDamage(int damage)
    {
        currentHealth -= damage;
        health -= 1;
    }
}