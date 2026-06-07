using UnityEngine;

public enum ExpressionType
{
    Happy,
    Sad,
    Angry,
    Pout,
    Proud
}

public class EggExpression : MonoBehaviour
{
    [Header("Expression Sprites")]
    public Sprite happyFace;
    public Sprite sadFace;
    public Sprite angryFace;
    public Sprite poutFace;
    public Sprite proudFace;

    private SpriteRenderer faceRenderer;
    private ExpressionType currentExpression = ExpressionType.Happy;

    void Awake()
    {
        faceRenderer = GetComponent<SpriteRenderer>();
        if (faceRenderer == null)
        {
            faceRenderer = transform.Find("Face").GetComponent<SpriteRenderer>();
        }
    }

    public void SetExpression(ExpressionType expression)
    {
        currentExpression = expression;
        Sprite targetSprite = GetExpressionSprite(expression);
        if (targetSprite != null && faceRenderer != null)
        {
            faceRenderer.sprite = targetSprite;
        }
    }

    private Sprite GetExpressionSprite(ExpressionType expression)
    {
        switch (expression)
        {
            case ExpressionType.Happy: return happyFace;
            case ExpressionType.Sad: return sadFace;
            case ExpressionType.Angry: return angryFace;
            case ExpressionType.Pout: return poutFace;
            case ExpressionType.Proud: return proudFace;
            default: return happyFace;
        }
    }

    public ExpressionType GetCurrentExpression() => currentExpression;
}
