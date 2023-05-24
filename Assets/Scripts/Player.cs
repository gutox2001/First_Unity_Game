using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Transform groundCheckTransform; // [SerializeField] = Força mostrar algo no Unity Game Object mesmo sendo private.
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rigidbody;
    private bool isGrounded; //Está no chão
    private int jumpNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
            rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //  Verificando se a tecla espaço foi teclada
        if (Input.GetKeyDown(KeyCode.Space)) // '== true' está subentendido
        {
            jumpKeyWasPressed = true;   
        }
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
    
    // FixedUpdate é chamado toda vez que física do corpo é atualizada
    private void FixedUpdate()
    {
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length == 0) // Checa se o colisor 'groundCheckTransform', em uma área de 0.5, está colidindo com algum objeto.
        {
            isGrounded = false;
            jumpNumber++;
        }

        else
        {
            if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length > 0)
            {
                isGrounded = true;
                jumpNumber = 0;
            }
        }
        
        if (jumpKeyWasPressed & (jumpNumber < 2)) // '== true' está subentendido
        {
            rigidbody.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jumpNumber++;
            jumpKeyWasPressed = false;
        } 

        rigidbody.velocity = new Vector3(horizontalInput * 3, rigidbody.velocity.y, verticalInput * 3);
    }

    /* private void OnCollisionEnter(Collision collision) 
    {
        isGrounded = true;    // Se o player está no chão 'isGrounded = true'
        jumpNumber = 0;
    }

    private void OnCollisionExit(Collision collision) 
    {
        isGrounded = false;     // Se o player não está no chão 'isGrounded = false'
    }  */ // ESSA FORMA DE VERIFICAR COLISÃO NÃO É BOA, POIS O OBJETO PODE COLIDIR COM DOIS OBJETOS AO MESMO TEMPO
}
