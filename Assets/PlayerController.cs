using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float sensi = 7f;

    private PlayerMotor motor;

    // Start is called before the first frame update
    private void Start()
    {
        motor = GetComponent<PlayerMotor>();   
    }

    // Update is called once per frame
    private void Update()
    {
        //Calcul de la vélocité                     //renvoie 0 si on ne bouge pas
        float xMovement = Input.GetAxisRaw("Horizontal");  //Q(touche négative)=(-1) & D(touche positive)=(1)
        float zMovement = Input.GetAxisRaw("Vertical");    //S(touche négative)=(-1) & Z(touche positive)=(1)
        Vector3 movementHorizontal = transform.right * xMovement;
        Vector3 movementVertical = transform.forward * zMovement;
        Vector3 velocity = (movementHorizontal + movementVertical).normalized * speed;
        motor.Move(velocity);

        //Calcul de la rotation du joueur (Axe horizontal)
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0f, yRotation, 0f) * sensi;
        motor.Rotate(rotation);

        //Calcul de la rotation de la camera (Axe vertical)
        float xRotation = Input.GetAxisRaw("Mouse Y");
        Vector3 rotationCam = new Vector3(xRotation, 0f, 0f) * sensi;
        motor.RotateCam(rotationCam);
    }
}
