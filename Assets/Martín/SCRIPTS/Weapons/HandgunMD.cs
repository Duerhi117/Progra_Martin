using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Martín.Weapons
{
    /// <summary>
    /// EJERCICIO
    /// 
    /// 
    /// Realizar la lógica detras de la recarga de la pistola como si esta fuera un revolver
    /// 
    /// </summary>
    public class Handgun : Weapon
    {
        protected internal override void Shoot()
        {
            base.Shoot();
        }

        protected internal override void Reload()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
              Debug.Log("Recargo");

            }
        }

    }

}

