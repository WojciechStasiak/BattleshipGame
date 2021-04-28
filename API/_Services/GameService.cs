using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using API.Entities;

namespace API._Services
{

    public class GameService
    {
        #region declarations
        static List<int> row1 = new List<int>();
        static List<int> row2 = new List<int>();
        static List<int> row3 = new List<int>();
        static List<int> row4 = new List<int>();
        static List<int> row5 = new List<int>();
        static List<int> row6 = new List<int>();
        static List<int> row7 = new List<int>();
        static List<int> row8 = new List<int>();
        static List<int> row9 = new List<int>();
        static List<int> row10 = new List<int>();
        Random randX = new Random();
        Random randY = new Random();
        int numberOfSetPatrolBoats = 0;
        int numberOfCruisersSet = 0;
        int numberOfDinghiesSet = 0;
        private static List<MyPoint> leftPlayerBattleshipPoints = new List<MyPoint>();
        private static List<MyPoint> rightPlayerBattleshipPoints = new List<MyPoint>();
        private static List<MyPoint> leftPlayerDinghtiesPoints = new List<MyPoint>();
        private static List<MyPoint> rightPlayerDinghtiesPoints = new List<MyPoint>();
        private static List<List<MyPoint>> leftPlayerPatrolBoatsPoints = new List<List<MyPoint>>();
        private static List<List<MyPoint>> rightPlayerPatrolBoatsPoints = new List<List<MyPoint>>();
        private static List<List<MyPoint>> leftPlayerCruisersPoints = new List<List<MyPoint>>();
        private static List<List<MyPoint>> rightPlayerCruisersPoints = new List<List<MyPoint>>();
        private static List<MyPoint> pointsLeftPlayer = new List<MyPoint>();
        private static List<MyPoint> pointsRightPlayer = new List<MyPoint>();
        private static MyPoint leftPlayerLastHitPoint = new MyPoint();
        private static MyPoint rightPlayerLastHitPoint = new MyPoint();
        private static bool leftPlayerPatrolBoatWasHit = false;
        private static bool leftPlayerCruiserWasHit = false;
        private static bool leftPlayerBattleshipWasHit = false;
        private static bool rightPlayerPatrolBoatWasHit = false;
        private static bool rightPlayerCruiserWasHit = false;
        private static bool rightPlayerBattleshipWasHit = false;
        /// <summary>
        /// number of cells that has been hit in particular ship
        /// </summary>
        private static int numberOfPointsHitL = 0;
        private static int numberOfPointsHitR = 0;
        private static int i = 1;
        private static int j = 1;
        private static bool rightSideCheckL = true;
        private static bool leftSideCheckL = false;
        private static bool upSideCheckL = true;
        private static bool downSideCheckL = false;
        private static bool breakBoolL = false;
        private static bool breakBoolR = false;
        private static bool rightSideCheckR = true;
        private static bool leftSideCheckR = false;
        private static bool upSideCheckR = false;
        private static bool downSideCheckR = false;
        private static int numberOfBoatsThatPlayerRightHit = 0;
        private static int numberOfBoatsThatPlayerLeftHit = 0;
        /// <summary>
        /// this is the point which start the clearing from the top of ship or from the left side of the ship, 
        /// depends if its vertical or horizontal 
        /// </summary>
        private static MyPoint startingPointOfClearing = new MyPoint();
        private static MyPoint startingPointOfClearingR = new MyPoint();

