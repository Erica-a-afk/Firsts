using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour {
    public void Load(string n) => SceneManager.LoadScene(n);
}
