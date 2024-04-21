using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int total_eggs;
    public bool control_mode;
    public float movement_speed;
    public float rotation_speed;
    public float attack_speed;
    private float time_of_last_spawn;
    public GameObject bullet_prefab;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        control_mode = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        FireControl();
    }

    void FireControl()
    {
        //fire every attack speed if space pressed/held
        if (Input.GetKey(KeyCode.Space) && Time.time >= time_of_last_spawn + attack_speed)
        {
            //Instantiate Bullet
            GameObject bullet = Instantiate(bullet_prefab, transform.localPosition, transform.rotation);
            bullet.GetComponent<BulletControl>().cam = cam;
            bullet.GetComponent<BulletControl>().playerControl = this;
            time_of_last_spawn = Time.time;
            total_eggs++;
        }
    }

    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.M))
        {
            control_mode = !control_mode;
        }
        //default of mouse control
        if (control_mode)
        {
            //position = mouse placement
            Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            temp.z = 0;
            transform.localPosition = temp;
        }
        else
        {
            //position = move forward
            transform.Translate(0, Time.deltaTime * movement_speed, 0);
        }

        //acceleration check
        if (Input.GetKey(KeyCode.W))
        {
            movement_speed += 0.1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement_speed -= 0.1f;
        }

        //rotation check
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, rotation_speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -1 * rotation_speed);
        }
    }
}
