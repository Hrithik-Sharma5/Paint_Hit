using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] GameObject UiScreen;
    [SerializeField] Text gameConditionTxt;
    [SerializeField] Text levelTxt;
    [SerializeField] Text currentCircle, totalCircles;
    void Start()
    {
        instance = this;
        gameConditionTxt.gameObject.SetActive(false);
        levelTxt.text = "Level : " + PlayerPrefs.GetInt("CurrentLevel", 1).ToString();
    }

    public void ShowCircleCount()
    {
        currentCircle.text = 1+"";
        totalCircles.text = GameManager.instance.circleCount.ToString();
    }

    public void IncreaseCurrentCount()
    {
        currentCircle.text = (int.Parse(currentCircle.text) + 1).ToString();
    }

    public void OngameComplete(bool gameCondition)
    {
        StartCoroutine(GameWin(gameCondition));
    }

    IEnumerator GameWin(bool gameWin)
    {
        yield return new WaitForSeconds(2);
        gameConditionTxt.gameObject.SetActive(true);
        if (gameWin)
        {
            gameConditionTxt.text = "You Win";
            levelTxt.text = "Level : "+PlayerPrefs.GetInt("CurrentLevel", 1).ToString();
        }
        else gameConditionTxt.text = "Game Over";
        UiScreen.SetActive(true);
    }
}
