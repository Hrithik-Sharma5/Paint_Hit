//using System;
using System.Collections;
using System.Collections.Generic;
//using FlyingWormConsole3.LiteNetLib;
using UnityEngine;
using UnityEngine.UI;
//using System.Security;


public class OptimizeThis : MonoBehaviour
{
    
    //public List<int> randomizeList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 5, 5, 5, 5 };//Size of 14
    public List<int> removeFromList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 5, 5, 5, 5 };//Size of 14
    //public List<Image> imageList;//Size of 30
    //public List<MeshRenderer> meshRendererList;//Size of 30

    //We should use array if you dont want to change the size the array
    public int[] randomizeList = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 5, 5, 5, 5 };//Size of 14
    public Image[] imageList= new Image[30];//Size of 30
    public MeshRenderer[] meshRendererList= new MeshRenderer[30];//Size of 30

    private Transform mainCam;
    private Transform lookAtObject;

    private void Start()
    {
        mainCam = Camera.main.transform;
        lookAtObject= GameObject.Find("LookAtObject").GetComponent<Transform>();
    }


    //should not use Update if you dont write any logic in it
    //private void Update()
    //{

    //}

    /// <summary>
    /// In Late Update we are making the Camera look at the object called LookAtObject 
    /// </summary>
    private void LateUpdate()
    {
        //lookAtObject = GameObject.Find("LookAtObject").GetComponent<Transform>();
        //Camera.main.transform.LookAt(lookAtObject);

        //this will prevent compiler to look for main camera and LookObject every frame
        mainCam.LookAt(lookAtObject);
    }

    /// <summary>
    /// This method is used to get the value of X that was saved in Player Prefs
    /// </summary>
    private int GetSavedXValue()
    {
        int x = 0;
        //if (PlayerPrefs.HasKey("X_Value"))
        //{
        //    x = PlayerPrefs.GetInt("X_Value");
        //}
        //else
        //{
        //    x = 0;
        //}
        //return x;

        x = (PlayerPrefs.HasKey("X_Value"))? PlayerPrefs.GetInt("X_Value"): 0;
        return x;

        //Alternatively way to write this if you want to have a playerpref named X_Value will be--
        return PlayerPrefs.GetInt("X_Value", 0);

    }

    /// <summary>
    /// This is the OnClick method associated with a button. On Click it triggers an animation.
    /// </summary>
    public void OnClickDoAnimation()
    {
        //StartCoroutine(CoroutineMethod());

        //Should use invoke instead of coroutine if you just want to call the function after a delay
        InvokeRepeating("AlternativeCoroutineMethod", 5, 1);
    }

    /// <summary>
    /// This CoRoutine is liked to a button which is called repeatedly.
    /// </summary>
    //private IEnumerator CoroutineMethod()
    //{
    //    yield return new WaitForSeconds(1.0f);
    //    Debug.Log("This Coroutine is called every 5 seconds");
    //}
    private void AlternativeCoroutineMethod()
    {
        Debug.Log("This Coroutine is called every 5 seconds");
    }

    /// <summary>
    /// This method changes the color of all images to Black
    /// </summary>
    private void UpdateImageColor()
    {
        //foreach (var v in imageList)
        //{
        //    v.color = Color.black;
        //}

        //for loop is more performance friendly then foreach loop
        for (int i = 0; i < imageList.Length; i++)
        {
            imageList[i].color = Color.black;
        }
    }

    /// <summary>
    /// This method is used to enable/disable the mesh renderer of the assigned objects
    /// </summary>
    private void UpdateMeshRenderer(bool status)
    {
        //for (int i = 0; i < meshRendererList.Count; i++)
        for (int i = 0; i < meshRendererList.Length; i++)
        {
            meshRendererList[i].enabled = status;
        }
    }

    /// <summary>
    /// This method is used to remove the number 5 from the List
    /// </summary>
    private void RemoveNumberFromList()
    {
        int x = removeFromList.Count;
        for (int i = 0; i < x; i++)
        {
            if (removeFromList[i] == 5)
            {
                removeFromList.RemoveAt(i);
            }
        }
    }
}