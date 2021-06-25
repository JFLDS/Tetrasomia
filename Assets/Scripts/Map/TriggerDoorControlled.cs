using UnityEngine;

public class TriggerDoorControlled : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public bool opening = false;
    public bool closing = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (opening) OpenDoor();
        if (closing) CloseDoor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            opening = true;
            closing = false;
        }
    }

    void OpenDoor()
    {
        animator.Play("door_1_open", 0, 0.0f);
        opening = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            opening = false;
            closing = true;
        }
    }

    void CloseDoor()
    {
        animator.Play("door_1_close", 0, 0.0f);
        closing = false;
    }
}
