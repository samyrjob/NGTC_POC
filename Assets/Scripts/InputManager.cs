using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;


    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {

        onFoot.Disable();
        
    }

}
