using System.Collections;
using UnityEngine;

public class DataStorage : MonoBehaviour {
    public static Hashtable saveValues = new Hashtable();
    /*
    int volume => Stored in PlayerPrefs

    int health;

    int maxHealth;

    Vector2 position;

    string currScene;
    
    int facingDirection

    int deaths;
    after 3 deaths, trigger blacksmith

    //Upgrades and Blacksmith stuff

    int blacksmith;
    0 for Lumin haven't talked to blacksmith, 1 for seen blacksmith already

    int upgrade;
    0 for No Upgrade, 1 for Vampric Embrace, 2 for Fleet Foot, 3 for Nova Impact

    int upgradeBar;
    tracks upgrade progress till can trigger upgrade

    int healAfterBosses;
    how much health do you heal after every boss - default is 2 (1 heart) from 0 to 10.
    //

    int introSceneDone;
    0 for not done, 1 for done: triggers bedroom tutorial

    int tutorialDojo;
    0 for tutorial to trigger, 1 for talking to the Senior Warden to fire at the dummies, 2 for finishing the tutorial

    int messHall;
    0 - haven't finished wax dungeon
    1 - finished wax dungeon

    int progress; 
    0 - haven't done tutorial yet
    1 - finished tutorial, haven't talked to priest
    2 - talked to priest, haven't entered dungeon (enable Wax Dungeon Button)
    3 - attempted wax dungeon, died
    4 - completed wax dungeon, haven't talked to priest
    5 - completed wax dungeon, talked to priest

    int blessings;
    0 - have not died yet
    1 - died the first time (need a way to differentiate which part the player died from)
    2 - already introduced to blessings

    int usedBlessings;
    0 - did not use blessings (easter egg available)
    1 - used blessings (no easter egg available)

    int waxDungeonRoom;
    0 to 8 - tracks what room the player is in for Wax Dungeon

    int[] randWaxDungeonArray;
    [] - Randomly generates what rooms the Player goes through

    int completedWaxDungeon;
    0 - haven't finished the Wax Dungeon
    1 - attempted but haven't finished Wax Dungeon
    2 - finished the Wax Dungeon

    int waxDungeonGolem;
    0 - haven't seen golem
    1 - fought and lost against golem
    2 - fought and won golem

    int waxDungeonFourArms;
    0 - haven't seen four arms
    1 - fought and lost against four arms
    2 - fought and won four arms

    int waxDungeonGabriel;
    0 - haven't seen Gabriel
    1 - fought and lost against Gabriel
    2 - fought and won Gabriel

    int seenSavePoint;
    0 - haven't seen Save Point
    1 - Seen Save Point

    int savedWaxGolem
    0 - did not save at Golem
    1 - saved at Golem

    int savedFourArms
    0 - did not save at Four Arms
    1 - saved at Four Arms

    //For end game//

    int finalBossBeatenCount;
    how many times you've beaten the final boss

    int introToEnd;
    0 - havent introed to end yet
    1 - finished last boss but havent introed
    2 - priest walking phase
    3 - introed to end already

    int sunShardsCollected;
    0 - 4 depending on how many shards you have

    int sunShardsInserted;
    depends on number of shard inserted

    int reversedControls;

    int blackOut;

    int timeTrial;

    float timeTrialTime;
    Keeps track of how much time you have left

    int completedReversedControls

    int completedBlackOut;

    int completedTimeTrial;

    int introToTrials
    0 - Not reached end game yet
    1 - In intro to trials
    2 - Finished trials intro
    3 - Finished trials intro and panel intro

    int newMission
    0 - End Game not started
    1 - End Game started
    2 - End Game bedroom done

    int finishGame
    0 - havent finished game
    1 - just need to put 1 shard in
    2 - finished game already

    //Achievements// (Achievements are in PlayerPrefs and do not get overriden if you reset the game)
    
    int golemSlain;
    Bigger they are, harder they fall
    You have defeated the Wax Golem. 

    int fourArmsSlain;
    Handicapable
    You have defeated Four Arms.

    int gabrielSlain;
    Dewinged
    You have defeated Gabriel.

    int reverseControlsTrialed;
    Topsy Turvy
    You have completed the Reverse Controls Challenge.

    int blackOutTrialed;
    Night Vision
    You have completed the Black Out Challenge.

    int timeTrialTrialed;
    Mister Sandman
    You have completed the Timed Trial Challenge.

    int endGame;
    The End
    You have completed the game.

    int noUpgradeRun;
    By your own Strength
    You have completed a run with no upgrades.

    int threeHeartsRun;
    No Blessing Needed
    You have completed a run on 3 hearts.

    int noHeal;
    Heal Blocked
    You have completed a run without healing after every boss.

    int hardMode;
    God of Gaming
    You have completed a run with no upgrades, 3 hearts and without healing after every boss.

    int kill100;
    Cut 'em Down
    You have slain 100 enemies.

    int kill500;
    Slayer of Worlds
    You have slain 500 enemies.

    int died5;
    Grave Hugger
    You have died 10 times.

    int died10;
    Grave Lover
    You have died 20 times.
    */
}