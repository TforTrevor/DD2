using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.SceneManagement;

namespace DD2.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actions/Load Level")]
    public class LoadLevel : Action
    {
        [SerializeField] int scene;

        public override void DoAction(Entity target, Entity caller, object payload)
        {
            Timing.RunCoroutine(LoadSceneRoutine());
        }

        IEnumerator<float> LoadSceneRoutine()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
            asyncLoad.allowSceneActivation = false;

            while (!asyncLoad.isDone)
            {
                Debug.Log(asyncLoad.progress * 100 + "%");

                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                }

                yield return Timing.WaitForOneFrame;
            }            
        }
    }
}