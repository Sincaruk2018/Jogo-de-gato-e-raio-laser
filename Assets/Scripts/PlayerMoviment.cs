using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    public int speed = 10; // Controla a velocidade
    private Rigidbody2D characterBody; // Permite que o corpo do objeto se mova e interaja 
    private Vector2 velocity; // Velocidade x e y
    private Vector2 inputMoviment; // Definir qual tecla o usuario esta apertando 

    void Start()
    {
        velocity = new Vector2(speed, speed); // Define a velocidade na direção x e y
        characterBody = GetComponent<Rigidbody2D>(); // pega o corpo do objeto

    }

    void Update()
    {
        inputMoviment = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // Define o movimento de entrada como um novo vetor (horizontal e vertical)
    }
    private void FixedUpdate()
    {
        Vector2 delta = inputMoviment * velocity * Time.deltaTime; // Multiplica a direção do movimento pela velocidade e o tempo
        Vector2 newPosition = characterBody.position + delta; // Cria a nova posição
        characterBody.MovePosition(newPosition);  // Move para a posição definida
    }
}
