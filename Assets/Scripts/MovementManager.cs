using UnityEngine;
using UnityEngine.UI;

public enum TipoJugador
{
    JUGADOR1, JUGADOR2
}

public class MovementManager : MonoBehaviour
{
    public float speed;
    public TipoJugador tipo;
    public bool moverse = false;
    public float limite=8;
    public float timer =0;
    public float cooldown = 0;
    public bool poder = true;
    public bool enCooldown = false;
    public GameObject cooldownPoderUI;
    public float elinput;

    private void Update()
    {
        if (moverse)
        {
            float movement;
            if (tipo == TipoJugador.JUGADOR1)
            {
                // Leer el input del usuario
                movement = Input.GetAxis("Vertical");
            }
            else
            {
                movement = Input.GetAxis("Mouse Y");
            }
            elinput = movement;
            Vector3 actualPos = GetComponent<Transform>().position;
            /*GetComponent<Transform>().position = new Vector3(
                    actualPos.x,
                    Mathf.Clamp(actualPos.y + (speed * movement * Time.deltaTime), -limite, limite),
                    actualPos.z
            );*/
            if (tipo==TipoJugador.JUGADOR2 && movement!=0)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(actualPos.x, actualPos.y + 10, actualPos.z), movement/Mathf.Abs(movement) * speed * Time.deltaTime);
            }
            else { transform.position = Vector3.MoveTowards(transform.position, new Vector3(actualPos.x, actualPos.y + 10, actualPos.z), movement * speed * Time.deltaTime); }
            
            if (transform.position.y < -limite) { transform.position = new Vector3(transform.position.x, -limite, 0); }
            else if(transform.position.y > limite) { transform.position = new Vector3(transform.position.x, limite, 0); }


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
            if(cooldown > 0 && enCooldown)
            {
                cooldown -= Time.deltaTime;
                cooldownPoderUI.transform.GetChild(0).GetComponent<Image>().fillAmount = 1f - (cooldown/30f);
            }
            else if(cooldown<=0 && enCooldown)
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
        
    }
    public void reiniciar()
    {
        timer = 0;
        cooldown = 0;
        poder = true;
        enCooldown=false;
        desactivarPoder();
        cooldownPoderUI.transform.GetChild(1).gameObject.SetActive(true);
        cooldownPoderUI.transform.GetChild(2).gameObject.SetActive(false);
        cooldownPoderUI.transform.GetChild(3).gameObject.SetActive(false);
        cooldownPoderUI.transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
        transform.position = new Vector3(transform.position.x, 0, 0);
        moverse = true;
    }
    public void activarPoder()
    {
        if (poder)
        {
            poder = false;
            speed = 17;
            transform.localScale = new Vector3(transform.localScale.x, 6, 1);
            limite = 7;
            timer = 5;
        }
    }
    public void desactivarPoder()
    {
        speed = 12;
        transform.localScale = new Vector3(transform.localScale.x, 4, 1);
        limite = 8;
    }
}
