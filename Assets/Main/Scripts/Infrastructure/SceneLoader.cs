using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Main.Scripts.Infrastructure
{
    public class SceneLoader
    {
        public async Task Load(string name)
        {
            if (SceneManager.GetActiveScene().name != name)
            {
                await SceneManager.LoadSceneAsync(name);
            }
        }
    }
}