using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Variables
    public Rigidbody rb;
    [SerializeField] private CapsuleCollider cap;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private bool isGrounded;

    [SerializeField] private float speed = 4f;
    [SerializeField] private float jump = 5f;

    public bool isCrouching;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (MenuPause.isOn) { return; }

        //Reading the Input
        float horizontal = Input.GetAxis("Horizontal") * speed;
        float vertical = Input.GetAxis("Vertical") * speed;

        //Moving
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        //Grounding
        isGrounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), 0.4f, layerMask);

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
        }

        //Crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
        }

        //On reSize la capsule collider du player afin qu'elle soit à la bonne taille lorsque que le joueur s'accroupit.
        if (isCrouching)
        {
            cap.center = new Vector3(0, -0.43f, 0);
            cap.height = 1.42f;
        }
        else
        {
            cap.center = new Vector3(0, -0.03f, 0);
            cap.height = 1.9f;
        }


    }
}
