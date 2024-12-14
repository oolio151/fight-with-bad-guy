using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Powers {
        Speed, 
        Jump,
        FireRate, 
    }

    public Material speed;
    public Material firerate;
    public Material jump;

    public float poweruptimer;

    Powers power;
    new Collider collider; 

    Vector3 rotate = new Vector3(0, 0, 1);

    // Start is called before the first frame update
    void Start()
    {
        power = (Powers) Random.Range(0, 2);

        if (power == Powers.Speed) {
            gameObject.GetComponent<Renderer>().material = speed;
        }  else if (power == Powers.FireRate) {
            gameObject.GetComponent<Renderer>().material = firerate;
        }  else if (power == Powers.Jump) {
            gameObject.GetComponent<Renderer>().material = jump;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = transform.eulerAngles - rotate;
    }

    void OnTriggerEnter(Collider player)
    {
        collider = player;
        if (power == Powers.Speed) {
            player.gameObject.GetComponent<PlayerController>().playerSpeed *= 1.5f;
            Invoke(nameof(ResetSpeed), poweruptimer);
        } else if (power == Powers.Jump) {
            player.gameObject.GetComponent<PlayerController>().jumpForce *= 1.5f;
            Invoke(nameof(ResetJump), poweruptimer);
        } else if (power == Powers.FireRate) {
            
        }
        Destroy(gameObject);
    }

    void ResetSpeed() {
        collider.gameObject.GetComponent<PlayerController>().playerSpeed /= 1.5f;
    }

    void ResetJump() {
        collider.gameObject.GetComponent<PlayerController>().jumpForce /= 1.5f;
    }
}
