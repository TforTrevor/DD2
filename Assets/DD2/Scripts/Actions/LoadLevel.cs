using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.SceneManagement;
using UnityAtoms.SceneMgmt;
using DD2.UI;

namespace DD2.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actions/Load Level")]
    public class LoadLevel : Action
    {
        [SerializeField] SceneFieldVariable scene;
        [SerializeField] LoadingScreen loadingScreen;

        public override void DoAction(Entity target, Entity caller, object payload)
        {
            Timing.RunCoroutine(LoadSceneRoutine());
        }

        IEnumerator<float> LoadSceneRoutine()
        {
            LoadingScreen instance = Instantiate(loadingScreen);
            DontDestroyOnLoad(instance);
            
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene.Value);
            asyncLoad.allowSceneActivation = false;

            instance.Show(asyncLoad);

            while (!asyncLoad.isDone)
            {
                //Debug.Log(asyncLoad.progress * 100 + "%");

                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                }

                yield return Timing.WaitForOneFrame;
            }
        }
    }
}