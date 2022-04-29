using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trazarRuta : MonoBehaviour
{
    public GameObject cuadrado;
    public PaddleIA paddle2IA;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ball"))
        {
            float distY;
            float posY = 0;
            BallMovementManager manager = collision.GetComponent<BallMovementManager>();
            if (manager.speed.x > 0 )
            {
                //Debug.Log("angulo: "+Vector2.Angle(new Vector2(1, 0),new Vector2(manager.speed.x,manager.speed.y).normalized));
                if (manager.speed.y >= 0)
                {
                    distY = ((Mathf.Tan(Vector2.Angle(new Vector2(1, 0), new Vector2(manager.speed.x, manager.speed.y).normalized) * Mathf.Deg2Rad)) * (13.5f));
                    float distParaLimiteSuperior;
                   
                    distParaLimiteSuperior = 9.5f - collision.transform.position.y;
                    if (distParaLimiteSuperior < distY)
                    {
                        posY = 9.5f - (distY - distParaLimiteSuperior);
                    }
                    else posY = distY + collision.transform.position.y;
                }
                else
                {
                    float distParaLimiteInferior;
                    if (collision.transform.position.y < 0)
                    {
                        distY = -1 * ((Mathf.Tan(Vector2.Angle(new Vector2(1, 0), new Vector2(manager.speed.x, manager.speed.y).normalized) * Mathf.Deg2Rad)) * (13.5f));
                    }
                    else
                    {
                        distY = ((Mathf.Tan(Vector2.Angle(new Vector2(1, 0), new Vector2(manager.speed.x, manager.speed.y).normalized) * Mathf.Deg2Rad)) * (13.5f));
                    }
                    //Debug.Log("distancia en velocidad hacia abajo: " + distY);
                    distParaLimiteInferior = 9.5f + collision.transform.position.y;
                    if (distParaLimiteInferior < Mathf.Abs(distY))
                    {
                        if (distY < 0)
                        {
                            posY = -9.5f + (Mathf.Abs(distY) - distParaLimiteInferior);
                        }
                        else
                        {
                            posY = -9.5f + (distY - distParaLimiteInferior);
                        }
                    }
                    else 
                    {
                        if (distY < 0)
                        {
                            posY = collision.transform.position.y + distY;
                        }
                        else
                        {
                            posY = collision.transform.position.y - distY;
                        }
                        
                    }
                }
                /*GameObject g = Instantiate(cuadrado, new Vector3(13.5f, posY, 1), Quaternion.identity);
                StartCoroutine(destruirCubo(g));*/
                paddle2IA.posicionEsperada = posY;
                paddle2IA.seguirDestino= true;
            }
        }
    }
    IEnumerator destruirCubo(GameObject dest)
    {
        yield return new WaitForSeconds(5);
        Destroy(dest);
        yield return null;
    }
}
