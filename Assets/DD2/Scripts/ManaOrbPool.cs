using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public class ManaOrbPool : ComponentPool<ManaOrb>
    {
        public List<ManaOrb> GetManaOrbs(int amount)
        {
            List<ManaOrb> manaOrbs = new List<ManaOrb>();
            int count = 0;
            int index = 0;
            while (count < amount)
            {
                int mana = int.Parse(pools[index].tag);
                if (mana > amount - count)
                {
                    index++;
                    if (index > pools.Count)
                    {
                        break;
                    }
                }
                else
                {
                    ManaOrb orb = GetObject(pools[index].tag);
                    if (orb != null)
                    {
                        manaOrbs.Add(orb);
                        count += mana;
                    }                    
                }
            }
            return manaOrbs;
        }
    }
}