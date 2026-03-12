
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    // Room camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    // Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    // >>> ДОБАВЛЕНО: ограничения камеры
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    // <<<

    private void Update()
    {
        // Follow player (как у тебя было)
        Vector3 targetPos = new Vector3(
            player.position.x + lookAhead,
            player.position.y,
            transform.position.z
        );

        lookAhead = Mathf.Lerp(
            lookAhead,
            (aheadDistance * player.localScale.x),
            Time.deltaTime * cameraSpeed
        );

        // >>> ДОБАВЛЕНО: зажимаем камеру в пределах
        float clampedX = Mathf.Clamp(targetPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(targetPos.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, targetPos.z);
        // <<<
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
