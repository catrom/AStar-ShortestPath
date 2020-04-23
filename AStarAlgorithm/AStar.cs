using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStarAlgorithm
{
    // Creating a shortcut for int, int pair type 
    using Pair = KeyValuePair<int, int>;

    // Creating a shortcut for pair<int, pair<int, int>> type 
    using pPair = KeyValuePair<double, KeyValuePair<int, int>>;

    // Creating a shortcut for step by step tracking, {(row, col), (parent_row, parent_col)}
    using ppPair = KeyValuePair<KeyValuePair<int, int>, KeyValuePair<int, int>>;

    class AStar
    {
        private int rows;
        private int cols;
        private Stack<Pair> result = new Stack<Pair>();
        private bool allowDiagonalMove = true;
        public enum Heuristics:int { Manhattan, Diagonal, Euclidean };
        private int heuristicModel = (int)Heuristics.Manhattan; // by default, it will be set to Manhattan model

        List<List<int>> arrayTraverse; // array store 2D index to traverse for finding cell's neighbour
        List<ppPair> cellTracking = new List<ppPair>(); // array store all the tracked cells

        // for showing step by step, set to default
        private bool goStepByStep = false;

        // Declare a 2D array of structure to hold the details 
        //of that cell 
        cell[,] cellDetails;

        // Create a closed list and initialise it to false which means 
        // that no cell has been included yet 
        // This closed list is implemented as a boolean 2D array with all default value were set to false
        bool[,] closedList = new bool[0, 0];

        /*
        Create an open list having information as-
        <f, <i, j>>
        where f = g + h,
        and i, j are the rows and column index of that cell
        Note that 0 <= i <= rows-1 & 0 <= j <= COL-1
        This open list is implenented as a set of pair of pair.*/
        HashSet<pPair> openList = new HashSet<pPair>();

        // We set this boolean value as false as initially 
        // the destination is not reached. 
        bool foundDest;

        // 
        bool isInitAstar = false;

        public int Rows { get => rows; set => rows = value; }
        public int Cols { get => cols; set => cols = value; }
        public Stack<Pair> Result { get => result; set => result = value; }
        public bool AllowDiagonalMove { get => allowDiagonalMove; set => allowDiagonalMove = value; }
        public int HeuristicModel { get => heuristicModel; set => heuristicModel = value; }
        public bool GoStepByStep { get => goStepByStep; set => goStepByStep = value; }
        private cell[,] CellDetails { get => cellDetails; set => cellDetails = value; }
        public bool[,] ClosedList { get => closedList; set => closedList = value; }
        public HashSet<pPair> OpenList { get => openList; set => openList = value; }
        public bool FoundDest { get => foundDest; set => foundDest = value; }
        public bool IsInitAstar { get => isInitAstar; set => isInitAstar = value; }
        public List<ppPair> CellTracking { get => cellTracking; set => cellTracking = value; }

        public void ResetOpenList()
        {
            openList = new HashSet<pPair>();
        }

        public void ResetCloseList()
        {
            closedList = new bool[0, 0];
        }

        public void ResetCellTracking()
        {
            cellTracking = new List<ppPair>();
        }

        // A structure to hold the neccesary parameters 
        struct cell
        {
            // Row and Column index of its parent 
            // Note that 0 <= i <= ROW-1 & 0 <= j <= COL-1 
            int parent_i, parent_j;

            // f = g + h 
            double f, g, h;

            public int Parent_i { get => parent_i; set => parent_i = value; }
            public int Parent_j { get => parent_j; set => parent_j = value; }
            public double F { get => f; set => f = value; }
            public double G { get => g; set => g = value; }
            public double H { get => h; set => h = value; }
        };

        // A Utility Function to check whether given cell (row, col) 
        // is a valid cell or not. 
        bool isValid(int row, int col)
        {
            // Returns true if row number and column number 
            // is in range 
            return (row >= 0) && (row < Rows) &&
                (col >= 0) && (col < Cols);
        }

        // A Utility Function to check whether the given cell is 
        // blocked or not 
        bool isUnBlocked(int[,] grid, int row, int col)
        {
            // Returns true if the cell is not blocked else false 
            if (grid[row, col]== 1)
                return (true);
            else
                return (false);
        }

        // A Utility Function to check whether destination cell has 
        // been reached or not 
        bool isDestination(int row, int col, Pair dest)
        {
            if (row == dest.Key && col == dest.Value)
                return (true);
            else
                return (false);
        }

        // A Utility Function to calculate the 'h' heuristics. 
        double calculateHValue(int row, int col, Pair dest)
        {
            // Return using the distance formula 
            if (heuristicModel == (int)Heuristics.Manhattan) 
            {
                return Math.Abs(row - dest.Key) + Math.Abs(col - dest.Value);    
            }

            if (heuristicModel == (int)Heuristics.Diagonal)
            {

                return Math.Max(Math.Abs(row - dest.Key), Math.Abs(col - dest.Value));
            }

            if (heuristicModel == (int)Heuristics.Euclidean)
            {
                return ((double)Math.Sqrt((row - dest.Key) * (row - dest.Key)
                    + (col - dest.Value) * (col - dest.Value)));
            }

            return 0; // by default, this is Dijkstra
        }

        // A Utility Function to trace the path from the source 
        // to destination 
        void tracePath(cell[,] cellDetails, Pair dest)
        {
            int row = dest.Key;
            int col = dest.Value;

            Stack<Pair> Path = new Stack<Pair>();

            while (!(cellDetails[row, col].Parent_i == row
                && cellDetails[row, col].Parent_j == col))
            {
                Path.Push(new Pair(row, col));
                int temp_row = cellDetails[row, col].Parent_i;
                int temp_col = cellDetails[row, col].Parent_j;
                row = temp_row;
                col = temp_col;
            }

            Path.Push(new Pair(row, col));
            while (Path.Count() > 0)
            {
                Pair p = Path.Pop();
                result.Push(p);
            }

            return;
        }

        public void getArrayTraverse()
        {
            arrayTraverse = new List<List<int>>();
            arrayTraverse.Add(new List<int> { -1, 0 }); // north
            arrayTraverse.Add(new List<int> { 1, 0 }); // south
            arrayTraverse.Add(new List<int> { 0, 1 }); // east
            arrayTraverse.Add(new List<int> { 0, -1 }); // west

            if (allowDiagonalMove)
            {
                arrayTraverse.Add(new List<int> { -1, 1 }); // north-east
                arrayTraverse.Add(new List<int> { -1, -1 }); // north-west
                arrayTraverse.Add(new List<int> { 1, 1 }); // south-east
                arrayTraverse.Add(new List<int> { 1, -1 }); // south-west
            }
        }

        // Some inits and config before start A* Search
        public bool HandleBeforeSearching(int[,] grid, Pair src, Pair dest)
        {
            // if all is set, just return
            if (isInitAstar) return true;

            // Clear the result stack
            Result.Clear();

            // clear the list tracked cells
            cellTracking.Clear();

            // If the source is out of range 
            if (isValid(src.Key, src.Value) == false)
            {
                MessageBox.Show("Source is invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // If the destination is out of range 
            if (isValid(dest.Key, dest.Value) == false)
            {
                MessageBox.Show("Destination is invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Either the source or the destination is blocked 
            if (isUnBlocked(grid, src.Key, src.Value) == false ||
                isUnBlocked(grid, dest.Key, dest.Value) == false)
            {
                MessageBox.Show("Source or destination is blocked!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // If the destination cell is the same as source cell 
            if (isDestination(src.Key, src.Value, dest) == true)
            {
                MessageBox.Show("We are already at the destination!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            closedList = new bool[rows, cols];
            cellDetails = new cell[rows, cols];

            int i, j;

            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < cols; j++)
                {
                    cellDetails[i, j].F = float.MaxValue;
                    cellDetails[i, j].G = float.MaxValue;
                    cellDetails[i, j].H = float.MaxValue;
                    cellDetails[i, j].Parent_i = -1;
                    cellDetails[i, j].Parent_j = -1;
                }
            }

            // Initialising the parameters of the starting node 
            i = src.Key; j = src.Value;
            cellDetails[i, j].F = 0.0;
            cellDetails[i, j].G = 0.0;
            cellDetails[i, j].H = 0.0;
            cellDetails[i, j].Parent_i = i;
            cellDetails[i, j].Parent_j = j;

            openList = new HashSet<pPair>();

            // Put the starting cell on the open list and set its 
            // 'f' as 0 
            openList.Add(new pPair(0.0, new Pair(i, j)));

            foundDest = false;

            // update this
            isInitAstar = true;
            return true; // can start search
        }

        // A Function to find the shortest path between 
        // a given source cell to a destination cell according 
        // to A* Search Algorithm 
        public void aStarSearch(int[,] grid, Pair src, Pair dest)
        {
            int i = src.Key, j = src.Value;
            // Get Array Traversing Direction
            getArrayTraverse();

            while (openList.Count() > 0)
            {
                pPair p = openList.First();

                // Remove this vertex from the open list 
                openList.Remove(openList.First());

                // Add this vertex to the closed list 
                i = p.Value.Key;
                j = p.Value.Value;
                closedList[i, j] = true;

                // update list tracked cell
                int p_parent_row = cellDetails[i, j].Parent_i;
                int p_parent_col = cellDetails[i, j].Parent_j;
                cellTracking.Add(new ppPair(new Pair(i, j), new Pair(p_parent_row, p_parent_col)));

                // If the destination cell is the same as the 
                // current successor 
                //if (isDestination(i, j, dest) == true)
                //{
                //    //// Set the Parent of the destination cell 
                //    //cellDetails[neighbourCell_i, neighbourCell_j].Parent_i = i;
                //    //cellDetails[neighbourCell_i, neighbourCell_j].Parent_j = j;
                //    MessageBox.Show("The destination cell is found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    tracePath(cellDetails, dest);
                //    foundDest = true;
                //    return;
                //}

                // To store the 'g', 'h' and 'f' of the 8 successors 
                double gNew, hNew, fNew;

                foreach (List<int> dir in arrayTraverse)
                {
                    int neighbourCell_i = i + dir[0];
                    int neighbourCell_j = j + dir[1];

                    // Only process this cell if this is a valid one 
                    if (isValid(neighbourCell_i, neighbourCell_j) == true)
                    {
                        if (isDestination(neighbourCell_i, neighbourCell_j, dest) == true)
                        {
                            // Set the Parent of the destination cell 
                            cellDetails[neighbourCell_i, neighbourCell_j].Parent_i = i;
                            cellDetails[neighbourCell_i, neighbourCell_j].Parent_j = j;
                            cellTracking.Add(new ppPair(new Pair(neighbourCell_i, neighbourCell_j), new Pair(i, j)));
                            
                            if (!goStepByStep)
                                MessageBox.Show("The destination cell is found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tracePath(cellDetails, dest);
                            foundDest = true;
                            return;
                        }

                        // If the successor is already on the closed 
                        // list or if it is blocked, then ignore it. 
                        // Else do the following 
                        if (closedList[neighbourCell_i, neighbourCell_j] == false &&
                            isUnBlocked(grid, neighbourCell_i, neighbourCell_j) == true)
                        {
                            gNew = cellDetails[i, j].G + 1.0;
                            hNew = calculateHValue(neighbourCell_i, neighbourCell_j, dest);
                            fNew = gNew + hNew;

                            // If it isn’t on the open list, add it to 
                            // the open list. Make the current square 
                            // the parent of this square. Record the 
                            // f, g, and h costs of the square cell 
                            //                OR 
                            // If it is on the open list already, check 
                            // to see if this path to that square is better, 
                            // using 'f' cost as the measure. 
                            if (cellDetails[neighbourCell_i, neighbourCell_j].F == float.MaxValue ||
                                cellDetails[neighbourCell_i, neighbourCell_j].F > fNew)
                            {
                                openList.Add(new pPair(fNew, new Pair(neighbourCell_i, neighbourCell_j)));

                                // Update the details of this cell 
                                cellDetails[neighbourCell_i, neighbourCell_j].F = fNew;
                                cellDetails[neighbourCell_i, neighbourCell_j].G = gNew;
                                cellDetails[neighbourCell_i, neighbourCell_j].H = hNew;
                                cellDetails[neighbourCell_i, neighbourCell_j].Parent_i = i;
                                cellDetails[neighbourCell_i, neighbourCell_j].Parent_j = j;
                            }
                        }
                    }
                }

                // cannot go next until user click Go Next Button
                if (goStepByStep) return;
            }

            // When the destination cell is not found and the open 
            // list is empty, then we conclude that we failed to 
            // reach the destiantion cell. This may happen when the 
            // there is no way to destination cell (due to blockages) 
            if (foundDest == false)
            {
                MessageBox.Show("Failed to find the Destination Cell!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result.Clear();
            }

            return;
        }
    }
}
