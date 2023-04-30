using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSettings : MonoBehaviour
{
    public AudioSource stepVol;
    public AudioSource runVol;
    public AudioSource landVol;
    public AudioSource jumpVol;
    public AudioSource crouchVol;

    // Start is called before the first frame update
    void Start()
    {
        stepVol.volume = Data.volume;
        runVol.volume = Data.volume;
        landVol.volume = Data.volume;
        jumpVol.volume = Data.volume;
        crouchVol.volume = Data.volume * 0.3f;
    }
}
