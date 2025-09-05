using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isGrounded = true; // состояние на земле
    public float jumpForce = 11f; // сила прыжка 
    public PlayerMovement movement;
    public Rigidbody rb;
    private ScoreDisplay scoreDisplay;  // кэш для ссылки
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position; // запоминаем стартовую позицию при старте сцены
    }
    void Awake()
    {
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
        if (scoreDisplay == null)
        {
            Debug.LogError("ScoreDisplay не найден в сцене!");
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A)) { rb.AddForce(-0.5f, 0, 0, ForceMode.VelocityChange); }
        if (Input.GetKey(KeyCode.D)) { rb.AddForce(0.5f, 0, 0, ForceMode.VelocityChange); }
        if (Input.GetKeyDown(KeyCode.W) && isGrounded) // прыжок при нажатии и только если на земле
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
            isGrounded = false; // теперь в воздухе
        }
        // Проверка падения ниже определенного уровня
        if ((transform.position.y < -5f) | (transform.position.x < -14f))
        {
            FallReset();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("apple"))
        {
            Destroy(collision.gameObject);
            if (scoreDisplay != null)
            {
               GameManager.Instance.AddScore(1);
            }
        }
        else if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("checkpoint"))
        {
            movement.enabled = false;
            FindObjectOfType<GameManager>().CompleteLevel();
        }
        else if (collision.gameObject.CompareTag("dude"))
        {
            movement.enabled = false;
            GameManager.Instance.TakeDamage();
            transform.position = startPosition;
            movement.enabled = true;
        }
    }
        void FallReset()
    {
        movement.enabled = false;
        GameManager.Instance.TakeDamage();
        transform.position = startPosition;
        rb.velocity = Vector3.zero;
        movement.enabled = true;
    }
    }
