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

    void Awake()
    {
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScore() {
        player1Score.text = "Player 1 Score: " + gameData.white_score.ToString();
        player2Score.text = "Player 2 Score: " + gameData.black_score.ToString();

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
            yield return new WaitForSeconds(Time.deltaTime*0.001f);
        }
        SceneManager.LoadScene("End");
        yield return null;
    }
}
