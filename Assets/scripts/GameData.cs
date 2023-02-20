﻿using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects", order = 1)]
public class GameData : ScriptableObject
{
    public int white_score = 0, black_score = 0;
}