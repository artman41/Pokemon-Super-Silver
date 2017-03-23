using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    public Text move1Txt;
    public Text move2Txt;
    public Text move3Txt;
    public Text move4Txt;

    // Use this for initialization
    void Start () {
        encounter = Intermediate.pokeEncounter;
        player = Intermediate.pokePlayer;
        move1Txt.text = player.move1;
        move2Txt.text = player.move2;
        move3Txt.text = player.move3;
        move4Txt.text = player.move4;
    }
    

    //Navigating The Layers-m
    public void Button_Run()
    {
        Debug.Log(Intermediate.playerPos);
        Application.LoadLevel(0);
    }

    public void Button_Fight_Click()
    {
        mainMenu.gameObject.SetActive(false);
        fightMenu.gameObject.SetActive(true);
    }

    public void Button_Back_Click()
    {
        fightMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    //Moves
    public void Button_Move_Click(int move)
    {
        //The Player's Move
        switch (move)
        {
            case(0):
                oldHP1 = encounter.hpCurrent;
                Debug.Log(computeMove(player.move1, player, encounter));
                encounter.hpCurrent = encounter.hpMax * computeMove(player.move1, player, encounter);
                hpChange = -1*((oldHP1 - encounter.hpCurrent) / encounter.hpMax);
                Debug.Log(encounter.hpCurrent);
                Debug.Log(encounter.hpMax);
                Debug.Log(hpChange);
                Debug.Log(oldHP1);
                break;
            case(1):
                oldHP1 = encounter.hpCurrent;
                encounter.hpCurrent = encounter.hpMax * computeMove(player.move1, player, encounter);
                hpChange = (oldHP1 - encounter.hpCurrent)/encounter.hpMax;
                break;
            case (2):
                oldHP1 = encounter.hpCurrent;
                encounter.hpCurrent = encounter.hpMax * computeMove(player.move1, player, encounter);
                hpChange = (oldHP1 - encounter.hpCurrent) / encounter.hpMax;
                break;
            case (3):
                oldHP1 = encounter.hpCurrent;
                encounter.hpCurrent = encounter.hpMax * computeMove(player.move1, player, encounter);
                hpChange = (oldHP1 - encounter.hpCurrent) / encounter.hpMax;
                break;
        }
        if (healthBar.fillAmount > 1)
        {
            healthBar.fillAmount = 1;
        } 
        if (healthBar.fillAmount < 0)
        {
            healthBar.fillAmount = 0;
        }
    }


    // Update is called once per frame
    void Update () {
        HealthBarColorHandler1();
        HealthBarDropHandler1();
        HealthBarColorHandler0();
        HealthBarDropHandler0();
	}

    void HealthBarDropHandler0()
    {
        if (hpChange0 < 0)
        {
            hpChange0 = hpChange0 + 0.01f;
            healthBar0.fillAmount = healthBar0.fillAmount - 0.01f;
        }
        if (hpChange0 > 0)
        {
            hpChange0 = hpChange0 - 0.01f;
            healthBar0.fillAmount = healthBar0.fillAmount + 0.01f;
        }
    }

    void HealthBarColorHandler0()
    {
        if (healthBar0.fillAmount < 0.5)
        {
            healthBar0.color = healthBarColorHalf;
        }
        if (healthBar0.fillAmount < 0.25)
        {
            healthBar0.color = HealthBarColorLow;
        }
        if (healthBar0.fillAmount > 0.5)
        {
            healthBar0.color = healthBarColorFull;
        }
    }


    void HealthBarDropHandler1()
    {
        if(hpChange < 0)
        {
            hpChange = hpChange + 0.01f;
            healthBar.fillAmount = healthBar.fillAmount - 0.01f;
        }
        if(hpChange > 0)
        {
            hpChange = hpChange - 0.01f;
            healthBar.fillAmount = healthBar.fillAmount + 0.01f;
        }
    }

    void HealthBarColorHandler1()
    {
        if (healthBar.fillAmount < 0.5)
        {
            healthBar.color = healthBarColorHalf;
        }
        if (healthBar.fillAmount < 0.25)
        {
            healthBar.color = HealthBarColorLow;
        }
        if (healthBar.fillAmount > 0.5)
        {
            healthBar.color = healthBarColorFull;
        }
    }

    //Move Handler
    float computeMove(string move, Pokemon moveMaker, Pokemon moveReceiver)
    {
        float damageDif;
        float hpBuffer;
        float movePower;
        float newHP;
        
        switch (move)
        {
            case("Tackle"):
                movePower = (1 + (40/100) - (moveReceiver.pDefence/100));
                damageDif = moveMaker.pAttack * movePower;
                hpBuffer = moveReceiver.hpCurrent;
                newHP = hpBuffer - damageDif;
                return (newHP / moveReceiver.hpMax);

            case ("Growl"):
                movePower = 0 / 100;
                damageDif = moveMaker.pAttack * movePower;
                hpBuffer = moveReceiver.hpCurrent + moveReceiver.pDefence;
                newHP = hpBuffer - damageDif;
                return (newHP / moveReceiver.hpMax);

            case ("WineWhip"):
                movePower = 50 / 100;
                damageDif = moveMaker.pAttack * movePower;
                if(moveReceiver.typing == "Water")
                {
                    damageDif = damageDif * 2;
                }
                if(moveReceiver.typing == "Fire")
                {
                    damageDif = damageDif / 2;
                }
                hpBuffer = moveReceiver.hpCurrent + moveReceiver.pDefence;
                newHP = hpBuffer - damageDif;
                return (newHP / moveReceiver.hpMax);

            case ("Take Down"):
                movePower = 60 / 100;
                damageDif = moveMaker.pAttack * movePower;
                hpBuffer = moveReceiver.hpCurrent + moveReceiver.pDefence;
                newHP = hpBuffer - damageDif;
                return (newHP / moveReceiver.hpMax);

            case ("Razor Leaf"):
                movePower = 80 / 100;
                damageDif = moveMaker.sAttack * movePower;
                hpBuffer = moveReceiver.hpCurrent + moveReceiver.sDefence;
                newHP = hpBuffer - damageDif;
                return (newHP / moveReceiver.hpMax);

            case ("Ember"):
                movePower = 50 / 100;
                damageDif = moveMaker.sAttack * movePower;
                if (moveReceiver.typing == "Water")
                {
                    damageDif = damageDif / 2;
                }
                if (moveReceiver.typing == "Grass")
                {
                    damageDif = damageDif * 2;
                }
                hpBuffer = moveReceiver.hpCurrent + moveReceiver.sDefence;
                newHP = hpBuffer - damageDif;
                return (newHP / moveReceiver.hpMax);

            case ("Flame Thrower"):
                movePower = 80 / 100;
                damageDif = moveMaker.sAttack * movePower;
                if (moveReceiver.typing == "Water")
                {
                    damageDif = damageDif / 2;
                }
                if (moveReceiver.typing == "Grass")
                {
                    damageDif = damageDif * 2;
                }
                hpBuffer = moveReceiver.hpCurrent + moveReceiver.sDefence;
                newHP = hpBuffer - damageDif;
                return (newHP / moveReceiver.hpMax);

            case ("Water Gun"):
                movePower = 50 / 100;
                damageDif = moveMaker.pAttack * movePower;
                if (moveReceiver.typing == "Fire")
                {
                    damageDif = damageDif * 2;
                }
                if (moveReceiver.typing == "Grass")
                {
                    damageDif = damageDif / 2;
                }
                hpBuffer = moveReceiver.hpCurrent + moveReceiver.pDefence;
                newHP = hpBuffer - damageDif;
                return (newHP / moveReceiver.hpMax);

            case ("Bite"):
                movePower = 60 / 100;
                damageDif = moveMaker.pAttack * movePower;
                hpBuffer = moveReceiver.hpCurrent + moveReceiver.pDefence;
                newHP = hpBuffer - damageDif;
                return (newHP / moveReceiver.hpMax);

            case ("Aqua Tail"):
                movePower = 80 / 100;
                damageDif = moveMaker.pAttack * movePower;
                if (moveReceiver.typing == "Fire")
                {
                    damageDif = damageDif * 2;
                }
                if (moveReceiver.typing == "Grass")
                {
                    damageDif = damageDif / 2;
                }
                hpBuffer = moveReceiver.hpCurrent + moveReceiver.pDefence;
                newHP = hpBuffer - damageDif;
                return (newHP / moveReceiver.hpMax);

            case ("Peck"):
                movePower = 70 / 100;
                damageDif = moveMaker.pAttack * movePower;
                if (moveReceiver.typing == "Grass")
                {
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
