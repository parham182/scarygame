using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPicup : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    
    Vector3 defualtPos;

    public PlayerInput player;
    InputActionMap playermap;
    public InputActionReference picupinput;

   ItemCheck itemCheck;

    void OnEnable()
    {
        playermap.Enable();
    }
    void OnDisable()
    {
        playermap.Disable();
    }

    void Start()
    {
        defualtPos = this.gameObject.transform.position;
        playermap = player.actions.FindActionMap("PlayerMove");
        itemCheck = FindAnyObjectByType<ItemCheck>();
    }

    void Update()
    {
        PicUpProcces();
    }

    private void PicUpProcces()
    {
        if (!itemCheck.canPicup) return;
        
        if (picupinput.action.WasPressedThisFrame())
        {
            Debug.Log("You Pressed the Button");
            Debug.Log(itemCheck.picupGameobject);
        }
    }
}
