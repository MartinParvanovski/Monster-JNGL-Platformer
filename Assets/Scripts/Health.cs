using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public static int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emprtyHeart;
    public Transform respawnPoint;

    private GameObject player;

    private void Start()
    {
        health = numOfHearts * 2;
    }

    private void Update()
    {
        if (numOfHearts > hearts.Length)
        {
            numOfHearts = hearts.Length;
        }
        if (health > numOfHearts * 2)
        {
            health = numOfHearts * 2;
        }
        int noHearts = health / 2;
        int noHalfs = health % 2;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
            if (i < noHearts)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (noHalfs == 1)
            {
                hearts[i].sprite = halfHeart;
                noHalfs = 0;
            }
            else
            {
                hearts[i].sprite = emprtyHeart;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage >= health)
        {
            health = 0;
            GameOver();
        }
        else
        {
            health -= damage;
        }
    }

    private void Kill()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);
        player.transform.position = respawnPoint.position;
        player.SetActive(true);
        health = 20;
    }
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
