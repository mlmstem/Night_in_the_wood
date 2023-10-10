using UnityEngine;

//Modified code from Unity tutorial: https://www.youtube.com/watch?v=m9hj9PdO328

[System.Serializable]
[CreateAssetMenu(fileName = "Lighting Preset", menuName = "Scriptables/Lighting Preset", order = 1)]
public class LightingPreset : ScriptableObject
{
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
}
