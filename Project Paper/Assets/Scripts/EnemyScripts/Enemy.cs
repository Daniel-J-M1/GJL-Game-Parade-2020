using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform PlayerPosition;
    GameObject Player;
    private Rigidbody Rigid;
    private Vector3 Movement;
    public float MoveSpeed = 5f;



    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerPosition = Player.transform;

        Rigid = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Direction = PlayerPosition.position - transform.position;
        Debug.Log(Direction);
        //Look = (PlayerPosition.position);
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Rigid.rotation = Quaternion.Euler(0f, angle, 0f);
        transform.LookAt(PlayerPosition.position);
        Direction.Normalize();
        Movement = Direction;
    }

    private void FixedUpdate()
    {
        MoveCharacter(Movement);
    }

    void MoveCharacter(Vector3 Direction)
    {
        Rigid.MovePosition(transform.position + (Direction * MoveSpeed * Time.deltaTime));
    }

}
