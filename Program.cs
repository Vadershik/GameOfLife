using System;

namespace GameOfLife
{
    class Program {
        public static int cols=50;
        public static int rows=15;
        public static Random rn = new Random();
        static void Main(string[] args) {
            int[][] grid = make2DArr(cols,rows); //Создание 2-ух мерного массива
            for(int i = 0; i < cols; i++) { //генерация рандомных клеток
                for(int j = 0; j < rows; j++) {
                    grid[i][j] = rn.Next(2);
                }
            }
            while(true) {
                int[][] next = make2DArr(cols, rows); //пускай живёт
                
                for(int i = 0; i < cols; i++) {
                    for(int j = 0; j < rows; j++) {
                        int state = grid[i][j]; //статус клетки
                        //счётчик живых соседей

                        if(i==0 || i == cols-1 || j==0 || j==rows-1) {
                            next[i][j] = state;
                        } else {
                            int neighbors = countNeighbors(grid,i,j);
                            //условия клеток
                            
                            if(state==0 && neighbors==3) { //если вокруг 3 соседа и клетка пустая
                                next[i][j] = 1;
                            }
                            else if(state==1 && neighbors < 2 || state==1 && neighbors > 3) { //если вокруг меньше 2 или больше 3 клеток
                                next[i][j] = 0;
                            } else { //если ничего не изменилось
                                next[i][j] = state;
                            }
                        }
                    }
                }
                grid = next;
                show2DArr(cols,rows,grid); //показ получившегося
                System.Threading.Thread.Sleep(500);
            }
        }
        public static int[][] make2DArr(int cols,int rows) { //создание 2-ух мерного массива
            int[][] arr = new int[cols][];
            for(int i = 0; i < arr.Length; i++) {
                arr[i] = new int[rows];
            }
            return arr;
        } 
        public static void show2DArr(int cols, int rows,int[][] grid) {
            int counter=0;
            Console.Clear();
            for(int i = 0; i < cols; i++) {
                for(int j = 0; j < rows; j++) {



                    if(counter==cols) {
                        Console.WriteLine();
                        counter=0;
                    }
                    if(grid[i][j]==1) { //активная клетка
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("  ");
                        Console.ResetColor();
                        counter++;
                    }
                    else {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write("  ");
                        Console.ResetColor();
                        counter++;
                    }
                }
            }
            System.Threading.Thread.Sleep(500);
        }
        public static int countNeighbors(int[][] grid, int x, int y) { //проверка соседних клеток
            int sum = 0;
            for(int i = -1; i < 2; i++) { //все в округе от 1 клетки
                for(int j = -1; j < 2; j++) {
                    if(x > 0 && y > 0 && x < cols && y < rows) {
                        sum += grid[x+i][y+j];
                    }
                }
            }
            sum -= grid[x][y];
            return sum;
        }
    }
}