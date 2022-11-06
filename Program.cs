//Jonathan Hoyt 11/4/2022 Final Project. ASCII Art taken from https://www.asciiart.eu

Console.Clear();

//Variables//
Random rand = new Random();
int dTwenty = 21;
int dEight = 9;
int dSix = 7;
int dFour = 5;

int rollDie(int dice)
{
    return rand.Next(1, dice);
}

//Dungeon Creator
int dungeonLevel = 0;
int roomNumber = 0;
int numberOfRooms = 0;
int maxRooms = 10;
int maxDungeonLevels = 4;



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
printCastle();
Console.WriteLine("The Castle of Terath");
Console.WriteLine("Press Enter to Start");
Console.ReadLine();
combat();
}



//Rooms
int randRoom = rand.Next(1, 8);
roomNumber = randRoom;

//Items
Items shield = new Items();
shield.name = "Shield";
shield.level = 1;


Items sword = new Items();
sword.name = "Sword";
sword.level = 1;
sword.attackBonus = 1 + sword.level * 2;


//Merchant
void shop()
{
    string [] merchantRows = File.ReadAllLines("merchant.txt");
    char[][] merchantChar = merchantRows.Select(items => items.ToArray()).ToArray();
    
}


//Monster
int monstInti = monsterType(dungeonLevel, roomNumber) * 5;
int monsterLevel = monsterType(dungeonLevel, roomNumber);
int monsterIntiative = monstInti + monsterLevel;
int monsterHitPoints = monsterType(dungeonLevel, roomNumber) * 10;
int monsterAC = 10 + monsterType(dungeonLevel, roomNumber) * 2;

//Player
var equippedWeapon = sword;
var equippedShield = shield;
int goldCount = 0;
int hitPoints = 10;
int Playerlevel = 1;
int experience = 0;
int inventoryCount = 5;
string [] backPack = new string[inventoryCount];
int playerHp = hitPoints + Playerlevel * 2;
int playerIntiative = 10 + Playerlevel;
int playerAttackBonus = Playerlevel + equippedWeapon.level;
void changeEquipment()
{

}


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
    while(fight)
    {


        while(playerTurn)
        {
            bool makeAChoice = true;
            while(makeAChoice)
            {
            printKnight();
            
            
                Console.WriteLine($"Hit Points: {playerHp}");
                Console.WriteLine("(1) Attack! (2) Use Item (3) Raise Shield (4) Run Away!!");
                int choice;
                bool success = int.TryParse(Console.ReadLine(), out choice);
                if(success != true || choice > 4 || choice <= 0)
                {
                    Console.WriteLine("That is not a valid choice, press enter and try again.");
                    Console.ReadLine();
                    continue;
                }
                switch(choice)
                {
                    case 1:
                        if(determineHit(monsterAC,equippedWeapon.attackBonus ))
                        makeAChoice = false;
                        break;
                    case 2:
                        makeAChoice = false;
                        break;
                    case 3: 
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
            Console.ReadLine();
            monsterTurn = false;
            playerTurn = true;
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
class Items
{
    public string name;
    public int level;
    public int damageDie;
    public int attackBonus;
    public int damageBouns;
    
}