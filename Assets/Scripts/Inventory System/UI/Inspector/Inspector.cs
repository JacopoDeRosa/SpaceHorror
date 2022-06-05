using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaceHorror.InventorySystem.UI
{
    public class Inspector : MonoBehaviour
    {
        [SerializeField] private Sprite _defaultSprite;

        [SerializeField] private GameItemData _debugItem;

        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text[] _fieldsTexts;

        private Queue<TMP_Text> _freeTexts;

        private void Start()
        {
            ResetInspector();
        }

        public void ReadItem(GameItemData data)
        {
            ResetInspector();
            _image.sprite = data.Icon;
            _description.text = data.Description;
            foreach (string field in GetInspectorFields(data))
            {
                if (_freeTexts.Count == 0) break;
                TMP_Text text = _freeTexts.Dequeue();
                text.gameObject.SetActive(true);
                text.text = field;
            }
        }

        private void ResetInspector()
        {
            foreach (TMP_Text text in _fieldsTexts)
            {
                text.text = "";
                text.gameObject.SetActive(false);
            }
            _freeTexts = new Queue<TMP_Text>(_fieldsTexts);
            _image.sprite = _defaultSprite;
            _description.text = "";
        }

        private IEnumerable<string> GetInspectorFields(object data)
        {     
            var type = data.GetType();
            var propeties = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            Debug.Log(propeties.Length);

            foreach (var field in propeties)
            {
                if (field.IsDefined(typeof(InspectorField)))
                {
                    var attribute = field.GetCustomAttribute<InspectorField>();
                    string value = field.GetValue(data).ToString();
                    yield return attribute.Name + ": " + value;
                }
            }
        }       
    }
}
