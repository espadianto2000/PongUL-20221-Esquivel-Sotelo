using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaddleIA : MonoBehaviour
{
    public GameObject ball;
    public bool activo = false;
    public float speed = 5;
    public float limite = 8;
    public float timer = 0;
    public float cooldown = 0;
    public bool poder = true;
    public bool enCooldown = false;
    public float posicionEsperada;
    private float temp = 0;
    public bool seguirDestino=false;
    public GameObject cooldownPoderUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (posicionEsperada != temp)
        {
            temp = posicionEsperada;
            float posiblePos = transform.position.y + ((13.5f*ball.transform.position.x/ball.GetComponent<BallMovementManager>().speed.x)*speed);
            if (Mathf.Abs(posicionEsperada - transform.position.y) > 10)
            {
                activarPoder();
            }
        }
        if(activo && ball.GetComponent<BallMovementManager>().speed.x>0)
        {
            if(transform.position.y <= limite && transform.position.y >= -limite)
            {
                if (seguirDestino)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, posicionEsperada, 0), speed * Time.deltaTime);
                }else transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, ball.transform.position.y, 0), speed * Time.deltaTime);
            }
            else if(transform.position.y < -limite) { transform.position = new Vector3(transform.position.x, -limite, 0); }
            else { transform.position = new Vector3(transform.position.x, limite, 0); }
            
            if((ball.transform.position.x > 9 && Mathf.Abs(transform.position.y - posicionEsperada) > 5f) || (ball.transform.position.x > 11 && Mathf.Abs(transform.position.y - posicionEsperada) > 3f))
            {
                activarPoder();
            }

        }
        if (activo)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                cooldownPoderUI.transform.GetChild(1).gameObject.SetActive(false);
                cooldownPoderUI.transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                if (!enCooldown && !poder)
                {
                    desactivarPoder();
                    cooldown = 30;
                    enCooldown = true;
                    cooldownPoderUI.transform.GetChild(1).gameObject.SetActive(true);
                    cooldownPoderUI.transform.GetChild(2).gameObject.SetActive(false);
                    cooldownPoderUI.transform.GetChild(3).gameObject.SetActive(true);
                }
            }
            if (cooldown > 0 && enCooldown)
            {
                cooldown -= Time.deltaTime;
                cooldownPoderUI.transform.GetChild(0).GetComponent<Image>().fillAmount = 1f - (cooldown / 30f);
            }
            else if (cooldown <= 0 && enCooldown)
            {
                if (!poder)
                {
                    cooldownPoderUI.transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
                    cooldownPoderUI.transform.GetChild(3).gameObject.SetActive(false);
                    enCooldown = false;
                    poder = true;
                }
            }
        }
        if (ball.GetComponent<BallMovementManager>().speed.x < 0 && seguirDestino)
        {
            seguirDestino = false;
        }
    }

    public void reiniciar()
    {
        seguirDestino = false;
        timer = 0;
        cooldown = 0;
        poder = true;
        enCooldown = false;
        cooldownPoderUI.transform.GetChild(1).gameObject.SetActive(true);
        cooldownPoderUI.transform.GetChild(2).gameObject.SetActive(false);
        cooldownPoderUI.transform.GetChild(3).gameObject.SetActive(false);
        cooldownPoderUI.transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
        desactivarPoder();
        transform.position = new Vector3(transform.position.x, 0, 0);
        activo = true;
    }
    public void activarPoder()
    {
        if (poder)
        {
            poder = false;
            speed = 10;
            transform.localScale = new Vector3(transform.localScale.x, 6, 1);
            limite = 7;
            timer = 5;
        }
    }
    public void desactivarPoder()
    {
        speed = 5;
        transform.localScale = new Vector3(transform.localScale.x, 4, 1);
        limite = 8;
    }
}
