using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyEvent : UnityEngine.Events.UnityEvent
{

}

[System.Serializable]
public class MyStringEvent : UnityEngine.Events.UnityEvent<string>
{

}


public class Fighter : MonoBehaviour
{
   
    protected static Vector2 LimiteX = new Vector2(-4.5f, 128.42f);
    protected static Vector2 LimitesY = new Vector2(1.8f, -4.8f);

    public int currentLife { get; private set; }
    public int _life;
    protected int life { get { return _life; }
        set
        {
            _life = value;
            whenLifeChange.Invoke(_life.ToString());
        }
    
    
    }
    [SerializeField]
    protected MyEvent whenDied;
    [SerializeField]
    protected MyStringEvent whenLifeChange;

    public float speedY;
    public float speedX;
    public Transform RPunch;
    public Transform LPunch;
    public float PunchRange = 0.4f;


    public Rigidbody2D rd;
    public SpriteRenderer spr;
    public Animator anim;


    protected virtual void OnDrawGizmosSelected()
    {
        if (LPunch == null || RPunch == null)
            return;
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(LPunch.position, PunchRange);
        Gizmos.DrawWireSphere(RPunch.position, PunchRange);

    
    }

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        whenLifeChange.Invoke(_life.ToString());
        currentLife = life;
    }

    public void GetPunch()
    {
        //anim.SetTrigger("IsAttacking");
        //print("Golpe");
        //life -= 10;
        if(life <= 0)
        {
            whenDied.Invoke();
        }
    }

    public void AutoDestroy()
    {
        Destroy(gameObject);
        life += 10;
    }

    protected IEnumerator Puños()
    {
        anim.SetTrigger("IsAttacking");
        yield return new WaitForSeconds(0.001f);
        Vector2 punchPosition = spr.flipX ? LPunch.position : RPunch.position;
        var ob = Physics2D.CircleCast(punchPosition, PunchRange, Vector2.up);
        if (ob.collider != null)
        {
            if (ob.collider.gameObject != null)
            {
                GetPunch();
                //ob.collider.SendMessage("Ataque");
                life -= 10;
            }
        }
    }
    /*public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Puños() != null)
        {

        }
    }*/
    public void takeDamage(int damage)
    {
        currentLife -= damage;
    }
    // Update is called once per frame
    void Update()
    {
        if(life <= 0)
        {
            AutoDestroy();
        }
    }
}
