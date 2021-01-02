using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    bool collidedWithCiecle;
    private void OnCollisionEnter(Collision collision)
    {
        if (collidedWithCiecle) return;
        if (collision.gameObject.tag == "Colored")
        {
            GameManager.instance.LevelFailed();
        }
        else
        {
            collidedWithCiecle = true;
            collision.gameObject.tag = "Colored";
            collision.gameObject.GetComponent<MeshRenderer>().enabled = true;
            collision.gameObject.GetComponent<Renderer>().material.color = GameManager.instance.currentColor;
            if (GameManager.instance.ballCount == 0)
            {
                GameManager.instance.OnSuccessfulShoot();
            }
        }
        
        Destroy(this.gameObject);
    }
}
