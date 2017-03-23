using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;
using System;


public class jsonMod : MonoBehaviour {
    private string jsonstring;
    private JsonData pokemonData;
    private System.Random randNo = new System.Random();
	
	public void Start () {
        jsonstring = File.ReadAllText(Application.dataPath + "/Resources/pokemon.json");
        pokemonData = JsonMapper.ToObject(jsonstring);
        print("m   e    m    e ");
	}
    public Pokemon createPokemon(int dexNo)
    {
        //Debug.Log(pokemonData/*["BasePokemon"][dexNo]["Name"].ToString()*/);
        dexNo = dexNo - 152; //converts the dex Number given to the one the system can read
        int Move1 = randNo.Next(0, pokemonData["BasePokemon"][dexNo]["Moves"].Count);
        int Move2 = randNo.Next(0, pokemonData["BasePokemon"][dexNo]["Moves"].Count);
        int Move3 = randNo.Next(0, pokemonData["BasePokemon"][dexNo]["Moves"].Count);
        int Move4 = randNo.Next(0, pokemonData["BasePokemon"][dexNo]["Moves"].Count);//Random Move between the count of the max moves that pokemon has
                                                                                     //Int64.Parse(pokemonData["BasePokemon"][dexNo]["HPMax"].ToString());
        Pokemon P = new Pokemon(
                dexNo,
                pokemonData["BasePokemon"][dexNo]["Name"].ToString(),
                1,
                pokemonData["BasePokemon"][dexNo]["Typing"].ToString(),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["HPMax"].ToString()),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["Speed"].ToString()),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["PAttack"].ToString()),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["SAttack"].ToString()),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["PDefence"].ToString()),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["SDefence"].ToString()),
                pokemonData["BasePokemon"][dexNo]["Moves"][Move1].ToString(),
                pokemonData["BasePokemon"][dexNo]["Moves"][Move2].ToString(),
                pokemonData["BasePokemon"][dexNo]["Moves"][Move3].ToString(),
                pokemonData["BasePokemon"][dexNo]["Moves"][Move4].ToString(),
                "none",
                pokemonData["BasePokemon"][dexNo]["Ability"].ToString()
            );
        return P;
        //return null;
    }
    public Pokemon generateStarter(int dexNo)
    {
        //Debug.Log(pokemonData/*["BasePokemon"][dexNo]["Name"].ToString()*/);
        dexNo = dexNo - 152; //converts the dex Number given to the one the system can read
        int Move1 = 0;
        int Move2 = 1;
        int Move3 = 2;
                                                                                     
        Pokemon Pl = new Pokemon(
                dexNo,
                pokemonData["BasePokemon"][dexNo]["Name"].ToString(),
                1,
                pokemonData["BasePokemon"][dexNo]["Typing"].ToString(),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["HPMax"].ToString()),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["Speed"].ToString()),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["PAttack"].ToString()),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["SAttack"].ToString()),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["PDefence"].ToString()),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["SDefence"].ToString()),
                pokemonData["BasePokemon"][dexNo]["Moves"][Move1].ToString(),
                pokemonData["BasePokemon"][dexNo]["Moves"][Move2].ToString(),
                pokemonData["BasePokemon"][dexNo]["Moves"][Move3].ToString(),
                "-",
                "none",
                pokemonData["BasePokemon"][dexNo]["Ability"].ToString()
            );
        return Pl;
        //return null;
    }
}
