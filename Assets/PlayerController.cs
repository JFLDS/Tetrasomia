using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;

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
        float xM = Input.GetAxisRaw("Horizontal");  //Q(touche négative)=(-1) & D(touche positive)=(1)
        float zM = Input.GetAxisRaw("Vertical");    //S(touche négative)=(-1) & Z(touche positive)=(1)

        Vector3 moveHorizontal = transform.right * xM;
        Vector3 moveVertical = transform.forward * zM;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;
    }
}
