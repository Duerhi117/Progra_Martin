using UnityEngine;

public abstract class Arma : MonoBehaviour
{
    public int balasPorCargador = 6;
    public int balasActuales { get; protected set; }

    public Transform shootPoint;

    protected virtual void Start()
    {
        balasActuales = balasPorCargador;
    }

    public abstract void ComenzarDisparo();
    public abstract void DetenerDisparo();
    public abstract void Recargar();
}


