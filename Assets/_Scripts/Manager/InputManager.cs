using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    [SerializeField] private bool canInteract = true;
    private bool interactPressed = false;
    private bool endInteractPressed = false;
    private bool interactConsumed = false;
    private bool endInteractConsumed = false;
    public static InputManager Instance;

    private void Awake()
    {
        canInteract = true;
        if (Instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        Instance = this;
    }

    public static InputManager GetInstance()
    {
        return Instance;
    }

    public void EndInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            endInteractPressed = true;
            endInteractConsumed = false;
        }
        else if (context.canceled)
        {
            endInteractPressed = false;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("called interact" + canInteract);
        if (context.performed && canInteract)
        {
            interactPressed = true;
            interactConsumed = false;
        }
        else if (context.canceled)
        {
            interactPressed = false;
        }
    }
    private void LateUpdate()
    {
        if (interactConsumed)
        {
            interactPressed = false;
        }
        if (endInteractConsumed)
        {
            endInteractPressed = false;
        }
    }

    public void OnInteractButtonPressed()
    {
        StartCoroutine(InteractButton());
    }
    public IEnumerator InteractButton()
    {
        if (canInteract)
        {
            interactPressed = true;
            interactConsumed = false;
        }
        yield return new WaitForSeconds(0.1f);
        interactPressed = false;
    }

    public bool GetEndInteractPressed()
    {
        if (endInteractPressed && !endInteractConsumed)
        {
            endInteractConsumed = true;
            return true;
        }
        return false;
    }

    public bool GetInteractPressed()
    {
        if (interactPressed && !interactConsumed)
        {
            interactConsumed = true;
            return true;
        }
        return false;
    }

    public void DisableInteract()
    {
        canInteract = false;
        endInteractPressed = false;
    }

    public void EnableInteract()
    {
        canInteract = true;
        endInteractPressed = false;
    }

}
