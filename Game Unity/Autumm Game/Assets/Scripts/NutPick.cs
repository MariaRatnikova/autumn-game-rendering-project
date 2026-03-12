using UnityEngine;

public class NutPick : MonoBehaviour
{
    private Nut nut;

    private void Awake()
    {
        nut = GetComponent<Nut>();
        if (nut == null)
            nut = gameObject.AddComponent<Nut>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerCarry carry = other.GetComponent<PlayerCarry>();
        if (carry == null) return;

        if (carry.IsCarrying) return;

        carry.PickUp(nut);

        // SOUND: Nuss aufgehoben
        AudioManager.Instance.PlayPick();
    }
}