using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //Variables
    private Transform PlayerPosition;

    GameObject Player;

    private Rigidbody Rigid;

    private Vector3 Movement;

    public float Acceleration = 5f;
    public float MaxSpeed = 5f;

    public float Health = 50f;
    bool isInvincible = false;
    public float maxIvincibilityTime = 0.2f;
    float invincibleTimer;

    // Start is called before the first frame update
    void Start()
    {
        // Assigning Variable Values
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerPosition = Player.transform;

        Rigid = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Health <= 0)
            Destroy(this.gameObject);
        print(Health);
        //Enemy Looking and Moving in the Player's Direction
        Vector3 Direction = PlayerPosition.position - transform.position;
        
        transform.LookAt(PlayerPosition.position);
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
        //Rigid.MovePosition(transform.position + (Direction * MoveSpeed * Time.deltaTime));
        Rigid.AddForce(Direction * Acceleration * 100 * Time.deltaTime);

        Rigid.velocity = new Vector3(
            Mathf.Clamp(Rigid.velocity.x, -MaxSpeed, MaxSpeed), 0,
            Mathf.Clamp(Rigid.velocity.z, -MaxSpeed, MaxSpeed));

        //if ((Rigid.velocity.x < MaxSpeed && -Rigid.velocity.x > -MaxSpeed) || (Rigid.velocity.z < MaxSpeed && -Rigid.velocity.z > -MaxSpeed))
        //{
        //    Rigid.AddForce(Direction * Acceleration * 100 * Time.deltaTime);
        //    //Rigid.velocity = currForce;
        //}
        //else
        //{
        //}
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
        Vector3 dir = transform.position - PlayerPosition.position;
        dir.y = 0;
        dir.Normalize();
        Rigid.velocity = dir * intensity;
    }
}
