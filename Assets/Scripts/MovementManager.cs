using UnityEngine;

public enum TipoJugador
{
    JUGADOR1, JUGADOR2
}

public class MovementManager : MonoBehaviour
{
    public float speed;
    public TipoJugador tipo;
    public bool moverse = false;

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

            Vector3 actualPos = GetComponent<Transform>().position;
            GetComponent<Transform>().position = new Vector3(
                    actualPos.x,
                    Mathf.Clamp(actualPos.y + (speed * movement * Time.deltaTime), -8f, 8f),
                    actualPos.z
            );
        }
    }
    public void reiniciar()
    {
        transform.position = new Vector3(transform.position.x, 0, 0);
        moverse = true;
    }
}
