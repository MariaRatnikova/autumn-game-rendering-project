using UnityEngine;

public class Enemy_Run : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField]private float speed;
    [SerializeField] private float damage;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag =="Player"){
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
    private void Update(){
        if(movingLeft){
            if(transform.position.x > leftEdge)
            {
                
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position = new Vector3(transform.position.x - speed *Time.deltaTime,transform.position.y, transform.position.z);
            }

         else
                movingLeft =false;
            
        }   
        else{
            if(transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed *Time.deltaTime,transform.position.y, transform.position.z);
                
                transform.localScale = Vector3.one;

            }
            else   
                movingLeft =true;
            
        }
            
    }

    private void Awake(){
        
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }
}
