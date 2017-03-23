using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {
    
    public string levelName;
    public GameObject playerRef;
    private int chance = 1;
    public ArrayList EncounterablePokemon;
    private System.Random randNo = new System.Random();
    jsonMod jM = new jsonMod();
    public Pokemon encounter;
    
    
    // Use this for initialization
    void Start () {
        jM.Start();
    }

    // Update is called once per frame
    void Update () {
        chance = FirstPersonControls.chanceOfEncounter;
    }


    // Update is called when player enters area 
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (chance == 0)
            {
                FirstPersonControls.chanceOfEncounter = 1;
                encounter = jM.createPokemon(randNo.Next(152, 160));
                Debug.Log(encounter);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Intermediate.pokeEncounter = encounter;
                Intermediate.returnFromSave = true;
                Application.LoadLevel(levelName);
            }
        }
    }
  //  public void LoadSceneTest()
   // {
    //    Application.LoadLevel("battle");
   // }
}
