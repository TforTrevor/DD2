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
            if (!isUsed && Util.Utilities.HasLayer(collider.gameObject.layer, layerMask))
            {
                List<ManaOrb> manaOrbs = ((ManaOrbPool)ManaOrbPool.Instance).GetManaOrbs(manaAmount);
                foreach (ManaOrb orb in manaOrbs)
                {
                    orb.transform.position = dropPosition.position;
                    orb.gameObject.SetActive(true);
                    orb.SetIsPickedUp(true);
                    orb.Burst(3f);
                    Timing.CallDelayed(0.5f, () =>
                    {
                        orb.SetIsPickedUp(false);
                    });
                }
                isUsed = true;
            }
        }
    }
}