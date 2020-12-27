using UnityEngine;
/// <summary>
/// Struct of random
/// </summary>
[System.Serializable]
public struct RandomStruct 
{
    public int fromInt;
    public int toInt;
    public AnimationCurve subdividePercent;
    /// <summary>
    /// This func calculate random number
    /// </summary>
    /// <param name="distance"> based on it we calculate random number</param>
    /// <returns>random number</returns>
    public int CalculateRandom(float distance) {
        int difference = Mathf.Abs(toInt - fromInt);
        int toVal = fromInt + (int)(difference * subdividePercent.Evaluate(distance));
        return Random.Range(fromInt, toVal);
    }
}