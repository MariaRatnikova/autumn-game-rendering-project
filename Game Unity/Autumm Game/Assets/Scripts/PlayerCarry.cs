using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    [Header("Carry Settings")]
    public Transform holdPoint; // im Player als Child anlegen (HoldPoint)

    private Nut carriedNut;

    public bool IsCarrying => carriedNut != null;

    // Wird von der Nuss aufgerufen
    public bool PickUp(Nut nut)
    {
        if (nut == null) return false;
        if (IsCarrying) return false; // nur 1 Nuss gleichzeitig

        carriedNut = nut;
        carriedNut.OnPickedUp(holdPoint);
        return true;
    }

    // Wird vom Cache aufgerufen
    public Nut DropOff()
    {
        if (!IsCarrying) return null;

        Nut nut = carriedNut;
        carriedNut = null;
        return nut;
    }
}