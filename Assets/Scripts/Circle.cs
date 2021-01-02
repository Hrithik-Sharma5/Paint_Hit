using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] GameObject lastCircle;
    Tweener rotationTween;
    float rotationSpeed;
    private void Start()
    {
        SetRotationSpeed();
        SetObsticles();
        transform.DOMove(new Vector3(0, -14.8f, 58.8f), 0.5f).SetEase(Ease.Linear).OnComplete(delegate 
        { 
            GameManager.instance.canShoot = true;
            //transform.parent = GameManager.instance.circleHolder.transform;
        });
        rotationTween = this.transform.DORotate(new Vector3(0, 180, 0), rotationSpeed).SetLoops(-1, LoopType.Incremental).SetEase(GameManager.instance.rotationType[Random.Range(0, GameManager.instance.rotationType.Length)]);
    }

    public void FillCircle()
    {
        foreach (Transform child in this.transform)
        {
            if (child.tag == "Colored")
            {
                child.gameObject.SetActive(false);
            }
        }
        lastCircle.GetComponent<Renderer>().material.color = GameManager.instance.currentColor;
        rotationTween.Kill(false);
    }

    public void StopTween()
    {
        rotationTween.Kill(false);
    }

    void SetRotationSpeed()
    {
        if (GameManager.instance.currentLevel < 3) rotationSpeed = Random.Range(1.5f, 2.2f);
        else if (GameManager.instance.currentLevel < 10) rotationSpeed = Random.Range(0.8f, 1.8f);
        else if (GameManager.instance.currentLevel >= 10) rotationSpeed = Random.Range(0.5f, 0.9f);
    }

    void SetObsticles()
    {
        int obsticleCount;
        List<int> obsticleList=new List<int>();
        int obsticleProbablity = Random.Range(0, 5);
        

        if (GameManager.instance.currentLevel > 2 && GameManager.instance.currentLevel <5)
        {
            if (obsticleProbablity > 2)
            {
                obsticleCount = Random.Range(0, 5);
                FillObsticles(obsticleCount, obsticleList);
            }
        }
        else if (GameManager.instance.currentLevel > 6)
        {
            if (obsticleProbablity > 1)
            {
                obsticleCount = Random.Range(0, 9);
                FillObsticles(obsticleCount, obsticleList);
            }
        }
    }

    void FillObsticles(int obsticleCount, List<int> obsticleList)
    {
        int obsticleNum;
        for (int i = 0; i < obsticleCount; i++)
        {
            obsticleNum = Random.Range(0, transform.childCount - 1);
            if (!obsticleList.Contains(obsticleNum))
            {
                obsticleList.Add(obsticleNum);
                GameObject currentObsticle = transform.GetChild(obsticleNum).gameObject;
                currentObsticle.tag = "Colored";
                currentObsticle.GetComponent<MeshRenderer>().enabled = true;
                currentObsticle.GetComponent<Renderer>().material.color = GameManager.instance.currentColor;
            }
            else i--;
        }
    }

}
