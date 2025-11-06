using UnityEngine;
using UnityEngine.InputSystem;

public class CapsuleControler : MonoBehaviour
{
    // Pour les contrôles, of course :
    [SerializeField] private InputActionAsset actions;
    [SerializeField] private float speed;
    private InputAction yAxis;
    private InputAction xAxis;


    void Awake()
    {
        xAxis = actions.FindActionMap("Jack").FindAction("MoveX");
        yAxis = actions.FindActionMap("Jack").FindAction("MoveY");
    }

    void OnEnable()
    {
        actions.FindActionMap("Jack").Enable();
    }

    void OnDisable()
    {
        actions.FindActionMap("Jack").Disable();
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

}