using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonScript : MonoBehaviour {
	public string ChangeSceneName;

	public void ChangeScene() {
		SceneManager.LoadScene(ChangeSceneName, LoadSceneMode.Single);
	}
}
