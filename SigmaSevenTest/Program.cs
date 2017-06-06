using System;
using System.Collections.Generic;

/// <summary>
/// Daniel Kerr,  6/6/17
/// Sigma seven Recruitment Technical test Question 2
/// </summary>
namespace SigmaSevenTest
{
    class Program
    {
        static string folder = @"C:\Users\DanielAdmin\Desktop\sigmaseven\";
        static string featuresFile = @"Features.txt";
        static string featuresToRemoveFile = @"FeaturesToDelete.txt";
        static string newFeaturesFile = @"FeaturesNew.txt";

        static void Main(string[] args)
        {   //I've listed possible exceptions that should be considered. It would be better to check things exist before 
            // attempting to load them.
            // Should also consider the possibility of running out of disk space if copying large files.
            // I did this task first. I've spent 46 minutes and I need to move on to question 1.

            try
            {
                StripFeatures();
            }
            catch (System.IO.DirectoryNotFoundException) {
                //not implemented
            }
            catch (System.IO.DriveNotFoundException)
            {
                //not implemented
            }
            catch (System.IO.FileNotFoundException)
            {
                //not implemented
            }
            catch (System.IO.IOException)
            {
                //not implemented
            }
            catch (Exception)
            {

                throw;
            }           
   
            Console.WriteLine("done");
            Console.ReadKey();
        }

        /// <summary>
        /// Create FeaturesNew.txt containing the subset of rows in Features.txt where the first cell is not in 
        /// FeaturesToDelete.txt
        /// </summary>
        static void StripFeatures() {
            //List<string> would accomplish same goal, but we'll be calling contains method:hashset will be quicker
            HashSet<string> featuresToDelete = GetFeaturesToDelete();

            // using means that streams will be automatically flushed and closed
            using (System.IO.StreamReader file = new System.IO.StreamReader(folder + featuresFile))
            {
                // the second parameter means do not append, i.e. replace any pre-existing file that may have been created in a previous test
                using (System.IO.StreamWriter newFile = new System.IO.StreamWriter(folder + newFeaturesFile,false))
                {
                    //go through the source file one line at a time - don't load the whole thing in one go, such an approach would not scale
                    while (!(file.EndOfStream))
                    {
                        string line = file.ReadLine();
                        // need access to the UDB Number (first field). Suspect split is quickest way, given more time test regex or substr based on position of first comma
                        string[] items = line.Split(',');
                        if (!featuresToDelete.Contains(items[0]))
                        {
                            //write the line to the new file if it isn't matched in featuresToDelete
                            newFile.WriteLine(line);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Parse FeaturesToDelete.txt into a format taht we can easliy check against
        /// </summary>
        /// <returns></returns>
        static HashSet<string> GetFeaturesToDelete()
        {
            HashSet<string> stringList = new HashSet<string>();
            using (System.IO.StreamReader file = new System.IO.StreamReader(folder + featuresToRemoveFile))
            {
                while (!(file.EndOfStream))
                {
                    stringList.Add(file.ReadLine());
                }
                file.Close();
            }
            return stringList;
        }
    }
}
