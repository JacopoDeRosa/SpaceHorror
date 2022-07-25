using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class Flashlight : EquippableItem
    {
        [SerializeField] private Light _light;
        [SerializeField] private GameItemData _batteryType;
        [SerializeField] private AudioClip _toggleClip;

        private bool _on = true;

        public override void PrimaryUse()
        {
            _on = !_on;

            if(_on)
            {
                _light.enabled = true;
            }
            else
            {
                _light.enabled = false;
            }

            AudioSource.PlayClipAtPoint(_toggleClip, transform.position);
        }

        public override void UtilityUse()
        {
           if(User == null)
           {
                Debug.LogError("Item tried to access a null user");
           }

            if (User.Inventory.ContainsItem(_batteryType))
            {
                Debug.Log("Reload");
            }
            else
            {
                Debug.Log("User does not posses the correct battery");
            }
        }
    }
}
