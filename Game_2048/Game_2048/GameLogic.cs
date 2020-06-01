using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Game_2048
{
    class GameLogic
    {
        public int BoardSize;
        public int[][] Board;
        public bool gameOver;
        public int BestScore;
        private bool canAdd;
        public bool isWin;
        public int Score;
        Random rand = new Random();

        public GameLogic()
        {
            BoardSize = 4;
            Score = 0;
            Board = new int[BoardSize][];
            for (int i = 0; i < BoardSize; i++)
            {
                Board[i] = new int[BoardSize];
            }
        }
        
        public void Right() //Right
        {
            SumRight();
            AddNumber();
            UpdateScore();
            CheckGameOver();
            CheckWin();
        }

        public void Up() //Up
        {
            SumUp();
            AddNumber();
            UpdateScore();
            CheckGameOver();
            CheckWin();
        }

        public void NewGame()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    Board[i][j] = 0;
                }
            }
            canAdd = true;
            AddNumber();
            canAdd = true;
            AddNumber();
            Score = 0;
            gameOver = false;
            isWin = false;
        }

        public void Down() //Down
        {
            SumDown();
            AddNumber();
            UpdateScore();
            CheckGameOver();
            CheckWin();
        }

        public void Left() //Left
        {
            SumLeft();
            AddNumber();
            UpdateScore();
            CheckGameOver();
            CheckWin();
        }
        public void SumRight()
        {
            canAdd = false;
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = BoardSize-1; j >= 0; j--)
                {
                    for (int k = j - 1; k >= 0; k--)
                    {
                        if (Board[i][k] == 0)
                        {
                            continue;
                        }
                        else if (Board[i][k] == Board[i][j])
                        {
                            Board[i][j] *= 2;
                            Score += Board[i][j];
                            Board[i][k] = 0;
                            canAdd = true;
                            break;
                        }
                        else
                        {
                            if (Board[i][j] == 0 && Board[i][k] != 0)
                            {
                                Board[i][j] = Board[i][k];
                                Board[i][k] = 0;
                                j++;
                                canAdd = true;
                                break;
                            }
                            else if (Board[i][j] != 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
        public void SumLeft()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    for (int k = j + 1; k < BoardSize; k++)
                    {
                        if (Board[i][k] == 0)
                        {
                            continue;
                        }
                        else if (Board[i][k] == Board[i][j])
                        {
                            Board[i][j] *= 2;
                            Score += Board[i][j];
                            Board[i][k] = 0;
                            canAdd = true;
                            break;
                        }
                        else
                        {
                            if (Board[i][j] == 0 && Board[i][k] != 0)
                            {
                                Board[i][j] = Board[i][k];
                                Board[i][k] = 0;
                                j--;
                                canAdd = true;
                                break;
                            }
                            else if (Board[i][j] != 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
        public void SumUp()
        {
            for (int j = 0; j < BoardSize; j++)
            {
                for (int i = 0; i < BoardSize; i++)
                {
                    for (int k = i + 1; k < BoardSize; k++)
                    {
                        if (Board[k][j] == 0)
                        {
                            continue;
                        }
                        else if (Board[k][j] == Board[i][j])
                        {
                            Board[i][j] *= 2;
                            Score += Board[i][j];
                            Board[k][j] = 0;
                            canAdd = true;
                            break;
                        }
                        else
                        {
                            if (Board[i][j] == 0 && Board[k][j] != 0)
                            {
                                Board[i][j] = Board[k][j];
                                Board[k][j] = 0;
                                i--;
                                canAdd = true;
                                break;
                            }
                            else if (Board[i][j] != 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
        public void SumDown()
        {
            for (int j = 0; j < BoardSize; j++)
            {
                for (int i = BoardSize-1; i >= 0; i--)
                {
                    for (int k = i - 1; k >= 0; k--)
                    {
                        if (Board[k][j] == 0)
                        {
                            continue;
                        }
                        else if (Board[k][j] == Board[i][j])
                        {
                            Board[i][j] *= 2;
                            Score += Board[i][j];
                            Board[k][j] = 0;
                            canAdd = true;
                            break;
                        }
                        else
                        {
                            if (Board[i][j] == 0 && Board[k][j] != 0)
                            {
                                Board[i][j] = Board[k][j];
                                Board[k][j] = 0;
                                i++;
                                canAdd = true;
                                break;
                            }
                            else if (Board[i][j] != 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        void AddNumber()
        {
            while (!gameOver && canAdd)
            {
                int nX = rand.Next(0, BoardSize), nY = rand.Next(0, BoardSize);

                if (Board[nX][nY] == 0)
                {
                    if (rand.NextDouble() > 0.25)
                    {
                        Board[nX][nY] = 2;
                    }
                    else
                    {
                        Board[nX][nY] = 4;

                    }
                    canAdd = false;
                }
            }
        }
        void CheckGameOver()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (i - 1 >= 0)
                    {
                        if (Board[i - 1][j] == Board[i][j])
                        {
                            return;
                        }
                    }

                    if (i + 1 < 4)
                    {
                        if (Board[i + 1][j] == Board[i][j])
                        {
                            return;
                        }
                    }

                    if (j - 1 >= 0)
                    {
                        if (Board[i][j - 1] == Board[i][j])
                        {
                            return;
                        }
                    }

                    if (j + 1 < 4)
                    {
                        if (Board[i][j + 1] == Board[i][j])
                        {
                            return;
                        }
                    }

                    if (Board[i][j] == 0)
                    {
                        return;
                    }
                }
            }

            gameOver = true;
        }
        void UpdateScore()
        {
            if (Score > BestScore)
            {
                BestScore = Score;
            }
        }
        void CheckWin()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (Board[i][j] == 2048)
                    {
                        isWin = true;
                        break;
                    }
                }
            }
        }

    }
    
}
