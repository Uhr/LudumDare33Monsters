using UnityEngine;
using System.Collections;

public class LoadSceneOnEnter : MonoBehaviour {

    [SerializeField]
    string sceneToLoad;


	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadScene();
        }
	}

    public void LoadScene()
    {
        Application.LoadLevel(sceneToLoad);
    }

}
