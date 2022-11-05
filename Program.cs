//Jonathan Hoyt 11/4/2022 Final Project. ASCII Art taken from https://www.asciiart.eu

Console.Clear();

//Variables//
Random rand = new Random();
int dTwenty = rand.Next(1, 20);
int dEight = rand.Next(1, 8);
int dSix = rand.Next(1, 6);
int dFour = rand.Next(1, 4);
int hitPoints = 10;


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
//Monster
int monstInti = monsterType();
int monsterLevel = monsterType();
int monsterIntiative = monstInti + monsterLevel;

//Player
int level = 1;
int experience = 0;
int inventoryCount = 5;
string [] inventory = new string[inventoryCount];
int playerHp = hitPoints + level;
int playerIntiative = 10 + level;


//Combat//
string monsterPic = "monster.txt";
if(monsterType() == 1)
{
    monsterPic = "monster.txt";
}

string [] monsterRows = File.ReadAllLines($"{monsterPic}");
char [][] monsterChar = monsterRows.Select(item => item.ToArray()).ToArray();

string [] knightRows = File.ReadAllLines("knight.txt");
char[][] knightChar = knightRows.Select(items => items.ToArray()).ToArray();
combat();
void combat()
{
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
            printKnight();
            Console.ReadLine();
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

static int monsterType()
{
    return 1;
}
