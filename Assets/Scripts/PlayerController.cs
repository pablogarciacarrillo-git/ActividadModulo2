using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject blood, bloodPoint;
    public bool isAlive = true;
    public int score = 0;
    public int initialScore = 200;
    public float currentHealth, maxHealth = 100;
    public float speed = 5.5f;
    public string playerName = "Timmy";
    public CharacterController character = null;
    Vector3 velocityY = Vector3.zero;
    public float gravityScaler = 1f;
    public Animator animator;
    public AudioSource damageSound;
    
    public float damage = 35;
    public BoxCollider colliderAtaque;
    //public UnityEngine.Object puntero;

    
    public Image heart;
    public TextMeshProUGUI scoreText;
     
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //Cursor.lockState = CursorLockMode.Confined;
        score = initialScore;
        damageSound = GetComponent<AudioSource>();
        UpdateScore();

        colliderAtaque = GetComponent<BoxCollider>();
        colliderAtaque.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        setMovementAnimation();
        if (currentHealth <= 0) {
            Die();
            //SceneManager.LoadScene(0);
        }
        UpdateUI();
    }

    private void LateUpdate()
    {
        
    }
    // Funci�n que analiza los inputs, setea las variables para el animator y da movimiento al personaje
    private void setMovementAnimation() {
        //Boolean moving = false;
        
        //Pillamos el movimiento del personaje en sus dos ejes
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        /*
        Debug.Log("Angulo: " + Vector2.SignedAngle(new Vector2(transform.forward.x, transform.forward.z), new Vector2(horizontalInput, verticalInput)));
        Debug.Log("vertical = " + verticalInput);
        Debug.Log("horizontal = " + horizontalInput);
        Debug.Log("Forward: " + transform.forward);
        */

        // Transforma la posición del jugador a una posición 2D de la pantalla
        Vector2 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        //Debug.Log("PlayerPos: " + playerPos);

        // Obtiene la posición del ratón en la pantalla
        Vector2 mousePos = Input.mousePosition;
        //Debug.Log("MousePos: " + mousePos);

        // Crea un vector desde la posición del jugador hasta la del ratón
        Vector2 vector = (mousePos - playerPos);
        //Debug.Log("vector: " + vector);

        // Rota el personaje hacia la posición del ratón (negativo porque la cámara está dada la vuelta (oops))
        if (vector.magnitude > 10) // Añadida una distancia mínima para que no vibre el personaje si el cursor está encima
        {
            transform.forward = -(new Vector3(vector.x, 0, vector.y));
        }

        // Si la animaci�n est� en "atacar", ponemos la variable en falso para volver a Idle al acabar
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("BlendAtaque1"))
        {
            //Debug.Log("Attack1");
            animator.SetBool("Attack", false);
        } else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BlendAtaque2"))
        {
            //Debug.Log("Attack2");
            animator.SetBool("Attack2", false);
        } else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BlendAtaque3"))
        {
            //Debug.Log("Attack3");
            animator.SetBool("Attack3", false);
        }

        // Si se pulsa el bot�n, ponemos en true la variable de "Attack"
        if (Input.GetMouseButtonDown(0))
        {
            // Con esto se pueden encadenar los ataques:
            //      Si se está realizando el primer ataque y se pulsa el boton,
            //      se pone "en cola" el segundo ataque de la cadena
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("BlendAtaque1"))
            {
                animator.SetBool("Attack2", true);
            } 
            //      Y si se está realizando el segundo ataque y se pulsa el boton,
            //      se pone "en cola" el tercer ataque
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BlendAtaque2"))
            {
                animator.SetBool("Attack3", true);
            } else
            {
                animator.SetBool("Attack", true);
            }
        }

        // Calculo el ángulo de rotación del input para las animaciones
        float rotationAngle = Vector2.SignedAngle(new Vector2(0, 1), vector);
        //Debug.Log("Angulo: " + rotationAngle);

        // Aplico la rotación al input
        Vector3 aux = Quaternion.Euler(0, rotationAngle, 0) * new Vector3(horizontalInput, 0, verticalInput);
        aux.Normalize();
        //Debug.Log("Aux: " + aux);

        // Y lo establezco en el Animator
        animator.SetFloat("MovX", aux.z);
        animator.SetFloat("MovY", aux.x);
        
        //animator.SetFloat("Speed", character.velocity.magnitude/speed);


        /*
        // Si la animaci�n est� en "atacar", ponemos la variable en falso para volver a Idle al acabar
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01_SwordAndShiled"))
        {
            //Debug.Log("Attack1");
            animator.SetBool("Attack", false);
        } else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack02_SwordAndShiled"))
        {
            //Debug.Log("Attack2");
            animator.SetBool("Attack2", false);
        } else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack03_SwordAndShiled"))
        {
            //Debug.Log("Attack3");
            animator.SetBool("Attack3", false);
        }

        // Si se pulsa el bot�n, ponemos en true la variable de "Attack"
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("click");
            
            // Con esto se pueden encadenar los ataques:
            //      Si se está realizando el primer ataque y se pulsa el boton,
            //      se pone "en cola" el segundo ataque de la cadena
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01_SwordAndShiled"))
            {
                animator.SetBool("Attack2", true);
            } 
            //      Y si se está realizando el segundo ataque y se pulsa el boton,
            //      se pone "en cola" el tercer ataque
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack02_SwordAndShiled"))
            {
                animator.SetBool("Attack3", true);
            } else
            {
                animator.SetBool("Attack", true);
            }
        }
        
        // Si hay movimiento, entramos al if
        if (verticalInput != 0 || horizontalInput != 0)
        {
            //Debug.Log("Moving");
            // Seteamos variables que vamos a usar
            //moving = true;
            //animator.SetBool("Moving", moving);
            animator.SetBool("Moving", true);
            //animator.SetBool("MovingLeft", false);
            //animator.SetBool("MovingRight", false);
            

            //Comprobamos en que direcci�n est� movi�ndose el personaje y seteamos las variables en el animador
            if (horizontalInput < 0)
            {
                //Debug.Log("Left");
                animator.SetBool("MovingLeft", true);
                animator.SetBool("MovingRight", false);
            }
            else if (horizontalInput > 0)
            {
                //Debug.Log("Right");
                animator.SetBool("MovingRight", true);
                animator.SetBool("MovingLeft", false);
            }
            else
            {
                animator.SetBool("MovingRight", false);
                animator.SetBool("MovingLeft", false);
            } 
            
            
            if (verticalInput > 0)
            {
                //Debug.Log("Up");
                animator.SetBool("MovingForward", true);
                animator.SetBool("MovingBackward", false);
            }
            else if (verticalInput < 0)
            {
                //Debug.Log("Down");
                animator.SetBool("MovingBackward", true);
                animator.SetBool("MovingForward", false);
            }
            else
            {
                animator.SetBool("MovingBackward", false);
                animator.SetBool("MovingForward", false);
            }
        }
        // Si no hay movimiento, directito al else 
        else
        {
            //animator.SetBool("Moving", moving);
            animator.SetBool("Moving", false);

            animator.SetBool("MovingBackward", false);
            animator.SetBool("MovingForward", false);

            animator.SetBool("MovingRight", false);
            animator.SetBool("MovingLeft", false);
            
        }
        */

        // Calculamos la velocidad y aplicamos movimiento al personaje
        velocityY += gravityScaler * Physics.gravity * Time.deltaTime;

        //character.Move(verticalInput * transform.forward * speed * Time.deltaTime
        //    + horizontalInput * transform.right * speed * Time.deltaTime + velocityY);

        character.Move(verticalInput * (new Vector3(0, 0, -1)) * speed * Time.deltaTime
            + horizontalInput * (new Vector3(-1, 0, 0)) * speed * Time.deltaTime + velocityY);
        
        if (character.isGrounded)
        {
            velocityY = Vector3.zero;
        }
        //return moving;
    }

    void Die()
    {
        isAlive = false;

        Invoke("loadMenu", 1);

    }

    void loadMenu() {
        List<int> puntuaciones = new List<int>();

        for (int i = 0; i < MenuController.numPuntuaciones; i++)
        {
            puntuaciones.Add(PlayerPrefs.GetInt("BestScore" + (i+1), 0));
        }

        puntuaciones.Add(score);

        puntuaciones.Sort();
        puntuaciones.Reverse();

        for (int i = 0; i < MenuController.numPuntuaciones; i++)
        {
            PlayerPrefs.SetInt("BestScore" + (i+1), puntuaciones[i]);
        }
        
        SceneManager.LoadScene("Menu");
    }

    void UpdateScore()
    {
        score -= 1;

        if (isAlive)
        {
            Invoke("UpdateScore", 1);
        }
    }

    void UpdateUI()
    {
        heart.fillAmount = currentHealth/maxHealth;
        scoreText.text = "Score: " + score;
    }

    public void receiveDamage(float damage)
    {
        //Debug.Log("OW (" + damage + " damage)");
        currentHealth -= damage;
        GameObject bloodSpash = Instantiate(blood, bloodPoint.transform.position, Quaternion.identity);
        //bloodSpash.transform.LookAt(bloodPoint.transform.position + Vector3.up * 0.5f);
        damageSound.Play();

        //Aquí se puede añadir un tiempo de invencibilidad desactivando el collider
    }

    public void ActivateAttack()
    {
        colliderAtaque.enabled = true;
    }

    public void DeactivateAttack()
    {
        colliderAtaque.enabled = false;
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        Enemy controller = other.gameObject.GetComponent<Enemy>();

        if (controller != null)
        {
            controller.ReceiveDamage(damage);
            Debug.Log("Attacked Enemy for " + damage + " damage");
        }
        Debug.Log("Attack");
    }
}
