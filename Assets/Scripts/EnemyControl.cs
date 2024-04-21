using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float health;
    public GameControl game_controller;
    public int id;
    private void Start()
    {
        health = 100;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if player instant destroy & escalate to game control
        if (collision.GameObject().CompareTag("Player"))
        {
            health = 0;
            game_controller.enemy_hit_list[id] = 2;
        }
        else if (collision.GameObject().CompareTag("Bullet") && collision.GameObject().GetComponent<BulletControl>().hit != true)
        {
            health *= .8f;
            collision.GameObject().GetComponent<BulletControl>().hit = true;
            //send to game controller for cleanup
            game_controller.bullet_list.Add(collision.GameObject());
            if (health <= 50)
            {
                //enemy has reached death
                game_controller.enemy_hit_list[id] = 1;
            }
            else
            {
                //change opacity
                Color temp = gameObject.GetComponent<SpriteRenderer>().color;
                temp.a = health/100;
                gameObject.GetComponent<SpriteRenderer>().color = temp;
            }
        }
    }
}
