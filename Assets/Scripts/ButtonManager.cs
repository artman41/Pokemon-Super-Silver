using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {
    // Use this for initialization
    public Transform EscapeMenuAnchor;
    public Transform GameBlur;
    public 
    /*void OnGUI () {
        if(buttonSkin == null){
            GUI.skin = buttonSkin;
        }
	}*/
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (EscapeMenuAnchor.gameObject.activeInHierarchy == false)
            {
                EscapeMenuAnchor.gameObject.SetActive(true);
            }
            else
            {
                EscapeMenuAnchor.gameObject.SetActive(false);
            }
        }
    }

    public void ClickQuit()
    {
        Application.Quit();
    }
}
