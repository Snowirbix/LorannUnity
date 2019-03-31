using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{
    public GameObject ball;
    protected Animator animBall;
    protected ProjectileMovement projScript;

    protected GameObject instantBall;
    // Start is called before the first frame update
    void Start()
    {
        animBall = ball.GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var x = Input.GetAxisRaw("Fire1");
        var y = Input.GetAxis("Fire2");

        if( (x != 0 || y != 0) && !GameObject.Find(ball.name + "(Clone)")){
            instantBall = Instantiate(ball, new Vector3(transform.position.x + x , transform.position.y + y , transform.position.z), Quaternion.identity );

            instantBall.AddComponent(typeof(ProjectileMovement));
            projScript = instantBall.GetComponent<ProjectileMovement>();
            projScript.SetDir(new Vector2(x,y));
        }
    }
}
