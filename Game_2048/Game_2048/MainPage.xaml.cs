using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Game_2048
{
    public partial class MainPage : ContentPage
    {
        public Grid grid;
        public double AppWidth = 320;
        GameLogic Game = new GameLogic();
        public Dictionary<int, string> numberToColor = new Dictionary<int, string>();
        
        public MainPage()
        {
            InitializeComponent();

            updateGrid();
            //Swipe swipe = new Swipe(overlay, this);
            Game.NewGame();
            this.numberToColor.Add(0, "bbada0");
            this.numberToColor.Add(2, "eee4da");
            this.numberToColor.Add(4, "ede0c8");
            this.numberToColor.Add(8, "f2b179");
            this.numberToColor.Add(16, "f59563");
            this.numberToColor.Add(32, "f67c5f");
            this.numberToColor.Add(64, "f65e3b");
            this.numberToColor.Add(128, "edcf72");
            this.numberToColor.Add(256, "edcc61");
            this.numberToColor.Add(512, "edc850");
            this.numberToColor.Add(1024, "edc53f");
            this.numberToColor.Add(2048, "edc22e");

        }
        public void OnSwiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    Game.Left();
                    break;
                case SwipeDirection.Right:
                    Game.Right();
                    break;
                case SwipeDirection.Up:
                    Game.Up();
                    break;
                case SwipeDirection.Down:
                    Game.Down();
                    break;
            }
            updateBoard();
            scoreLabel.Text = Game.Score.ToString();
            if (Game.gameOver)
            {
                DisplayAlert("Game Over", "Click New Game to start again", "OK");
            }
            if (Game.isWin)
            {
                DisplayAlert("You won", "If you want to continue click OK or New Game to start again", "OK");
            }
        }
        public void updateGrid()
        {
            mainLayout.Children.Clear();

            var grid = new Grid();
            for (int i = 0; i < Game.BoardSize; i++)
                grid.RowDefinitions.Add(new RowDefinition { Height = AppWidth / Game.BoardSize });

            for (int i = 0; i < Game.BoardSize; i++)
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            mainLayout.Children.Add(grid);
            this.grid = grid;
        }
        void updateBoard()
        {
            this.grid.Children.Clear();
            for (int i = 0; i < Game.BoardSize; i++)
                for (int j = 0; j < Game.BoardSize; j++)
                {
                    var btn = new Button
                    {
                        Text = Game.Board[i][j] == 0 ? "" : Game.Board[i][j].ToString(),
                        FontSize = getTileFontSize(),
                        BackgroundColor = Color.FromHex(numberToColor.ContainsKey(Game.Board[i][j]) ? numberToColor[Game.Board[i][j]] : numberToColor[2048])
                    };
                    btn.SetValue(Grid.RowProperty, i);
                    btn.SetValue(Grid.ColumnProperty, j);
                    this.grid.Children.Add(btn);
                }
        }
        public int getTileFontSize()
        {
            switch (Game.BoardSize)
            {
                case 5:
                    return 25;
                case 6:
                    return 18;
                case 7:
                    return 20;
                case 8:
                    return 15;
                default:
                    return 25;
            }
        }
        private void new_game_Clicked(object sender, EventArgs e)
        {
            
            Game.NewGame();
            updateBoard();
            
        }
    }
}
