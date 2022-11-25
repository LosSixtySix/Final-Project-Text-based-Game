﻿//Jonathan Hoyt 11/4/2022 Final Project. ASCII Art taken from https://www.asciiart.eu

Console.Clear();

//Variables//
bool hardCoreMode = false;
Random rand = new Random();
int dTwenty = 21;
int dEight = 9;
int dSix = 7;
int dFour = 5;
int dFive = 6;
int dTen = 11;
int dThree = 4;
bool playGame = true;

int rollDie(int dice)
{
    return rand.Next(1, dice);
}

//Dungeon Creator Variables//
int dungeonLevel = 1;
int roomNumber = 0;
int numberOfRooms = 0;
int maxRooms = 10;
int maxDungeonLevels = 4;
bool bossRoom = false;



//Print castle image as grid//
string [] castleRows = File.ReadAllLines("castle.txt");
char[][] castleChar = castleRows.Select(item => item.ToArray()).ToArray();

void printCastle()
{
    int temp_x = 0;
    while(temp_x < 25)
    {
        string temp_row = "";
        for(int i = 0; i < castleRows[0].Length; i++)
        {
            temp_row += castleChar[temp_x][i];
        }
        Console.WriteLine(temp_row);
        temp_x++;
    }
}

//Game Start//




//Items
Items bootsofSpeed = new Items();
bootsofSpeed.name = "Boots of Speed";
bootsofSpeed.intiativeBonus = 5;

Items magicSword = new Items();
magicSword.name = "Magic Sword";
magicSword.level = 2;
magicSword.attackBonus = 7;
magicSword.damageDie = dEight;

Items magicShield = new Items();
magicShield.name = "Holy Shield";
magicShield.level = 2;
magicShield.attackBonus = 7;
magicShield.raiseShieldBonus = 5;

Items shield = new Items();
shield.name = "Shield";
shield.level = 1;
shield.attackBonus = 5;
shield.raiseShieldBonus = 3;

Items sword = new Items();
sword.name = "Sword";
sword.level = 1;
sword.attackBonus = 1 + sword.level * 2;
sword.damageDie = dSix;

Items wornBoots = new Items();
wornBoots.name = "Worn Boots";
wornBoots.intiativeBonus = 0;

Items emptySlot = new Items();
emptySlot.name = "[Empty Slot]";

Items healthPotion = new Items();
healthPotion.name = "Health Potion";
healthPotion.healBonus = 5;
healthPotion.goldValue = 5;

Items greaterHealthPotion = new Items();
greaterHealthPotion.name = "Greater Health Potion";
greaterHealthPotion.healBonus = 10;
greaterHealthPotion.goldValue = 15;

Items expertHealthPotion = new Items();
expertHealthPotion.name = "Expert Health Potion";
expertHealthPotion.healBonus = 15;
expertHealthPotion.goldValue = 25;

Items throwingDagger = new Items();
throwingDagger.name = "Throwing Dagger";
throwingDagger.damageDie = dSix;
throwingDagger.goldValue = 5;

Items bomb = new Items();
bomb.name = "Bomb";
bomb.goldValue = 15;

List<Items> treasureRoomPullList = new List<Items>(){magicShield, magicSword, bootsofSpeed};

List<Items> merchantPullList = new List<Items>(){healthPotion, greaterHealthPotion, expertHealthPotion, throwingDagger, bomb};
Items[] shopList = new Items[5];
for(int shopListPlace = 0; shopListPlace < shopList.Length; shopListPlace++)
{
    shopList[shopListPlace] = emptySlot;
}





//Monsters//

Monsters skeleton = new Monsters();
skeleton.name = "Skeleton";
skeleton.level = 1;
skeleton.MonsterAC = 15;
skeleton.MonsterAttackDamage = 1;
skeleton.MonsterIntiative = 3;
skeleton.MonsterHitPoints = 5;

Monsters rat = new Monsters();
rat.name = "Dire Rat";
rat.level = 1;
rat.MonsterAC = 10;
rat.MonsterAttackDamage = 5;
rat.MonsterIntiative = 6;
rat.MonsterHitPoints = 3;

Monsters minotaur = new Monsters();
minotaur.name = "Minotaur";
minotaur.level = 1;
minotaur.MonsterAC = 12;
minotaur.MonsterAttackDamage = 2;
minotaur.MonsterIntiative = 4;
minotaur.MonsterHitPoints = 10;

