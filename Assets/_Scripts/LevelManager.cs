using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public float autoLoadNextLevelAfter;


	void Start () {
		Scene scene = SceneManager.GetActiveScene ();

		if (scene.name == "00_Splash") {
			Invoke ("LoadNextScene", autoLoadNextLevelAfter);
		}
	}

	public void LoadNextScene (){
		int currentIndex =  UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (currentIndex + 1);
	}

	public void LoadPreviousScene (){
		int currentIndex =  UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (currentIndex - 1);
	}

	public void LoadLevel (string name){
		SceneManager.LoadScene (name);
	}

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("01_Main");
    }

	public void ResetScene (){
		int currentIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }

	public void Quit()	{
		Application.Quit ();
		print ("Game will quit.");
	}

}
