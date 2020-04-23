using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace DD2
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] [ReorderableList] List<Core> cores;

        protected override void Awake()
        {
            base.Awake();

            Core[] temp = FindObjectsOfType<Core>();
            if (cores == null)
            {
                cores = new List<Core>(temp);
            }
            else
            {
                foreach (Core core in temp)
                {
                    if (!cores.Contains(core))
                    {
                        cores.Add(core);
                    }
                }
            }            
        }

        public List<Core> GetCores()
        {
            return cores;
        }
    }
}