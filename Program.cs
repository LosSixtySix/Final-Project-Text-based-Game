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


//Player
int level = 1;
int experience = 0;
int inventoryCount = 5;
string [] inventory = new string[inventoryCount];
int playerHp = hitPoints + level;


//Combat//

string [] monsterRows = File.ReadAllLines("monster.txt");
char [][] monsterChar = monsterRows.Select(item => item.ToArray()).ToArray();

string [] knightRows = File.ReadAllLines("knight.txt");
char[][] knightChar = knightRows.Select(items => items.ToArray()).ToArray();
combat();
void combat()
{
    void printKnight()
    {
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
    Console.Clear();
    printKnight();
    Console.ReadLine();


    void printMonster()
    {
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
    Console.Clear();
    printMonster();
    Console.ReadLine();

}
