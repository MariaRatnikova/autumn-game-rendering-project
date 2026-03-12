using UnityEngine;

public class NutCache : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerCarry carry = other.GetComponent<PlayerCarry>();
        if (carry == null) return;

        if (!carry.IsCarrying) return;

        Nut nut = carry.DropOff();
        if (nut != null)
        {
            nut.OnDeposited();
            GameManager.Instance.DepositNut();

            // 🔊 SOUND: Nuss abgelegt
            AudioManager.Instance.PlayPick();
        }
    }
}