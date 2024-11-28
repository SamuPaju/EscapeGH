using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxUp : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public bool Up => up;
    private bool up = false;

    public void GoesUp()
    {
        up = true;
        anim.SetBool("GoesUp", up);
    }
    
}
