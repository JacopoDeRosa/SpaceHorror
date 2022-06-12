
using UnityEngine;

namespace FPS.Interaction
{
    public interface IDraggable
    {

        void Select();


        void DeSelect();


        void Drag(GameObject actor, float liftingForce, float holdindDistnce);
             

        
        

    }
}