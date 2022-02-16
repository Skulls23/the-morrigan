using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Blessing", order = 1)]
public class Blessing : ScriptableObject
{
    public string Name;
    public string _ID;

    public string Effect;
    public string Description;

    public Sprite Sprite;

    public int weight;
}
