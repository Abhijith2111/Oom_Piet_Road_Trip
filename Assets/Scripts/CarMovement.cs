using UnityEngine;
using UnityEngine.EventSystems; // Needed for button hold events

public class CarMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 10f;             // Forward speed
    public float horizontalSpeed = 5f;    // Side movement speed
    public float xBound = 5f;             // Left/right limits
    public float acceleration = 0.5f;     // Speed increase per second
    public float maxSpeed = 50f;          // Maximum forward speed

    [Header("UI Buttons")]
    public GameObject leftButton;         // Left button UI
    public GameObject rightButton;        // Right button UI

    private int buttonDirection = 0;      // -1 = left, 1 = right, 0 = none

    private void Start()
    {
        // Setup hold-to-steer events for mobile buttons
        if (leftButton != null) AddHoldListener(leftButton, -1);
        if (rightButton != null) AddHoldListener(rightButton, 1);
    }

    private void Update()
    {
        // Gradually increase forward speed
        speed = Mathf.Min(speed + acceleration * Time.deltaTime, maxSpeed);

        // Keyboard input (A/D or arrow keys)
        float keyboardInput = Input.GetAxis("Horizontal");

        // Dead zone fix (ignore tiny unwanted drift)
        if (Mathf.Abs(keyboardInput) < 0.1f)
            keyboardInput = 0f;

        // Combine keyboard + button input
        float movementX = keyboardInput + buttonDirection;

        // --- Forward movement ---
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);

        // --- Side movement ---
        float newX = transform.position.x + movementX * horizontalSpeed * Time.deltaTime;

        // Clamp X position so car stays on the road
        newX = Mathf.Clamp(newX, -xBound, xBound);

        // Apply new position (keep Y and Z unchanged)
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void AddHoldListener(GameObject buttonObj, int dir)
    {
        EventTrigger trigger = buttonObj.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = buttonObj.AddComponent<EventTrigger>();

        trigger.triggers.Clear(); // prevent duplicate entries

        // PointerDown = start steering
        EventTrigger.Entry downEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        downEntry.callback.AddListener((data) => { buttonDirection = dir; });
        trigger.triggers.Add(downEntry);
        
        // PointerUp = stop steering
        EventTrigger.Entry upEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        upEntry.callback.AddListener((data) => { buttonDirection = 0; });
        trigger.triggers.Add(upEntry);
    }
}