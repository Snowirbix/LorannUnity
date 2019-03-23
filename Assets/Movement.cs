using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    protected Tilemap tilemap;
    protected List<Vector2Int> anim = new List<Vector2Int>();
    protected Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        tilemap = FindObjectOfType<Tilemap>();
        // blend tree values
        anim.Add(new Vector2Int( 0, -1));
        anim.Add(new Vector2Int(-1, -1));
        anim.Add(new Vector2Int(-1,  0));
        anim.Add(new Vector2Int(-1,  1));
        anim.Add(new Vector2Int( 0,  1));
        anim.Add(new Vector2Int( 1,  1));
        anim.Add(new Vector2Int( 1,  0));
        anim.Add(new Vector2Int( 1, -1));
    }

    private void FixedUpdate()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        var pos = transform.position;
        pos.x += x;
        pos.y += y;

        if (tilemap.GetColliderType(new Vector3Int((int)(pos.x-0.5f), (int)(pos.y-0.5f), 0)) == Tile.ColliderType.None)
        {
            transform.position = pos;
        }

        if (x == 0 && y == 0)
        {
            // get current anim values
            x = animator.GetFloat("x");
            y = animator.GetFloat("y");
            // find current pose
            var i = anim.FindIndex(v => v.Equals(new Vector2Int((int)x, (int)y)));
            // following pose
            i = (i + 1) % 8;
            // set following anim values
            x = anim[i].x;
            y = anim[i].y;
        }
        animator.SetFloat("x", x);
        animator.SetFloat("y", y);
    }
}
