using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public Vector3 offset = new Vector3(0.5f, 0.5f, -0.02f);
    public float tick = 0.13f;
    public Tilemap tilemap;

    private float time = 0;
    private Animator anim;
    private Vector3Int position;
    private float x = 1;
    private float y = 0;
    private float animTime = 0;

    void Start ()
    {
        position = Vector3Int.FloorToInt(transform.position);
        anim = GetComponent<Animator>();
    }
	
	void Update ()
    {
        time += Time.deltaTime;
        animTime += Time.deltaTime;

        if (time > tick)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (horizontal != 0 || vertical != 0)
            {
                Vector3Int directionX = new Vector3Int((int)horizontal, 0, 0);
                Vector3Int directionY = new Vector3Int(0, (int)vertical, 0);

                if (tilemap.GetColliderType(position + directionX + directionY) == Tile.ColliderType.None)
                {
                    position = position + directionX + directionY;
                    transform.position = position + offset;
                    time = 0;
                }
                else if (tilemap.GetColliderType(position + directionX) == Tile.ColliderType.None)
                {
                    position = position + directionX;
                    transform.position = position + offset;
                    time = 0;
                }
                else if (tilemap.GetColliderType(position + directionY) == Tile.ColliderType.None)
                {
                    position = position + directionY;
                    transform.position = position + offset;
                    time = 0;
                }

                x = Vector3.Normalize(directionX + directionY).x;
                y = Vector3.Normalize(directionX + directionY).y;
            }
            else
            {
                if (animTime > tick)
                {
                    animTime = 0;
                    float angle = Mathf.Atan2(y, x) - (Mathf.PI / 4);
                    x = Mathf.Cos(angle);
                    y = Mathf.Sin(angle);
                }
            }

            anim.SetFloat("x", x);
            anim.SetFloat("y", y);
        }
    }

    void OnTriggerEnter2D ()
    {
        Debug.Log("trigger");
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        Debug.Log("collide");
    }
}
