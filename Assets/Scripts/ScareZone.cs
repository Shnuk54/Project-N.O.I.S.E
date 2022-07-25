using System.Collections;
using UnityEngine;


    public class ScareZone : MonoBehaviour
    {
    [SerializeField] float Scare;
    [SerializeField] float ScareSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Events.instance.OnPlayerScared(Scare, ScareSpeed);
        }
    }
}
