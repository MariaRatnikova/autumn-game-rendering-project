using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Health health = collision.GetComponent<Health>();
        if (health == null) return;

        health.TakeDamage(damage);

        // SOUND: Schaden bekommen
        AudioManager.Instance.PlayDamage();
    }
}