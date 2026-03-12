using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn1 : MonoBehaviour
{
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uIManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uIManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
         if (currentCheckpoint == null)
        {
           // uIManager.GameOver();
            return;
         }
        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();
        

        Camera.main.GetComponent<CameraControler>().MoveToNewRoom(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
