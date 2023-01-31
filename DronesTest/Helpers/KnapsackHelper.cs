namespace DronesTest
{
    public static class KnapsackHelper
    {
        /// <summary>
        /// Here we fill a 2D matrix with the "subproblems" of the main problem.
        /// Temp storage of the max weight one drone is able to deliver.
        /// </summary>
        /// <param name="numDrones"></param>
        /// <param name="numPackages"></param>
        /// <param name="packageWeights"></param>
        /// <param name="droneWeights"></param>
        /// <returns></returns>
        public static int [,] FillMatrix(int numDrones, int numPackages, int[] packageWeights, int[] droneWeights)
        {
            // Create a 2D array to store temp values
            int[,] matrix = new int[numDrones + 1, numPackages + 1];

            // Fill in the array using dynamic programming
            for (int i = 1; i <= numDrones; i++)
            {
                for (int j = 1; j <= numPackages; j++)
                {
                    // If the current package weight exceeds the current drone's maximum weight,
                    // we can't use this drone to deliver this package
                    if (packageWeights[j - 1] > droneWeights[i - 1])
                    {
                        matrix[i, j] = matrix[i, j - 1];
                    }
                    else
                    {
                        // Otherwise, we can either use this drone to deliver this package or not
                        // The number of trips will be the minimum of these two options
                        matrix[i, j] = Math.Max(matrix[i, j - 1], matrix[i - 1, j - 1] + packageWeights[j - 1]);
                    }
                }
            }
            return matrix;
        }


        /// <summary>
        /// Assigning packages to drones
        /// </summary>
        /// <param name="numPackages"></param>
        /// <param name="numDrones"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static int[] FillAssignments(int numPackages, int numDrones, int[,] matrix)
        {
            int[] assignments = new int[numPackages];
            int r = numDrones;
            int s = numPackages;
            while (r > 0 && s > 0)
            {
                if (matrix[r, s] == matrix[r, s - 1])
                {
                    s--;
                }
                else
                {
                    // This package was assigned to drone i
                    assignments[s - 1] = r - 1;
                    r--;
                    s--;
                }
            }
            return assignments;
        }
    }
}
