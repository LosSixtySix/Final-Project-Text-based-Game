//Jonathan Hoyt 11/4/2022 Final Project. ASCII Art taken from https://www.asciiart.eu

Console.Clear();

//Variables//
Random rand = new Random();
int dTwenty = 21;
int dEight = 9;
int dSix = 7;
int dFour = 5;
bool playGame = true;

int rollDie(int dice)
{
    return rand.Next(1, dice);
}

//Dungeon Creator Variables//
int dungeonLevel = 0;
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

void main()
{
while(playGame)
{
    printCastle();
    Console.WriteLine("The Castle of Terath");
    Console.WriteLine("Press Enter to Start");
    Console.ReadLine();
    RoomZero();
    while(numberOfRooms <= maxRooms && playGame)
    {
        randRoomGenerator();
        Console.WriteLine("Press Enter to continue");
        Console.ReadLine();
    }
}

}


//Items
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

Items emptySlot = new Items();
emptySlot.name = "[Empty Slot]";

Items healthPotion = new Items();
healthPotion.name = "Health Potion";



//Monster
int monstInti = monsterType(dungeonLevel, roomNumber) * 5;
int monsterLevel = monsterType(dungeonLevel, roomNumber);
int monsterIntiative = monstInti + monsterLevel;
int monsterHitPoints = monsterType(dungeonLevel, roomNumber) * 10;
int monsterAC = 10 + monsterType(dungeonLevel, roomNumber) * 2;
int monsterAttackDamage = 5;

//Player
var equippedWeapon = sword;
var equippedShield = shield;
int goldCount = 0;
int hitPoints = 10;
int Playerlevel = 1;
int experience = 0;
int inventoryCount = 5;
Items [] backPack = new Items[inventoryCount];
int playerHp = hitPoints + Playerlevel * 2;
int playerIntiative = 10 + Playerlevel;
int playerAttackBonus = Playerlevel + equippedWeapon.level;
int playerAC = 10 + Playerlevel + equippedShield.attackBonus;
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
    string monsterPicNumber = monsterPictureType(monsterType(dungeonLevel, roomNumber)).ToString();
    string monsterPic = $"monster{monsterPicNumber}.txt";
    string [] monsterRows = File.ReadAllLines($"{monsterPic}");
    char [][] monsterChar = monsterRows.Select(item => item.ToArray()).ToArray();

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
    int MonsterDamage()
    {
        return monsterLevel + monsterAttackDamage;
    }

    //Determine who goes first//
    bool playerTurn = false;
    bool monsterTurn = false;
    if(playerIntiative > monsterIntiative)
    {
        playerTurn = true;
    }
    else
    {
        monsterTurn = true;
    }
    bool fight = true;
    int temp_playerAC = playerAC;
    int temp_MonsterAC = monsterAC;
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
                if(successChoice != true || choice > 4 || choice <= 0)
                {
                    Console.WriteLine("That is not a valid choice, press enter and try again.");
                    Console.ReadLine();
                    continue;
                }
                switch(choice)
                {
                    case 1:
                        if(determineHit(temp_MonsterAC,equippedWeapon.attackBonus ))
                        {
                            Console.WriteLine("You Hit!!");
                            monsterHitPoints -= damageDealt;
                            temp_MonsterAC -= damageDealt;
                            Console.ReadLine();
                        }
                        else
                        {
                            temp_MonsterAC -= damageDealt;
                            Console.WriteLine("You missed!");
                            Console.ReadLine();
                        }
                        makeAChoice = false;
                        break;
                    case 2:
                        Console.Clear();
                        PrintInventory();
                        Console.ReadLine();
                        makeAChoice = false;
                        break;
                    case 3: 
                        temp_playerAC += shield.raiseShieldBonus *2;
                        Console.WriteLine("Your shield is raised, making it easier to deflect attacks");
                        Console.ReadLine();
                        makeAChoice = false;
                        break;
                    case 4:
                        makeAChoice = false;
                        break;

                }
            }
            playerTurn = false;
            monsterTurn = true;
            


        }
        while(monsterTurn)
        {
            printMonster();
            Console.WriteLine($"HP: {monsterHitPoints}");
            bool hit = determineHit(temp_playerAC, monsterLevel*2);
            int monsterDamageDealt = MonsterDamage();
            if(monsterHitPoints <= 0)
            {
                fight = false;
                Console.WriteLine("You have defeated the foe!!");
            }
            else if(hit)
            {
                Console.WriteLine("The monster hits you");
                playerHp -= monsterDamageDealt;
                temp_playerAC -= monsterDamageDealt;
            }
            else if(hit == false)
            {
                Console.WriteLine("The monster misses");
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
        int temp_x = 0;
        while(temp_x < 23)
        {
            string temp_row ="";
            for(int i = 0; i < monsterRows[12].Length; i++)
            {
                temp_row += monsterChar[temp_x][i];
            }
            Console.WriteLine(temp_row);
            temp_x++;
        }

    }

}
static int monsterType(int dungeonLevel, int roomNumber)
{
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
void shop()
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
    
}
void TresureRoom()
{
    Console.Clear();
    Console.WriteLine("This is the treasuer room");
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
//Inventory Printer
void PrintInventory()
{

    for(int i = 0; i < backPack.Length; i++)
    {
        Console.WriteLine($"{backPack[i].name}");
    }
}

//Dungeon Creator
void randRoomGenerator()
{
    numberOfRooms++;
    if(numberOfRooms == maxRooms)
    {
        numberOfRooms = 0;
        dungeonLevel ++;
        bossRoom = true;
    }
    int randRoom = rand.Next(1,7);
    if(randRoom == 1)
    {
        shop();
    }
    else if(randRoom == 2)
    {
        TresureRoom();
    }
    else if(randRoom == 3)
    {
        ItemDropRoom();
    }
    else if(randRoom == 4)
    {
        combat();
    }
    else if(randRoom == 5)
    {
        TrapRoom();
    }
    else if(randRoom == 6)
    {
        MiniBoss();
    }
}
void RoomZero()
{

}
class Items
{
    public string name;
    public int level;
    public int damageDie;
    public int attackBonus;
    public int raiseShieldBonus;
    public int damageBouns;
    
}