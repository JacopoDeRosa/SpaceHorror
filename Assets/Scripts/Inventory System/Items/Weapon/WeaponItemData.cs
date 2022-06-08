using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    [CreateAssetMenu(fileName = "Weapon Data", menuName = "Items/New Weapon Data")]
    public class WeaponItemData : GameItemData<WeaponItem>
    {
        [SerializeField][InspectorField("Attack Rate")]
        private float _fireRate;

        [SerializeField][InspectorField("Damage")]
        private float _baseDamage;
    }
}