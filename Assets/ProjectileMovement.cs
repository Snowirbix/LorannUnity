using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProjectileMovement : MonoBehaviour
{
    protected Tilemap tilemap;
    protected Animator animator;
    protected Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = FindObjectOfType<Tilemap>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var pos = transform.position;
        pos.x += dir.x;
        pos.y += dir.y;

        if (tilemap.GetColliderType(new Vector3Int((int)(pos.x-0.5f), (int)(pos.y-0.5f), 0)) == Tile.ColliderType.None)
        {
            transform.position = pos;
        }else
        {
            Destroy(gameObject);
        }
    }

    public void SetDir(Vector2 direction){
        this.dir = direction;
    }
}
