using UnityEngine;

[RequireComponent(typeof(EggExpression))]
[RequireComponent(typeof(EggDecoration))]
public class EggCharacter : MonoBehaviour
{
    [Header("Character Info")]
    public string characterName;
    public int characterId;
    public bool isRare;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    private EggExpression expression;
    private EggDecoration decoration;
    private Rigidbody rb;
    private Vector3 moveInput;

    void Awake()
    {
        expression = GetComponent<EggExpression>();
        decoration = GetComponent<EggDecoration>();
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    public void Move(Vector3 direction)
    {
        moveInput = direction.normalized;
    }

    void FixedUpdate()
    {
        if (moveInput.magnitude > 0.1f)
        {
            Vector3 targetPosition = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(targetPosition);

            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    public void SetExpression(ExpressionType expressionType)
    {
        expression.SetExpression(expressionType);
    }

    public void EquipDecorationItem(DecorationItem item)
    {
        decoration.EquipDecoration(item);
    }

    public void TeleportTo(Vector3 position)
    {
        rb.position = position;
    }
}
