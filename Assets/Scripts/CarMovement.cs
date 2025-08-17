using UnityEngine;
using UnityEngine.EventSystems; 

public class CarMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 10f;             
    public float horizontalSpeed = 5f;    
    public float xBound = 5f;            
    public float acceleration = 0.5f;     
    public float maxSpeed = 50f;          

    [Header("UI Buttons")]
    public GameObject leftButton;         
    public GameObject rightButton;        

    private int buttonDirection = 0;      

    private void Start()
    {
        if (leftButton != null) AddHoldListener(leftButton, -1);
        if (rightButton != null) AddHoldListener(rightButton, 1);
    }

    private void Update()
    {
        speed = Mathf.Min(speed + acceleration * Time.deltaTime, maxSpeed);

        float keyboardInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(keyboardInput) < 0.1f)
            keyboardInput = 0f;

        float movementX = keyboardInput + buttonDirection;

        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);

        float newX = transform.position.x + movementX * horizontalSpeed * Time.deltaTime;

        newX = Mathf.Clamp(newX, -xBound, xBound);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void AddHoldListener(GameObject buttonObj, int dir)
    {
        EventTrigger trigger = buttonObj.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = buttonObj.AddComponent<EventTrigger>();

        trigger.triggers.Clear(); 

        EventTrigger.Entry downEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        downEntry.callback.AddListener((data) => { buttonDirection = dir; });
        trigger.triggers.Add(downEntry);
        
        EventTrigger.Entry upEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        upEntry.callback.AddListener((data) => { buttonDirection = 0; });
        trigger.triggers.Add(upEntry);
    }
}