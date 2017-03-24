using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;
using System;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.Poke {
    public class jsonMod : MonoBehaviour {
        private string jsonstring;
        private JsonData pokemonData;
        private System.Random randNo = new System.Random();

        public void Start() {
            jsonstring = File.ReadAllText(Application.dataPath + "/Resources/pokemon.json");
            pokemonData = JsonMapper.ToObject(jsonstring);
            Pokemon.dexStart = int.Parse(pokemonData["BasePokemon"][0]["DexNumber"].ToString());
            print("m   e    m    e ");
        }

        public Pokemon createPokemon(int dexNo) {
            //Debug.Log(pokemonData/*["BasePokemon"][dexNo]["Name"].ToString()*/);

            dexNo = dexNo - Pokemon.dexStart; //converts the dex Number given to the one the system can read


            var x = pokemonData["BasePokemon"][dexNo]["Typing"];
            List<int> li = new List<int>();

            for (int i = 0; i < x.Count; i++) {
                li.Add(int.Parse(x[i].ToString()));
            }

            int Move1 = randNo.Next(0, pokemonData["BasePokemon"][dexNo]["Moves"].Count);
            int Move2 = randNo.Next(0, pokemonData["BasePokemon"][dexNo]["Moves"].Count);
            int Move3 = randNo.Next(0, pokemonData["BasePokemon"][dexNo]["Moves"].Count);
            int Move4 = randNo.Next(0, pokemonData["BasePokemon"][dexNo]["Moves"].Count); //Random Move between the count of the max moves that pokemon has
            //Int64.Parse(pokemonData["BasePokemon"][dexNo]["HPMax"].ToString());
            Pokemon P = new Pokemon(
                dexNo,
                pokemonData["BasePokemon"][dexNo]["Name"].ToString(),
                1,
                ExtensionsEnum.Parse(typeof(Types), li.ToArray()).Cast<Types>().ToArray(), //type
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["HPMax"].ToString()), //hp
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["Speed"].ToString()), //speed
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["PAttack"].ToString()), //phys atk
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["SAttack"].ToString()), //spec atk
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["PDefence"].ToString()), //phys def
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["SDefence"].ToString()), //spec def
                new Moves[] {
                    (Moves)Enum.Parse(typeof(Moves), pokemonData["BasePokemon"][dexNo]["Moves"][Move1].ToString()),
                    (Moves)Enum.Parse(typeof(Moves), pokemonData["BasePokemon"][dexNo]["Moves"][Move2].ToString()),
                    (Moves)Enum.Parse(typeof(Moves), pokemonData["BasePokemon"][dexNo]["Moves"][Move3].ToString()),
                    (Moves)Enum.Parse(typeof(Moves), pokemonData["BasePokemon"][dexNo]["Moves"][Move4].ToString()),
                }, //moves
                "none", //item
                (Abilities)Enum.Parse(typeof(Abilities), pokemonData["BasePokemon"][dexNo]["Ability"].ToString()) //ability
            );
            return P;
            //return null;
        }

        public Pokemon generateStarter(int dexNo) {
            //Debug.Log(pokemonData/*["BasePokemon"][dexNo]["Name"].ToString()*/);

            dexNo = dexNo - Pokemon.dexStart; //converts the dex Number given to the one the system can read

            var x = pokemonData["BasePokemon"][dexNo]["Typing"];
            List<int> li = new List<int>();

            for (int i = 0; i < x.Count; i++) {
                li.Add(int.Parse(x[i].ToString()));
            }

            Pokemon Pl = new Pokemon(
                dexNo,
                pokemonData["BasePokemon"][dexNo]["Name"].ToString(),
                1,
                ExtensionsEnum.Parse(typeof(Types), li.ToArray()).Cast<Types>().ToArray(),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["HPMax"].ToString()),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["Speed"].ToString()),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["PAttack"].ToString()),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["SAttack"].ToString()),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["PDefence"].ToString()),
                Int32.Parse(pokemonData["BasePokemon"][dexNo]["SDefence"].ToString()),
                new Moves[] {
                    (Moves)Enum.Parse(typeof(Moves), pokemonData["BasePokemon"][dexNo]["Moves"][0].ToString()),
                    (Moves)Enum.Parse(typeof(Moves), pokemonData["BasePokemon"][dexNo]["Moves"][1].ToString()),
                    (Moves)Enum.Parse(typeof(Moves), pokemonData["BasePokemon"][dexNo]["Moves"][2].ToString()),
                },
                "none",
                (Abilities)Enum.Parse(typeof(Abilities), pokemonData["BasePokemon"][dexNo]["Ability"].ToString())
            );
            return Pl;
            //return null;
        }
    }
}