using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.Poke {

    public class BattleHandler : MonoBehaviour {


        public Transform mainMenu;
        public Transform fightMenu;
        public Image healthBar;
        public Image healthBar0;
        public Color healthBarColorFull;
        public Color healthBarColorHalf;
        public Color HealthBarColorLow;
        public Pokemon encounter;
        public Pokemon player;
        private float hpChange;
        private float hpChange0;
        private float oldHP0;
        private float oldHP1;
        public Moves[] moves;

        // Use this for initialization
        void Start() {
            encounter = Intermediate.pokeEncounter;
            player = Intermediate.pokePlayer;
            moves = player.moves;
        }


        //Navigating The Layers-m
        public void Button_Run() {
            Debug.Log(Intermediate.playerPos);
            Application.LoadLevel(0);
        }

        public void Button_Fight_Click() {
            mainMenu.gameObject.SetActive(false);

            foreach (Transform child in fightMenu) {
                if (child.tag == "MoveButton") {
                    Destroy(child);
                }
            }

            for (int i = 0; i < moves.Length; i++) {
                GameObject go = (GameObject) Instantiate(Resources.Load("ButtonMove"));
                if (moves[i] != Moves.NULL) {
                    go.GetComponentInChildren<Text>().text = moves[i].ToString();
                    var extra = i > 0 ? i * (((RectTransform)go.transform).rect.width + 20) : 0;
                    go.transform.position = new Vector3(70 + extra, fightMenu.transform.Cast<Transform>().Where(o => o.tag!="MoveButton").ElementAt(0).position.y);
                    go.transform.parent = fightMenu;
                    go.name = i.ToString();
                    go.GetComponent<Button>().onClick.AddListener(() => {
                        Button_Move_Click(int.Parse(go.name));
                    });
                }
            }

            fightMenu.gameObject.SetActive(true);
        }

        public void Button_Back_Click() {
            fightMenu.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
        }

        //Moves
        public void Button_Move_Click(int i) {
            //The Player's Move
            oldHP1 = encounter.hpCurrent;
            Debug.Log(computeMove(moves[i], player, encounter));
            encounter.hpCurrent = encounter.hpMax * computeMove(moves[i], player, encounter);
            hpChange = -1 * ((oldHP1 - encounter.hpCurrent) / encounter.hpMax);
            Debug.Log(encounter.hpCurrent);
            Debug.Log(encounter.hpMax);
            Debug.Log(hpChange);
            Debug.Log(oldHP1);
            /*switch (move) {
                case (0):
                    oldHP1 = encounter.hpCurrent;
                    Debug.Log(computeMove(move, player, encounter));
                    encounter.hpCurrent = encounter.hpMax * computeMove(move, player, encounter);
                    hpChange = -1 * ((oldHP1 - encounter.hpCurrent) / encounter.hpMax);
                    Debug.Log(encounter.hpCurrent);
                    Debug.Log(encounter.hpMax);
                    Debug.Log(hpChange);
                    Debug.Log(oldHP1);
                    break;
                case (1):
                    oldHP1 = encounter.hpCurrent;
                    encounter.hpCurrent = encounter.hpMax * computeMove(move, player, encounter);
                    hpChange = (oldHP1 - encounter.hpCurrent) / encounter.hpMax;
                    break;
                case (2):
                    oldHP1 = encounter.hpCurrent;
                    encounter.hpCurrent = encounter.hpMax * computeMove(move, player, encounter);
                    hpChange = (oldHP1 - encounter.hpCurrent) / encounter.hpMax;
                    break;
                case (3):
                    oldHP1 = encounter.hpCurrent;
                    encounter.hpCurrent = encounter.hpMax * computeMove(move, player, encounter);
                    hpChange = (oldHP1 - encounter.hpCurrent) / encounter.hpMax;
                    break;
            }*/
            if (healthBar.fillAmount > 1) {
                healthBar.fillAmount = 1;
            }
            if (healthBar.fillAmount < 0) {
                healthBar.fillAmount = 0;
            }
        }


        // Update is called once per frame
        void Update() {
            HealthBarColorHandler1();
            HealthBarDropHandler1();
            HealthBarColorHandler0();
            HealthBarDropHandler0();
        }

        void HealthBarDropHandler0() {
            if (hpChange0 < 0) {
                hpChange0 = hpChange0 + 0.01f;
                healthBar0.fillAmount = healthBar0.fillAmount - 0.01f;
            }
            if (hpChange0 > 0) {
                hpChange0 = hpChange0 - 0.01f;
                healthBar0.fillAmount = healthBar0.fillAmount + 0.01f;
            }
        }

        void HealthBarColorHandler0() {
            if (healthBar0.fillAmount < 0.5) {
                healthBar0.color = healthBarColorHalf;
            }
            if (healthBar0.fillAmount < 0.25) {
                healthBar0.color = HealthBarColorLow;
            }
            if (healthBar0.fillAmount > 0.5) {
                healthBar0.color = healthBarColorFull;
            }
        }


        void HealthBarDropHandler1() {
            if (hpChange < 0) {
                hpChange = hpChange + 0.01f;
                healthBar.fillAmount = healthBar.fillAmount - 0.01f;
            }
            if (hpChange > 0) {
                hpChange = hpChange - 0.01f;
                healthBar.fillAmount = healthBar.fillAmount + 0.01f;
            }
        }

        void HealthBarColorHandler1() {
            if (healthBar.fillAmount < 0.5) {
                healthBar.color = healthBarColorHalf;
            }
            if (healthBar.fillAmount < 0.25) {
                healthBar.color = HealthBarColorLow;
            }
            if (healthBar.fillAmount > 0.5) {
                healthBar.color = healthBarColorFull;
            }
        }

        //Move Handler
        float computeMove(Moves move, Pokemon moveMaker, Pokemon moveReceiver) {
            float damageDif;
            float hpBuffer;
            float movePower;
            float newHP;

            switch (move) {
                case Moves.Tackle:
                    movePower = (1 + (40 / 100) - (moveReceiver.pDefence / 100));
                    damageDif = moveMaker.pAttack * movePower;
                    hpBuffer = moveReceiver.hpCurrent;
                    newHP = hpBuffer - damageDif;
                    return (newHP / moveReceiver.hpMax);

                case Moves.Growl:
                    movePower = 0 / 100;
                    damageDif = moveMaker.pAttack * movePower;
                    hpBuffer = moveReceiver.hpCurrent + moveReceiver.pDefence;
                    newHP = hpBuffer - damageDif;
                    return (newHP / moveReceiver.hpMax);

                case Moves.VineWhip:
                    movePower = 50 / 100;
                    damageDif = moveMaker.pAttack * movePower;
                    if (moveReceiver.typing.Contains(Types.Water)) {
                        damageDif = damageDif * 2;
                    }
                    if (moveReceiver.typing.Contains(Types.Fire)) {
                        damageDif = damageDif / 2;
                    }
                    hpBuffer = moveReceiver.hpCurrent + moveReceiver.pDefence;
                    newHP = hpBuffer - damageDif;
                    return (newHP / moveReceiver.hpMax);

                case Moves.TakeDown:
                    movePower = 60 / 100;
                    damageDif = moveMaker.pAttack * movePower;
                    hpBuffer = moveReceiver.hpCurrent + moveReceiver.pDefence;
                    newHP = hpBuffer - damageDif;
                    return (newHP / moveReceiver.hpMax);

                case Moves.RazorLeaf:
                    movePower = 80 / 100;
                    damageDif = moveMaker.sAttack * movePower;
                    hpBuffer = moveReceiver.hpCurrent + moveReceiver.sDefence;
                    newHP = hpBuffer - damageDif;
                    return (newHP / moveReceiver.hpMax);

                case Moves.Ember:
                    movePower = 50 / 100;
                    damageDif = moveMaker.sAttack * movePower;
                    if (moveReceiver.typing.Contains(Types.Water)) {
                        damageDif = damageDif / 2;
                    }
                    if (moveReceiver.typing.Contains(Types.Grass)) {
                        damageDif = damageDif * 2;
                    }
                    hpBuffer = moveReceiver.hpCurrent + moveReceiver.sDefence;
                    newHP = hpBuffer - damageDif;
                    return (newHP / moveReceiver.hpMax);

                case Moves.FlameThrower:
                    movePower = 80 / 100;
                    damageDif = moveMaker.sAttack * movePower;
                    if (moveReceiver.typing.Contains(Types.Water)) {
                        damageDif = damageDif / 2;
                    }
                    if (moveReceiver.typing.Contains(Types.Grass)) {
                        damageDif = damageDif * 2;
                    }
                    hpBuffer = moveReceiver.hpCurrent + moveReceiver.sDefence;
                    newHP = hpBuffer - damageDif;
                    return (newHP / moveReceiver.hpMax);

                case Moves.WaterGun:
                    movePower = 50 / 100;
                    damageDif = moveMaker.pAttack * movePower;
                    if (moveReceiver.typing.Contains(Types.Fire)) {
                        damageDif = damageDif * 2;
                    }
                    if (moveReceiver.typing.Contains(Types.Grass)) {
                        damageDif = damageDif / 2;
                    }
                    hpBuffer = moveReceiver.hpCurrent + moveReceiver.pDefence;
                    newHP = hpBuffer - damageDif;
                    return (newHP / moveReceiver.hpMax);

                case Moves.Bite:
                    movePower = 60 / 100;
                    damageDif = moveMaker.pAttack * movePower;
                    hpBuffer = moveReceiver.hpCurrent + moveReceiver.pDefence;
                    newHP = hpBuffer - damageDif;
                    return (newHP / moveReceiver.hpMax);

                case Moves.AquaTail:
                    movePower = 80 / 100;
                    damageDif = moveMaker.pAttack * movePower;
                    if (moveReceiver.typing.Contains(Types.Fire)) {
                        damageDif = damageDif * 2;
                    }
                    if (moveReceiver.typing.Contains(Types.Grass)) {
                        damageDif = damageDif / 2;
                    }
                    hpBuffer = moveReceiver.hpCurrent + moveReceiver.pDefence;
                    newHP = hpBuffer - damageDif;
                    return (newHP / moveReceiver.hpMax);

                case Moves.Peck:
                    movePower = 70 / 100;
                    damageDif = moveMaker.pAttack * movePower;
                    if (moveReceiver.typing.Contains(Types.Grass)) {
                        damageDif = damageDif * 2;
                    }
                    hpBuffer = moveReceiver.hpCurrent + moveReceiver.pDefence;
                    newHP = hpBuffer - damageDif;
                    return (newHP / moveReceiver.hpMax);

                default:
                    print("No Such Move Existed");
                    return 0f;
            }
        }
    }
}