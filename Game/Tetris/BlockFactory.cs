using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public enum Shape
    {
        Square,
        Line,
        L,
        J,
        S,
        Z,
        T
    }

    public class BlockFactory
    {

        public BlockFactory(ContentManager gameContent)
        {
            GameContent = gameContent;
        }

        public ContentManager GameContent { get; set; }

        public Block GenerateBlock()
        {
            Block block = null;
            Shape randomShape = ChooseShape();
            var squares = new Square[4];
            Texture2D blockTexture;
            switch (randomShape)
            {
                case Shape.Square:
                    blockTexture = GameContent.Load<Texture2D>("square");
                    squares[0] = new Square(new Vector2(160.0f, 0.0f), blockTexture);
                    squares[1] = new Square(new Vector2(160.0f, 32.0f), blockTexture);
                    squares[2] = new Square(new Vector2(192.0f, 0.0f), blockTexture);
                    squares[3] = new Square(new Vector2(192.0f, 32.0f), blockTexture);
                    block = new Block(squares, Shape.Square, blockTexture);
                    break;
                case Shape.Line:
                    blockTexture = GameContent.Load<Texture2D>("line");
                    squares[0] = new Square(new Vector2(96.0f, 0.0f), blockTexture);
                    squares[1] = new Square(new Vector2(128.0f, 0.0f), blockTexture);
                    squares[2] = new Square(new Vector2(160.0f, 0.0f), blockTexture);
                    squares[3] = new Square(new Vector2(192.0f, 0.0f), blockTexture);
                    block = new Block(squares, Shape.Line, blockTexture);
                    break;
                case Shape.L:
                    blockTexture = GameContent.Load<Texture2D>("lblock");
                    squares[0] = new Square(new Vector2(160.0f, 32.0f), blockTexture);
                    squares[1] = new Square(new Vector2(160.0f, 0.0f), blockTexture);
                    squares[2] = new Square(new Vector2(192.0f, 0.0f), blockTexture);
                    squares[3] = new Square(new Vector2(224.0f, 0.0f), blockTexture);
                    block = new Block(squares, Shape.L, blockTexture);
                    break;
                case Shape.J:
                    blockTexture = GameContent.Load<Texture2D>("jblock");
                    squares[0] = new Square(new Vector2(160.0f, 0.0f), blockTexture);
                    squares[1] = new Square(new Vector2(192.0f, 0.0f), blockTexture);
                    squares[2] = new Square(new Vector2(224.0f, 0.0f), blockTexture);
                    squares[3] = new Square(new Vector2(224.0f, 32.0f), blockTexture);
                    block = new Block(squares, Shape.J, blockTexture);
                    break;
                case Shape.S:
                    blockTexture = GameContent.Load<Texture2D>("sblock");
                    squares[0] = new Square(new Vector2(160.0f, 32.0f), blockTexture);
                    squares[1] = new Square(new Vector2(192.0f, 32.0f), blockTexture);
                    squares[2] = new Square(new Vector2(192.0f, 0.0f), blockTexture);
                    squares[3] = new Square(new Vector2(224.0f, 0.0f), blockTexture);
                    block = new Block(squares, Shape.S, blockTexture);
                    break;
                case Shape.Z:
                    blockTexture = GameContent.Load<Texture2D>("zblock");
                    squares[0] = new Square(new Vector2(160.0f, 0.0f), blockTexture);
                    squares[1] = new Square(new Vector2(192.0f, 0.0f), blockTexture);
                    squares[2] = new Square(new Vector2(192.0f, 32.0f), blockTexture);
                    squares[3] = new Square(new Vector2(224.0f, 32.0f), blockTexture);
                    block = new Block(squares, Shape.Z, blockTexture);
                    break;
                case Shape.T:
                    blockTexture = GameContent.Load<Texture2D>("tblock");
                    squares[0] = new Square(new Vector2(128.0f, 0.0f), blockTexture);
                    squares[1] = new Square(new Vector2(160.0f, 0.0f), blockTexture);
                    squares[2] = new Square(new Vector2(192.0f, 0.0f), blockTexture);
                    squares[3] = new Square(new Vector2(160.0f, 32.0f), blockTexture);
                    block = new Block(squares, Shape.T, blockTexture);
                    break;
            }
            return block;
        }

        /// <summary>
        ///     Randomly chooses a shape to drop next. Unless you need a line piece, then it will first ask the almighty Tetris God
        ///     if you deserve it.
        /// </summary>
        /// <returns></returns>
        private Shape ChooseShape()
        {
            Array shapes = Enum.GetValues(typeof (Shape));
            var random = new Random();
            return (Shape) shapes.GetValue(random.Next(shapes.Length));
        }
    }

    // A block always consists of 4 squares. This class keeps them together
    public class Block
    {
        // The four squares this block consists of
        public Block(Square[] squares, Shape shape, Texture2D texture)
        {
            Squares = squares;
            BlockShape = shape;
            BlockTexture = texture;
            NextDirection = Directions.East;
            PreviousDirection = Directions.North;
        }

        public Square[] Squares { get; private set; }
        public Shape BlockShape { get; set; }
        public Texture2D BlockTexture { get; set; }
        // the next diretion to rotate to
        public Direction NextDirection { get; set; }
        public Direction PreviousDirection { get; set; }

        /// <summary>
        ///     returns true if the drop was without collision, returns false if the block collided, and was put on the gameboard
        /// </summary>
        /// <returns>bool</returns>
        public bool MoveDown()
        {
            int checksum = Squares.Count(s => s.CanMoveDown());
            if (checksum != Squares.Length)
            {
                GameBoard.Instance.PutBlock(this);
                
                return false;
            }
            foreach (Square s in Squares)
            {
                s.MoveDown();
            }
            return true;
        }

        public void MoveLeft()
        {
            int checksum = Squares.Count(s => s.CanMoveLeft());
            if (checksum != Squares.Length) return;
            foreach (Square s in Squares)
            {
                s.MoveLeft();
            }
        }

        public void MoveRight()
        {
            int checksum = Squares.Count(s => s.CanMoveRight());
            if (checksum != Squares.Length) return;
            foreach (Square s in Squares)
            {
                s.MoveRight();
            }
        }


        public void Rotate()
        {
            if (BlockShape == Shape.Square) return;
            PreviousDirection = NextDirection;
            Square[] newSquares = NextDirection.Rotate(this);
            if (CheckRotation(newSquares))
                Squares = newSquares;
            else
            {
                // stay as before
                NextDirection = PreviousDirection;
            }
        }

        private bool CheckRotation(Square[] newSquares)
        {
            return newSquares[0].CheckSquare() && newSquares[1].CheckSquare() && newSquares[2].CheckSquare() &&
                   newSquares[3].CheckSquare();
        }
    }

    public class Square
    {
        private Vector2 _position;

        public Square(Vector2 position, Texture2D texture)
        {
            Position = position;
            Texture = texture;
        }

        public Texture2D Texture { get; set; }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public void MoveDown()
        {
            _position.Y += 32.0f;
        }

        public void MoveLeft()
        {
            _position.X -= 32.0f;
        }

        public void MoveRight()
        {
            _position.X += 32.0f;
        }

        public bool CanMoveDown()
        {
            return GameBoard.Instance.CanMoveY(_position.Y + 32.0f, Position.X);
        }

        public bool CanMoveLeft()
        {
            return GameBoard.Instance.CanMoveX(_position.X - 32.0f, _position.Y);
        }

        public bool CanMoveRight()
        {
            return GameBoard.Instance.CanMoveX(_position.X + 32.0f, _position.Y);
        }

        public bool CheckSquare()
        {
            return GameBoard.Instance.CheckSquare(this);
        }
    }
}