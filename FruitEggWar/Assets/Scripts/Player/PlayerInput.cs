using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private EggCharacter eggCharacter;
    private Camera mainCamera;

    void Start()
    {
        eggCharacter = GetComponent<EggCharacter>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (eggCharacter == null) return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * vertical + right * horizontal;
        eggCharacter.Move(moveDirection);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            eggCharacter.SetExpression(ExpressionType.Happy);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            eggCharacter.SetExpression(ExpressionType.Sad);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            eggCharacter.SetExpression(ExpressionType.Angry);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            eggCharacter.SetExpression(ExpressionType.Pout);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            eggCharacter.SetExpression(ExpressionType.Proud);
    }
}
