using UnityEngine;
using System.Collections;

public class EnemyBossMovement : MonoBehaviour
{
    public float speed;
    private Transform target;
    // Use this for initialization
    void Start(){
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update(){
        if (Vector2.Distance(transform.position, target.position) > 1.5){
        float bossSpeed = Mathf.Pow(Vector2.Distance(transform.position, target.position),2);
        transform.position = Vector2.MoveTowards(transform.position, target.position, bossSpeed*speed*Time.deltaTime );
        }
    }
}
