using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    [SerializeField] private float _noiseRange;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _noiseRange);
    }
   

    public void Activate()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hits = Physics.SphereCastAll(ray, _noiseRange, 0);

        foreach (RaycastHit hit in hits)
        {
            hit.transform.SendMessage("OnNoiseMade", transform.position,SendMessageOptions.DontRequireReceiver);
        }
    }


}
