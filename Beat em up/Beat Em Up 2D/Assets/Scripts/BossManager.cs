using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : Fighter
{


    enum States { patrol, chase }

    States state = States.patrol;

    public float searchRange = 4;
    public float stopAt = 1;

    Transform player;

    public Vector3 target;
    Vector2 vel;

    // Start is called before the first frame update
    void Start()
    {
        SetTarget();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("SetTarget", 0, 2);
        InvokeRepeating("Punching", 0, 3);
    }

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRange);
        Gizmos.DrawWireSphere(target, 1f);

        base.OnDrawGizmosSelected();
    }

    void Punching()
    {
        if (state != States.chase)
        {
            return;
        }
        if (vel.magnitude != 0)
        {
            return;
        }
        //anim.SetTrigger("IsAttacking");
        StartCoroutine(Puños());
    }
    void SetTarget()
    {

        if (state != States.patrol)
        {

            return;

        }
        target = new Vector2(transform.position.x + Random.Range(-searchRange, searchRange), Random.Range(LimitesY.y, LimitesY.x));
    }

    // Update is called once per frame
    void Update()
    {

        if (state == States.chase)
        {
            target = player.transform.position;
            if (Vector3.Distance(target, transform.position) > searchRange * 1.2f)
            {
                target = transform.position;
                state = States.patrol;
                return;
            }
        }

        else if (state == States.patrol)
        {
            var ob = Physics2D.CircleCast(transform.position, searchRange, Vector2.up);
            if (ob.collider != null)
            {
                if (ob.collider.CompareTag("Player"))
                {
                    state = States.chase;
                    return;
                }
            }
        }
        vel = target - transform.position;
        spr.flipX = vel.x < 0;
        if (vel.magnitude < stopAt)
        {
            vel = Vector2.zero;

        }
        vel.Normalize();
        anim.SetBool("IsWalking", vel.magnitude != 0);
        rd.velocity = new Vector2(vel.x * speedX, vel.y * speedY);
    }
}