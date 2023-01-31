using DronesTest;
using DronesTest.Helpers;

class Drones
{
    /// <summary>
    /// Start here!
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        FileHelper fh = new FileHelper();
        //Reading input text file
        var response = fh.ProcessInput("input.txt");
        var drones = response.Item1;
        var packages = response.Item2;

        int[] droneWeights = drones.Select(x => x.droneWeight).ToArray();
        int[] packageWeights = packages.Select(y => y.packageWeight).ToArray();

        int numDrones = droneWeights.Length;
        int numPackages = packageWeights.Length;

        int[,] matrix = KnapsackHelper.FillMatrix(numDrones, numPackages, packageWeights, droneWeights);

        // Now we can backtrack to find the actual package-drone assignments
        int[] assignments = KnapsackHelper.FillAssignments(numPackages, numDrones, matrix);

        // Creating output text file
        fh.ProcessOutput("output.txt", drones, packages, packageWeights, droneWeights, assignments);
    }
}



