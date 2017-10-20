using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twozerofoureight
{
    class TwoZeroFourEightModel : Model
    {
        protected int boardSize; // default is 4
        protected int[,] board;
        
        protected Random rand;
        public int score = 0;
        protected int ISOver = 0;
        public bool isFull = false;



        public TwoZeroFourEightModel() : this(4)
        {
            // default board size is 4 
        }

        public int[,] GetBoard()
        {
            return board;
        }
       

        public int GetScore()
        {
            return score;
        }


        public TwoZeroFourEightModel(int size)
        {
            boardSize = size;
            board = new int[boardSize, boardSize];
            var range = Enumerable.Range(0, boardSize);
            foreach (int i in range)
            {
                foreach (int j in range)
                {
                    board[i, j] = 0;
                }
            }
            rand = new Random();
            board = Random(board);
            NotifyAll();
        }


        private int[,] Random(int[,] input)
        {
       
                while (true)
                {
                int x = rand.Next(boardSize);
                int y = rand.Next(boardSize);
                if (board[x, y] == 0)
                {
                    board[x, y] = 2;
                    break;
                }
              }
           
            return input;
        }


        public void PerformDown()
        {
            int[] check;

            int[] buffer;
            int pos;
            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeY);

            
            foreach (int i in rangeX)
            {
                check = new int[4];
                pos = 0;
                buffer = new int[4];
                foreach (int k in rangeX)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in rangeY)

                {
                    check[j] = board[j, i];
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in rangeX)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                       
                        buffer[j] = 0;
                        score += buffer[j - 1];
                    }

                }

                // shift left again
                pos = 3;
                foreach (int j in rangeX)
                {
                    if (buffer[j] != 0)
                    {
                        board[pos, i] = buffer[j];
                        pos--;

                    }
                }
                // copy back
                for (int k = pos; k != -1; k--)
                {
                    board[k, i] = 0;
                }
                for(int x=0; x < boardSize; x++)
                {
                    if(check[x] != board[x, i])
                    {
                        isFull = true;
                    }
                   
                }
            }
            //      result =
            if (isFull) {
                board = Random(board);
                isFull = false;
            }
            
            NotifyAll();
        }

        public void PerformUp()
        {
            int [] check;
            int[] buffer;
            int pos;
            
            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                check = new int[4];
                
                pos = 0;
                buffer = new int[4];
                foreach (int k in range)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in range)
                {
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in range)
                {
                    
                    check[j] = board[j, i];
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        
                        buffer[j] = 0;
                        score += buffer[j - 1];
                    }

                }
                // shift left again
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] != 0)
                    {
                        board[pos, i] = buffer[j];
                        pos++;
                    }
                }
                // copy back
                for (int k = pos; k != boardSize; k++)
                {
                    board[k, i] = 0;
                }
                for (int x = 0; x < boardSize; x++)
                {
                    if (check[x] != board[x, i])
                    {
                        isFull = true;
                    }
               
                }
            }


            if (isFull)
            {
                board = Random(board);
                isFull = false;
            }
            NotifyAll();
        }

        public void PerformRight()
        {
            int[] check;
            int[] buffer;
            int pos;
       
            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeX);
            foreach (int i in rangeY)
            {
                check = new int[4];
                pos = 0;
                buffer = new int[4];
                foreach (int k in rangeY)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in rangeX)
                {
                    check[j] = board[i,j];
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in rangeY)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                     
                        buffer[j] = 0;
                        score += buffer[j - 1];
                    }

                }
                // shift left again
                pos = 3;
                foreach (int j in rangeY)
                {
                    if (buffer[j] != 0)
                    {
                        board[i, pos] = buffer[j];

                        pos--;
                    }
                }
                // copy back
                for (int k = pos; k != -1; k--)
                {
                    board[i, k] = 0;
                }
                for (int x = 0; x < boardSize; x++)
                {
                    if (check[x] != board[i,x])
                    {
                        isFull = true;

                    }
                   
                }
            
        }
            if (isFull)
            {
                board = Random(board);
                isFull = false;
            }
            NotifyAll();
        }

        public void PerformLeft()
        {
            int[] check;
            int[] buffer;
            int pos;
          
            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                check = new int[4];
                pos = 0;
                buffer = new int[boardSize];
                foreach (int k in range)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in range)
                {
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in range)
                {
                   
                    check[j] = board[i,j];
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                    
                        buffer[j] = 0;
                        score += buffer[j - 1];
                    }
                }
                // shift left again
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] != 0)
                    {
                        board[i, pos] = buffer[j];
                        pos++;
                    }
                }
                for (int k = pos; k != boardSize; k++)
                {
                    board[i, k] = 0;
                }
                for (int x = 0; x < boardSize; x++)
                {
                     
                    if (check[x] != board[i,x])
                    {
                        isFull = true;
                    }
                    
                  
                }
            
        }
            if (isFull)
            {
                board = Random(board);
                isFull = false;
            }
            NotifyAll();
        }
    }
}