List<Monsters> listOfMonstersLevelOne = new List<Monsters>(){minotaur, skeleton, skeleton, skeleton, rat, rat, rat, rat, rat, rat};
List<Monsters> listOfMonstersLevelTwo = new List<Monsters>(){minotaur};
List<Monsters> listOfMonstersLevelThree = new List<Monsters>(){minotaur};

//Player
Items equippedGloves = emptySlot;
Items equippedBoots = wornBoots;
Items equippedWeapon = sword;
Items equippedShield = shield;
int goldCount = 0;
int hitPoints = 10;
int Playerlevel = 1;
int experience = 0;
int inventoryCount = 5;
Items [] backPack = new Items[inventoryCount];
int playerHp = hitPoints + Playerlevel * 2;
int playerMaxHP = hitPoints + Playerlevel *2;
int playerIntiative = 10 + Playerlevel + equippedBoots.intiativeBonus;
int playerAttackBonus = Playerlevel + equippedWeapon.level;
for(int backPackPlace = 0; backPackPlace < backPack.Length; backPackPlace++)
{
    backPack[backPackPlace] = emptySlot;
}
backPack.SetValue(bomb, 0);
backPack.SetValue(throwingDagger, 1);

//Combat//
string [] knightRows = File.ReadAllLines("knight.txt");
char[][] knightChar = knightRows.Select(items => items.ToArray()).ToArray();
void combat()
{
    // Determine monster type
    Monsters ChosenMonster = listOfMonstersLevelOne[0];
    if(dungeonLevel == 1)
    {
        int randoMonster = rollDie(dTen - 1);
        ChosenMonster = listOfMonstersLevelOne[randoMonster]; 
    }
    else if(dungeonLevel == 2)
    {
        ChosenMonster = listOfMonstersLevelTwo[0];
    }
    else if(dungeonLevel == 3)
    {
        ChosenMonster = listOfMonstersLevelThree[0];
    }
        string monsterPicNumber = monsterPictureType(monsterType(ChosenMonster.name)).ToString();
        string monsterPic = $"monster{monsterPicNumber}.txt";
        string [] monsterRows = File.ReadAllLines($"{monsterPic}");
        char [][] monsterChar = monsterRows.Select(item => item.ToArray()).ToArray();
        int temp_monsterHp = ChosenMonster.MonsterHitPoints;

    //Values declared in Combat//
    int playerAC = 10 + Playerlevel + equippedShield.attackBonus;

    bool determineHit(int targetAC, int attackBonus)
    {
        if(rollDie(dTwenty) + attackBonus > targetAC)
        {
            return true;
        }
        return false;
    }
    int Playerdamage()
    {
        return equippedWeapon.damageDie + equippedWeapon.damageBouns + playerAttackBonus;
    }
    int MonsterDamage(Monsters x)
    {
        if(x.level == 1)
        {
            return x.level + x.MonsterAttackDamage + rollDie(dFour);
        }
        if(x.level == 2)
        {
            return x.level + x.MonsterAttackDamage + rollDie(dSix);
        }
        if(x.level == 3)
        {
            return x.level + x.MonsterAttackDamage + rollDie(dEight);
        }
        return x.level + x.MonsterAttackDamage + rollDie(dFour);
    }

    //Determine who goes first//
    bool playerTurn = false;
    bool monsterTurn = false;
    if(playerIntiative > ChosenMonster.MonsterIntiative)
    {
        playerTurn = true;
    }
    else
    {
        monsterTurn = true;
    }
    bool fight = true;
    int temp_playerAC = playerAC;
    int temp_MonsterAC = ChosenMonster.MonsterAC;
    while(fight)
    {
        


        while(playerTurn)
        {   
            bool makeAChoice = true;
            while(makeAChoice)
            {
            printKnight();
                Console.WriteLine($"Hit Points: {playerHp}");
                Console.WriteLine($"AC:{temp_playerAC}");
                Console.WriteLine("(1) Attack! (2) Use Item (3) Raise Shield (4) Run Away!!");
                int choice;
                int damageDealt = Playerdamage();
                bool successChoice = int.TryParse(Console.ReadLine(), out choice);
                int numberEmptySlots = 0;
                bool allEmpty = false;
                bool CheckingInventory = false;
                if(successChoice != true || choice > 4 || choice <= 0)
                {
                    Console.WriteLine("That is not a valid choice, press enter and try again.");
                    Console.ReadLine();
                    continue;
                }
                //Checking if inventory is empty or not//
                for(int testingInventorySlots = 0; testingInventorySlots < inventoryCount; testingInventorySlots++)
                {
                    if(backPack[testingInventorySlots] == emptySlot)
                    {
                        numberEmptySlots++;
                    }
                }
                if(numberEmptySlots == inventoryCount)
                {
                    allEmpty = true;
                }
                if(choice == 2 && allEmpty)
                {
                    Console.WriteLine("Your inventory is empty, choose a different action");
                    Console.ReadLine();
                    CheckingInventory = true;
                }
                else if(allEmpty == false)
                {
                    CheckingInventory = false;
                }
                
                while(CheckingInventory == false)
                {
                switch(choice)
                {
                    case 1:
                        if(determineHit(temp_MonsterAC,equippedWeapon.attackBonus ))
                        {
                            Console.WriteLine("You Hit!!");
                            temp_monsterHp -= damageDealt;
                            temp_MonsterAC -= damageDealt;
                            Console.ReadLine();
                        }
                        else
                        {
                            temp_MonsterAC -= damageDealt;
                            Console.WriteLine("You missed!");
                            Console.ReadLine();
                        }
                        CheckingInventory = true;
                        makeAChoice = false;
                        monsterTurn = true;
                        break;
                    case 2:
                        Console.Clear();
                        PrintInventory(backPack);
                        bool useInventory = true;
                        int inventoryChoice = 0;
                        int ammountOfFailedChoicesMade = 0;
                        bool usingItem = false;
                        Items ChosenItem = emptySlot;
                        while(useInventory)
                        {

                            bool successInventroySelect = int.TryParse(Console.ReadLine(), out inventoryChoice);
                            
                            if(successInventroySelect == true && inventoryChoice < backPack.Length && inventoryChoice > 0)
                            {
                                inventoryChoice--;
                                useInventory = false;
                                usingItem = true;
                                ChosenItem = backPack[inventoryChoice];
                            }
                            else
                            {
                                Console.WriteLine("That is not a valid selection");
                            }
                        }   
                        while(usingItem == true)
                        {

                            if(ammountOfFailedChoicesMade >= 5)
                            {
                                Console.WriteLine("You have taken too long, this is your last chance before your turn ends");
                                useInventory = false;
                                usingItem = false;
                            }
                            if(ChosenItem.name.Contains("Health"))
                            {
                                playerHp += backPack[inventoryChoice].healBonus;
                                backPack[inventoryChoice] = emptySlot;
                                useInventory = false;
                                usingItem = false;
                                Console.WriteLine($"You heal {backPack[inventoryChoice].healBonus} points");
                            }
                            if(ChosenItem.name.Contains("Throwing"))
                            {
                                temp_monsterHp -= ChosenItem.damageDie;
                                backPack[inventoryChoice] = emptySlot;
                                useInventory = false;
                                usingItem = false;
                                Console.WriteLine($"You use a {ChosenItem.name} on {ChosenMonster.name}");
                            }
                            if(ChosenItem.name.Contains("Bomb"))
                            {
                                temp_monsterHp -= ChosenMonster.MonsterHitPoints;
                                backPack[inventoryChoice] = emptySlot;
                                useInventory = false;
                                usingItem = false;
                                Console.WriteLine($"You use a {ChosenItem.name} on {ChosenMonster.name}");
                            }
                            else if(ChosenItem == emptySlot)
                            {
                                Console.WriteLine("That is an empty slot please make a different selection");
                                ammountOfFailedChoicesMade ++;
                            }
                        }
                            Console.ReadLine();
                        
                        CheckingInventory = true;
                        makeAChoice = false;
                        monsterTurn = true;
                        break;
                    case 3: 
                        temp_playerAC += shield.raiseShieldBonus *2;
                        Console.WriteLine("Your shield is raised, making it easier to deflect attacks");
                        Console.ReadLine();
                        CheckingInventory = true;
                        makeAChoice = false;
                        monsterTurn = true;
                        break;
                    case 4:
                        if(fledBattle(rollDie(dTwenty)))
                        {
                            if(playerHp % 2 == 0)
                            {
                                playerHp = playerHp/2;
                            }
                            else if(playerHp % 2 != 0)
                            {
                                int temp_PlayerHp = playerHp +1;
                                playerHp = temp_PlayerHp/2;
                            }
                            Console.WriteLine("You don't come away without injury but you escape");
                            Console.WriteLine($"Your current Hit Points: {playerHp}");
                            CheckingInventory = true;
                            makeAChoice = false;
                            monsterTurn = false;
                            fight = false;
                        }
                        else
                        {
                        CheckingInventory = true;
                        makeAChoice = false;
                        monsterTurn = false;
                        fight = false;
                        Console.WriteLine("You flee the battle unscathed");
                        }
                        break;

                }
                }
            }
            playerTurn = false;
            


        }
        while(monsterTurn)
        {
            printMonster();
            Console.WriteLine($"HP: {temp_monsterHp}");
            bool hit = determineHit(temp_playerAC, ChosenMonster.level*2);
            int monsterDamageDealt = MonsterDamage(ChosenMonster);
            if(temp_monsterHp <= 0)
            {
                fight = false;
                Console.WriteLine($"You have defeated the {ChosenMonster.name}!!");
                int goldGain = rollDie(dEight) + (ChosenMonster.level *4);
                goldCount += goldGain;
                Console.WriteLine($"You gained {goldGain} gold pieces");

            }
            else if(hit)
            {
                Console.WriteLine($"The {ChosenMonster.name} hits you");
                playerHp -= monsterDamageDealt;
                temp_playerAC -= monsterDamageDealt;
            }
            else if(hit == false)
            {
                Console.WriteLine($"The {ChosenMonster.name} misses");
                temp_playerAC -= monsterDamageDealt;
            }
            Console.ReadLine();
            monsterTurn = false;
            playerTurn = true;
            if(playerHp <= 0)
            {
                fight = false;
                playGame = false;
            }
        }

    }
    void printKnight()
    {
        Console.Clear();
        int temp_x = 0;
        while(temp_x < 22)
        {
            string temp_row ="";
            for(int i = 0; i < knightRows[4].Length; i++)
            {
                temp_row += knightChar[temp_x][i];
            }
            Console.WriteLine(temp_row);
            temp_x++;
        }

    }


    void printMonster()
    {
        Console.Clear();
        int temp_y = 0;
        while(temp_y < 23)
        {
            string temp_row ="";
            for(int i = 0; i < monsterRows[0].Length; i++)
            {
                temp_row += monsterChar[temp_y][i];
            }
            Console.WriteLine(temp_row);
            temp_y++;
        }

    }

}
static int monsterType(string name)
{
    if(name == "Minotaur")
    {
        return 1;
    }
    else if(name == "Skeleton")
    {
        return 2;
    }
    else if(name == "Dire Rat")
    {
        return 3;
    }
    return 1;
}
static int monsterPictureType(int monsterType)
{
    
    return monsterType;
}
main();
if(playGame == false && playerHp <= 0)
{
    Console.WriteLine("You have been defeated");
}



