namespace ConsoleApp1
{
    internal class Program
    {
        class Board
        {
            char[,] board;
            bool Xturn;
            public Board()
            {
                Xturn = true; //передача хода крестикам
                board = new char[,] //создание матрицы доски
                    {
                {'.','.','.' },
                {'.','.','.' },
                {'.','.','.' }
                    };
            }
            public char[,] Mape() => board; //получение доски
            public void Move(byte x, byte y) //ход
            {
                if (x > 2 || y > 2 || board[x, y] != '.') //проверка на возможность хода
                {
                    Console.WriteLine("Такой ход невозможен");
                    return;
                }
                board[x, y] = Xturn ? 'X' : 'O'; //изменение ячейки в зависимости от хода
                Xturn = !Xturn; //смена хода
            }
            public bool IsWin(out char winner) //проверка на победу
            {
                string str;
                winner = Xturn == true ? 'O' : 'X'; //установка победителем игрока прошлого хода
                //объединение диагоналей в строку с проверкой победы
                str = String.Join("", board[0, 0], board[1, 1], board[2, 2]);
                if (str.Equals("XXX") || str.Equals("OOO")) return false;
                str = String.Join("", board[2, 0], board[1, 1], board[0, 2]);
                if (str.Equals("XXX") || str.Equals("OOO")) return false;
                //объединение строк и столбцов в строку с проверкой победы
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    str = String.Join("", board[i, 0], board[i, 1], board[i, 2]);
                    if (str.Equals("XXX") || str.Equals("OOO")) return false;
                    str = String.Join("", board[0, i], board[1, i], board[2, i]);
                    if (str.Equals("XXX") || str.Equals("OOO")) return false;
                }
                return true;
            }
        }
        static void Main()
            {
                Board board = new Board();
                char winner;
                byte y = 4; //костыли
                byte x = 4;

                MapDraw(board.Mape()); //отрисовка поля в начале игры
                while (board.IsWin(out winner))
                {
                    Console.Write("Столбец:");
                    try { y = Convert.ToByte(Console.ReadLine()); } catch { } //игнорирование ввода если он не конвертируется
                    //byte.TryParse(Console.ReadLine(), out byte y);
                    Console.Write("Строка:");
                    try { x = Convert.ToByte(Console.ReadLine()); } catch { }
                    //byte.TryParse(Console.ReadLine(), out byte x);
                    board.Move((byte)(x - 1), (byte)(y - 1));
                    MapDraw(board.Mape()); //отрисовка поля после хода
            }
                Console.WriteLine($"\nПобедитель - {winner}");
            }
        static void MapDraw(char[,] board)
            {
                Console.WriteLine("  1 2 3");
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    Console.Write(i+1+" ");
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        Console.Write($"{board[i, j]} ");
                    }
                    Console.WriteLine();
                }
            }
    }
}
