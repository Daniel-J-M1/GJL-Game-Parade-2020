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

    public float MoveSpeed = 5f;



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
        //Enemy Looking and Moving in the Player's Direction
        Vector3 Direction = PlayerPosition.position - transform.position;
        transform.LookAt(PlayerPosition.position);
        Direction.Normalize();
        Movement = Direction;
    }

    private void FixedUpdate()
    {
        //Move the Enemy
        MoveCharacter(Movement);
    }

    void MoveCharacter(Vector3 Direction)
    {
        //Enemy Movement Speed
        Rigid.MovePosition(transform.position + (Direction * MoveSpeed * Time.deltaTime));
    }

}
