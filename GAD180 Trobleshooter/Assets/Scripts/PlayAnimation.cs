using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    public string animationName;  

    public void Play()
    {
        if(gameObject.GetComponent<Animator>() && animationName != null)
        {
            gameObject.GetComponent<Animator>().Play(animationName);
        }
    }
}
