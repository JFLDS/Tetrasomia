using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //permet de obliger l'utilisation de l'ature fichier et on empecher une suppression du fichier utile.
public class PlayerMotor : MonoBehaviour
{
    //meme variable que dans PlayerControler
    private Vector3 velocity;
    private Vector3 rotation;
    private float cameraRotationX = 0f;
    private float currentRotationCameraX = 0f;
    private Vector3 JumpForceVelocity;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float cameraRotationLimit = 70f;

    //variable pour l'application des mouvements
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //fonction move qui prend la valeur de la vitesse
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    public void ApplyJumpForce(Vector3 _JumpForceVelocity)
    {
        JumpForceVelocity = _JumpForceVelocity;
    }

    //delay d'appel
    private void FixedUpdate()
    {
        performMovement();
        performRotation();
    }

    //applique la force ou le mouvement sur le RigidBody
    private void performMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if(JumpForceVelocity != Vector3.zero)
        {
            rb.AddForce(JumpForceVelocity * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    private void performRotation()
    {
        //on calcul la rotation de la camera
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        currentRotationCameraX -= cameraRotationX;
        currentRotationCameraX = Mathf.Clamp(currentRotationCameraX, -cameraRotationLimit, cameraRotationLimit);

        //on applique la rotation de la camera
        cam.transform.localEulerAngles = new Vector3(currentRotationCameraX, 0f, 0f);
    }
}
