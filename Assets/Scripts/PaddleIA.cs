using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleIA : MonoBehaviour
{
    public GameObject ball;
    public bool activo = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activo && ball.GetComponent<BallMovementManager>().speed.x>0)
        {
            if(transform.position.y <= 8 && transform.position.y >= -8)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, ball.transform.position.y, 0), 4.5f * Time.deltaTime);
            }
            else if(transform.position.y < -8) { transform.position = new Vector3(transform.position.x, -8, 0); }
            else { transform.position = new Vector3(transform.position.x, 8, 0); }
            
        }
    }

    public void reiniciar()
    {
        transform.position = new Vector3(transform.position.x, 0, 0);
        activo = true;
    }

}
