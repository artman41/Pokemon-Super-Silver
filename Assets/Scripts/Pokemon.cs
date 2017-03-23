using UnityEngine;
using System.Collections;

public class Pokemon : MonoBehaviour {
    public int dexNumber;
    public string pokeName;
    public int level;
     public string typing;
     public float hpMax;
     public float hpCurrent;
     public int speed;
     public int pAttack;
     public int sAttack;
     public int pDefence;
     public int sDefence;
     public string move1;
     public string move2;
     public string move3;
     public string move4;
     public string heldItem;
     public string ability;
     public object prefab;
  
    // Use this for initialization
    public Pokemon(int dexNo, string name, int lvl, string type, int HPmax, int Spd, int PA, int SA, int PD, int SD, string M1, string M2, string M3, string M4, string item, string ably)
    {
        dexNumber = dexNo;
        pokeName = name;
        level = lvl;
        typing = type;
        hpMax = HPmax;
        hpCurrent = HPmax;
        speed = Spd;
        pAttack = PA;
        sAttack = SA;
        pDefence = PD;
        sDefence = SD;
        move1 = M1;
        move2 = M2;
        move3 = M3;
        move4 = M4;
        heldItem = item;
        ability = ably;        
    }
	
    public Pokemon(int dexNo)
    {
        dexNumber = dexNo;
    }

    override public string ToString()
    {
        return pokeName+" is here, Level: "+level+", PokeDex Number: "+dexNumber+", Current Health: "+hpCurrent;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
