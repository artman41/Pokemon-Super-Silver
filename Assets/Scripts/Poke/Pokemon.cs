using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Poke {

    public class Pokemon : MonoBehaviour {
        public static int dexStart { get; set; }

        public int dexNumber;
        public Types[] typing;

        public string pokeName;
        public int level;
        public float hpMax;
        public float hpCurrent;
        public int speed;
        public int pAttack;
        public int sAttack;
        public int pDefence;
        public int sDefence;

        public Moves[] moves = new Moves[4];
        public string heldItem;
        public Abilities ability;
        public object prefab;

        // Use this for initialization
        public Pokemon(int dexNo, string name, int lvl, Types[] type, int HPmax, int Spd, int PA, int SA, int PD, int SD, Moves[] M, string item, Abilities ably) {
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
            for (int i = 0; i < M.Length; i++) {
                moves[i] = M[i];
            }
            heldItem = item;
            ability = ably;
        }

        public Pokemon(int dexNo) {
            dexNumber = dexNo;
        }

        public override string ToString() {
            return pokeName + " is here, Level: " + level + ", PokeDex Number: " + dexNumber + ", Current Health: " + hpCurrent;
        }
        // Update is called once per frame
        void Update() {

        }
    }
}