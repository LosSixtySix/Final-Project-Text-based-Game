//Jonathan Hoyt 11/4/2022 Final Project. ASCII Art taken from https://www.asciiart.eu

Console.Clear();

//Variables//
Random rand = new Random();
int dTwenty = rand.Next(1, 20);
int dEight = rand.Next(1, 8);
int dSix = rand.Next(1, 6);
int dFour = rand.Next(1, 4);


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
printCastle();

Console.WriteLine("The Castle of Terath");
Console.WriteLine("Press Enter to Continue");
Console.ReadLine();

//Rooms
int randRoom = rand.Next(1, 8);
roomNumber = randRoom;

//Items


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

//Player
int goldCount = 0;
int hitPoints = 10;
int level = 1;
int experience = 0;
int inventoryCount = 5;
string [] backPack = new string[inventoryCount];
int playerHp = hitPoints + level * 2;
int playerIntiative = 10 + level;



//Combat//


string [] knightRows = File.ReadAllLines("knight.txt");
char[][] knightChar = knightRows.Select(items => items.ToArray()).ToArray();
combat();
void combat()
{
    // Determine monster type
    string monsterPicNumber = monsterPictureType(monsterType(dungeonLevel, roomNumber)).ToString();
    string monsterPic = $"monster{monsterPicNumber}.txt";
    string [] monsterRows = File.ReadAllLines($"{monsterPic}");
    char [][] monsterChar = monsterRows.Select(item => item.ToArray()).ToArray();



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
