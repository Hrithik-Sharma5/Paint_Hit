using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBall : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform spawnPos;
    bool coolingDown;

    public void Shoot()
    {
        if (!GameManager.instance.canShoot || coolingDown || GameManager.instance.ballCount==0) return;

        GameObject ball= Instantiate(ballPrefab, spawnPos.position, Quaternion .identity);
        ball.GetComponent<Rigidbody>().AddForce(Vector3.forward * 100, ForceMode.Impulse);
        GameManager.instance.ReduceBall();
        coolingDown = true;
        StartCoroutine(BallCoolDown());
    }

    IEnumerator BallCoolDown()
    {   
        if(GameManager.instance.ballCount==0)yield return null;
        yield return new WaitForSeconds(0.2f);
        coolingDown = false;
    }
}
