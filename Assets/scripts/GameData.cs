using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects", order = 1)]
public class GameData : ScriptableObject
{
    public int white_score;
    public int black_score;
    public string playername1;
    public string playername2;
    public void OnEnable()
    {
        white_score = 0;
        black_score = 0;
        playername1 = "Player 1";
        playername2 = "Player 2";
    }
}