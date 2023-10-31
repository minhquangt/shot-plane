using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public int[] price =
    {
            0, 150, 500,
            600, 1000, 1500,
            2000, 3000, 3000, 5000,
            0, 250, 500,
            750, 1000, 1200,
            1750, 3000, 4000, 7500,
            0, 100, 300,
            900, 750, 950,
            2500, 2500, 3500, 9500,
    };

    public string[] title =
    {
            "Weapon: Double Pistols",
            "Weapon: Double SMGs",
            "Skill: Napalm Wave",
            "Weapon: Shotgun",
            "Weapon: Jackhammer",
            "Weapon: Minigun",
            "Skill: Airstrike",
            "Weapon: Heavy Minigun",
            "Weapon: Rocket Launcher",
            "Weapon: Missile Launcher",
            "Ally: Rifleman",
            "Ally: Grenadier",
            "Ally Upgrade: Automatic Rifleman",
            "Ally: Marksman",
            "Ally Upgrade: Flamethrower",
            "Ally: Rocketeer",
            "Ally Upgrade: Advanced Sniper Rifle",
            "Weapon Training",
            "Ally Upgrade: Homing Missiies",
            "Skill: Air Support",
            "Skill: Adrenaline injection",
            "Logistics",
            "Fireteam: Rifleman",
            "Fireteam: Grenadier",
            "Battle Experience",
            "Fireteam: Marksman",
            "Inspiration",
            "Fireteam: Rocketeer",
            "War Supplies",
            "Drilling"
    };

    public string[] descriptions =
    {
            "1 bullet",
            "2 bullets",
            "3 bullets",
            "4 bullets",
            "5 bullets",
            "6 bullets",
            "7 bullets",
            "8 bullets",
            "9 bullets",
            "10 bullets",
            "1 bullet",
            "Throw boom",
            "Shoot bullet target",
            "Unlocks allied Marksman. Marksmen shoot right in the target",
            "Equips Grenadiers with flamethrowers. Flamethrowers spray out burning fuel",
            "Unlock allied Rocketeer. Rocketeers launch rockets straight forward",
            "Marksmen shoot two bullets",
            "All weapons reload 30% faster",
            "Rocketeers launch homing missiles",
            "Allied battle helicopter comes to support your squad",
            "Makes enemies and enemy bullets move slower",
            "+100% to the money and XP magnet radius",
            "The game starts with one Rifleman in the squad",
            "The game starts with one Grenadier in the squad",
            "Enemies drop 20% more money and XP",
            "The game starts with one Marksman in the squad",
            "Skills reload 30% faster",
            "The game starts with one Rocketeer in the squad",
            "+30% to bonuses drop rate",
            "All the allies get 50% fire rate boost",
    };

    public Sprite[] sprite;
    public Sprite[] spriteBought;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
