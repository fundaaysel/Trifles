#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private int _levelIndex;
    private int _activeIndex;
    private GameManager _gameManager;

    private void OnEnable()
    {
        _levelIndex = PlayerPrefs.GetInt(GameManager.LevelPrefName);
        _activeIndex = _levelIndex;
        _gameManager = (GameManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space(10f);
        _levelIndex = EditorGUILayout.IntField("Current Level Index ", _levelIndex);

        _levelIndex = Mathf.Clamp(_levelIndex, 0, _gameManager.TotalLevelCount());
        GUILayout.Label("Type the number next to level prefab to select the level");
        if (GUILayout.Button("Set level index"))
        {
            PlayerPrefs.SetInt(GameManager.LevelPrefName, _levelIndex);
            _activeIndex = _levelIndex;
        }

        EditorGUILayout.HelpBox("Current level to played: " + _activeIndex, MessageType.Info);
    }
}
#endif