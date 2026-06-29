using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemCheck : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    public Transform picupGameobject;

    float distance = 10f;

    Vector3 defualtPos;
    RaycastHit hit;

    public bool canPicup = false;

    void Start()
    {
        defualtPos = this.gameObject.transform.position;
    }

    void Update()
    {
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, distance) || canPicup)
        {
            Debug.Log(hit.collider.name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPicup = true;
            Debug.Log("Now You Can PicUp The Item");
            picupGameobject = GetComponent<Transform>().gameObject.transform;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPicup = false;
            Debug.Log("You can`t picup item anymore");
        }
    }
}
