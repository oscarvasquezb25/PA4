using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class EfectoSonido : MonoBehaviour
{
    private AudioSource audioSourse;

    private void Start()
    {
        audioSourse = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSourse.Play(); // Reproducir el sonido al entrar en contacto con el jugador
        }
    }


}
