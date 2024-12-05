using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // ƒ‰nil‰hteet sis‰lt‰v‰ oliotaulukko
    public Sound[] sounds;

    // Vain yksi esiintym‰ (Sinkelton)
    public static AudioManager instance;

    // Use this for initialization before Start-metohod
    void Awake()
    {

        // Onko AudioManageri olemassa?
        if (instance == null)
            // AudioManager ei ole olemassa, joten luodaan se
            instance = this;
        else
        {
            // AudioManager on jom olemassa, joten tuhotaan se
            Destroy(gameObject);

            // Varmistetaan ett‰ muuta koodia ei en‰‰ suoriteta
            return;
        }

        // ƒl‰ tuhoa GameObjektia ladattaessa
        //DontDestroyOnLoad(gameObject);

        // N‰ytt‰‰ oliotaulukon kaikki ‰‰nil‰hteet
        foreach (Sound s in sounds)
        {
            // Yhetys ‰‰nil‰hteeseen
            s.source = gameObject.AddComponent<AudioSource>();

            // ƒ‰ni joka halutaan soittaa
            s.source.clip = s.clip;

            // P‰ivitt‰‰ tehdyt s‰‰dˆt Audio-komponenttiin
            s.source.volume = s.volume; // voimakkuus
            s.source.pitch = s.pitch;   // 
            s.source.loop = s.loop;     // soitetaanko loopissa
        }
    }

    public void Start()
    {
        // Soitetaan Teema niminen ‰‰ni
        //Play("WinFanfare");
    }

    /// <summary>
    /// Soittaa halutun ‰‰nen.
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        // Etsit‰‰n haluttu ‰‰ni
        Sound s = Array.Find(sounds, sound => sound.name == name);
        // Onko ‰‰nt‰ olemassa?
        if (s == null)
            // Ei ole, joten hyp‰t‰‰n metodista pois
            return;
        // Soitetaan ‰‰ni
        s.source.Play();
    }
}
