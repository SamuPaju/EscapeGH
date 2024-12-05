using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable] // N�kyy Inspectorissa
public class Sound : MonoBehaviour
{
    // ��ni
    public AudioClip clip;

    // ��nen nimi
    public string name;

    // liukukytkimet
    [Range(0f, 1f)]
    public float volume; // voimakkuus ( 0 - 1)
    [Range(0.1f, 3f)]
    public float pitch; // s�velkorkeus (0.1 - 3)

    // Merkkilippu joka kertoo soitetaanko ��ni loopissa
    public bool loop;

    // ��nen l�hde, joka piilotetaan
    [HideInInspector]
    public AudioSource source; // AudioManager -luokka k�ytt�� t�t� muuttujaa
}
