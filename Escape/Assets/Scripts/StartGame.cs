using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
    [SerializeField] private UnityEvent welcome;
 
    public UnityEvent Welcome => welcome;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            welcome.Invoke();
        }
    }
}
