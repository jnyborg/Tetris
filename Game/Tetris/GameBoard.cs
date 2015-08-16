using System;
using Microsoft.Xna.Framework;

namespace Tetris
{
    public class GameBoard
    {
        private static GameBoard _instance;
        public static bool GameOver { get; set; }

        private GameBoard()
        {
            Board = new Square[10, 20];
            for (int i1 = 0; i1 < 10; i1++)
            {
                for (int i2 = 0; i2 < 20; i2++)
                {
                    Board[i1, i2] = null;
                }
            }
            GameOver = false;
        }

        public Square[,] Board { get; set; }
        public int Score { get; set; }

        public static GameBoard Instance
        {
            get { return _instance ?? (_instance = new GameBoard()); }
        }

        public void NewGame()
        {
            _instance = new GameBoard();
        }

        public bool CanMoveX(float newX, float currentY)
        {
            int indexValueX = Convert.ToInt32(newX/32);
            int indexValueY = Convert.ToInt32(currentY/32);
            // check if within the game boards borders
            if (indexValueX >= 0 && indexValueX < 10)
            {
                // true if unoccupied, false if occupied 
                return Board[indexValueX, indexValueY] == null;
            }
            return false;
        }

        public bool CanMoveY(float newY, float currentX)
        {
            int indexValueY = Convert.ToInt32(newY/32);
            int indexValueX = Convert.ToInt32(currentX/32);
            // check if within the game boards borders
            if (indexValueY >= 0 && indexValueY < 20)
            {
                // true if unoccupied, false if occupied 
                return Board[indexValueX, indexValueY] == null;
            }
            return false;
        }

        // Used when rotating
        public bool CheckSquare(Square square)
        {
            int indexValueX = Convert.ToInt32(square.Position.X/32);
            int indexValueY = Convert.ToInt32(square.Position.Y/32);
            if (indexValueX >= 0 && indexValueX < 10 && indexValueY >= 0 && indexValueY < 20)
            {
                // true if unoccupied, false if occupied 
                return Board[indexValueX, indexValueY] == null;
            }
            return false;
        }

        // put ur mom in a block, bitch.
        public void PutBlock(Block block)
        {
            foreach (Square s in block.Squares)
            {
                int indexValueX = Convert.ToInt32(s.Position.X/32);
                int indexValueY = Convert.ToInt32(s.Position.Y/32);
                Board[indexValueX, indexValueY] = s;
            }
            CheckFullLines();
            CheckGameOver();
        }

        private void CheckGameOver()
        {
            for (int i = 0; i < 10; i++)
            {
                if (Board[i,0] != null)
                {
                    GameOver = true;
                }
            }
        }

        public void CheckFullLines()
        {
            int linesCleared = 0;
            for (int y = 19; y >= 0; y--)
            {
                int squareNum = 0;
                for (int x = 0; x < 10; x++)
                {
                    if (Board[x, y] != null) squareNum++;
                }
                if (squareNum == 10)
                {
                    ClearLine(y);
                    y++;
                    linesCleared++;
                }
            }
            UpdateScore(linesCleared);
        }

        private void UpdateScore(int linesCleared)
        {
            switch (linesCleared)
            {
                case 1:
                    Score += 40;
                    break;
                case 2:
                    Score += 100;
                    break;
                case 3:
                    Score += 300;
                    break;
                case 4:
                    Score += 1200;
                    break;
            }
        }

        private void ClearLine(int lineToClear)
        {
            for (int y = lineToClear - 1; y >= 0; y--)
            {
                for (int x = 0; x < 10; x++)
                {
                    Square s = Board[x, y];
                    if (s != null)
                        s.Position = new Vector2(s.Position.X, s.Position.Y + 32);
                    Board[x, y + 1] = s;
                    Board[x, y] = null;
                }
            }
        }
    }
}