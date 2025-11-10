using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptable Objects/Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] private string[] texts;
    private int index = 0;
    [field: SerializeField] public Dialogue Next { get; private set; }

    public string GetNextText()
    {
        if (index >= texts.Length) return null;
        return texts[index++];

    }
}
