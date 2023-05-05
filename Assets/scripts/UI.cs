using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameData gameData;
    [SerializeField] private TMP_Text player1Score, player1Name;
    [SerializeField] private TMP_Text player2Score, player2Name;
    [SerializeField] private GameObject BlackImage, QuitImg;
    [SerializeField] private TMP_Text showCharacter;
    [SerializeField] private GameObject HintImage;
    [SerializeField] private GameObject HintImageChinese;
    [SerializeField] private Button HintButton;
    [SerializeField] private TMP_Text hintButtonText;

    private bool isLineOn;

    public Light direcLight;
    public GameObject lineImg;
    public TMP_Text showLineTxt;
    public GameState gameState;
    public float lowerLightIntensity;

    public List<TMP_Text> uiTxt;

    private bool isHintOn = false;

    void Awake()
    {
        

    }
    void Start()
    {
        isLineOn = false;
        gameData.white_score = 0;
        gameData.black_score = 0;
        player1Name.text = gameData.playername1;
        player2Name.text = gameData.playername2;
        player1Score.text = gameData.white_score.ToString();
        player2Score.text = gameData.black_score.ToString();

        showCharacter.text = TextProvider.Instance.GetText("text0032");//"Hide Labels";
        hintButtonText.text = TextProvider.Instance.GetText("text0016"); //Rule book
        showLineTxt.text = TextProvider.Instance.GetText("text0014"); // show lines

        uiTxt[3].text = TextProvider.Instance.GetText("text0019_2");//Roll Stick
        uiTxt[4].text = TextProvider.Instance.GetText("text0034"); //Quit Game
        uiTxt[5].text = TextProvider.Instance.GetText("text0010"); //player
        uiTxt[6].text = TextProvider.Instance.GetText("text0010"); //player
        uiTxt[7].text = TextProvider.Instance.GetText("text0013"); //score
        uiTxt[8].text = TextProvider.Instance.GetText("text0013"); //score

        if (!gameData.isEN)
        {
            for (int i = 5; i <= 8; i++)
            {
                uiTxt[i].fontSize = 85f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //Close hint on any click
        if (isHintOn && Input.GetMouseButtonDown(0))
        {
            ToggleHintOff();
        }
        */
    }

    public void ChangeScore()
    {

        //player1Score.text = gameData.playername1 + " Score: " + gameData.white_score.ToString();
        //player2Score.text = gameData.playername2 + " Score: " + gameData.black_score.ToString();
        player1Score.text =  gameData.white_score.ToString();
        player2Score.text =  gameData.black_score.ToString();


        if (gameData.white_score >= 6 || gameData.black_score >= 6)
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
            showCharacter.text = TextProvider.Instance.GetText("text0015"); // "Show Labels";
        }
        else if (GetComponent<GameState>().isCharacterOn == true)
        {
            showCharacter.text = TextProvider.Instance.GetText("text0032"); //"Hide Labels";
        }
    }

    //Triggered by clicking hint button on UI
    public void ToggleHintOn()
    {
        if(!isHintOn)
        {   
            if(gameData.isEN){
                HintImage.SetActive(true);
            }
            else{
                HintImageChinese.SetActive(true);
            }
            isHintOn = true;
            hintButtonText.text = "Hide How to Capture";
            HintButton.enabled = false;
        }
    }

    //Triggered by any click while hint is on
    public void ToggleHintOff()
    {
        if(gameData.isEN){
            HintImage.SetActive(false);
        }
        else{
            HintImageChinese.SetActive(false);
        }
        isHintOn = false;
        hintButtonText.text = TextProvider.Instance.GetText("text0016"); // "Show Rulebook";
        HintButton.enabled = true;
    }


    public void ShowLines()
    {
        if(isLineOn == false)
        {
            direcLight.intensity = lowerLightIntensity;
            lineImg.SetActive(true);
            isLineOn = true;
            gameState.boardCharacter.SetActive(false);
        }
        else if(isLineOn == true)
        {
            direcLight.intensity = 1.8f;
            lineImg.SetActive(false);
            isLineOn = false;
            if(gameState.isCharacterOn == false)
            {
                gameState.boardCharacter.SetActive(false);
            } else if(gameState.isCharacterOn == true)
            {
                gameState.boardCharacter.SetActive(true);
                gameState.isCharacterOn = true;
            }
        }
    }

    public void LinesText()
    {
        if(isLineOn == true)
        {
            showLineTxt.text = TextProvider.Instance.GetText("text0031"); // "Hide Lines";
        } 
        else if(isLineOn == false)
        {
            showLineTxt.text = TextProvider.Instance.GetText("text0014"); // "Show Lines";
        }
    }

    public void showQuitIMG()
    {
        Time.timeScale = 0f;
        QuitImg.SetActive(true);

        uiTxt[0].GetComponent<TMP_Text>().text = TextProvider.Instance.GetText("text0035");
        uiTxt[1].GetComponent<TMP_Text>().text = TextProvider.Instance.GetText("text0036");
        uiTxt[2].GetComponent<TMP_Text>().text = TextProvider.Instance.GetText("text0037");
    }

    public void hideQuitIMG()
    {
        Time.timeScale = 1f;
        QuitImg.SetActive(false);
    }

    public void QuitGame()
    {
        //Application.Quit();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }
}