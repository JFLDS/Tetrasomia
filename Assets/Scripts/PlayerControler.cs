using UnityEngine;

[RequireComponent(typeof(PlayerMotor))] //permet de obliger l'utilisation de l'ature fichier et on empecher une suppression du fichier utile.
public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    private float speed = 10; //player speed value

    [SerializeField]
    private float mouseSensitivityX = 3; //mouse speed value
    [SerializeField]
    private float mouseSensitivityY = 3;

    //rajout du haut saut
    [SerializeField]
    private float AirJumpForce = 1000f;

    private PlayerMotor motor; //reference

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        /**
         * Recuperation des touches de clavier
         * 
         * Calculer la vitesse de mouvement du joueur.
         */
        //on recupere l'axe horizontal
        float xMov = Input.GetAxisRaw("Horizontal");
        //on recupere l'axe vertical
        float zMov = Input.GetAxisRaw("Vertical");

        //mouvement de base
        Vector3 moveHorizontal = transform.right * xMov;
        Vector3 moveVertical = transform.forward * zMov;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

        //faire bouger le perso
        motor.Move(velocity);

        /**
         * Recuperation des mouvements de la souris
         * 
         * On calcul la rotation du joueur en un vecteur.
         */
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0, yRot, 0) * mouseSensitivityX;

        motor.Rotate(rotation);

        //on calcul la rotation de la camera
        float xRot = Input.GetAxisRaw("Mouse Y");

        float cameraRotationX = xRot * mouseSensitivityY;

        motor.RotateCamera(cameraRotationX);

        /**
         * Calcul la force du Jump et applique le haut saut (competence vent)
         */
        Vector3 JumpForceVelocity = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            JumpForceVelocity = Vector3.up * AirJumpForce;
        }

        motor.ApplyJumpForce(JumpForceVelocity);
    }
}
