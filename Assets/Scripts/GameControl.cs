using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    private int total_enemies = 10; //stays static at ten
    private int total_enemies_destroyed;
    public PlayerControl player;
    public Text ui_counters;
    public Camera cam;
    public GameObject enemy_prefab;
    public GameObject[] enemy_list;
    public int[] enemy_hit_list; //0 not hit, 1 bullet hit, 2 player hit
    public List<GameObject> bullet_list; //queue used to store bullets to be deleted
    // Start is called before the first frame update
    void Start()
    {
        enemy_list = new GameObject[10];
        enemy_hit_list = new int[10];
        for (int i = 0; i < 10; i++)
        {
            //Instatiate and randomize location of enemy
            float ranx = Random.value;
            float rany = Random.value;
            while (ranx >= .95f || ranx <= .05f) { ranx = Random.value; }
            while (rany >= .95f || rany <= .05f) { rany = Random.value; }
            UnityEngine.Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new UnityEngine.Vector2(ranx, rany));
            enemy_list[i] = Instantiate(enemy_prefab, randomPositionOnScreen, UnityEngine.Quaternion.identity);
            enemy_hit_list[i] = 0;
            enemy_list[i].GetComponent<EnemyControl>().id = i;
            enemy_list[i].GetComponent<EnemyControl>().game_controller = gameObject.GetComponent<GameControl>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        QuitCheck();
        UpdateEnemies();
        ClearList();
        UIUpdate();
    }

    private void UpdateEnemies()
    {
        for (int i = 0; i < enemy_list.Length; i++) 
        {
            //check hit status, delete and reset new enemy
            if (enemy_hit_list[i] == 1 || enemy_hit_list[i] == 2)
            {
                enemy_hit_list[i] = 0;
                Destroy(enemy_list[i]);
                total_enemies_destroyed++;
                //Instatiate and randomize location of enemy
                float ranx = Random.value;
                float rany = Random.value;
                while (ranx >= .95f || ranx <= .05f) { ranx = Random.value; }
                while (rany >= .95f || rany <= .05f) { rany = Random.value; }
                UnityEngine.Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new UnityEngine.Vector2(ranx, rany));
                enemy_list[i] = Instantiate(enemy_prefab, randomPositionOnScreen, UnityEngine.Quaternion.identity);
                enemy_hit_list[i] = 0;
                enemy_list[i].GetComponent<EnemyControl>().id = i;
                enemy_list[i].GetComponent<EnemyControl>().game_controller = gameObject.GetComponent<GameControl>();
            }
        }
    }
    //Hero Mode: (Mouse)
    //Number of Eggs: #
    //Enemy Count: #
    //Enemies Destroyed: #
    private void UIUpdate()
    {
        string value = "Hero Mode: (";
        if (player.control_mode) { value += "Mouse)\nNumber of Eggs: "; }
        else { value += "WASD)\nNumber of Eggs: "; }
        value += player.total_eggs + "\nEnemy Count: " + total_enemies + "\nEnemies Destroyed: " + total_enemies_destroyed;

        ui_counters.text = value;
    }

    private void ClearList()
    {
        foreach (GameObject bullet in bullet_list)
        {
            if (bullet != null) { Destroy(bullet); player.total_eggs--; }
        }
        bullet_list.Clear();
    }

    private void QuitCheck()
    {
        //Q == quit game
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }
}
