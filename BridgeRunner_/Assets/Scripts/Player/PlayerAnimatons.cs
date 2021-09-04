using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatons : MonoBehaviour
{
    public static PlayerAnimatons instance;

    public Animator myAnimator;

    private void Start()
    {
        instance = this;
        myAnimator = GetComponentInChildren<Animator>();
    }
    public void Die()
    {
        myAnimator.SetBool("Dead", true);
        gameObject.layer = 6;
        Camera.main.transform.SetParent(null);
    }

}
