using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace SpaceHorror.InventorySystem.UI
{
    public class Inspector : MonoBehaviour
    {
        [SerializeField] private GameObject _mainWindow;
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text[] _fieldsTexts;

        private Queue<TMP_Text> _freeTexts;

        private bool _visible;

        private void Start()
        {
            ResetInspector();
        }

        public void ReadSlot(ItemSlot slot)
        {
            ResetInspector();

            GameItemData slotData = slot.ItemData;

            _image.sprite = slotData.Icon;

            _description.text = slotData.Description;

            AddFields(GetInspectorFields(slotData));

            AddFields(GetInspectorFields(slot.ItemParameters));

            SetVisible(true);
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
            if(data == null) yield break;

            var type = data.GetType();
            var propeties = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

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
        
        private void AddFields(IEnumerable<string> fields)
        {
            foreach (string field in fields)
            {
                if (_freeTexts.Count == 0) break;
                TMP_Text text = _freeTexts.Dequeue();
                text.gameObject.SetActive(true);
                text.text = field;
            }
        }

        public void SetVisible(bool visible)
        {
            if (_visible == visible) return;

            if (visible)
            {
                _mainWindow.SetActive(true);
            }
            else
            {
                _mainWindow.SetActive(false);
            }

            _visible = visible;
        }
    }
}
