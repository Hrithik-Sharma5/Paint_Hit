using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]public int ballCount;
    [HideInInspector]public int circleCount;
    [HideInInspector]public float circleSpeed;
    [HideInInspector]public Color currentColor;
    public bool canShoot;
    [HideInInspector]public int currentLevel;

    public GameObject circleHolder; 
    public Ease[] rotationType = new Ease[] { Ease.Linear, Ease.Linear, Ease.Linear, Ease.Linear, Ease.InBounce, Ease.InOutBack, Ease.InOutBounce, Ease.OutSine, Ease.OutCubic };

    [SerializeField] Material ballIconMatt;
    [SerializeField] GameObject[] totalBallsIcon;
    [SerializeField] Color[] allColors;
    [SerializeField] Transform newCirclePos;
    [SerializeField] GameObject circlePrefab;
    [SerializeField] GameObject winParticle, looseParticle;

    List<GameObject> activeBallIcons= new List<GameObject>();
    GameObject currentCircle;
    int tempColor;

    public delegate void OnGameOver();
    public event OnGameOver GameOver;

    public delegate void OnGameWin();
    public event OnGameWin Gamewin;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        //OnGameWin+=
    }

    private void Start()
    {
         //PlayerPrefs.SetInt("CurrentLevel", 1);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
    }

    public void StartGame()
    {
        if (currentLevel < 10) circleCount = currentLevel;
        else circleCount = 10;
        UIManager.instance.ShowCircleCount();
        ClearAllCircles();
        CreateNewCircle();
        ChooseRandomColor();
        ResetBalls();
    }

    public void OnSuccessfulShoot()
    {
        if (circleCount == 0) 
        {
            LevelCompleted();
            return; 
        }

        canShoot = false;
        FillOldCircle();
        MoveOldCirclesDown();
        CreateNewCircle();
        ChooseRandomColor();
        ResetBalls();
        UIManager.instance.IncreaseCurrentCount();
    }

    public void ReduceBall()
    {
        ballCount--;
        activeBallIcons[activeBallIcons.Count-1].SetActive(false);
        activeBallIcons.RemoveAt(activeBallIcons.Count-1);
    }

    public void LevelFailed()
    {
        canShoot = false;
        looseParticle.SetActive(true);
        currentCircle.GetComponent<Circle>().StopTween();
        UIManager.instance.OngameComplete(false);
    }

    void LevelCompleted()
    {
        IncreaseLevel();
        FillOldCircle();
        canShoot = false;
        winParticle.SetActive(true);
        UIManager.instance.OngameComplete(true);
    }

    void ClearAllCircles()
    {
        foreach (Transform child in circleHolder.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void IncreaseLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel", 1) + 1);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
    }

    void CreateNewCircle()
    {
        circleCount--;
        currentCircle = Instantiate(circlePrefab, newCirclePos.position, Quaternion.identity, circleHolder.transform);
    }

    void ChooseRandomColor()
    {
        int colorNum = Random.Range(0, allColors.Length);
        if(colorNum == tempColor)
        {
            ChooseRandomColor();
            return;
        }

        currentColor = allColors[colorNum];
        tempColor = colorNum;
    }

    void ResetBalls()
    {
        ballIconMatt.color = currentColor;
        foreach (var item in activeBallIcons)
        {
            item.SetActive(false);
        }
        activeBallIcons.Clear();

        if(currentLevel<10) ballCount = Random.Range(2, 6);
        else ballCount = Random.Range(5, 11);

        for (int i = 0; i < ballCount; i++)
        {
            activeBallIcons.Add(totalBallsIcon[i]);
            activeBallIcons[i].SetActive(true);
        }
    }

    void FillOldCircle()
    {
        currentCircle.GetComponent<Circle>().FillCircle();
    }

    void MoveOldCirclesDown()
    {
        circleHolder.transform.DOMoveY(circleHolder.transform.position.y-2.99f, 0.2f);
    }
}
