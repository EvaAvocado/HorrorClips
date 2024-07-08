using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class LevelSelection : MonoBehaviour
    {
        [SerializeField] private List<Button> _buttonForSelection;

        private void Start()
        {
            for (int i = 0; i < _buttonForSelection.Count; i++)
            {
                if (PlayerPrefs.GetInt($"{i + 1}") == 1)
                {
                    _buttonForSelection[i].interactable = true;
                    var i1 = i + 1;
                    _buttonForSelection[i].onClick.AddListener(() => SceneManager.LoadScene(i1));
                }
                else
                {
                    break;
                }
            }
        }
    }
}