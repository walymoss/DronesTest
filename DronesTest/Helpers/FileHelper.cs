using DronesTest.Entities;

namespace DronesTest.Helpers
{
    public class FileHelper
    {
        /// <summary>
        /// Reading input file and returning a Tuple of a list of drones and a list of packages
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Tuple<List<Drone>, List<Package>> ProcessInput(string path)
        {
            var lines = File.ReadAllLines(path);
            var drones = new List<Drone>();
            string[] droneData = lines[0].Split(',');
            for (int i = 0; i < droneData.Length; i += 2)
            {
                drones.Add(new Drone { droneName = droneData[i].Trim(), droneWeight = int.Parse(droneData[i + 1].Substring(2, droneData[i + 1].Length - 3).Trim()) });
            }

            var packages = lines.Skip(1).Select(line =>
            {
                var data = line.Split(',');
                return new Package { packageLocation = data[0].Trim(), packageWeight = int.Parse(data[1].Substring(2, data[1].Length - 3).Trim()) };
            }).ToList();

            return new Tuple<List<Drone>, List<Package>>(drones, packages);

        }
        /// <summary>
        /// Writing an output file with the trips of each drone
        /// </summary>
        /// <param name="path"></param>
        /// <param name="drones"></param>
        /// <param name="packages"></param>
        /// <param name="packageWeights"></param>
        /// <param name="droneWeights"></param>
        /// <param name="assignments"></param>
        public void ProcessOutput(string path, List<Drone> drones, List<Package> packages, int[] packageWeights, int[] droneWeights, int[] assignments)
        {
            string fileName = path;
            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                for (int droneIndex = 0; droneIndex < drones.Count; droneIndex++)
                {
                    Drone drone = drones[droneIndex];
                    outputFile.WriteLine(drone.droneName);
                    int trip = 1;
                    int weight = 0;
                    List<string> tripLocations = new List<string>();
                    for (int packageIndex = 0; packageIndex < packages.Count; packageIndex++)
                    {
                        Package package = packages[packageIndex];
                        if (assignments[packageIndex] == droneIndex)
                        {
                            if (weight + packageWeights[packageIndex] > droneWeights[droneIndex])
                            {
                                outputFile.WriteLine("Trip #" + trip + " " + string.Join(", ", tripLocations));
                                tripLocations.Clear();
                                weight = packageWeights[packageIndex];
                                trip++;
                            }
                            else
                            {
                                weight += packageWeights[packageIndex];
                            }
                            tripLocations.Add(package.packageLocation);
                        }
                    }
                    if (tripLocations.Count > 0)
                    {
                        outputFile.WriteLine("Trip #" + trip + " " + string.Join(", ", tripLocations));
                    }
                }
            }
        }
    }
}
