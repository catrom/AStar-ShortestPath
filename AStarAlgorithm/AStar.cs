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

    class AStar
    {
        private int rows;
        private int cols;
        private Stack<Pair> result = new Stack<Pair>();
        private bool allowDiagonalMove = true;

        public int Rows { get => rows; set => rows = value; }
        public int Cols { get => cols; set => cols = value; }
        public Stack<Pair> Result { get => result; set => result = value; }
        public bool AllowDiagonalMove { get => allowDiagonalMove; set => allowDiagonalMove = value; }

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

            //// Euclid
            //return ((double)Math.Sqrt((row - dest.Key) * (row - dest.Key)
            //    + (col - dest.Value) * (col - dest.Value)));

            // Manhattan
            return Math.Abs(row - dest.Key) + Math.Abs(col - dest.Value);
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

        // A Function to find the shortest path between 
        // a given source cell to a destination cell according 
        // to A* Search Algorithm 
        public void aStarSearch(int[,] grid, Pair src, Pair dest)
        {
            // Clear the result stack
            Result.Clear();

            // If the source is out of range 
            if (isValid(src.Key, src.Value) == false)
            {
                MessageBox.Show("Source is invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // If the destination is out of range 
            if (isValid(dest.Key, dest.Value) == false)
            {
                MessageBox.Show("Destination is invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Either the source or the destination is blocked 
            if (isUnBlocked(grid, src.Key, src.Value) == false ||
                isUnBlocked(grid, dest.Key, dest.Value) == false)
            {
                MessageBox.Show("Source or destination is blocked!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // If the destination cell is the same as source cell 
            if (isDestination(src.Key, src.Value, dest) == true)
            {
                MessageBox.Show("We are already at the destination!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Create a closed list and initialise it to false which means 
            // that no cell has been included yet 
            // This closed list is implemented as a boolean 2D array with all default value were set to false
            bool[,] closedList = new bool[rows, cols];

            // Declare a 2D array of structure to hold the details 
            //of that cell 
            cell[,] cellDetails = new cell[rows, cols];

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

            /*
             Create an open list having information as-
             <f, <i, j>>
             where f = g + h,
             and i, j are the rows and column index of that cell
             Note that 0 <= i <= rows-1 & 0 <= j <= COL-1
             This open list is implenented as a set of pair of pair.*/
            HashSet<pPair> openList = new HashSet<pPair>();

            // Put the starting cell on the open list and set its 
            // 'f' as 0 
            openList.Add(new pPair(0.0, new Pair(i, j)));

            // We set this boolean value as false as initially 
            // the destination is not reached. 
            bool foundDest = false;

            while (openList.Count() > 0)
            {
                pPair p = openList.First();

                // Remove this vertex from the open list 
                openList.Remove(openList.First());

                // Add this vertex to the closed list 
                i = p.Value.Key;
                j = p.Value.Value;
                closedList[i, j] = true;

                /*
                 Generating all the 8 successor of this cell

                     N.W   N   N.E
                       \   |   /
                        \  |  /
                     W----Cell----E
                          / | \
                        /   |  \
                     S.W    S   S.E

                 Cell-->Popped Cell (i, j)
                 N -->  North       (i-1, j)
                 S -->  South       (i+1, j)
                 E -->  East        (i, j+1)
                 W -->  West        (i, j-1)
                 N.E--> North-East  (i-1, j+1)
                 N.W--> North-West  (i-1, j-1)
                 S.E--> South-East  (i+1, j+1)
                 S.W--> South-West  (i+1, j-1)*/

                 // To store the 'g', 'h' and 'f' of the 8 successors 
                double gNew, hNew, fNew;

                //----------- 1st Successor (North) ------------ 

                // Only process this cell if this is a valid one 
                if (isValid(i - 1, j) == true)
                {
                    // If the destination cell is the same as the 
                    // current successor 
                    if (isDestination(i - 1, j, dest) == true)
                    {
                        // Set the Parent of the destination cell 
                        cellDetails[i - 1, j].Parent_i = i;
                        cellDetails[i - 1, j].Parent_j = j;
                        MessageBox.Show("The destination cell is found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tracePath(cellDetails, dest);
                        foundDest = true;
                        return;
                    }
                    // If the successor is already on the closed 
                    // list or if it is blocked, then ignore it. 
                    // Else do the following 
                    else if (closedList[i - 1, j] == false &&
                        isUnBlocked(grid, i - 1, j) == true)
                    {
                        gNew = cellDetails[i, j].G + 1.0;
                        hNew = calculateHValue(i - 1, j, dest);
                        fNew = gNew + hNew;

                        // If it isn’t on the open list, add it to 
                        // the open list. Make the current square 
                        // the parent of this square. Record the 
                        // f, g, and h costs of the square cell 
                        //                OR 
                        // If it is on the open list already, check 
                        // to see if this path to that square is better, 
                        // using 'f' cost as the measure. 
                        if (cellDetails[i - 1, j].F == float.MaxValue ||
                            cellDetails[i - 1, j].F > fNew)
                        {
                            openList.Add(new pPair(fNew, new Pair(i - 1, j)));

                            // Update the details of this cell 
                            cellDetails[i - 1, j].F = fNew;
                            cellDetails[i - 1, j].G = gNew;
                            cellDetails[i - 1, j].H = hNew;
                            cellDetails[i - 1, j].Parent_i = i;
                            cellDetails[i - 1, j].Parent_j = j;
                        }
                    }
                }

                //----------- 2nd Successor (South) ------------ 

                // Only process this cell if this is a valid one 
                if (isValid(i + 1, j) == true)
                {
                    // If the destination cell is the same as the 
                    // current successor 
                    if (isDestination(i + 1, j, dest) == true)
                    {
                        // Set the Parent of the destination cell 
                        cellDetails[i + 1, j].Parent_i = i;
                        cellDetails[i + 1, j].Parent_j = j;
                        MessageBox.Show("The destination cell is found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tracePath(cellDetails, dest);
                        foundDest = true;
                        return;
                    }
                    // If the successor is already on the closed 
                    // list or if it is blocked, then ignore it. 
                    // Else do the following 
                    else if (closedList[i + 1, j] == false &&
                        isUnBlocked(grid, i + 1, j) == true)
                    {
                        gNew = cellDetails[i, j].G + 1.0;
                        hNew = calculateHValue(i + 1, j, dest);
                        fNew = gNew + hNew;

                        // If it isn’t on the open list, add it to 
                        // the open list. Make the current square 
                        // the parent of this square. Record the 
                        // f, g, and h costs of the square cell 
                        //                OR 
                        // If it is on the open list already, check 
                        // to see if this path to that square is better, 
                        // using 'f' cost as the measure. 
                        if (cellDetails[i + 1, j].F == float.MaxValue ||
                            cellDetails[i + 1, j].F > fNew)
                        {
                            openList.Add(new pPair(fNew, new Pair(i + 1, j)));
                            // Update the details of this cell 
                            cellDetails[i + 1, j].F = fNew;
                            cellDetails[i + 1, j].G = gNew;
                            cellDetails[i + 1, j].H = hNew;
                            cellDetails[i + 1, j].Parent_i = i;
                            cellDetails[i + 1, j].Parent_j = j;
                        }
                    }
                }

                //----------- 3rd Successor (East) ------------ 

                // Only process this cell if this is a valid one 
                if (isValid(i, j + 1) == true)
                {
                    // If the destination cell is the same as the 
                    // current successor 
                    if (isDestination(i, j + 1, dest) == true)
                    {
                        // Set the Parent of the destination cell 
                        cellDetails[i, j + 1].Parent_i = i;
                        cellDetails[i, j + 1].Parent_j = j;
                        MessageBox.Show("The destination cell is found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tracePath(cellDetails, dest);
                        foundDest = true;
                        return;
                    }

                    // If the successor is already on the closed 
                    // list or if it is blocked, then ignore it. 
                    // Else do the following 
                    else if (closedList[i, j + 1] == false &&
                        isUnBlocked(grid, i, j + 1) == true)
                    {
                        gNew = cellDetails[i, j].G + 1.0;
                        hNew = calculateHValue(i, j + 1, dest);
                        fNew = gNew + hNew;

                        // If it isn’t on the open list, add it to 
                        // the open list. Make the current square 
                        // the parent of this square. Record the 
                        // f, g, and h costs of the square cell 
                        //                OR 
                        // If it is on the open list already, check 
                        // to see if this path to that square is better, 
                        // using 'f' cost as the measure. 
                        if (cellDetails[i, j + 1].F == float.MaxValue ||
                            cellDetails[i, j + 1].F > fNew)
                        {
                            openList.Add(new pPair(fNew, new Pair(i, j + 1)));

                            // Update the details of this cell 
                            cellDetails[i, j + 1].F = fNew;
                            cellDetails[i, j + 1].G = gNew;
                            cellDetails[i, j + 1].H = hNew;
                            cellDetails[i, j + 1].Parent_i = i;
                            cellDetails[i, j + 1].Parent_j = j;
                        }
                    }
                }

                //----------- 4th Successor (West) ------------ 

                // Only process this cell if this is a valid one 
                if (isValid(i, j - 1) == true)
                {
                    // If the destination cell is the same as the 
                    // current successor 
                    if (isDestination(i, j - 1, dest) == true)
                    {
                        // Set the Parent of the destination cell 
                        cellDetails[i, j - 1].Parent_i = i;
                        cellDetails[i, j - 1].Parent_j = j;
                        MessageBox.Show("The destination cell is found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tracePath(cellDetails, dest);
                        foundDest = true;
                        return;
                    }

                    // If the successor is already on the closed 
                    // list or if it is blocked, then ignore it. 
                    // Else do the following 
                    else if (closedList[i, j - 1] == false &&
                        isUnBlocked(grid, i, j - 1) == true)
                    {
                        gNew = cellDetails[i, j].G + 1.0;
                        hNew = calculateHValue(i, j - 1, dest);
                        fNew = gNew + hNew;

                        // If it isn’t on the open list, add it to 
                        // the open list. Make the current square 
                        // the parent of this square. Record the 
                        // f, g, and h costs of the square cell 
                        //                OR 
                        // If it is on the open list already, check 
                        // to see if this path to that square is better, 
                        // using 'f' cost as the measure. 
                        if (cellDetails[i, j - 1].F == float.MaxValue ||
                            cellDetails[i, j - 1].F > fNew)
                        {
                            openList.Add(new pPair(fNew, new Pair(i, j - 1)));

                            // Update the details of this cell 
                            cellDetails[i, j - 1].F = fNew;
                            cellDetails[i, j - 1].G = gNew;
                            cellDetails[i, j - 1].H = hNew;
                            cellDetails[i, j - 1].Parent_i = i;
                            cellDetails[i, j - 1].Parent_j = j;
                        }
                    }
                }

                //----------- 5th Successor (North-East) ------------ 

                // Only process this cell if this is a valid one 
                if (isValid(i - 1, j + 1) == true && allowDiagonalMove)
                {
                    // If the destination cell is the same as the 
                    // current successor 
                    if (isDestination(i - 1, j + 1, dest) == true)
                    {
                        // Set the Parent of the destination cell 
                        cellDetails[i - 1, j + 1].Parent_i = i;
                        cellDetails[i - 1, j + 1].Parent_j = j;
                        MessageBox.Show("The destination cell is found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tracePath(cellDetails, dest);
                        foundDest = true;
                        return;
                    }

                    // If the successor is already on the closed 
                    // list or if it is blocked, then ignore it. 
                    // Else do the following 
                    else if (closedList[i - 1, j + 1] == false &&
                        isUnBlocked(grid, i - 1, j + 1) == true)
                    {
                        gNew = cellDetails[i, j].G + 1.414;
                        hNew = calculateHValue(i - 1, j + 1, dest);
                        fNew = gNew + hNew;

                        // If it isn’t on the open list, add it to 
                        // the open list. Make the current square 
                        // the parent of this square. Record the 
                        // f, g, and h costs of the square cell 
                        //                OR 
                        // If it is on the open list already, check 
                        // to see if this path to that square is better, 
                        // using 'f' cost as the measure. 
                        if (cellDetails[i - 1, j + 1].F == float.MaxValue ||
                            cellDetails[i - 1, j + 1].F > fNew)
                        {
                            openList.Add(new pPair(fNew, new Pair(i - 1, j + 1)));

                            // Update the details of this cell 
                            cellDetails[i - 1, j + 1].F = fNew;
                            cellDetails[i - 1, j + 1].G = gNew;
                            cellDetails[i - 1, j + 1].H = hNew;
                            cellDetails[i - 1, j + 1].Parent_i = i;
                            cellDetails[i - 1, j + 1].Parent_j = j;
                        }
                    }
                }

                //----------- 6th Successor (North-West) ------------ 

                // Only process this cell if this is a valid one 
                if (isValid(i - 1, j - 1) == true && allowDiagonalMove)
                {
                    // If the destination cell is the same as the 
                    // current successor 
                    if (isDestination(i - 1, j - 1, dest) == true)
                    {
                        // Set the Parent of the destination cell 
                        cellDetails[i - 1, j - 1].Parent_i = i;
                        cellDetails[i - 1, j - 1].Parent_j = j;
                        MessageBox.Show("The destination cell is found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tracePath(cellDetails, dest);
                        foundDest = true;
                        return;
                    }

                    // If the successor is already on the closed 
                    // list or if it is blocked, then ignore it. 
                    // Else do the following 
                    else if (closedList[i - 1, j - 1] == false &&
                        isUnBlocked(grid, i - 1, j - 1) == true)
                    {
                        gNew = cellDetails[i, j].G + 1.414;
                        hNew = calculateHValue(i - 1, j - 1, dest);
                        fNew = gNew + hNew;

                        // If it isn’t on the open list, add it to 
                        // the open list. Make the current square 
                        // the parent of this square. Record the 
                        // f, g, and h costs of the square cell 
                        //                OR 
                        // If it is on the open list already, check 
                        // to see if this path to that square is better, 
                        // using 'f' cost as the measure. 
                        if (cellDetails[i - 1, j - 1].F == float.MaxValue ||
                            cellDetails[i - 1, j - 1].F > fNew)
                        {
                            openList.Add(new pPair(fNew, new Pair(i - 1, j - 1)));
                            // Update the details of this cell 
                            cellDetails[i - 1, j - 1].F = fNew;
                            cellDetails[i - 1, j - 1].G = gNew;
                            cellDetails[i - 1, j - 1].H = hNew;
                            cellDetails[i - 1, j - 1].Parent_i = i;
                            cellDetails[i - 1, j - 1].Parent_j = j;
                        }
                    }
                }

                //----------- 7th Successor (South-East) ------------ 

                // Only process this cell if this is a valid one 
                if (isValid(i + 1, j + 1) == true && allowDiagonalMove)
                {
                    // If the destination cell is the same as the 
                    // current successor 
                    if (isDestination(i + 1, j + 1, dest) == true)
                    {
                        // Set the Parent of the destination cell 
                        cellDetails[i + 1, j + 1].Parent_i = i;
                        cellDetails[i + 1, j + 1].Parent_j = j;
                        MessageBox.Show("The destination cell is found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tracePath(cellDetails, dest);
                        foundDest = true;
                        return;
                    }

                    // If the successor is already on the closed 
                    // list or if it is blocked, then ignore it. 
                    // Else do the following 
                    else if (closedList[i + 1, j + 1] == false &&
                        isUnBlocked(grid, i + 1, j + 1) == true)
                    {
                        gNew = cellDetails[i, j].G + 1.414;
                        hNew = calculateHValue(i + 1, j + 1, dest);
                        fNew = gNew + hNew;

                        // If it isn’t on the open list, add it to 
                        // the open list. Make the current square 
                        // the parent of this square. Record the 
                        // f, g, and h costs of the square cell 
                        //                OR 
                        // If it is on the open list already, check 
                        // to see if this path to that square is better, 
                        // using 'f' cost as the measure. 
                        if (cellDetails[i + 1, j + 1].F == float.MaxValue ||
                            cellDetails[i + 1, j + 1].F > fNew)
                        {
                            openList.Add(new pPair(fNew, new Pair(i + 1, j + 1)));

                            // Update the details of this cell 
                            cellDetails[i + 1, j + 1].F = fNew;
                            cellDetails[i + 1, j + 1].G = gNew;
                            cellDetails[i + 1, j + 1].H = hNew;
                            cellDetails[i + 1, j + 1].Parent_i = i;
                            cellDetails[i + 1, j + 1].Parent_j = j;
                        }
                    }
                }

                //----------- 8th Successor (South-West) ------------ 

                // Only process this cell if this is a valid one 
                if (isValid(i + 1, j - 1) == true && allowDiagonalMove)
                {
                    // If the destination cell is the same as the 
                    // current successor 
                    if (isDestination(i + 1, j - 1, dest) == true)
                    {
                        // Set the Parent of the destination cell 
                        cellDetails[i + 1, j - 1].Parent_i = i;
                        cellDetails[i + 1, j - 1].Parent_j = j;
                        MessageBox.Show("The destination cell is found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tracePath(cellDetails, dest);
                        foundDest = true;
                        return;
                    }

                    // If the successor is already on the closed 
                    // list or if it is blocked, then ignore it. 
                    // Else do the following 
                    else if (closedList[i + 1, j - 1] == false &&
                        isUnBlocked(grid, i + 1, j - 1) == true)
                    {
                        gNew = cellDetails[i, j].G + 1.414;
                        hNew = calculateHValue(i + 1, j - 1, dest);
                        fNew = gNew + hNew;

                        // If it isn’t on the open list, add it to 
                        // the open list. Make the current square 
                        // the parent of this square. Record the 
                        // f, g, and h costs of the square cell 
                        //                OR 
                        // If it is on the open list already, check 
                        // to see if this path to that square is better, 
                        // using 'f' cost as the measure. 
                        if (cellDetails[i + 1, j - 1].F == float.MaxValue ||
                            cellDetails[i + 1, j - 1].F > fNew)
                        {
                            openList.Add(new pPair(fNew, new Pair(i + 1, j - 1)));

                            // Update the details of this cell 
                            cellDetails[i + 1, j - 1].F = fNew;
                            cellDetails[i + 1, j - 1].G = gNew;
                            cellDetails[i + 1, j - 1].H = hNew;
                            cellDetails[i + 1, j - 1].Parent_i = i;
                            cellDetails[i + 1, j - 1].Parent_j = j;
                        }
                    }
                }
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
