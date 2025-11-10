using UnityEngine;
using UnityEngine.InputSystem;

public class CapsuleControler : MonoBehaviour
{
    // Pour les contrôles, of course :
    [SerializeField] private InputActionAsset actions;
    [SerializeField] private float speed;
    private InputAction yAxis;
    private InputAction xAxis;

    private bool interactIsPressed = false;

    void Awake()
    {
        xAxis = actions.FindActionMap("Jack").FindAction("MoveX");
        yAxis = actions.FindActionMap("Jack").FindAction("MoveY");
    }

    void OnEnable()
    {
        actions.FindActionMap("Jack").Enable();
        //// Avant de faire les lignes de code Interact, il faut configurer l'InputActionAsset.
        // actions.FindActionMap("Jack").FindAction("Interact").performed += OnInteractPressed; 
    }

    void OnDisable()
    {
        actions.FindActionMap("Jack").Disable();
        // actions.FindActionMap("Jack").FindAction("Interact").performed -= OnInteractPressed;

    }

    void Update()
    {
        MoveX();
        MoveY();
    }

    private void MoveX()
    {
        transform.Translate(xAxis.ReadValue<float>() * speed * Time.deltaTime, 0f, 0f);
    }
    private void MoveY()
    {
        transform.Translate(0f, yAxis.ReadValue<float>() * speed * Time.deltaTime, 0f);
    }

    public bool OnInteractPressed()
    {
        bool result = interactIsPressed;
        interactIsPressed = false;
        return result;
    }
}