using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //Variables
    GameObject Player;
    private Rigidbody Rigid;
    private Vector3 Movement;

    public float Acceleration = 5f;
    public float MaxSpeed = 5f;

    public bool Killed = false;

    public float Health = 50f;
    bool isInvincible = false;
    public float maxIvincibilityTime = 0.2f;
    float invincibleTimer;

    // Start is called before the first frame update
    void Start()
    {
        // Assigning Variable Values
        Player = GameObject.FindGameObjectWithTag("Player");
        Rigid = this.GetComponent<Rigidbody>();
        MaxSpeed = MaxSpeed + Random.Range(-2f, 2f);
        Acceleration = Acceleration + Random.Range(-2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Health <= 0)
        {
            Destroy(this.gameObject);
            Player.gameObject.GetComponent<Spawner>().Died();
            Killed = true;
        }
            

        //print(Health);
        //Enemy Looking and Moving in the Player's Direction
        Vector3 Direction = Player.transform.position - transform.position;
        
        transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z));
        Direction.Normalize();
        Direction.y = 0;
        Movement = Direction;
        if(isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                ResetInvincible();
            }
        }
        
    }

    private void FixedUpdate()
    {
        //Move the Enemy
        MoveCharacter(Movement);
    }

    void MoveCharacter(Vector3 Direction)
    {
        //Enemy Movement Speed
        Rigid.AddForce(Direction * Acceleration * 100 * Time.deltaTime);

        Rigid.velocity = new Vector3(
            Mathf.Clamp(Rigid.velocity.x, -MaxSpeed, MaxSpeed), 0,
            Mathf.Clamp(Rigid.velocity.z, -MaxSpeed, MaxSpeed));
    }

    public void AlterHealth(float altHP)
    {
        if (!isInvincible)
        {
            Health += altHP;
            isInvincible = true;
            invincibleTimer = maxIvincibilityTime;
        }
    }

    void ResetInvincible()
    {
        isInvincible = false;
    }

    public void KnockBack(float intensity)
    {
        Vector3 dir = transform.position - Player.transform.position;
        dir.y = 0;
        dir.Normalize();
        Rigid.velocity = dir * intensity;
    }
}