        /// <summary>
        /// 0 - horizontal;                   
        /// 1 - vertical;
        /// </summary>
        private int direction;
        /// <summary>
        /// board of the left hand side player
        /// </summary>
        private static int[,] board1 = {
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0}
        };
        /// <summary>
        /// board of the right hand side player
        /// </summary>
        /// <value></value>
        private static int[,] board2 = {
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0}
        };

        private static int[,] board3 = {
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0}
        };

        ListOfCells listOfCells1;
        ListOfCells listOfCells2;
        #endregion declarations


        #region privateFunctions
        private List<int> createRandomTable()
        {
            List<int> row = new List<int>();
            var rand = new Random();
            for (int i = 0; i < 10; i++)
                row.Add(rand.Next(0, 4));

            return row;
        }
        /// <summary>
        /// this function returns one row from table
        /// </summary>
        private List<int> getRowFromTable(int[,] table, int row)
        {
            int[] g = new int[10];
            g = Enumerable.Range(0, table.GetLength(1))
                        .Select(x => table[row, x])
                        .ToArray();
            return g.Select(x => Convert.ToInt32(x.ToString())).ToList();

        }
        /// <summary>
        /// this function sets random direction of ship
        /// 0 - horizontal
        /// 1 - vertical
        /// </summary>
        private void setDirection()
        {
            direction = randX.Next(0, 2);
        }
        /// <summary>
        /// sets Battleship on gameboard (4 cells)
        /// </summary>
        private void setBattleship(int[,] table, string leftOrRightPlayer)
        {
            int y = randY.Next(0, 10);
            int x = randX.Next(0, 10);
            setDirection();
            try
            {
                if (direction == 0)
                {
                    table[y, x - 3] = 2;
                    table[y, x - 2] = 2;
                    table[y, x - 1] = 2;
                    table[y, x] = 2;
                    if (leftOrRightPlayer.Equals("left", StringComparison.OrdinalIgnoreCase))
                    {
                        leftPlayerBattleshipPoints.Add(new MyPoint(y, x - 3));
                        leftPlayerBattleshipPoints.Add(new MyPoint(y, x - 2));
                        leftPlayerBattleshipPoints.Add(new MyPoint(y, x - 1));
                        leftPlayerBattleshipPoints.Add(new MyPoint(y, x));

                    }
                    else if (leftOrRightPlayer.Equals("right", StringComparison.OrdinalIgnoreCase))
                    {
                        rightPlayerBattleshipPoints.Add(new MyPoint(y, x - 3));
                        rightPlayerBattleshipPoints.Add(new MyPoint(y, x - 2));
                        rightPlayerBattleshipPoints.Add(new MyPoint(y, x - 1));
                        rightPlayerBattleshipPoints.Add(new MyPoint(y, x));
                    }

                }
                if (direction == 1)
                {
                    table[y - 3, x] = 2;
                    table[y - 2, x] = 2;
                    table[y - 1, x] = 2;
                    table[y, x] = 2;
                    if (leftOrRightPlayer.Equals("left", StringComparison.OrdinalIgnoreCase))
                    {
                        leftPlayerBattleshipPoints.Add(new MyPoint(y - 3, x));
                        leftPlayerBattleshipPoints.Add(new MyPoint(y - 2, x));
                        leftPlayerBattleshipPoints.Add(new MyPoint(y - 1, x));
                        leftPlayerBattleshipPoints.Add(new MyPoint(y, x));

                    }
                    else if (leftOrRightPlayer.Equals("right", StringComparison.OrdinalIgnoreCase))
                    {
                        rightPlayerBattleshipPoints.Add(new MyPoint(y - 3, x));
                        rightPlayerBattleshipPoints.Add(new MyPoint(y - 2, x));
                        rightPlayerBattleshipPoints.Add(new MyPoint(y - 1, x));
                        rightPlayerBattleshipPoints.Add(new MyPoint(y, x));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e + " x=====>" + x + " y=======>" + y + " direction======>" + direction + " functionName=" + GetCurrentMethod());
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                setBattleship(table, leftOrRightPlayer);
            }
        }
        /// <summary>
        /// function thats checks if it is space to draw cruiser ship horizontally
        /// </summary>
        private bool checkIfItsFreeSpaceCruiserHorizontal(int[,] table, int y, int x)
        {
            if ((x + 1) <= 9)
            {
                if (table[y, x + 1] != 0)
                    return false;
            }

            if ((x - 3) >= 0)
            {
                if (table[y, x - 3] != 0)
                {
                    return false;
                }
            }

            if ((y - 1) >= 0)
            {
                if (table[y - 1, x] != 0 || table[y - 1, x - 1] != 0 || table[y - 1, x - 2] != 0)
                {
                    return false;
                }
            }

            if ((y + 1) <= 9)
            {
                if (table[y + 1, x] != 0 || table[y + 1, x - 1] != 0 || table[y + 1, x - 2] != 0)
                {
                    return false;
                }
            }

            if (table[y, x] != 0 || table[y, x - 1] != 0 || table[y, x - 2] != 0)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// function thats checks if it is space to draw cruiser ship vertically
        /// </summary>
        private bool checkIfItsFreeSpaceCruiserVertical(int[,] table, int y, int x)
        {
            if ((y + 1) <= 9)
            {
                if (table[y + 1, x] != 0)
                    return false;
            }

            if ((y - 3) >= 0)
            {
                if (table[y - 3, x] != 0)
                {
                    return false;
                }
            }

            if ((x - 1) >= 0)
            {
                if (table[y, x - 1] != 0 || table[y - 1, x - 1] != 0 || table[y - 2, x - 1] != 0)
                {
                    return false;
                }
            }

            if (table[y, x] != 0 || table[y - 1, x] != 0 || table[y - 2, x] != 0)
            {
                return false;
            }

            if ((x + 1) <= (table.GetLength(0) - 1))
            {
                if (table[y, x + 1] != 0 || table[y - 1, x + 1] != 0 || table[y - 2, x + 1] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// set Cruiser on gameboard (3 cells)
        /// </summary>
        private void setCruiser(int[,] table, string leftOrRightPlayer)
        {
            int y = new int();
            int x = new int();
            try
            {
                while (numberOfCruisersSet < 2)
                {
                    setDirection();
                    if (direction == 1)
                    {
                        y = randY.Next(2, 10);
                        x = randX.Next(0, 10);
                        if (checkIfItsFreeSpaceCruiserVertical(table, y, x) && numberOfCruisersSet < 2)
                        {

                            table[y - 2, x] = 2;
                            table[y - 1, x] = 2;
                            table[y, x] = 2;
                            if (leftOrRightPlayer.Equals("left", StringComparison.OrdinalIgnoreCase))
                            {
                                leftPlayerCruisersPoints.Add(new List<MyPoint> {
                                new MyPoint(y, x), new MyPoint(y - 1, x), new MyPoint(y - 2, x) });
                            }
                            else if (leftOrRightPlayer.Equals("right", StringComparison.OrdinalIgnoreCase))
                            {
                                rightPlayerCruisersPoints.Add(new List<MyPoint> {
                                new MyPoint(y, x), new MyPoint(y - 1, x), new MyPoint(y - 2, x) });
                            }
                            numberOfCruisersSet++;
                        }

                    }
                    if (direction == 0)
                    {
                        y = randY.Next(0, 10);
                        x = randX.Next(2, 10);
                        if (checkIfItsFreeSpaceCruiserHorizontal(table, y, x) && numberOfCruisersSet < 2)
                        {

                            table[y, x - 2] = 2;
                            table[y, x - 1] = 2;
                            table[y, x] = 2;
                            if (leftOrRightPlayer.Equals("left", StringComparison.OrdinalIgnoreCase))
                            {
                                leftPlayerCruisersPoints.Add(new List<MyPoint> {
                                new MyPoint(y, x), new MyPoint(y, x - 1), new MyPoint(y, x - 2) });
                            }
                            else if (leftOrRightPlayer.Equals("right", StringComparison.OrdinalIgnoreCase))
                            {
                                rightPlayerCruisersPoints.Add(new List<MyPoint> {
                                new MyPoint(y, x), new MyPoint(y, x - 1), new MyPoint(y, x - 2) });
                            }
                            numberOfCruisersSet++;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e + " x=====>" + x + " y=======>" + y + " direction======>" + direction + " functionName=" + GetCurrentMethod());
                Console.WriteLine();
                setCruiser(table, leftOrRightPlayer);
            }
        }

        /// <summary>
        /// function thats checks if it is space to draw patrol boat horizontally
        /// </summary>
        private bool checkSpaceForPatrolBoatHorizontal(int y, int x, int[,] table)
        {
            if ((x + 1) <= 9)
            {
                if (table[y, x + 1] != 0)
                    return false;
            }

            if ((x - 2) >= 0)
            {
                if (table[y, x - 2] != 0)
                {
                    return false;
                }
            }

            if (table[y, x] != 0 || table[y, x - 1] != 0)
            {
                return false;
            }

            if ((y - 1) >= 0)
            {
                if (table[y - 1, x] != 0 || table[y - 1, x - 1] != 0)
                {
                    return false;
                }
            }

            if ((y + 1) <= (table.GetLength(0) - 1))
            {
                if (table[y + 1, x] != 0 || table[y + 1, x - 1] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// function thats checks if it is space to draw patrol boat vertically 
        /// </summary>
        private bool checkSpaceForPatrolBoatVertical(int y, int x, int[,] table)
        {
            if ((y + 1) <= 9)
            {
                if (table[y + 1, x] != 0)
                {
                    return false;
                }
            }

            if ((y - 2) >= 0)
            {
                if (table[y - 2, x] != 0)
                {
                    return false;
                }
            }
            if (table[y, x] != 0 || table[y - 1, x] != 0)
            {
                return false;
            }

            if ((x + 1) <= 9)
            {
                if (table[y, x + 1] != 0 || table[y - 1, x + 1] != 0)
                {
                    return false;
                }
            }

            if ((x - 1) >= 0)
            {
                if (table[y, x - 1] != 0 || table[y - 1, x - 1] != 0)
                {
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// set PatrolBoat on gameboard (2 cells)
        /// </summary>
        private void setPatrolBoat(int[,] table, string leftOrRightPlayer)
        {
            int y = new int();
            int x = new int();
            setDirection();
            try
            {
                if (direction == 0 && numberOfSetPatrolBoats < 3)
                {
                    y = randY.Next(0, 10);
                    x = randX.Next(1, 10);
                    bool p = checkSpaceForPatrolBoatHorizontal(y, x, table);
                    if (checkSpaceForPatrolBoatHorizontal(y, x, table))
                    {
                        table[y, x - 1] = 2;
                        table[y, x] = 2;
                        if (leftOrRightPlayer.Equals("left", StringComparison.OrdinalIgnoreCase))
                        {
                            leftPlayerPatrolBoatsPoints.Add(new List<MyPoint> { new MyPoint(y, x), new MyPoint(y, x - 1) });
                        }
                        else if (leftOrRightPlayer.Equals("right", StringComparison.OrdinalIgnoreCase))
                        {
                            rightPlayerPatrolBoatsPoints.Add(new List<MyPoint> { new MyPoint(y, x), new MyPoint(y, x - 1) });
                        }
                        numberOfSetPatrolBoats++;
                    }
                    else
                    {
                        setPatrolBoat(table, leftOrRightPlayer);
                    }
                }


                if (direction == 1 && numberOfSetPatrolBoats < 3)
                {
                    y = randY.Next(1, 10);
                    x = randX.Next(0, 10);
                    if (checkSpaceForPatrolBoatVertical(y, x, table))
                    {

                        table[y - 1, x] = 2;
                        table[y, x] = 2;
                        if (leftOrRightPlayer.Equals("left", StringComparison.OrdinalIgnoreCase))
                        {
                            leftPlayerPatrolBoatsPoints.Add(new List<MyPoint> { new MyPoint(y, x), new MyPoint(y - 1, x) });
                        }
                        else if (leftOrRightPlayer.Equals("right", StringComparison.OrdinalIgnoreCase))
                        {
                            rightPlayerPatrolBoatsPoints.Add(new List<MyPoint> { new MyPoint(y, x), new MyPoint(y - 1, x) });
                        }
                        numberOfSetPatrolBoats++;
                    }
                    else
                    {
                        setPatrolBoat(table, leftOrRightPlayer);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e + " x=====>" + x + " y=======>" + y + " direction======>" + direction + " functionName=" + GetCurrentMethod());
                Console.WriteLine();
                Console.WriteLine();
                setPatrolBoat(table, leftOrRightPlayer);
            }
        }
        /// <summary>
        /// this function checks if there is space to draw dinghy
        /// </summary>
        private bool checkSpaceForDinghy(int y, int x, int[,] table)
        {
            if (table[y, x] != 0)
            {
                return false;
            }
            if ((x + 1) <= 9)
            {
                if (table[y, x + 1] != 0)
                {
                    return false;
                }
            }

            if ((x - 1) >= 0)
            {
                if (table[y, x - 1] != 0)
                {
                    return false;
                }
            }

            if ((y + 1) <= 9)
            {
                if (table[y + 1, x] != 0)
                {
                    return false;
                }
            }

            if ((y - 1) >= 0)
            {
                if (table[y - 1, x] != 0)
                {
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// sets dinghy on gameboard
        /// </summary>
        private void setDinghy(int[,] table, string leftOrRightPlayer)
        {
            int x = randX.Next(0, 10);
            int y = randY.Next(0, 10);
            try
            {
                if (numberOfDinghiesSet < 4 && checkSpaceForDinghy(y, x, table))
                {
                    table[y, x] = 2;
                    if (leftOrRightPlayer.Equals("left", StringComparison.OrdinalIgnoreCase))
                    {
                        leftPlayerDinghtiesPoints.Add(new MyPoint(y, x));
                    }
                    else if (leftOrRightPlayer.Equals("right", StringComparison.OrdinalIgnoreCase))
                    {
                        rightPlayerDinghtiesPoints.Add(new MyPoint(y, x));
                    }
                    numberOfDinghiesSet++;
                }
                else
                {
                    setDinghy(table, leftOrRightPlayer);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e + " x=====>" + x + " y=======>" + y + " functionName=" + GetCurrentMethod());
                Console.WriteLine();
                setDinghy(table, leftOrRightPlayer);
            }

        }
        /// <summary>
        /// this function prepares list that will be send to frontend. the list stores ewery point of players game board
        /// </summary>
        private ListOfCells setList(ListOfCells list, int[,] board)
        {
            list = new ListOfCells(
                                getRowFromTable(board, 0), getRowFromTable(board, 1), getRowFromTable(board, 2),
                                getRowFromTable(board, 3), getRowFromTable(board, 4), getRowFromTable(board, 5),
                                getRowFromTable(board, 6), getRowFromTable(board, 7), getRowFromTable(board, 8),
                                getRowFromTable(board, 9)
                            );
            return list;
        }
        /// <summary>
        /// this function sets new game for left player
        /// </summary>
        private void setUpBoard()
        {
            numberOfBoatsThatPlayerLeftHit = 0;
            numberOfCruisersSet = 0;
            numberOfSetPatrolBoats = 0;
            numberOfDinghiesSet = 0;
            setBattleship(board1, "left");
            setCruiser(board1, "left");
            setCruiser(board1, "left");
            setPatrolBoat(board1, "left");
            setPatrolBoat(board1, "left");
            setPatrolBoat(board1, "left");
            setDinghy(board1, "left");
            setDinghy(board1, "left");
            setDinghy(board1, "left");
            setDinghy(board1, "left");
            listOfCells1 = setList(listOfCells1, board1);

        }

        /// <summary>
        /// this function sets new game for left player
        /// </summary>
        private void setUpBoard2()
        {
            numberOfBoatsThatPlayerRightHit = 0;
            numberOfCruisersSet = 0;
            numberOfSetPatrolBoats = 0;
            numberOfDinghiesSet = 0;
            setBattleship(board2, "right");
            setCruiser(board2, "right");
            setCruiser(board2, "right");
            setPatrolBoat(board2, "right");
            setPatrolBoat(board2, "right");
            setPatrolBoat(board2, "right");
            setDinghy(board2, "right");
            setDinghy(board2, "right");
            setDinghy(board2, "right");
            setDinghy(board2, "right");
            listOfCells2 = setList(listOfCells2, board2);

        }
        /// <summary>
        /// this function gets current method that is invoked 
        /// </summary>
        private string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return sf.GetMethod().Name.ToString();
        }
        #region ShotsAtRightPlayersBoard
        /// <summary>
        /// this function restores the default settings of shooting at the right's player ship
        /// </summary>
        private void restoreDefaultBoolSettingsR()
        {
            numberOfPointsHitR = 0;
            rightPlayerBattleshipWasHit = false;
            rightPlayerCruiserWasHit = false;
            rightPlayerPatrolBoatWasHit = false;
            rightSideCheckR = true;
            leftSideCheckR = false;
            upSideCheckR = false;
            downSideCheckR = false;
            breakBoolR = false;
            j = 1;
            numberOfBoatsThatPlayerLeftHit++;
        }
        /// <summary>
        /// this function checks if left player hit right's player ship
        /// </summary>
        private void checkShotRight(int indexOfPoint)
        {
            MyPoint shotPoint = pointsRightPlayer[indexOfPoint];
            if (board2[shotPoint.getY(), shotPoint.getX()] == 3 && board2[shotPoint.getY(), shotPoint.getX()] == 1)
            {
                Console.WriteLine("Player has already shot at that point");
            }
            else
            {
                if (rightPlayerPatrolBoatWasHit == true)
                {
                    checkIfPatrolBoatWasHitR(shotPoint);
                }
                else if (rightPlayerCruiserWasHit == true)
                {
                    checkIfCruiserWasHitR(shotPoint);
                }
                else if (rightPlayerBattleshipWasHit == true)
                {
                    checkIfBattleshipWasHitR(shotPoint);
                }
                else
                {
                    checkIfRightPlayerDinghyWasHit(shotPoint);
                    checkIfPatrolBoatWasHitR(shotPoint);
                    checkIfCruiserWasHitR(shotPoint);
                    checkIfBattleshipWasHitR(shotPoint);
                }
            }
        }
        /// <summary>
        /// this function checks if battleship of right player was hit 
        /// </summary>
        private void checkIfBattleshipWasHitR(MyPoint shotPoint)
        {
            if (rightPlayerBattleshipWasHit == true)
            {

                if (rightSideCheckR == true)
                {
                    checkRightSideOfTheShipR(shotPoint, 4);
                    if (breakBoolR == true)
                    {
                        breakBoolR = false;
                        goto END;
                    }
                }
                if (leftSideCheckR == true)
                {
                    checkLeftSideOfTheShipR(shotPoint, 4);
                    if (breakBoolR == true)
                    {
                        breakBoolR = false;
                        goto END;
                    }
                }
                if (downSideCheckR == true)
                {
                    checkDownSideOfTheShipR(shotPoint, 4);
                    if (breakBoolR == true)
                    {
                        breakBoolR = false;
                        goto END;
                    }
                }
                if (upSideCheckR == true)
                {
                    checkUpperSideOfTheShipR(shotPoint, 4);
                    if (breakBoolR == true)
                    {
                        breakBoolR = false;
                        goto END;
                    }
                }
            END: Console.WriteLine("breakBool == true");

            }
            else
            {
                foreach (MyPoint myPoint in rightPlayerBattleshipPoints)
                {
                    if (board2[shotPoint.getY(), shotPoint.getX()] == 2 && myPoint.getX() == shotPoint.getX() && myPoint.getY() == shotPoint.getY())
                    {
                        board2[shotPoint.getY(), shotPoint.getX()] = 3;
                        Console.WriteLine("X====>" + shotPoint.getX() + "Y=====>" + shotPoint.getY());
                        removeRecordFromRightPlayerListOfPoints(shotPoint);
                        rightPlayerBattleshipWasHit = true;
                        startingPointOfClearingR = shotPoint;
                        rightPlayerLastHitPoint = shotPoint;
                        numberOfPointsHitR++;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// this function checks if cruiser of right player was hit 
        /// </summary>
        private void checkIfCruiserWasHitR(MyPoint shotPoint)
        {
            if (rightPlayerCruiserWasHit == true)
            {
                if (rightSideCheckR == true)
                {
                    checkRightSideOfTheShipR(shotPoint, 3);
                    if (breakBoolR == true)
                    {
                        breakBoolR = false;
                        goto END;
                    }
                }
                if (leftSideCheckR == true)
                {
                    checkLeftSideOfTheShipR(shotPoint, 3);
                    if (breakBoolR == true)
                    {
                        breakBoolR = false;
                        goto END;
                    }
                }
                if (downSideCheckR == true)
                {
                    checkDownSideOfTheShipR(shotPoint, 3);
                    if (breakBoolR == true)
                    {
                        breakBoolR = false;
                        goto END;
                    }
                }
                if (upSideCheckR == true)
                {
                    checkUpperSideOfTheShipR(shotPoint, 3);
                    if (breakBoolR == true)
                    {
                        breakBoolR = false;
                        goto END;
                    }
                }
            END: Console.WriteLine("breakBool == true");

            }
            else
            {
                foreach (List<MyPoint> list in rightPlayerCruisersPoints)
                    foreach (MyPoint myPoint in list)
                    {
                        if (board2[shotPoint.getY(), shotPoint.getX()] == 2 && myPoint.getX() == shotPoint.getX() && myPoint.getY() == shotPoint.getY())
                        {
                            board2[shotPoint.getY(), shotPoint.getX()] = 3;
                            Console.WriteLine("X====>" + shotPoint.getX() + "Y=====>" + shotPoint.getY());
                            removeRecordFromListOfPoints(shotPoint);
                            rightPlayerCruiserWasHit = true;
                            startingPointOfClearingR = shotPoint;
                            rightPlayerLastHitPoint = shotPoint;
                            numberOfPointsHitR++;
                            return;
                        }
                    }
            }
        }

        /// <summary>
        /// this function checks if patrol boat of right player was hit 
        /// </summary>
        private void checkIfPatrolBoatWasHitR(MyPoint shotPoint)
        {

            if (rightPlayerPatrolBoatWasHit == true)
            {
                if (rightSideCheckR == true)
                {
                    checkRightSideOfTheShipR(shotPoint, 2);
                    if (breakBoolR == true)
                    {
                        breakBoolR = false;
                        goto END;
                    }
                }
                if (leftSideCheckR == true)
                {
                    checkLeftSideOfTheShipR(shotPoint, 2);
                    if (breakBoolR == true)
                    {
                        breakBoolR = false;
                        goto END;
                    }
                }
                if (downSideCheckR == true)
                {
                    checkDownSideOfTheShipR(shotPoint, 2);
                    if (breakBoolR == true)
                    {
                        breakBoolR = false;
                        goto END;
                    }
                }
                if (upSideCheckR == true)
                {
                    checkUpperSideOfTheShipR(shotPoint, 2);
                    if (breakBoolR == true)
                    {
                        breakBoolR = false;
                        goto END;
                    }
                }
            END: Console.WriteLine("breakBool == true");
            }
            else
            {
                foreach (List<MyPoint> list in rightPlayerPatrolBoatsPoints)
                    foreach (MyPoint myPoint in list)
                    {
                        if (board2[shotPoint.getY(), shotPoint.getX()] == 2 && myPoint.getX() == shotPoint.getX() && myPoint.getY() == shotPoint.getY())
                        {
                            board2[shotPoint.getY(), shotPoint.getX()] = 3;
                            Console.WriteLine("X====>" + shotPoint.getX() + "Y=====>" + shotPoint.getY());
                            removeRecordFromRightPlayerListOfPoints(shotPoint);
                            rightPlayerPatrolBoatWasHit = true;
                            rightPlayerLastHitPoint = shotPoint;
                            startingPointOfClearingR = shotPoint;
                            numberOfPointsHitR++;
                            return;
                        }
                    }
            }

        }
        /// <summary>
        /// this function checks if points of the right side of the ship are part of the ship and hits it, function for right board
        /// </summary>
        private void checkRightSideOfTheShipR(MyPoint shotPoint, int numberOfCellsInShip)
        {
            if (rightPlayerLastHitPoint.getX() + j <= 9)
            {
                if (board2[rightPlayerLastHitPoint.getY(), rightPlayerLastHitPoint.getX() + j] == 2)
                {
                    board2[rightPlayerLastHitPoint.getY(), rightPlayerLastHitPoint.getX() + j] = 3;
                    numberOfPointsHitR++;
                    removeRecordFromRightPlayerListOfPoints(new MyPoint(rightPlayerLastHitPoint.getY(), rightPlayerLastHitPoint.getX() + j));
                    j++;
                    breakBoolR = true;
                    if (numberOfPointsHitR == numberOfCellsInShip)
                    {
                        clearCellsNextToShipHorizontalR(numberOfCellsInShip);
                        restoreDefaultBoolSettingsR();
                    }
                }
                else
                {
                    if (board2[rightPlayerLastHitPoint.getY(), rightPlayerLastHitPoint.getX() + j] == 0)
                    {
                        board2[rightPlayerLastHitPoint.getY(), rightPlayerLastHitPoint.getX() + j] = 1;
                        removeRecordFromRightPlayerListOfPoints(new MyPoint(rightPlayerLastHitPoint.getY(), rightPlayerLastHitPoint.getX() + j));
                    }
                    rightSideCheckR = false;
                    leftSideCheckR = true;
                    j = 1;
                    breakBoolR = true;
                }
            }
            else
            {
                rightSideCheckR = false;
                leftSideCheckR = true;
                j = 1;
            }
        }
        /// <summary>
        /// this function checks if points of the left side of the ship are part of the ship and hits it, function for right board
        /// </summary>
        private void checkLeftSideOfTheShipR(MyPoint shotPoint, int numberOfCellsInShip)
        {
            if (rightPlayerLastHitPoint.getX() - j >= 0)
            {
                if (board2[rightPlayerLastHitPoint.getY(), rightPlayerLastHitPoint.getX() - j] == 2)
                {
                    board2[rightPlayerLastHitPoint.getY(), rightPlayerLastHitPoint.getX() - j] = 3;
                    startingPointOfClearingR = new MyPoint(rightPlayerLastHitPoint.getY(), rightPlayerLastHitPoint.getX() - j);
                    numberOfPointsHitR++;
                    removeRecordFromRightPlayerListOfPoints(new MyPoint(rightPlayerLastHitPoint.getY(), rightPlayerLastHitPoint.getX() - j));
                    j++;
                    breakBoolR = true;
                    if (numberOfPointsHitR == numberOfCellsInShip)
                    {
                        clearCellsNextToShipHorizontalR(numberOfCellsInShip);
                        restoreDefaultBoolSettingsR();
                    }
                }
                else
                {
                    if (board2[rightPlayerLastHitPoint.getY(), rightPlayerLastHitPoint.getX() - j] == 0)
                    {
                        board2[rightPlayerLastHitPoint.getY(), rightPlayerLastHitPoint.getX() - j] = 1;
                        removeRecordFromRightPlayerListOfPoints(new MyPoint(rightPlayerLastHitPoint.getY(), rightPlayerLastHitPoint.getX() - j));
                    }
                    rightSideCheckR = false;
                    leftSideCheckR = false;
                    downSideCheckR = true;
                    j = 1;
                    breakBoolR = true;
                }
            }
            else
            {
                rightSideCheckR = false;
                leftSideCheckR = false;
                downSideCheckR = true;
                j = 1;
            }
        }

        /// <summary>
        /// this function checks if points of the down side of the ship are part of the ship and hits it, function for right board
        /// </summary>
        private void checkDownSideOfTheShipR(MyPoint shotPoint, int numberOfCellsInShip)
        {
            if (rightPlayerLastHitPoint.getY() + j <= 9)
            {
                if (board2[rightPlayerLastHitPoint.getY() + j, rightPlayerLastHitPoint.getX()] == 2)
                {
                    board2[rightPlayerLastHitPoint.getY() + j, rightPlayerLastHitPoint.getX()] = 3;
                    numberOfPointsHitR++;
                    removeRecordFromRightPlayerListOfPoints(new MyPoint(rightPlayerLastHitPoint.getY() + j, rightPlayerLastHitPoint.getX()));
                    j++;
                    breakBoolR = true;
                    if (numberOfPointsHitR == numberOfCellsInShip)
                    {
                        clearCellsNextToShipVerticalR(numberOfCellsInShip);
                        restoreDefaultBoolSettingsR();
                    }
                }
                else
                {
                    if (board2[rightPlayerLastHitPoint.getY() + j, rightPlayerLastHitPoint.getX()] == 0)
                    {
                        board2[rightPlayerLastHitPoint.getY() + j, rightPlayerLastHitPoint.getX()] = 1;
                        removeRecordFromRightPlayerListOfPoints(new MyPoint(rightPlayerLastHitPoint.getY() + j, rightPlayerLastHitPoint.getX()));
                    }
                    rightSideCheckR = false;
                    leftSideCheckR = false;
                    downSideCheckR = false;
                    upSideCheckR = true;
                    j = 1;
                    breakBoolR = true;
                }
            }
            else
            {
                rightSideCheckR = false;
                leftSideCheckR = false;
                downSideCheckR = false;
                upSideCheckR = true;
                j = 1;
            }
        }

        /// <summary>
        /// this function checks if points of the upper side of the ship are part of the ship and hits it, function for right board
        /// </summary>
        private void checkUpperSideOfTheShipR(MyPoint shotPoint, int numberOfCellsInShip)
        {
            if (rightPlayerLastHitPoint.getY() - j >= 0)
            {
                if (board2[rightPlayerLastHitPoint.getY() - j, rightPlayerLastHitPoint.getX()] == 2)
                {
                    board2[rightPlayerLastHitPoint.getY() - j, rightPlayerLastHitPoint.getX()] = 3;
                    startingPointOfClearingR = new MyPoint(rightPlayerLastHitPoint.getY() - j, rightPlayerLastHitPoint.getX());
                    numberOfPointsHitR++;
                    removeRecordFromRightPlayerListOfPoints(new MyPoint(rightPlayerLastHitPoint.getY() - j, rightPlayerLastHitPoint.getX()));
                    j++;
                    breakBoolR = true;
                    if (numberOfPointsHitR == numberOfCellsInShip)
                    {
                        clearCellsNextToShipVerticalR(numberOfCellsInShip);
                        restoreDefaultBoolSettingsR();
                    }
                }
                else
                {
                    if (board2[rightPlayerLastHitPoint.getY() - j, rightPlayerLastHitPoint.getX()] == 0)
                    {
                        board2[rightPlayerLastHitPoint.getY() - j, rightPlayerLastHitPoint.getX()] = 1;
                        removeRecordFromRightPlayerListOfPoints(new MyPoint(rightPlayerLastHitPoint.getY() - j, rightPlayerLastHitPoint.getX()));
                    }
                    rightSideCheckR = false;
                    leftSideCheckR = false;
                    downSideCheckR = false;
                    upSideCheckR = false;
                    j = 1;
                    breakBoolR = true;
                }
            }
            else
            {
                rightSideCheckR = false;
                leftSideCheckR = false;
                downSideCheckR = false;
                upSideCheckR = false;
                j = 1;
            }

        }
        /// <summary>
        /// if the right player ship has been sunk, this function places an X on the cells that are on 
        /// upper, left, right and down side of the ship, this function works for ships that are horizontal
        /// </summary>
        private void clearCellsNextToShipHorizontalR(int numberOfCellsThatShipIsContaining)
        {
            if (startingPointOfClearingR.getX() - 1 >= 0)
            {
                board2[startingPointOfClearingR.getY(), startingPointOfClearingR.getX() - 1] = 1;
                removeRecordFromRightPlayerListOfPoints(new MyPoint(startingPointOfClearingR.getY(), startingPointOfClearingR.getX() - 1));
            }
            if (startingPointOfClearingR.getX() + numberOfCellsThatShipIsContaining <= 9)
            {
                board2[startingPointOfClearingR.getY(), startingPointOfClearingR.getX() + numberOfCellsThatShipIsContaining] = 1;
                removeRecordFromRightPlayerListOfPoints(new MyPoint(startingPointOfClearingR.getY(), startingPointOfClearingR.getX() + numberOfCellsThatShipIsContaining));
            }
            if (startingPointOfClearingR.getY() - 1 >= 0)
            {
                for (int h = 0; h < numberOfCellsThatShipIsContaining; h++)
                {
                    board2[startingPointOfClearingR.getY() - 1, startingPointOfClearingR.getX() + h] = 1;
                    removeRecordFromRightPlayerListOfPoints(new MyPoint(startingPointOfClearingR.getY() - 1, startingPointOfClearingR.getX() + h));
                }
            }

            if (startingPointOfClearingR.getY() + 1 <= 9)
            {
                for (int h = 0; h < numberOfCellsThatShipIsContaining; h++)
                {
                    board2[startingPointOfClearingR.getY() + 1, startingPointOfClearingR.getX() + h] = 1;
                    removeRecordFromRightPlayerListOfPoints(new MyPoint(startingPointOfClearingR.getY() + 1, startingPointOfClearingR.getX() + h));
                }

            }
        }

        /// <summary>
        /// if the right player ship has been sunk, this function places an X on the cells that 
        /// are on upper, left, right and down side of the ship, 
        /// this function works for ships that are vertial 
        /// </summary>
        private void clearCellsNextToShipVerticalR(int numberOfCellsThatShipIsContaining)
        {
            if (startingPointOfClearingR.getY() - 1 >= 0)
            {
                if (board2[startingPointOfClearingR.getY() - 1, startingPointOfClearingR.getX()] == 0)
                {
                    board2[startingPointOfClearingR.getY() - 1, startingPointOfClearingR.getX()] = 1;
                    removeRecordFromRightPlayerListOfPoints(new MyPoint(startingPointOfClearingR.getY() - 1, startingPointOfClearingR.getX()));
                }
            }

            if (startingPointOfClearingR.getY() + numberOfCellsThatShipIsContaining <= 9)
            {
                if (board2[startingPointOfClearingR.getY() + numberOfCellsThatShipIsContaining, startingPointOfClearingR.getX()] == 0)
                {
                    board2[startingPointOfClearingR.getY() + numberOfCellsThatShipIsContaining, startingPointOfClearingR.getX()] = 1;
                    removeRecordFromRightPlayerListOfPoints(new MyPoint(startingPointOfClearingR.getY() + 1, startingPointOfClearingR.getX()));
                }
            }

            if (startingPointOfClearingR.getX() - 1 >= 0)
            {
                for (int h = 0; h < numberOfCellsThatShipIsContaining; h++)
                {
                    if (board2[startingPointOfClearingR.getY() + h, startingPointOfClearingR.getX() - 1] == 0)
                    {
                        board2[startingPointOfClearingR.getY() + h, startingPointOfClearingR.getX() - 1] = 1;
                        removeRecordFromRightPlayerListOfPoints(new MyPoint(startingPointOfClearingR.getY() + h, startingPointOfClearingR.getX() - 1));
                    }
                }
            }
            if (startingPointOfClearingR.getX() + 1 <= 9)
            {
                for (int h = 0; h < numberOfCellsThatShipIsContaining; h++)
                {
                    if (board2[startingPointOfClearingR.getY() + h, startingPointOfClearingR.getX() + 1] == 0)
                    {
                        board2[startingPointOfClearingR.getY() + h, startingPointOfClearingR.getX() + 1] = 1;
                        removeRecordFromRightPlayerListOfPoints(new MyPoint(startingPointOfClearingR.getY() + h, startingPointOfClearingR.getX() + 1));
                    }
                }
            }

        }

        /// <summary>
        /// this function checks if the right player dinghy was hit 
        /// </summary>
        private void checkIfRightPlayerDinghyWasHit(MyPoint shotPoint)
        {
            foreach (MyPoint myPoint in rightPlayerDinghtiesPoints)
            {
                if (board2[shotPoint.getY(), shotPoint.getX()] == 2 && myPoint.getX() == shotPoint.getX() && myPoint.getY() == shotPoint.getY())
                {
                    if (shotPoint.getX() - 1 >= 0)
                    {
                        board2[shotPoint.getY(), shotPoint.getX() - 1] = 1;
                        removeRecordFromRightPlayerListOfPoints(new MyPoint(shotPoint.getY(), shotPoint.getX() - 1));
                    }
                    if (shotPoint.getX() + 1 <= 9)
                    {
                        board2[shotPoint.getY(), shotPoint.getX() + 1] = 1;
                        removeRecordFromRightPlayerListOfPoints(new MyPoint(shotPoint.getY(), shotPoint.getX() + 1));
                    }
                    if (shotPoint.getY() + 1 <= 9)
                    {
                        board2[shotPoint.getY() + 1, shotPoint.getX()] = 1;
                        removeRecordFromRightPlayerListOfPoints(new MyPoint(shotPoint.getY() + 1, shotPoint.getX()));
                    }
                    if (shotPoint.getY() - 1 >= 0)
                    {
                        board2[shotPoint.getY() - 1, shotPoint.getX()] = 1;
                        removeRecordFromRightPlayerListOfPoints(new MyPoint(shotPoint.getY() - 1, shotPoint.getX()));
                    }

                    board2[shotPoint.getY(), shotPoint.getX()] = 3;
                    Console.WriteLine("X====>" + shotPoint.getX() + "Y=====>" + shotPoint.getY());
                    removeRecordFromRightPlayerListOfPoints(shotPoint);
                    numberOfBoatsThatPlayerLeftHit++;
                }
                else if (board2[shotPoint.getY(), shotPoint.getX()] == 0)
                {
                    board2[shotPoint.getY(), shotPoint.getX()] = 1;
                    removeRecordFromRightPlayerListOfPoints(shotPoint);
                }
            }
        }
        /// <summary>
        /// if one point was hit then this function removes that point form list of all points that are on the board
        /// </summary>
        private void removeRecordFromRightPlayerListOfPoints(MyPoint point)
        {
            MyPoint temp = pointsRightPlayer.Find(x => x.getX() == point.getX() && x.getY() == point.getY());
            pointsRightPlayer.Remove(temp);
        }

        #endregion ShotsAtRightPlayersBoard

        #region ShotsAtLeftPlayersBoard

        /// <summary>
        /// this function checks if right player hit left's player ship
        /// </summary>
        private void checkShotLeft(int indexOfPoint)
        {
            MyPoint shotPoint = pointsLeftPlayer[indexOfPoint];
            if (board1[shotPoint.getY(), shotPoint.getX()] == 3 && board1[shotPoint.getY(), shotPoint.getX()] == 1)
            {
                Console.WriteLine("Player has already shot at that point");
            }
            else
            {
                if (leftPlayerPatrolBoatWasHit == true)
                {
                    checkIfPatrolBoatWasHit(shotPoint);
                }
                else if (leftPlayerCruiserWasHit == true)
                {
                    checkIfCruiserWasHit(shotPoint);
                }
                else if (leftPlayerBattleshipWasHit == true)
                {
                    checkIfBattleshipWasHit(shotPoint);
                }
                else
                {
                    checkIfLeftPlayerDinghyWasHit(shotPoint);
                    checkIfPatrolBoatWasHit(shotPoint);
                    checkIfCruiserWasHit(shotPoint);
                    checkIfBattleshipWasHit(shotPoint);
                }
            }
        }
        /// <summary>
        /// this function restores the default settings of shooting at the left's player ship, 
        /// it is used when the ship has been sunk
        /// </summary>
        private void restoreDefaultBoolSettings()
        {
            numberOfPointsHitL = 0;
            leftPlayerBattleshipWasHit = false;
            leftPlayerCruiserWasHit = false;
            leftPlayerPatrolBoatWasHit = false;
            rightSideCheckL = true;
            leftSideCheckL = false;
            upSideCheckL = false;
            downSideCheckL = false;
            breakBoolL = false;
            i = 1;
            numberOfBoatsThatPlayerRightHit++;
        }

        /// <summary>
        /// this function checks if points of the left side of the ship are part of the ship and hits it,
        ///  function for left board
        /// </summary>
        private void checkRightSideOfTheShip(MyPoint shotPoint, int numberOfCellsInShip)
        {
            if (leftPlayerLastHitPoint.getX() + i <= 9)
            {
                if (board1[leftPlayerLastHitPoint.getY(), leftPlayerLastHitPoint.getX() + i] == 2)
                {
                    board1[leftPlayerLastHitPoint.getY(), leftPlayerLastHitPoint.getX() + i] = 3;
                    numberOfPointsHitL++;
                    removeRecordFromListOfPoints(new MyPoint(leftPlayerLastHitPoint.getY(), leftPlayerLastHitPoint.getX() + i));
                    i++;
                    breakBoolL = true;
                    if (numberOfPointsHitL == numberOfCellsInShip)
                    {
                        clearCellsNextToShipHorizontal(numberOfCellsInShip);
                        restoreDefaultBoolSettings();
                    }
                }
                else
                {
                    if (board1[leftPlayerLastHitPoint.getY(), leftPlayerLastHitPoint.getX() + i] == 0)
                    {
                        board1[leftPlayerLastHitPoint.getY(), leftPlayerLastHitPoint.getX() + i] = 1;
                        removeRecordFromListOfPoints(new MyPoint(leftPlayerLastHitPoint.getY(), leftPlayerLastHitPoint.getX() + i));
                    }
                    rightSideCheckL = false;
                    leftSideCheckL = true;
                    i = 1;
                    breakBoolL = true;
                }
            }
            else
            {
                rightSideCheckL = false;
                leftSideCheckL = true;
                i = 1;
            }
        }
        /// <summary>
        /// this function checks if points of the right side of the ship are part of the ship and hits it,
        ///  function for left board
        /// </summary>
        private void checkLeftSideOfTheShip(MyPoint shotPoint, int numberOfCellsInShip)
        {
            if (leftPlayerLastHitPoint.getX() - i >= 0)
            {
                if (board1[leftPlayerLastHitPoint.getY(), leftPlayerLastHitPoint.getX() - i] == 2)
                {
                    board1[leftPlayerLastHitPoint.getY(), leftPlayerLastHitPoint.getX() - i] = 3;
                    startingPointOfClearing = new MyPoint(leftPlayerLastHitPoint.getY(), leftPlayerLastHitPoint.getX() - i);
                    numberOfPointsHitL++;
                    removeRecordFromListOfPoints(new MyPoint(leftPlayerLastHitPoint.getY(), leftPlayerLastHitPoint.getX() - i));
                    i++;
                    breakBoolL = true;
                    if (numberOfPointsHitL == numberOfCellsInShip)
                    {
                        clearCellsNextToShipHorizontal(numberOfCellsInShip);
                        restoreDefaultBoolSettings();
                    }
                }
                else
                {
                    if (board1[leftPlayerLastHitPoint.getY(), leftPlayerLastHitPoint.getX() - i] == 0)
                    {
                        board1[leftPlayerLastHitPoint.getY(), leftPlayerLastHitPoint.getX() - i] = 1;
                        removeRecordFromListOfPoints(new MyPoint(leftPlayerLastHitPoint.getY(), leftPlayerLastHitPoint.getX() - i));
                    }
                    rightSideCheckL = false;
                    leftSideCheckL = false;
                    downSideCheckL = true;
                    i = 1;
                    breakBoolL = true;
                }
            }
            else
            {
                rightSideCheckL = false;
                leftSideCheckL = false;
                downSideCheckL = true;
                i = 1;
            }
        }
        /// <summary>
        /// this function checks if points of the down side of the ship are part of the ship and hits it,
        ///  function for left board
        /// </summary>
        private void checkDownSideOfTheShip(MyPoint shotPoint, int numberOfCellsInShip)
        {
            if (leftPlayerLastHitPoint.getY() + i <= 9)
            {
                if (board1[leftPlayerLastHitPoint.getY() + i, leftPlayerLastHitPoint.getX()] == 2)
                {
                    board1[leftPlayerLastHitPoint.getY() + i, leftPlayerLastHitPoint.getX()] = 3;
                    numberOfPointsHitL++;
                    removeRecordFromListOfPoints(new MyPoint(leftPlayerLastHitPoint.getY() + i, leftPlayerLastHitPoint.getX()));
                    i++;
                    breakBoolL = true;
                    if (numberOfPointsHitL == numberOfCellsInShip)
                    {
                        clearCellsNextToShipVertical(numberOfCellsInShip);
                        restoreDefaultBoolSettings();
                    }
                }
                else
                {
                    if (board1[leftPlayerLastHitPoint.getY() + i, leftPlayerLastHitPoint.getX()] == 0)
                    {
                        board1[leftPlayerLastHitPoint.getY() + i, leftPlayerLastHitPoint.getX()] = 1;
                        removeRecordFromListOfPoints(new MyPoint(leftPlayerLastHitPoint.getY() + i, leftPlayerLastHitPoint.getX()));
                    }
                    rightSideCheckL = false;
                    leftSideCheckL = false;
                    downSideCheckL = false;
                    upSideCheckL = true;
                    i = 1;
                    breakBoolL = true;
                }
            }
            else
            {
                rightSideCheckL = false;
                leftSideCheckL = false;
                downSideCheckL = false;
                upSideCheckL = true;
                i = 1;
            }
        }
        /// <summary>
        /// this function checks if points of the upper side of the ship are part of the ship and hits it,
        ///  function for left board
        /// </summary>
        private void checkUpperSideOfTheShip(MyPoint shotPoint, int numberOfCellsInShip)
        {
            if (leftPlayerLastHitPoint.getY() - i >= 0)
            {
                if (board1[leftPlayerLastHitPoint.getY() - i, leftPlayerLastHitPoint.getX()] == 2)
                {
                    board1[leftPlayerLastHitPoint.getY() - i, leftPlayerLastHitPoint.getX()] = 3;
                    startingPointOfClearing = new MyPoint(leftPlayerLastHitPoint.getY() - i, leftPlayerLastHitPoint.getX());
                    numberOfPointsHitL++;
                    removeRecordFromListOfPoints(new MyPoint(leftPlayerLastHitPoint.getY() - i, leftPlayerLastHitPoint.getX()));
                    i++;
                    breakBoolL = true;
                    if (numberOfPointsHitL == numberOfCellsInShip)
                    {
                        clearCellsNextToShipVertical(numberOfCellsInShip);
                        restoreDefaultBoolSettings();
                    }
                }
                else
                {
                    if (board1[leftPlayerLastHitPoint.getY() - i, leftPlayerLastHitPoint.getX()] == 0)
                    {
                        board1[leftPlayerLastHitPoint.getY() - i, leftPlayerLastHitPoint.getX()] = 1;
                        removeRecordFromListOfPoints(new MyPoint(leftPlayerLastHitPoint.getY() - i, leftPlayerLastHitPoint.getX()));
                    }
                    rightSideCheckL = false;
                    leftSideCheckL = false;
                    downSideCheckL = false;
                    upSideCheckL = false;
                    i = 1;
                    breakBoolL = true;
                }
            }
            else
            {
                rightSideCheckL = false;
                leftSideCheckL = false;
                downSideCheckL = false;
                upSideCheckL = false;
                i = 1;
            }

        }
        /// <summary>
        /// this function checks if the battleship of left player was hit 
        /// </summary>
        private void checkIfBattleshipWasHit(MyPoint shotPoint)
        {
            if (leftPlayerBattleshipWasHit == true)
            {

                if (rightSideCheckL == true)
                {
                    checkRightSideOfTheShip(shotPoint, 4);
                    if (breakBoolL == true)
                    {
                        breakBoolL = false;
                        goto END;
                    }
                }
                if (leftSideCheckL == true)
                {
                    checkLeftSideOfTheShip(shotPoint, 4);
                    if (breakBoolL == true)
                    {
                        breakBoolL = false;
                        goto END;
                    }
                }
                if (downSideCheckL == true)
                {
                    checkDownSideOfTheShip(shotPoint, 4);
                    if (breakBoolL == true)
                    {
                        breakBoolL = false;
                        goto END;
                    }
                }
                if (upSideCheckL == true)
                {
                    checkUpperSideOfTheShip(shotPoint, 4);
                    if (breakBoolL == true)
                    {
                        breakBoolL = false;
                        goto END;
                    }
                }
            END: Console.WriteLine("breakBool == true");

            }
            else
            {
                foreach (MyPoint myPoint in leftPlayerBattleshipPoints)
                {
                    if (board1[shotPoint.getY(), shotPoint.getX()] == 2 && myPoint.getX() == shotPoint.getX() && myPoint.getY() == shotPoint.getY())
                    {
                        board1[shotPoint.getY(), shotPoint.getX()] = 3;
                        Console.WriteLine("X====>" + shotPoint.getX() + "Y=====>" + shotPoint.getY());
                        removeRecordFromListOfPoints(shotPoint);
                        leftPlayerBattleshipWasHit = true;
                        startingPointOfClearing = shotPoint;
                        leftPlayerLastHitPoint = shotPoint;
                        numberOfPointsHitL++;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// if the left player ship has been sunk, this function places an X on the cells that are on 
        /// upper, left, right and down side of the ship, this function works for ships that are horizontal
        /// </summary>
        private void clearCellsNextToShipHorizontal(int numberOfCellsThatShipIsContaining)
        {
            if (startingPointOfClearing.getX() - 1 >= 0)
            {
                board1[startingPointOfClearing.getY(), startingPointOfClearing.getX() - 1] = 1;
                removeRecordFromListOfPoints(new MyPoint(startingPointOfClearing.getY(), startingPointOfClearing.getX() - 1));
            }
            if (startingPointOfClearing.getX() + numberOfCellsThatShipIsContaining <= 9)
            {
                board1[startingPointOfClearing.getY(), startingPointOfClearing.getX() + numberOfCellsThatShipIsContaining] = 1;
                removeRecordFromListOfPoints(new MyPoint(startingPointOfClearing.getY(), startingPointOfClearing.getX() + numberOfCellsThatShipIsContaining));
            }
            if (startingPointOfClearing.getY() - 1 >= 0)
            {
                for (int h = 0; h < numberOfCellsThatShipIsContaining; h++)
                {
                    board1[startingPointOfClearing.getY() - 1, startingPointOfClearing.getX() + h] = 1;
                    removeRecordFromListOfPoints(new MyPoint(startingPointOfClearing.getY() - 1, startingPointOfClearing.getX() + h));
                }
            }

            if (startingPointOfClearing.getY() + 1 <= 9)
            {
                for (int h = 0; h < numberOfCellsThatShipIsContaining; h++)
                {
                    board1[startingPointOfClearing.getY() + 1, startingPointOfClearing.getX() + h] = 1;
                    removeRecordFromListOfPoints(new MyPoint(startingPointOfClearing.getY() + 1, startingPointOfClearing.getX() + h));
                }

            }
        }

        /// <summary>
        /// if the left player ship has been sunk, this function places an X on the cells that are on 
        /// upper, left, right and down side of the ship, this function works for ships that are vertical
        /// </summary>
        private void clearCellsNextToShipVertical(int numberOfCellsThatShipIsContaining)
        {
            if (startingPointOfClearing.getY() - 1 >= 0)
            {
                if (board1[startingPointOfClearing.getY() - 1, startingPointOfClearing.getX()] == 0)
                {
                    board1[startingPointOfClearing.getY() - 1, startingPointOfClearing.getX()] = 1;
                    removeRecordFromListOfPoints(new MyPoint(startingPointOfClearing.getY() - 1, startingPointOfClearing.getX()));
                }
            }

            if (startingPointOfClearing.getY() + numberOfCellsThatShipIsContaining <= 9)
            {
                if (board1[startingPointOfClearing.getY() + numberOfCellsThatShipIsContaining, startingPointOfClearing.getX()] == 0)
                {
                    board1[startingPointOfClearing.getY() + numberOfCellsThatShipIsContaining, startingPointOfClearing.getX()] = 1;
                    removeRecordFromListOfPoints(new MyPoint(startingPointOfClearing.getY() + 1, startingPointOfClearing.getX()));
                }
            }

            if (startingPointOfClearing.getX() - 1 >= 0)
            {
                for (int h = 0; h < numberOfCellsThatShipIsContaining; h++)
                {
                    if (board1[startingPointOfClearing.getY() + h, startingPointOfClearing.getX() - 1] == 0)
                    {
                        board1[startingPointOfClearing.getY() + h, startingPointOfClearing.getX() - 1] = 1;
                        removeRecordFromListOfPoints(new MyPoint(startingPointOfClearing.getY() + h, startingPointOfClearing.getX() - 1));
                    }
                }
            }
            if (startingPointOfClearing.getX() + 1 <= 9)
            {
                for (int h = 0; h < numberOfCellsThatShipIsContaining; h++)
                {
                    if (board1[startingPointOfClearing.getY() + h, startingPointOfClearing.getX() + 1] == 0)
                    {
                        board1[startingPointOfClearing.getY() + h, startingPointOfClearing.getX() + 1] = 1;
                        removeRecordFromListOfPoints(new MyPoint(startingPointOfClearing.getY() + h, startingPointOfClearing.getX() + 1));
                    }
                }
            }

        }

        /// <summary>
        /// this function checks if the cruiser of left player was hit 
        /// </summary>
        private void checkIfCruiserWasHit(MyPoint shotPoint)
        {
            if (leftPlayerCruiserWasHit == true)
            {
                if (rightSideCheckL == true)
                {
                    checkRightSideOfTheShip(shotPoint, 3);
                    if (breakBoolL == true)
                    {
                        breakBoolL = false;
                        goto END;
                    }
                }
                if (leftSideCheckL == true)
                {
                    checkLeftSideOfTheShip(shotPoint, 3);
                    if (breakBoolL == true)
                    {
                        breakBoolL = false;
                        goto END;
                    }
                }
                if (downSideCheckL == true)
                {
                    checkDownSideOfTheShip(shotPoint, 3);
                    if (breakBoolL == true)
                    {
                        breakBoolL = false;
                        goto END;
                    }
                }
                if (upSideCheckL == true)
                {
                    checkUpperSideOfTheShip(shotPoint, 3);
                    if (breakBoolL == true)
                    {
                        breakBoolL = false;
                        goto END;
                    }
                }
            END: Console.WriteLine("breakBool == true");

            }
            else
            {
                foreach (List<MyPoint> list in leftPlayerCruisersPoints)
                    foreach (MyPoint myPoint in list)
                    {
                        if (board1[shotPoint.getY(), shotPoint.getX()] == 2 && myPoint.getX() == shotPoint.getX() && myPoint.getY() == shotPoint.getY())
                        {
                            board1[shotPoint.getY(), shotPoint.getX()] = 3;
                            Console.WriteLine("X====>" + shotPoint.getX() + "Y=====>" + shotPoint.getY());
                            removeRecordFromListOfPoints(shotPoint);
                            leftPlayerCruiserWasHit = true;
                            startingPointOfClearing = shotPoint;
                            leftPlayerLastHitPoint = shotPoint;
                            numberOfPointsHitL++;
                            return;
                        }
                    }
            }
        }
        /// <summary>
        /// this function checks if the patrol boat of left player was hit 
        /// </summary>
        private void checkIfPatrolBoatWasHit(MyPoint shotPoint)
        {

            if (leftPlayerPatrolBoatWasHit == true)
            {
                if (rightSideCheckL == true)
                {
                    checkRightSideOfTheShip(shotPoint, 2);
                    if (breakBoolL == true)
                    {
                        breakBoolL = false;
                        goto END;
                    }
                }
                if (leftSideCheckL == true)
                {
                    checkLeftSideOfTheShip(shotPoint, 2);
                    if (breakBoolL == true)
                    {
                        breakBoolL = false;
                        goto END;
                    }
                }
                if (downSideCheckL == true)
                {
                    checkDownSideOfTheShip(shotPoint, 2);
                    if (breakBoolL == true)
                    {
                        breakBoolL = false;
                        goto END;
                    }
                }
                if (upSideCheckL == true)
                {
                    checkUpperSideOfTheShip(shotPoint, 2);
                    if (breakBoolL == true)
                    {
                        breakBoolL = false;
                        goto END;
                    }
                }
            END: Console.WriteLine("breakBool == true");
            }
            else
            {
                foreach (List<MyPoint> list in leftPlayerPatrolBoatsPoints)
                    foreach (MyPoint myPoint in list)
                    {
                        if (board1[shotPoint.getY(), shotPoint.getX()] == 2 && myPoint.getX() == shotPoint.getX() && myPoint.getY() == shotPoint.getY())
                        {
                            board1[shotPoint.getY(), shotPoint.getX()] = 3;
                            Console.WriteLine("X====>" + shotPoint.getX() + "Y=====>" + shotPoint.getY());
                            removeRecordFromListOfPoints(shotPoint);
                            leftPlayerPatrolBoatWasHit = true;
                            leftPlayerLastHitPoint = shotPoint;
                            startingPointOfClearing = shotPoint;
                            numberOfPointsHitL++;
                            return;
                        }
                    }
            }

        }

        /// <summary>
        /// this function checks if the dinghy of left player was hit 
        /// </summary>
        private void checkIfLeftPlayerDinghyWasHit(MyPoint shotPoint)
        {
            foreach (MyPoint myPoint in leftPlayerDinghtiesPoints)
            {
                if (board1[shotPoint.getY(), shotPoint.getX()] == 2 && myPoint.getX() == shotPoint.getX() && myPoint.getY() == shotPoint.getY())
                {
                    if (shotPoint.getX() - 1 >= 0)
                    {
                        board1[shotPoint.getY(), shotPoint.getX() - 1] = 1;
                        removeRecordFromListOfPoints(new MyPoint(shotPoint.getY(), shotPoint.getX() - 1));
                    }
                    if (shotPoint.getX() + 1 <= 9)
                    {
                        board1[shotPoint.getY(), shotPoint.getX() + 1] = 1;
                        removeRecordFromListOfPoints(new MyPoint(shotPoint.getY(), shotPoint.getX() + 1));
                    }
                    if (shotPoint.getY() + 1 <= 9)
                    {
                        board1[shotPoint.getY() + 1, shotPoint.getX()] = 1;
                        removeRecordFromListOfPoints(new MyPoint(shotPoint.getY() + 1, shotPoint.getX()));
                    }
                    if (shotPoint.getY() - 1 >= 0)
                    {
                        board1[shotPoint.getY() - 1, shotPoint.getX()] = 1;
                        removeRecordFromListOfPoints(new MyPoint(shotPoint.getY() - 1, shotPoint.getX()));
                    }

                    board1[shotPoint.getY(), shotPoint.getX()] = 3;
                    Console.WriteLine("X====>" + shotPoint.getX() + "Y=====>" + shotPoint.getY());
                    removeRecordFromListOfPoints(shotPoint);
                    numberOfBoatsThatPlayerRightHit++;
                }
                else if (board1[shotPoint.getY(), shotPoint.getX()] == 0)
                {
                    board1[shotPoint.getY(), shotPoint.getX()] = 1;
                    removeRecordFromListOfPoints(shotPoint);
                }
            }
        }

        /// <summary>
        /// if one point was hit then this function removes that point form list of all points that are on the board
        /// </summary>
        private void removeRecordFromListOfPoints(MyPoint point)
        {
            MyPoint temp = pointsLeftPlayer.Find(x => x.getX() == point.getX() && x.getY() == point.getY());
            pointsLeftPlayer.Remove(temp);
        }

        #endregion ShotsAtLeftPlayersBoard
        /// <summary>
        /// this function clears the table, it is used when new game sets up
        /// </summary>
        private void clearTable(int[,] table)
        {
            for (int i = 0; i <= 9; i++)
                for (int j = 0; j <= 9; j++)
                {
                    table[i, j] = 0;
                }
        }

        /// <summary>
        /// this function clears localization points of the left player ships and points that weren't hit 
        /// </summary>
        private void clearPositionsOfLeftPlayer()
        {
            pointsLeftPlayer.Clear();
            leftPlayerBattleshipPoints.Clear();
            leftPlayerCruisersPoints.Clear();
            leftPlayerDinghtiesPoints.Clear();
            leftPlayerPatrolBoatsPoints.Clear();
        }
        /// <summary>
        /// this function clears localization points of the right player ships and points that weren't hit 
        /// </summary>
        private void clearPositionsOfRightPlayer()
        {
            pointsRightPlayer.Clear();
            rightPlayerBattleshipPoints.Clear();
            rightPlayerCruisersPoints.Clear();
            rightPlayerDinghtiesPoints.Clear();
            rightPlayerPatrolBoatsPoints.Clear();
        }
        /// <summary>
        /// fills the list with all the points that are on game board, the list is used to choose random point to hit, left player
        /// </summary>
        private void fillLeftListPoint()
        {
            for (int i = 0; i <= 9; i++)
                for (int j = 0; j <= 9; j++)
                    pointsLeftPlayer.Add(new MyPoint(i, j));

        }

        /// <summary>
        /// fills the list with all the points that are on game board, the list is used to choose random point to hit, right player
        /// </summary>
        private void fillRightListPoint()
        {
            for (int i = 0; i <= 9; i++)
                for (int j = 0; j <= 9; j++)
                    pointsRightPlayer.Add(new MyPoint(i, j));
        }
        #endregion privateFunctions

        #region publicFunctions
        /// <summary>
        /// this function sets up new game for left player and returns list with points 
        /// </summary>
        public ListOfCells setUpNewGameForLeftPlayer()
        {
            numberOfBoatsThatPlayerLeftHit = 0;
            clearPositionsOfLeftPlayer();
            fillLeftListPoint();
            clearTable(board1);
            setUpBoard();
            return listOfCells1;
        }
        /// <summary>
        /// this function sets up new game for right player and returns list with points 
        /// </summary>
        public ListOfCells setUpNewGameForRightPlayer()
        {
            numberOfBoatsThatPlayerRightHit = 0;
            clearPositionsOfRightPlayer();
            fillRightListPoint();
            clearTable(board2);
            setUpBoard2();
            return listOfCells2;
        }

        /// <summary>
        /// this function performs shot on left player game board
        /// </summary>
        public ListOfCells oneShotLeftPlayer()
        {
            checkShotLeft(randX.Next(pointsLeftPlayer.Count));
            listOfCells1 = setList(listOfCells1, board1);
            return listOfCells1;
        }
        /// <summary>
        /// this function performs shot on right player game board
        /// </summary>
        /// <returns></returns>
        public ListOfCells oneShotRightPlayer()
        {
            checkShotRight(randY.Next(pointsRightPlayer.Count));
            listOfCells2 = setList(listOfCells2, board2);
            return listOfCells2;
        }
/// <summary>
/// this function checks if there is a winner of the game and posiibly returns player that won
/// </summary>
        public List<int> getWinnerOfTheGame()
        {
            if (numberOfBoatsThatPlayerRightHit == 10)
            {
                return new List<int> { 2 };
            }
            if (numberOfBoatsThatPlayerLeftHit == 10)
            {
                return new List<int> { 1 };
            }

            return new List<int> { };
        }

        #endregion publicFunctions

    }
}