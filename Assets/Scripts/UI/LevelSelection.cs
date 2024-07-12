using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelSelection : MonoBehaviour
    {
        [SerializeField] private List<Button> _buttonForSelection;

        private void OnEnable()
        {
            for (int i = 0; i < _buttonForSelection.Count; i++)
            {
                _buttonForSelection[i].interactable = false;
                
                if (PlayerPrefs.HasKey(_buttonForSelection[i].gameObject.name))
                {
                    if (PlayerPrefs.GetInt(_buttonForSelection[i].gameObject.name) == 1)
                    {
                        _buttonForSelection[i].interactable = true;
                    }
                }
                
            }
        }
    }
}