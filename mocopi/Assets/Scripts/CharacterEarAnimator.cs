using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEarAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            animator.SetTrigger("defaultpose");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("face");
            return;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("donmy_ear_down");
            return;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("donmy_ear_ha-to");
            return;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.SetTrigger("donmy_ear_up1");
            return;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            animator.SetTrigger("donmy_ear_up2");
            return;
        }
    }
}
