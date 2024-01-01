using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Loader
{
    public class SceneLoader
    {
        private LoadingScreenDisplay m_loadingScreen;
        private ZenjectSceneLoader m_sceneLoader;

        [Inject]
        public SceneLoader(LoadingScreenDisplay loadingScreen, ZenjectSceneLoader sceneLoader)
        {
            m_loadingScreen = loadingScreen;
            m_sceneLoader = sceneLoader;
        }

        public async void LoadScene(string sceneName, float maxPercent)
        {
            m_loadingScreen.Show();

            var operation = m_sceneLoader.LoadSceneAsync(sceneName);

            while (!operation.isDone)
            {
                m_loadingScreen.Show(operation.progress * maxPercent);
                await Task.Yield();
            }
        }
    }
}