bool fledBattle(int randDieRoll)
{
    if(randDieRoll < 10)
    {
        return true;
    }
    return false;
}

// Change Equipment
void changeEquipment()
{

    int equipmentChoice;
    for(int i = 0; i < backPack.Length; i ++)
    {
        Console.WriteLine($"{i}: {backPack[i].name}");
    }

    bool successEquipmentChoice = int.TryParse(Console.ReadLine(), out equipmentChoice);
    equippedWeapon = backPack[equipmentChoice];
}

//Rooms


///SHOP///
void shop()
{
    int choicesMade = 0;
    for(int i = 0; i < shopList.Length; i ++)
    {
        if(shopList[i] == emptySlot)
        {
            int randomItem = rollDie(dFive) - 1;
            shopList[i] = merchantPullList[randomItem];
        }
    }
    bool shopping = true;
    while(shopping)
    {
        string [] merchantRows = File.ReadAllLines("merchant.txt");
        char[][] merchantChar = merchantRows.Select(items => items.ToArray()).ToArray();
        Console.Clear();
        int temp_x = 0;
        while(temp_x< 1)
        {
            string temp_row = "";
            for(int i = 0; i<merchantRows[0].Length; i++)
            {
                temp_row += merchantChar[temp_x][i];
            }
            Console.WriteLine(temp_row);
            temp_x++;
        }
        Console.Write("1: Exit");
        PrintShopInventory(shopList);
        Console.WriteLine();
        Console.Write($"You have {goldCount} gold pieces.");
        bool checkingChoice = true;
        int buy;
        bool successBuy = int.TryParse(Console.ReadLine(), out buy);
        while(checkingChoice)
        {
            if(choicesMade >= 4)
            {
                shop();
            }
            if(successBuy && buy <= 6 && buy > 0)
            {
                switch(buy)
                {
                    case 1:
                        Console.WriteLine("Exiting");
                        Console.ReadLine();
                        checkingChoice = false;
                        shopping = false;
                        break;
                    case 2:
                        if(AllEmpty(shopList))
                        {
                            Console.WriteLine("There is nothing left to buy from the merchant, You must Leave now");
                            checkingChoice = false;
                            shopping = false;
                        }
                        if(shopList[0] == emptySlot)
                        {
                            Console.WriteLine("There is nothing to buy here");
                            Console.ReadLine();
                            checkingChoice = false;
                            break;
                        }
                        if(goldCount >= shopList[0].goldValue)
                        {
                            int checkingEmptySlotReturn = nextEmptySlot(backPack);
                            if(checkingEmptySlotReturn < 100)
                            {
                                
                                backPack[checkingEmptySlotReturn] = shopList[0];
                                Console.WriteLine($"You have bought {shopList[0].name}.");
                                goldCount -= shopList[0].goldValue;                                
                                shopList[0] = emptySlot;
                                Console.ReadLine();
                                checkingChoice = false;
                                choicesMade ++;
                            }
                        }
                        else if(goldCount < shopList[0].goldValue)
                        {
                            Console.WriteLine("That Item costs too much");
                            Console.ReadLine();
                            checkingChoice = false;
                        }
                        break;
                    case 3:
                        if(AllEmpty(shopList))
                        {
                            Console.WriteLine("There is nothing left to buy from the merchant, You must Leave now");
                            checkingChoice = false;
                            shopping = false;
                        }
                        if(shopList[1] == emptySlot)
                        {
                            Console.WriteLine("There is nothing to buy here");
                            Console.ReadLine();
                            checkingChoice = false;
                            break;
                        }
                        if(goldCount >= shopList[1].goldValue)
                        {
                            int checkingEmptySlotReturn = nextEmptySlot(backPack);
                            if(checkingEmptySlotReturn < 100)
                            {
                                
                                backPack[checkingEmptySlotReturn] = shopList[1];
                                Console.WriteLine($"You have bought {shopList[1].name}.");
                                goldCount -= shopList[1].goldValue;                              
                                shopList[1] = emptySlot;
                                Console.ReadLine();
                                choicesMade ++;
                                checkingChoice = false;
                            }
                        }
                        else if(goldCount < shopList[1].goldValue)
                        {
                            Console.WriteLine("That Item costs too much");
                            Console.ReadLine();
                            checkingChoice = false;
                        }
                        break;
                    case 4:
                        if(AllEmpty(shopList))
                        {
                            Console.WriteLine("There is nothing left to buy from the merchant, You must Leave now");
                            checkingChoice = false;
                            shopping = false;
                        }
                        if(shopList[2] == emptySlot)
                        {
                            Console.WriteLine("There is nothing to buy here");
                            Console.ReadLine();
                            checkingChoice = false;
                            break;
                        }
                        if(goldCount >= shopList[2].goldValue)
                        {
                            int checkingEmptySlotReturn = nextEmptySlot(backPack);
                            if(checkingEmptySlotReturn < 100)
                            {
                                
                                backPack[checkingEmptySlotReturn] = shopList[2];
                                Console.WriteLine($"You have bought {shopList[2].name}.");
                                goldCount -= shopList[2].goldValue;                                
                                shopList[2] = emptySlot;
                                Console.ReadLine();
                                checkingChoice = false;
                                choicesMade ++;
                            }
                        }
                        else if(goldCount < shopList[2].goldValue)
                        {
                            Console.WriteLine("That Item costs too much");
                            Console.ReadLine();
                            checkingChoice = false;
                        }
                        break;
                    case 5:
                        if(AllEmpty(shopList))
                        {
                            Console.WriteLine("There is nothing left to buy from the merchant, You must Leave now");
                            checkingChoice = false;
                            shopping = false;
                        }
                        if(shopList[3] == emptySlot)
                        {
                            Console.WriteLine("There is nothing to buy here");
                            Console.ReadLine();
                            checkingChoice = false;
                            break;
                        }
                        if(goldCount >= shopList[3].goldValue)
                        {
                            int checkingEmptySlotReturn = nextEmptySlot(backPack);
                            if(checkingEmptySlotReturn < 100)
                            {
                                
                                backPack[checkingEmptySlotReturn] = shopList[3];
                                Console.WriteLine($"You have bought {shopList[3].name}.");
                                goldCount -= shopList[3].goldValue;                                
                                shopList[3] = emptySlot;
                                Console.ReadLine();
                                checkingChoice = false;
                                choicesMade ++;
                            }
                        }
                        else if(goldCount < shopList[3].goldValue)
                        {
                            Console.WriteLine("That Item costs too much");
                            Console.ReadLine();
                            checkingChoice = false;
                        }
                        break;
                    case 6:
                        if(AllEmpty(shopList))
                        {
                            Console.WriteLine("There is nothing left to buy from the merchant, You must Leave now");
                            checkingChoice = false;
                            shopping = false;
                        }
                        if(shopList[4] == emptySlot)
                        {
                            Console.WriteLine("There is nothing to buy here");
                            Console.ReadLine();
                            checkingChoice = false;
                            break;
                        }
                        if(goldCount >= shopList[4].goldValue)
                        {
                            int checkingEmptySlotReturn = nextEmptySlot(backPack);
                            if(checkingEmptySlotReturn < 100)
                            {
                                
                                backPack[checkingEmptySlotReturn] = shopList[4];
                                Console.WriteLine($"You have bought {shopList[4].name}.");
                                goldCount -= shopList[4].goldValue;                                
                                shopList[4] = emptySlot;
                                Console.ReadLine();
                                checkingChoice = false;
                                choicesMade ++;
                            }
                        }
                        else if(goldCount < shopList[4].goldValue)
                        {
                            Console.WriteLine("That Item costs too much");
                            Console.ReadLine();
                            checkingChoice = false;
                        }
                        break;


                }

            }
            else
            {
                Console.WriteLine("That is not a valid option try again");
                checkingChoice= false;
            }
        }
    }
    
    
}
void TresureRoom()
{
    Console.Clear();
    Console.WriteLine("This is the treasuer room");
    if(treasureRoomPullList.Count > 0)
    {
        int randoDrop = rand.Next(1, treasureRoomPullList.Count + 1);
        Items droppedItem = treasureRoomPullList[randoDrop];
        if(droppedItem == magicShield)
        {
            equippedShield = magicShield;
            treasureRoomPullList.Remove(magicShield);
        }
        if(droppedItem == magicSword)
        {
            equippedWeapon = droppedItem;
            treasureRoomPullList.Remove(droppedItem);
        }
        if(droppedItem == bootsofSpeed)
        {
            equippedBoots = droppedItem;
            treasureRoomPullList.Remove(droppedItem);
        }
    }
    else if(equippedShield == magicShield && equippedWeapon == magicSword && equippedBoots == bootsofSpeed)
    {
        goldCount += 10 + (dungeonLevel * 5);
    }
}
void ItemDropRoom()
{
    Console.Clear();
    Console.WriteLine("This room will drop an item");
}
void TrapRoom()
{
    Console.Clear();
    Console.WriteLine("It's a TRAP!!");
}
void MiniBoss()
{
    Console.Clear();
    Console.WriteLine("This is a mini Boss room");
}
void boss_Room()
{
    Console.Clear();
    Console.WriteLine("This is the boss room");
}

