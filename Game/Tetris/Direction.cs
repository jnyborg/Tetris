using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public interface Direction
    {
        Square[] Rotate(Block block);
    }

    public class Directions
    {
        public static North North = new North();
        public static South South = new South();
        public static East East = new East();
        public static West West = new West();
    }

    public class North : Direction
    {
        public Square[] Rotate(Block block)
        {
            Square oldSquare2 = block.Squares[1];
            Square oldSquare3 = block.Squares[2];
            Square[] squares = new Square[4];
            Texture2D blockTexture = block.BlockTexture;
            switch (block.BlockShape)
            {
                case Shape.Square:
                    break;
                case Shape.Line:
                    squares[0] = new Square(new Vector2(oldSquare3.Position.X - 64, oldSquare3.Position.Y), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare3.Position.X - 32, oldSquare3.Position.Y), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare3.Position.X, oldSquare3.Position.Y), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare3.Position.X + 32, oldSquare3.Position.Y), blockTexture);
                    block.NextDirection = Directions.East;
                    break;
                case Shape.J:
                    squares[0] = new Square(new Vector2(oldSquare2.Position.X - 32, oldSquare2.Position.Y), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare2.Position.X + 32, oldSquare2.Position.Y), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare2.Position.X + 32, oldSquare2.Position.Y + 32), blockTexture);
                    block.NextDirection = Directions.East;
                    break;
                case Shape.L:
                    squares[0] = new Square(new Vector2(oldSquare3.Position.X - 32, oldSquare3.Position.Y + 32), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare3.Position.X - 32, oldSquare3.Position.Y), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare3.Position.X, oldSquare3.Position.Y), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare3.Position.X + 32, oldSquare3.Position.Y), blockTexture);
                    block.NextDirection = Directions.East;
                    break;
                case Shape.S:
                    squares[0] = new Square(new Vector2(oldSquare3.Position.X - 64, oldSquare3.Position.Y + 32), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare3.Position.X - 32, oldSquare3.Position.Y + 32), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare3.Position.X - 32, oldSquare3.Position.Y), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare3.Position.X, oldSquare3.Position.Y), blockTexture);
                    block.NextDirection = Directions.East;
                    break;
                case Shape.T:
                    squares[0] = new Square(new Vector2(oldSquare2.Position.X - 32, oldSquare2.Position.Y), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare2.Position.X + 32, oldSquare2.Position.Y), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y + 32), blockTexture);
                    block.NextDirection = Directions.East;
                    break;
                case Shape.Z:
                    squares[0] = new Square(new Vector2(oldSquare2.Position.X - 64, oldSquare2.Position.Y), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare2.Position.X - 32, oldSquare2.Position.Y), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare2.Position.X - 32, oldSquare2.Position.Y + 32), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y + 32), blockTexture);
                    block.NextDirection = Directions.East;
                    break;

            }
            return squares;
        }


    }

    public class East : Direction
    {

        public Square[] Rotate(Block block)
        {
            Square oldSquare2 = block.Squares[1];
            Square oldSquare3 = block.Squares[2];
            Square[] squares = new Square[4];
            Texture2D blockTexture = block.BlockTexture;
            switch (block.BlockShape)
            {
                case Shape.Line:
                    squares[0] = new Square(new Vector2(oldSquare3.Position.X, oldSquare3.Position.Y - 64), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare3.Position.X, oldSquare3.Position.Y - 32), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare3.Position.X, oldSquare3.Position.Y), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare3.Position.X, oldSquare3.Position.Y + 32), blockTexture);
                    block.NextDirection = Directions.North;
                    break;
                case Shape.J:
                    squares[0] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y - 32), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y + 32), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare2.Position.X - 32, oldSquare2.Position.Y + 32),
                        blockTexture);
                    block.NextDirection = Directions.South;
                    break;
                case Shape.L:
                    squares[0] = new Square(new Vector2(oldSquare3.Position.X - 32, oldSquare3.Position.Y - 32), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare3.Position.X, oldSquare3.Position.Y - 32), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare3.Position.X, oldSquare3.Position.Y), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare3.Position.X, oldSquare3.Position.Y + 32), blockTexture);
                    block.NextDirection = Directions.South;
                    break;
                case Shape.S:
                    squares[0] = new Square(new Vector2(oldSquare3.Position.X, oldSquare3.Position.Y - 32), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare3.Position.X, oldSquare3.Position.Y), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare3.Position.X + 32, oldSquare3.Position.Y), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare3.Position.X + 32, oldSquare3.Position.Y + 32), blockTexture);
                    block.NextDirection = Directions.North;
                    break;
                case Shape.T:
                    squares[0] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y - 32), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y + 32), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare2.Position.X - 32, oldSquare2.Position.Y), blockTexture);
                    block.NextDirection = Directions.South;
                    break;
                case Shape.Z:
                     squares[0] = new Square(new Vector2(oldSquare2.Position.X + 32, oldSquare2.Position.Y - 32), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare2.Position.X + 32, oldSquare2.Position.Y), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y + 32), blockTexture);
                    block.NextDirection = Directions.North;
                    break;
            }

            return squares;
        }
    }

    public class South : Direction
    {
        public Square[] Rotate(Block block)
        {
            Square oldSquare2 = block.Squares[1];
            Square oldSquare3 = block.Squares[2];
            Square[] squares = new Square[4];
            Texture2D blockTexture = block.BlockTexture;
            switch (block.BlockShape)
            {
                case Shape.Square:
                    break;
                case Shape.J:
                    squares[0] = new Square(new Vector2(oldSquare2.Position.X + 32, oldSquare2.Position.Y), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare2.Position.X - 32, oldSquare2.Position.Y), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare2.Position.X - 32, oldSquare2.Position.Y - 32), blockTexture);
                    block.NextDirection = Directions.West;
                    break;
                case Shape.L:
                    squares[0] = new Square(new Vector2(oldSquare3.Position.X + 32, oldSquare3.Position.Y - 32), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare3.Position.X + 32, oldSquare3.Position.Y), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare3.Position.X, oldSquare3.Position.Y), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare3.Position.X - 32, oldSquare3.Position.Y), blockTexture);
                    block.NextDirection = Directions.West;
                    break;
                case Shape.T:
                    squares[0] = new Square(new Vector2(oldSquare2.Position.X + 32, oldSquare2.Position.Y), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare2.Position.X - 32, oldSquare2.Position.Y), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y - 32), blockTexture);
                    block.NextDirection = Directions.West;
                    break;

            }
            return squares;
        }

    }


    public class West : Direction
    {

        public Square[] Rotate(Block block)
        {
            Square oldSquare2 = block.Squares[1];
            Square oldSquare3 = block.Squares[2];
            Square[] squares = new Square[4];
            Texture2D blockTexture = block.BlockTexture;
            switch (block.BlockShape)
            {
                case Shape.J:
                    squares[0] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y + 32), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y - 32), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare2.Position.X + 32, oldSquare2.Position.Y - 32), blockTexture);
                    block.NextDirection = Directions.North;
                    break;
                case Shape.L:
                    squares[0] = new Square(new Vector2(oldSquare3.Position.X + 32, oldSquare3.Position.Y + 32), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare3.Position.X, oldSquare3.Position.Y + 32), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare3.Position.X, oldSquare3.Position.Y), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare3.Position.X, oldSquare3.Position.Y - 32), blockTexture);
                    block.NextDirection = Directions.North;
                    break;
                case Shape.T:
                    squares[0] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y + 32), blockTexture);
                    squares[1] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y), blockTexture);
                    squares[2] = new Square(new Vector2(oldSquare2.Position.X, oldSquare2.Position.Y - 32), blockTexture);
                    squares[3] = new Square(new Vector2(oldSquare2.Position.X + 32, oldSquare2.Position.Y), blockTexture);
                    block.NextDirection = Directions.North;
                    break;
            }

            return squares;
        }
    }
}
