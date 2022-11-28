//Jonathan Hoyt 11/4/2022 Final Project. ASCII Art taken from https://www.asciiart.eu

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
int numberOfRooms = 0;
int maxRooms = 20;
int maxDungeonLevels = 5;
bool bossRoom = false;
string[] roomsAlreadyRolled = new string[maxRooms];



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

Items throwingAxe = new Items();
throwingAxe.name = "Throwing Axe";
throwingAxe.goldValue = 10;
throwingAxe.damageDie = dTen;

Items bigBackPack = new Items();
bigBackPack.name = "Big Back Pack";
bigBackPack.goldValue = 15;
bigBackPack.inventorySlotsBonus =3;

List<Items> itemRoomPullList = new List<Items>(){throwingDagger, throwingDagger, throwingDagger, healthPotion, healthPotion, healthPotion, healthPotion, healthPotion, healthPotion, throwingAxe, expertHealthPotion, greaterHealthPotion, greaterHealthPotion, greaterHealthPotion, bomb };
List<Items> treasureRoomPullList = new List<Items>(){magicShield, magicSword, bootsofSpeed, bigBackPack};

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
skeleton.experienceWorth = 5;

Monsters rat = new Monsters();
rat.name = "Dire Rat";
rat.level = 1;
rat.MonsterAC = 10;
rat.MonsterAttackDamage = 5;
rat.MonsterIntiative = 6;
rat.MonsterHitPoints = 3;
skeleton.experienceWorth = 8;

Monsters minotaur = new Monsters();
minotaur.name = "Minotaur";
minotaur.level = 1;
minotaur.MonsterAC = 12;
minotaur.MonsterAttackDamage = 2;
minotaur.MonsterIntiative = 4;
minotaur.MonsterHitPoints = 10;
minotaur.experienceWorth = 5;

List<Monsters> listOfMonstersLevelOne = new List<Monsters>(){minotaur, skeleton, skeleton, skeleton, rat, rat, rat, rat, rat, rat};
List<Monsters> listOfMonstersLevelTwo = new List<Monsters>(){minotaur};
List<Monsters> listOfMonstersLevelThree = new List<Monsters>(){minotaur};

//Player
Items equippedBackPack = emptySlot;
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
backPack.SetValue(healthPotion, 0);

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
                        Console.WriteLine("Type 'leave' to leave the backpack.");

                        int inventoryChoice = 0;
                        int ammountOfFailedChoicesMade = 0;
                        bool usingItem = false;
                        Items ChosenItem = emptySlot;
                        while(useInventory)
                        {
                            string? leaveInventory = Console.ReadLine();

                            bool successInventroySelect = int.TryParse(leaveInventory, out inventoryChoice);
                            inventoryChoice--;   
                            if(successInventroySelect == true && inventoryChoice < backPack.Length && inventoryChoice >= 0)
                            {

                                useInventory = false;
                                usingItem = true;
                                ChosenItem = backPack[inventoryChoice];
                            }
                            else if(successInventroySelect == false && leaveInventory.ToLower() == "leave")
                            {
                                useInventory = false;
                                CheckingInventory = true;
                                makeAChoice = true;
                                Console.WriteLine("You are leaving the backpack");
                                playerTurn = true;
                                monsterTurn = false;
                                break;
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
                                CheckingInventory = true;
                                makeAChoice = false;
                                monsterTurn = true;
                            }
                            if(ChosenItem.name.Contains("Health"))
                            {
                                playerHp += ChosenItem.healBonus;
                                backPack[inventoryChoice] = emptySlot;
                                useInventory = false;
                                usingItem = false;
                                Console.WriteLine($"You heal {ChosenItem.healBonus} points");
                                CheckingInventory = true;
                                makeAChoice = false;
                                monsterTurn = true;
                            }
                            if(ChosenItem.name.Contains("Throwing"))
                            {
                                temp_monsterHp -= ChosenItem.damageDie;
                                backPack[inventoryChoice] = emptySlot;
                                useInventory = false;
                                usingItem = false;
                                Console.WriteLine($"You use a {ChosenItem.name} on {ChosenMonster.name}");
                                CheckingInventory = true;
                                makeAChoice = false;
                                monsterTurn = true;
                            }
                            if(ChosenItem.name.Contains("Bomb"))
                            {
                                temp_monsterHp -= ChosenMonster.MonsterHitPoints;
                                backPack[inventoryChoice] = emptySlot;
                                useInventory = false;
                                usingItem = false;
                                Console.WriteLine($"You use a {ChosenItem.name} on {ChosenMonster.name}");
                                CheckingInventory = true;
                                makeAChoice = false;
                                monsterTurn = true;
                            }
                            else if(ChosenItem == emptySlot)
                            {
                                Console.WriteLine("That is an empty slot please make a different selection");
                                usingItem = false;
                                useInventory = true;
                                ammountOfFailedChoicesMade ++;
                            }
                        }
                        Console.ReadLine();
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
                experience = ExperienceGain(ChosenMonster, experience);
                if(LevelUp(experience,Playerlevel))
                {
                    Console.WriteLine("You have gained enough experience that you have leveled up!!");
                    Playerlevel++;
                    experience = 0;
                }

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
    bool choosingSlot = true;
    while(choosingSlot)
    {
        bool successEquipmentChoice = int.TryParse(Console.ReadLine(), out equipmentChoice);

        if(successEquipmentChoice)
        {
            string? equipment = backPack[equipmentChoice].name;
            if(equipment.Contains("Sword"))
            {
                equippedWeapon = backPack[equipmentChoice];
            }
            else if(equipment.Contains("Shield"))
            {
                equippedShield = backPack[equipmentChoice];
            }
            else if(equipment.Contains("Boots"))
            {
                equippedBoots = backPack[equipmentChoice];
            }
            else if(equipment.Contains("Gloves"))
            {
                equippedGloves = backPack[equipmentChoice];
            }

        }
    }
}

