using UnityEngine;

public class Nut : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Beim Aufheben: an HoldPoint "dranhängen"
    public void OnPickedUp(Transform holdPoint)
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        if (col != null)
            col.enabled = false;

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    // Beim Abliefern: aktuell einfach entfernen
    public void OnDeposited()
    {
        Destroy(gameObject);
    }
}