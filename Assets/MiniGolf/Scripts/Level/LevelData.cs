using UnityEngine;

[System.Serializable]
public class LevelData
{
    [SerializeField] private int _id;
    [SerializeField] private int _parNumber;

    [SerializeField] private int[] _starsThreshold;

    public int ID => _id;
    
    public int ParNumber => _parNumber;

    public int[] StarsThreshold  => _starsThreshold;
}
