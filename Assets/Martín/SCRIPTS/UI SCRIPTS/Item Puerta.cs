using UnityEngine;

public class Llave : MonoBehaviour
{
    [SerializeField] private bool agarrarLlave;
    [SerializeField] private bool mouseIn;
    [SerializeField] private float radio;
    [SerializeField] private LayerMask jugadorMascara;

    public static int objetosRecogidos = 0; // Total de objetos recolectados
    public static int objetosNecesarios = 6; // Cantidad necesaria para abrir la puerta

    private void OnMouseEnter()
    {
        mouseIn = true;
    }

    private void OnMouseExit()
    {
        mouseIn = false;
    }

    private void Update()
    {
        agarrarLlave = Physics.CheckSphere(transform.position, radio, jugadorMascara);

        if (Input.GetKeyDown(KeyCode.E) && agarrarLlave && mouseIn)
        {
            objetosRecogidos++; // Incrementar el contador de objetos
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}


