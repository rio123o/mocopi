using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class blenderShapeControll5 : MonoBehaviour
{
    public GameObject head;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    private float m_weight;
    private int m_Index;
    private bool buttonTrigger = false;


    void Start()
    {

        m_weight = 0;
        //head = GameObject.Find("donmy_slice/head_object");
        skinnedMeshRenderer = head.GetComponent<SkinnedMeshRenderer>();
        m_Index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("blendShape1.smileShape");

    }

    void Update()
    {

        if (Input.GetKey(KeyCode.G))
        {
            if (!buttonTrigger)
            {
                if (m_weight == 0)
                {
                    m_weight = 100;
                }
                else
                {
                    m_weight = 0;
                }
                skinnedMeshRenderer.SetBlendShapeWeight(m_Index, m_weight);
                buttonTrigger = true;
            }
        }
        else buttonTrigger = false;

    }
}