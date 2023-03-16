using UnityEngine;

namespace IdleRPG.Components
{
    public class CheckingRadiusAttacking : MonoBehaviour
    {
        public LayerMask layerMask;

        public bool IsCheckingIsRadiusAtacking(float radius, out Collider2D hit)
        {
           return hit = Physics2D.OverlapCircle(transform.position, radius, layerMask);
        } 
        public void IsCheckingIsRadiusAtacking(float radius, out Collider2D[] hits)
        {
            hits = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);
        }
    }
}