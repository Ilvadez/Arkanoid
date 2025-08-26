using UnityEngine;

public class ReflectBall
{
    public Vector2 GetDirectionReflect(Vector2 inVector, Vector2 normal)
    {
        return Vector2.Reflect(inVector, normal);
    }
    public Vector2 GetDirectionFromPaddle(Collision2D collision, Transform transform)
    {
        float posPoint = transform.position.x;
        float posPaddle = collision.collider.bounds.center.x;
        float hitFactor = (posPoint - posPaddle) / (collision.collider.bounds.size.x / 2f);
        float normalPaddle = collision.contacts[0].normal.y;//1f;
        return new Vector2(hitFactor, normalPaddle).normalized;
    }
    public Vector2 GetDirectionFromBrick(Collision2D collision, Vector2 inVector)
    {
        Vector2 posHit = collision.GetContact(0).point;
        Vector2 posTarget = collision.collider.bounds.center;
        Vector2 extents = collision.collider.bounds.extents;
        Vector2 direction = posTarget - posHit;

        Vector2 normal;

        if (Mathf.Abs(direction.x / extents.x) > Mathf.Abs(direction.y / extents.y))
        {
            normal = direction.x > 0 ? Vector2.right : Vector2.left;
        }
        else
        {
            normal = direction.y > 0 ? Vector2.up : Vector2.down;
        }
        return Vector2.Reflect(inVector, normal);
    }
    public Vector2 FixedReflect(Vector2 direction, float minX)
    {
        if (Mathf.Abs(direction.x) < minX)
        {
            direction.x = Mathf.Sign(direction.x) == 0 ? minX : Mathf.Sign(direction.x) * minX;
        }
        return direction.normalized;
    }
}
