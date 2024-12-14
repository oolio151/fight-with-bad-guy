using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwingScript : MonoBehaviour
{

    public GameObject Sword;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("I'm going insane");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("update: I'm going insane");
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SwordSwing());
        }
    }

    IEnumerator SwordSwing()
    {
        Sword.GetComponent<Animator>().Play("SwordSwing");
        yield return new WaitForSeconds(1.0f);
        Sword.GetComponent<Animator>().Play("New State");
    }
}