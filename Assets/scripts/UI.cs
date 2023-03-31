using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameData gameData;
    [SerializeField] private Text player1Score;
    [SerializeField] private Text player2Score;
    [SerializeField] private GameObject BlackImage;
    [SerializeField] private Text showCharacter;
    [SerializeField] private GameObject HintImage;
    [SerializeField] private Button HintButton;
    [SerializeField] private Text hintButtonText;

    private bool isHintOn = false;

    void Awake()
    {
    }
    void Start()
    {
        gameData.white_score = 0;
        gameData.black_score = 0;
        player1Score.text = gameData.playername1 + " Score: " + gameData.white_score.ToString();
        player2Score.text = gameData.playername2 + " Score: " + gameData.black_score.ToString();



    }

    // Update is called once per frame
    void Update()
    {
        //Close hint on any click
        if (isHintOn && Input.GetMouseButtonDown(0))
        {
            ToggleHintOff();
        }
    }

    public void ChangeScore()
    {

        player1Score.text = gameData.playername1 + " Score: " + gameData.white_score.ToString();
        player2Score.text = gameData.playername2 + " Score: " + gameData.black_score.ToString();



        if (gameData.white_score == 6 || gameData.black_score == 6)
        {
            BlackImage.SetActive(true);
            StartCoroutine(Fade());
        }
    }

    public IEnumerator Fade()
    {
        float alphaColor = BlackImage.GetComponent<CanvasGroup>().alpha;
        while (alphaColor < 1f)
        {
            alphaColor += Time.deltaTime;
            BlackImage.GetComponent<CanvasGroup>().alpha = alphaColor;
            yield return new WaitForSeconds(Time.deltaTime * 0.001f);
        }
        SceneManager.LoadScene("End");
        yield return null;
    }

    public void ChangeCharacterButText()
    {
        if (GetComponent<GameState>().isCharacterOn == false)
        {
            showCharacter.text = "Show Characters";
        }
        else if (GetComponent<GameState>().isCharacterOn == true)
        {
            showCharacter.text = "Hide Characters";
        }
    }

    //Triggered by clicking hint button on UI
    public void ToggleHintOn()
    {
        if(!isHintOn)
        {
            HintImage.SetActive(true);
            isHintOn = true;
            hintButtonText.text = "Hide Rules Hint";
            HintButton.enabled = false;
        }
    }

    //Triggered by any click while hint is on
    public void ToggleHintOff()
    {
        HintImage.SetActive(false);
        isHintOn = false;
        hintButtonText.text = "Show Rules Hint";
        HintButton.enabled = true;
    }

}