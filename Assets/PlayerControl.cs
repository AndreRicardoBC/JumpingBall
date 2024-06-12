using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerControl : MonoBehaviour
{

    public float Speed;
    public float Deceleration;
    public float JumpForce;

    private Rigidbody2D RigBody;
    private bool IsJumping;
    // Start is called before the first frame update
    void Start()
    {
        RigBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input de movimento horizontal(teclas A/D ou setinhas esquerda/direita)
        float MoveInput = Input.GetAxis("Horizontal");

        //Calcula a velocidade desejada baseada no input
        float TargetVelocityX = MoveInput * Speed;
        
        //Suavemente muda a velocidade em direção à velocidade alvo
        float newVelocityX = Mathf.Lerp(RigBody.velocity.x, TargetVelocityX, Time.deltaTime * Deceleration);
        
        //Aplica a nova velocidade mantendo a velocidade vertical atual
        RigBody.velocity = new Vector2(newVelocityX, RigBody.velocity.y);

        //Funcionalidade de pulo
        if(Input.GetButtonDown("Jump") && !IsJumping)
        {
            RigBody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            IsJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.layer == 3){
            IsJumping = false;
        }
    }
}
        //Usado caso eu queira uma velocidade fixa sempre.
        //Vector2 MoveVelocity = new Vector2(MoveInput * Speed, RigBody.velocity.y);
        //RigBody.velocity = MoveVelocity;

        