//Rooms


///SHOP///
void shop()
{
    Items boughtItem = emptySlot;
    bool choosingSlot = false;
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
                        boughtItem = shopList[0];
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
                            else if(checkingEmptySlotReturn == 100)
                            {
                                choosingSlot = true;

                                while(choosingSlot)
                                {

                                    PrintInventory(backPack);
                                    Console.WriteLine("You have no empty slots left in your backpack.");
                                    Console.WriteLine("Which slot would you like to put it in?");
                                    int slotChoice;
                                    bool successChoice = int.TryParse(Console.ReadLine(), out slotChoice);
                                    slotChoice --;
                                    if(successChoice && slotChoice< backPack.Length && slotChoice >= 0)
                                    {
                                        backPack[slotChoice]= boughtItem;
                                        Console.WriteLine($"You have succesfully bought {boughtItem.name}. Press enter to continue");
                                        shopList[0] = emptySlot;
                                        choosingSlot = false;
                                        checkingChoice = false;
                                    }
                                    else if(successChoice == false || slotChoice >= backPack.Length || slotChoice < 0)
                                    {
                                        Console.WriteLine("That is not a valid choice, try again");
                                        Console.ReadLine();
                                    }
                                }

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
                        boughtItem = shopList[1];
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
                            else if(checkingEmptySlotReturn == 100)
                            {
                                choosingSlot = true;

                                while(choosingSlot)
                                {

                                    PrintInventory(backPack);
                                    Console.WriteLine("You have no empty slots left in your backpack.");
                                    Console.WriteLine("Which slot would you like to put it in?");
                                    int slotChoice;
                                    bool successChoice = int.TryParse(Console.ReadLine(), out slotChoice);
                                    slotChoice --;
                                    if(successChoice && slotChoice< backPack.Length && slotChoice >= 0)
                                    {
                                        backPack[slotChoice]= boughtItem;
                                        Console.WriteLine($"You have succesfully bought {boughtItem.name}. Press enter to continue");
                                        goldCount -= shopList[1].goldValue;
                                        shopList[1] = emptySlot;
                                        choosingSlot = false;
                                        checkingChoice = false;
                                    }
                                    else if(successChoice == false || slotChoice >= backPack.Length || slotChoice < 0)
                                    {
                                        Console.WriteLine("That is not a valid choice, try again");
                                        Console.ReadLine();
                                    }
                                }

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
                        boughtItem = shopList[2];
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
                            else if(checkingEmptySlotReturn == 100)
                            {
                                choosingSlot = true;

                                while(choosingSlot)
                                {

                                    PrintInventory(backPack);
                                    Console.WriteLine("You have no empty slots left in your backpack.");
                                    Console.WriteLine("Which slot would you like to put it in?");
                                    int slotChoice;
                                    bool successChoice = int.TryParse(Console.ReadLine(), out slotChoice);
                                    slotChoice --;
                                    if(successChoice && slotChoice< backPack.Length && slotChoice >= 0)
                                    {
                                        backPack[slotChoice]= boughtItem;
                                        Console.WriteLine($"You have succesfully bought {boughtItem.name}. Press enter to continue");
                                        goldCount -= shopList[2].goldValue;
                                        shopList[2] = emptySlot;
                                        choosingSlot = false;
                                        checkingChoice = false;
                                    }
                                    else if(successChoice == false || slotChoice >= backPack.Length || slotChoice < 0)
                                    {
                                        Console.WriteLine("That is not a valid choice, try again");
                                        Console.ReadLine();
                                    }
                                }

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
                        boughtItem = shopList[3];
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
                            else if(checkingEmptySlotReturn == 100)
                            {
                                choosingSlot = true;

                                while(choosingSlot)
                                {

                                    PrintInventory(backPack);
                                    Console.WriteLine("You have no empty slots left in your backpack.");
                                    Console.WriteLine("Which slot would you like to put it in?");
                                    int slotChoice;
                                    bool successChoice = int.TryParse(Console.ReadLine(), out slotChoice);
                                    slotChoice --;
                                    if(successChoice && slotChoice< backPack.Length && slotChoice >= 0)
                                    {
                                        backPack[slotChoice]= boughtItem;
                                        Console.WriteLine($"You have succesfully bought {boughtItem.name}. Press enter to continue");
                                        goldCount -= shopList[3].goldValue;
                                        shopList[3] = emptySlot;
                                        choosingSlot = false;
                                        checkingChoice = false;
                                    }
                                    else if(successChoice == false || slotChoice >= backPack.Length || slotChoice < 0)
                                    {
                                        Console.WriteLine("That is not a valid choice, try again");
                                        Console.ReadLine();
                                    }
                                }

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
                        boughtItem = shopList[4];
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
                            else if(checkingEmptySlotReturn == 100)
                            {
                                choosingSlot = true;

                                while(choosingSlot)
                                {

                                    PrintInventory(backPack);
                                    Console.WriteLine("You have no empty slots left in your backpack.");
                                    Console.WriteLine("Which slot would you like to put it in?");
                                    int slotChoice;
                                    bool successChoice = int.TryParse(Console.ReadLine(), out slotChoice);
                                    slotChoice --;
                                    if(successChoice && slotChoice< backPack.Length && slotChoice >= 0)
                                    {
                                        backPack[slotChoice]= boughtItem;
                                        Console.WriteLine($"You have succesfully bought {boughtItem.name}. Press enter to continue");
                                        goldCount -= shopList[4].goldValue;
                                        shopList[4] = emptySlot;
                                        choosingSlot = false;
                                        checkingChoice = false;
                                    }
                                    else if(successChoice == false || slotChoice >= backPack.Length || slotChoice < 0)
                                    {
                                        Console.WriteLine("That is not a valid choice, try again");
                                        Console.ReadLine();
                                    }
                                }

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
        Console.WriteLine("You stumble upon an old armory room, there may be something useful to use in here....");
        Console.WriteLine("Press enter to continue");
        Console.ReadLine();
    if(treasureRoomPullList.Count > 0)
    {
        int randoDrop = rand.Next(0, treasureRoomPullList.Count);
        Items droppedItem = treasureRoomPullList[randoDrop];
        if(droppedItem == magicShield)
        {
            equippedShield = magicShield;
            Console.WriteLine("You have found a Holy Shield");
            treasureRoomPullList.Remove(magicShield);
        }
        if(droppedItem == magicSword)
        {
            equippedWeapon = droppedItem;
            Console.WriteLine("You have picked up a Magic Sword");
            treasureRoomPullList.Remove(droppedItem);
        }
        if(droppedItem == bootsofSpeed)
        {
            equippedBoots = droppedItem;
            Console.WriteLine("You have found a pair of Boots of Speed");
            treasureRoomPullList.Remove(droppedItem);
        }
        if(droppedItem == bigBackPack)
        {
            equippedBackPack = bigBackPack;
            Console.WriteLine("You have found a larger Back Pack");
            treasureRoomPullList.Remove(droppedItem);
            Items[] BiggerBackPack = new Items[inventoryCount + equippedBackPack.inventorySlotsBonus];
            for(int i = 0; i < backPack.Length; i++)
            {
                BiggerBackPack[i] = backPack[i];
            }
            backPack = BiggerBackPack;
            for(int x = 5; x < backPack.Length; x ++)
            {
                backPack[x] = emptySlot;
            }
        }
    }
    else if(treasureRoomPullList.Count == 0)
    {
        int gainedGold = 10 + (dungeonLevel * 5) + rollDie(dEight);
        Console.WriteLine("You don't manage to find anything better than what you are already using, but you do manage to pick up some gold.");
        Console.WriteLine($"You find {gainedGold} gold coins");
        goldCount += gainedGold;
    }
}
void ItemDropRoom()
{
    Console.Clear();
    Console.WriteLine("You come across a storage looking room and decide to rumage through it for anything useful....");
    int randoDrop = rand.Next(0, itemRoomPullList.Count);
    Items droppedItem = itemRoomPullList[randoDrop];
    Console.WriteLine($"You found a {droppedItem.name}, would you like to pick it up? Press y for yes and n for no");
    PrintInventory(backPack);
    int decisionsMade = 0;
    bool makingDecision = true;
    while(makingDecision)
    {
        string? terminalInput = Console.ReadLine();
        int pickingUpItemInt;
        bool successDecision = int.TryParse(terminalInput, out pickingUpItemInt);
        if(successDecision)
        {
            Console.WriteLine("That is not a valid decision, try again.");
            Console.ReadLine();
            decisionsMade ++;
            if(decisionsMade >= 6)
            {
                Console.WriteLine("As you stand there attempting to decide you hear a low growl from behind you, you quickly turn to find a monster standing there. Press enter to continue");
                Console.ReadLine();
                combat();
                makingDecision = false;
            }
        }
        else if(successDecision == false)
        {
            string? decisionMade = terminalInput;

            if(decisionMade.ToLower() == "y")
            {
                bool choosingSlot = true;
                int slotChoicesMade = 0;
                while(choosingSlot)
                {
                    Console.WriteLine("Which slot would you like to put it in?");
                    int slotChoice;
                    bool successChoice = int.TryParse(Console.ReadLine(), out slotChoice);
                    slotChoice --;
                    if(successChoice && slotChoice< backPack.Length && slotChoice >= 0)
                    {
                        backPack[slotChoice]= droppedItem;
                        Console.WriteLine($"You have succesfully picked up {droppedItem.name}. Press enter to continue");
                        choosingSlot = false;
                        makingDecision = false;
                    }
                    else if(successChoice == false || slotChoice >= backPack.Length || slotChoice < 0)
                    {
                        Console.WriteLine("That is not a valid choice, try again");
                        Console.ReadLine();
                        slotChoicesMade ++;
                        if(slotChoicesMade >= 6)
                        {
                            Console.WriteLine("As you stand there attempting to decide you hear a low growl from behind you, you quickly turn to find a monster standing there. Press enter to continue");
                            Console.ReadLine();
                            combat();
                            makingDecision = false;
                            choosingSlot = false;

                        }
                    }
                }
            }
            else if(decisionMade.ToLower() == "n")
            {
                Console.WriteLine("You decide it isn't worth keeping and move on. Press enter to continue.");
                makingDecision= false;
            }
            else
            {
                Console.WriteLine("That is not a valid choice, try agian.");
                decisionsMade ++;
                if(decisionsMade >= 6)
                {
                    Console.WriteLine("As you stand there attempting to decide you hear a low growl from behind you, you quickly turn to find a monster standing there. Press enter to continue");
                    Console.ReadLine();
                    combat();
                    makingDecision = false;

                }

            }
        }
    }
}
void TrapRoom()
{
    Console.Clear();
    Console.WriteLine("It's a TRAP!!");
}
void RestArea()
{
    Console.Clear();
    Console.WriteLine("You find an area that seems safe enough to rest, what would you like to do before you sleep?");
    Console.WriteLine("1: Rest 2: Change Equipment 3: Use a Health Item");
    bool resting = true;
    while(resting)
    {
        int choiceInt;
        bool successChoice = int.TryParse(Console.ReadLine(), out choiceInt);
        if(successChoice)
        {
            switch(choiceInt)
            {
                case 1:
                    Console.WriteLine("You rest and dream of brighter places....");
                    if(playerHp != playerMaxHP)
                    {
                        if(playerHp <= playerMaxHP-5)
                        {
                            playerHp+= 5;
                        }
                        if(playerHp +4 == playerMaxHP)
                        {
                            playerHp += 4;
                        }
                        if(playerHp +3 == playerMaxHP)
                        {
                            playerHp += 3;
                        }
                        if(playerHp +2 == playerMaxHP)
                        {
                            playerHp += 2;
                        }
                        if(playerHp +1 == playerMaxHP)
                        {
                            playerHp += 1;
                        }
                    }
                    break;
                    case 2:
                        break;
                    case 3:
                        break;
            }
        }
    }
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
//Level Up Method//
static int ExperienceGain(Monsters x, int experience)
{
    return x.experienceWorth + experience;
}
static bool LevelUp(int experience, int level)
{
    if(experience >= 20 * level )
    {
        return true;
    }
    return false;
}

//Inventory Management 
void PrintInventory(Items[] x)
{

    for(int i = 0; i < x.Length; i++)
    {
        Console.WriteLine($"{i+1}: {x[i].name}");
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
        for(int i = 0; i< roomsAlreadyRolled.Count(); i++)
        {
            roomsAlreadyRolled[i] = "Null";
        }
        bossRoom = true;
    }
    bool determineRoom = true;
    while(determineRoom)
    {
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
                    if(roomsAlreadyRolled[0] != "Shop" && roomsAlreadyRolled[1] != "Shop")
                    {
                        shop();
                        if(roomsAlreadyRolled[0] != "Shop")
                        {
                            roomsAlreadyRolled[0] = "Shop";
                            determineRoom = false;
                        }
                        else if(roomsAlreadyRolled[0] == "Shop")
                        {
                            roomsAlreadyRolled[1] = "Shop";
                            determineRoom = false;
                        }
                    }
                }
            else if(randRoom == 2)
                {
                    TresureRoom();
                    determineRoom = false;
                }
            else if(randRoom == 3 || randRoom == 7 || randRoom == 8 || randRoom == 14)
                {
                    ItemDropRoom();
                    determineRoom = false;
                }
            else if(randRoom == 4 || randRoom == 11 || randRoom == 12 || randRoom == 13)
                {
                    combat();
                    determineRoom = false;
                }
            else if(randRoom == 5 || randRoom == 9 || randRoom == 10)
                {
                    TrapRoom();
                    determineRoom = false;
                }
            else if(randRoom == 6)
                {
                    MiniBoss();
                    determineRoom = false;
                    Console.WriteLine(dungeonLevel);
                    Console.WriteLine(numberOfRooms);
                }

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
        Console.ReadLine();
        while(numberOfRooms <= maxRooms && playGame)
        {
            if(dungeonLevel < maxDungeonLevels)
            {
                randRoomGenerator();
                Console.WriteLine("Press Enter to continue");
                Console.ReadLine();
                if(hardCoreMode == false && playerHp >0)
                {
                    if(playerHp +2< playerMaxHP)
                    {
                        Console.WriteLine("You rest a little and heal 2 HP");
                        Console.ReadLine();
                        playerHp += 2;
                    }
                    else if(playerHp +1 < playerMaxHP && playerHp> 0)
                    {
                        Console.WriteLine("You try to sleep in this gloomy place, but struggle to get comfortable. You still manage to heal 1 HP");
                        Console.ReadLine();
                        playerHp += 1;
                    }
                }
            }
            else if(dungeonLevel >= maxDungeonLevels)
            {
                playGame = false;
            }
        }
    }
    Console.WriteLine("You have beaten the final boss, Congratualtions! You have won! Press Enter to leave the game.");
    Console.ReadLine();
}
class Monsters
{
    public string? name;
    public int level;
    public int MonsterIntiative;
    public int MonsterHitPoints;
    public int MonsterAC;
    public int MonsterAttackDamage;
    public int experienceWorth;
}
class Items
{
    public int inventorySlotsBonus;
    public string? name;
    public int level;
    public int damageDie;
    public int attackBonus;
    public int raiseShieldBonus;
    public int damageBouns;
    public int healBonus;
    public int goldValue;
    public int intiativeBonus;
    
}