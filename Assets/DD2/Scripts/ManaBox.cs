using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace DD2
{
    public class ManaBox : MonoBehaviour
    {
        [SerializeField] int manaAmount;
        [SerializeField] Transform dropPosition;
        [SerializeField] LayerMask layerMask;

        bool isUsed;

        void Start()
        {
            isUsed = false;
        }

        void OnTriggerEnter(Collider collider)
        {
            if (!isUsed && Util.Utilities.IsInLayer(collider.gameObject, layerMask))
            {
                List<ManaOrb> manaOrbs = ((ManaOrbPool)ManaOrbPool.Instance).GetManaOrbs(manaAmount);
                foreach (ManaOrb orb in manaOrbs)
                {
                    orb.transform.position = dropPosition.position;
                    orb.gameObject.SetActive(true);
                    Timing.RunCoroutine(OrbRoutine(orb));
                    orb.Burst(3f);
                }
                isUsed = true;
            }
        }

        IEnumerator<float> OrbRoutine(ManaOrb orb)
        {
            orb.SetIsPickedUp(true);
            yield return Timing.WaitForSeconds(0.5f);
            if (orb != null)
            {
                orb.SetIsPickedUp(false);
            }            
        }

        public void Refill()
        {
            isUsed = false;
        }
    }
}