//Inventory Management 
void PrintInventory(Items[] x)
{

    for(int i = 0; i < x.Length; i++)
    {
        Console.WriteLine($"{x[i].name}");
    }
}
void PrintShopInventory(Items[] x)
{
        for(int i = 0; i < x.Length; i++)
    {
        Console.WriteLine();
        Console.Write($"{i+2}: {x[i].name} ");
        Console.Write($"{x[i].goldValue} gp");
        
    }
}

int nextEmptySlot(Items[] x)
{
    for(int i = 0;  i<x.Length; i ++)
    {
        if(x[i] == emptySlot)
        {
            return i;
        }
    }
    return 100;
}
bool AllEmpty(Items[] x)
{
    if(x[0] == emptySlot && x[1] == emptySlot && x[2] == emptySlot && x[3] == emptySlot && x[4] == emptySlot)
    {
        return true;
    }
    return false;
}

//Dungeon Creator
void randRoomGenerator()
{
    if(numberOfRooms == maxRooms)
    {
        numberOfRooms = 0;
        dungeonLevel ++;
        bossRoom = true;
    }
    int randRoom = rand.Next(1,15);
    if(bossRoom == true)
    {
        boss_Room();
        bossRoom = false;
    }
    else if(bossRoom != true)
    {
        numberOfRooms++;
        if(randRoom == 1)
            {
                shop();
            }
        else if(randRoom == 2)
            {
                TresureRoom();
            }
        else if(randRoom == 3 || randRoom == 7 || randRoom == 8 || randRoom == 14)
            {
                ItemDropRoom();
            }
        else if(randRoom == 4 || randRoom == 11 || randRoom == 12 || randRoom == 13)
            {
                combat();
            }
        else if(randRoom == 5 || randRoom == 9 || randRoom == 10)
            {
                TrapRoom();
            }
        else if(randRoom == 6)
            {
                MiniBoss();
                Console.WriteLine(dungeonLevel);
                Console.WriteLine(numberOfRooms);
            }

    }
    
}
void RoomZero()
{

}
void main()
{
while(playGame)
{
    printCastle();
    Console.WriteLine("The Castle of Terath");
    Console.WriteLine("Would you like to play hard core mode? type y for yes or n for no.");
    string? terminalInput = Console.ReadLine();
    int hardCoreDecisionInt;
    bool hardCoreDecisionSuccess = int.TryParse(terminalInput, out hardCoreDecisionInt);
    if(hardCoreDecisionSuccess == false)
    {
        string? hardCoreDecision = terminalInput;
        if(hardCoreDecision.ToLower() == "y")
        {
            hardCoreMode = true;
        }
        else if(hardCoreDecision.ToLower() == "n")
        {
            hardCoreMode = false;
        }
        else
        {
            Console.WriteLine("That is not a valid decision, hard core mode will be randomized......");
            int randoHardMode = rand.Next(1, 3);
            if(randoHardMode == 1)
            {
                hardCoreMode = true;
            }
            else if(randoHardMode == 2)
            {
                hardCoreMode = false;
            }
        }
    }
    if(hardCoreDecisionSuccess)
    {
        Console.WriteLine("That is not a valid decision, hard core mode will be randomized.......");
            int randoHardMode = rand.Next(1, 3);
            if(randoHardMode == 1)
            {
                hardCoreMode = true;
            }
            else if(randoHardMode == 2)
            {
                hardCoreMode = false;
            }
    }
    Console.WriteLine("Press Enter to Start");
    Console.ReadLine();
    RoomZero();
    while(numberOfRooms <= maxRooms && playGame)
    {
        randRoomGenerator();
        Console.WriteLine("Press Enter to continue");
        Console.ReadLine();
        if(hardCoreMode == false)
        {
            if(playerHp +2< playerMaxHP)
            {
                Console.WriteLine("You rest a little and heal 2 HP");
                Console.ReadLine();
                playerHp += 2;
            }
            else if(playerHp +1 < playerMaxHP)
            {
                Console.WriteLine("You try to sleep in this gloomy place, but struggle to get comfortable. You still manage to heal 1 HP");
                Console.ReadLine();
                playerHp += 1;
            }
        }
    }
}

}
class Monsters
{
    public string name;
    public int level;
    public int MonsterIntiative;
    public int MonsterHitPoints;
    public int MonsterAC;
    public int MonsterAttackDamage;
}
class Items
{
    public string name;
    public int level;
    public int damageDie;
    public int attackBonus;
    public int raiseShieldBonus;
    public int damageBouns;
    public int healBonus;
    public int goldValue;
    public int intiativeBonus;
    
}