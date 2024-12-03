
using UnityEngine;
using UnityEngine.AI;


namespace Mart�n
{
    public class PerseguirObjetivo : MonoBehaviour
    {

        public Transform objetivo;
        public float velocidad;

        private NavMeshAgent agent;

        private bool persiguiendo = false;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (persiguiendo && objetivo != null)
            {
                agent.destination = objetivo.position;
            }
        }

        public void Perseguir()
        {
            agent.speed = velocidad;
            persiguiendo = true;
        }

        public void DejarPersecusion() 
        {
            persiguiendo = false;
            agent.ResetPath();
        }

    }

}