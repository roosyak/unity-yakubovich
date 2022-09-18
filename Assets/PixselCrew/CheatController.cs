using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class CheatController : MonoBehaviour
{
    private string _currentInput;
    /// <summary>
    /// время жизни строки
    /// </summary>
    [SerializeField] private float _inputTTL;
    [SerializeField] private CheatItem[] _cheats;

    /// <summary>
    /// прошедшее время поле нажатия 
    /// </summary>
    private float _inputTime;
    private void Awake()
    {
        Keyboard.current.onTextInput += onTextInput;
    }
    private void OnDestroy()
    {
        Keyboard.current.onTextInput -= onTextInput;
    }
    private void onTextInput(char inputChar)
    {
        _currentInput += inputChar;
        _inputTime = _inputTTL;
        FindAnyCheats();
    }

    private void FindAnyCheats()
    {
        foreach (var cheatItem in _cheats)
            if (_currentInput.Contains(cheatItem.Name))
            {
                cheatItem.Action.Invoke();
                _currentInput = string.Empty;
                Debug.Log(string.Format("Cheat: {0}", cheatItem.Name));
                break;
            }
    }

    private void Update()
    {
        if (_inputTime < 0)
            _currentInput = string.Empty;
        else
            _inputTime -= Time.deltaTime;
    }
}

/// <summary>
/// элемент чита
/// </summary>
[Serializable]
public class CheatItem
{
    /// <summary>
    /// последовательность символов
    /// </summary>
    public string Name;

    /// <summary>
    /// событие чита
    /// </summary>
    public UnityEvent Action;
}