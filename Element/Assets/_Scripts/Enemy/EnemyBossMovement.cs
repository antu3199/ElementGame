using UnityEngine;
using System.Collections;

public class EnemyBossMovement : MonoBehaviour
{
    public float speed = 0.01f;
    private Transform target;
    
    // Gets a reference to the "Player"
    void Start(){
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update(){
        // Move near the player
        if (Vector2.Distance(transform.position, target.position) > 1.5){
          float bossSpeed = Mathf.Pow(Vector2.Distance(transform.position, target.position),2);
          transform.position = Vector2.MoveTowards(transform.position, target.position, bossSpeed*speed*Time.deltaTime );
        }
    }
}
