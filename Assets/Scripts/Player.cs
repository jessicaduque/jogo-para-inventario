using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    // Velocidade de movimento
    public float moveSpeed;

    public float groundDrag;

    // Para pular
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    // Escolha da tecla para o pulo
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    // Checar se o player está ou não no chão para se mover
    [Header("Ground check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    // Transform da orientação
    public Transform orientation;

    // Inputs do teclado do horizontal e vertical
    float horizontalInput;
    float verticalInput;

    // Direção de movimento
    Vector3 moveDirection;

    // Rigidbody do player
    Rigidbody rb;

    private void Start()
    {
        // Pegar o rigidbody do player e congelar sua rotação
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        // Usa o raycast apontado para baixo para checar se o player está ou não no chão
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();
        SpeedControl();

        // A partir do cheque de chão faz o rag do player
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    private void MyInput()
    {
        // Input do teclado
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Quando player deve pular
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            Debug.Log("Entrou");
            readyToJump = false;

            Jump();

            // Resetar o pulo usando o cooldown para delay, algo que serve para não pular direto se segurar a tecla
            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    private void MovePlayer()
    {
        // Calcular direção do movimento
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Adicionar força no player
        // No chão
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        // No ar
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        // Pega a velocidade normal do rigidbody
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    
        // Limita a velocidade se neccessário
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    
    }

    private void Jump()
    {
        // Confirmação que a velocidade do y está no 0
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Fazer a força dó pulo (usar o Impulse por estar fazendo força uma única vez)
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }

    // Função só para resetar o pulo
    private void ResetJump()
    {
        readyToJump = true;
    }

}   
