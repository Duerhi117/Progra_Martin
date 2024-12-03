using System.Collections;
using UnityEngine;
using UnityEngine.AI;


namespace Martín
{
    public class Patrullaje : MonoBehaviour
    {

        public Transform[] puntosDePatrullaje;
        public float tiempoDeVigilancia;

        private NavMeshAgent agent;

        private Transform ultimoPunto;


        private void Start()
        {

            agent = GetComponent<NavMeshAgent>();
            StartCoroutine(Patrullar());
        }

        private IEnumerator Patrullar()
        {
            Transform randomPos = RandomPos();

            ultimoPunto = randomPos;

            agent.destination = randomPos.position;

            yield return new WaitUntil(() => Vector3.Distance(transform.position, randomPos.position) < 2);

            Debug.Log("Ya llegó al punto");

            yield return new WaitForSeconds(tiempoDeVigilancia);

            StartCoroutine(Patrullar());
        }

        public void DejarDePatrullar()
        {
            StopAllCoroutines();
        }

        private Transform RandomPos()
        {
            int randomPoint = Random.Range(0, puntosDePatrullaje.Length);
            return puntosDePatrullaje[randomPoint];
        }

        public void RegresarUltimoPunto()
        {
            StartCoroutine(RegresarAPatrullar());
        }

        public IEnumerator RegresarAPatrullar() 
        {
            agent.destination = ultimoPunto.position;

            yield return new WaitUntil(() => Vector3.Distance(transform.position, ultimoPunto.position) < 2);

            Debug.Log("Regresó al último punto de patrullaje");

            yield return new WaitForSeconds(tiempoDeVigilancia);

            StartCoroutine(Patrullar());
        }
    }
}