using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public PlayerControl playerControl;
    public Camera cam;
    public float projectile_speed;
    public bool hit;
    // Start is called before the first frame update
    void Start()
    {
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        //destruction check (i.e., leave camera viewport)
        Vector3 view_port = cam.WorldToViewportPoint(transform.position);
        if (view_port.y > 1 || view_port.y < 0 || view_port.x > 1 || view_port.x < 0)
        {
            playerControl.total_eggs--;
            Destroy(gameObject);
        }

        //movement update
        transform.Translate(0, Time.deltaTime * projectile_speed, 0);
    }
}
