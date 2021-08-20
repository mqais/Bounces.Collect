using UnityEngine;
[CreateAssetMenu(fileName = "New Level", menuName = "Levels Info")]
public class LevelManager : ScriptableObject
{
    [SerializeField] internal GameObject[] levelPreb;
    [SerializeField] internal int[] MaxScoreInLevel;
}




