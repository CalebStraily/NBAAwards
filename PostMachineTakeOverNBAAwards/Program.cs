using System.Diagnostics.Metrics;
using System.IO;
using System.Linq.Expressions;

namespace PostMachineTakeOverNBAAwards
{
    internal class Program
    {

        public struct PlayerStats
        {
            public double tempWorstRating;
            public double tempLeastPoints;
            public double tempLeastRebounds;
            public double tempLeastAssists;
            public double tempWorstShotPercentage;
            public double tempWorstFreeThrowPercentage;
            public double tempBestRating;
            public double tempMostPoints;
            public double tempMostRebounds;
            public double tempMostAssists;
            public double tempBestShotPercentage;
            public double tempBestFreeThrowPercentage;

            public int tempLeastGamesPlayed;
            public int tempMostGamesPlayed;

            public List<string> worstRatingList;
            public List<string> bestRatingList;
            public List<string> leastGamesList;
            public List<string> leastPointsList;
            public List<string> leastReboundsList;
            public List<string> leastAssistsList;
            public List<string> worstShotPercentageList;
            public List<string> worstFreeThrowPercentageList;
            public List<string> mostGamesList;
            public List<string> mostPointsList;
            public List<string> mostReboundsList;
            public List<string> mostAssistsList;
            public List<string> bestShotPercentageList;
            public List<string> bestFreeThrowPercentageList;
        }

        public struct AwardStats
        {
            public double tempPriusPoints;
            public double tempPriusGameTime;
            public double tempGuzzlerPoints;
            public double tempGuzzlerGameTime;
            public double tempFoulShotPercentage;
            public double tempFoulFreeThrowPercentage;
            public double tempPositiveRating;
            public double tempNegativeRating;
            public double tempMostPlayTime;
            public double countNE;
            public double countNW;
            public double countSE;
            public double countSW;

            public string priusAward;
            public string gasGuzzlerAward;
            public string foulTargetAward;
            public string onTheFenceAwardPositive;
            public string onTheFenceAwardNegative;
            public string bangForYourBuckAward;
            
            public List<string> overachieverAward;
            public List<string> underachieverAward;
        }

        static void Main(string[] args)
        {
            //declares variables of struct PlayerStats
            PlayerStats stats;

            stats.tempWorstRating = 100;
            stats.tempLeastPoints = 100000;
            stats.tempLeastRebounds = 15;
            stats.tempLeastAssists = 15;
            stats.tempWorstShotPercentage = 101;
            stats.tempWorstFreeThrowPercentage = 101;
            stats.tempBestRating = -1;
            stats.tempMostPoints = -1;
            stats.tempMostRebounds = -1;
            stats.tempMostAssists = -1;
            stats.tempBestShotPercentage = -1;
            stats.tempBestFreeThrowPercentage = -1;

            stats.tempLeastGamesPlayed = 100;
            stats.tempMostGamesPlayed = -1;

            stats.worstRatingList = new List<string>();
            stats.bestRatingList = new List<string>();
            stats.leastGamesList = new List<string>();
            stats.leastPointsList = new List<string>();
            stats.leastReboundsList = new List<string>();
            stats.leastAssistsList = new List<string>();
            stats.worstShotPercentageList = new List<string>();
            stats.worstFreeThrowPercentageList = new List<string>();
            stats.mostGamesList = new List<string>();
            stats.mostPointsList = new List<string>();
            stats.mostReboundsList = new List<string>();
            stats.mostAssistsList = new List<string>();
            stats.bestShotPercentageList = new List<string>();
            stats.bestFreeThrowPercentageList = new List<string>();

            

            //declares variables of struct AwardStats
            AwardStats award;

            award.tempPriusPoints = 0;
            award.tempPriusGameTime = 500000;
            award.tempGuzzlerPoints = 500;
            award.tempGuzzlerGameTime = 0;
            award.tempFoulShotPercentage = 1000;
            award.tempFoulFreeThrowPercentage = 0;
            award.tempPositiveRating = 101;
            award.tempNegativeRating = -101;
            award.tempMostPlayTime = 0;
            award.countNE = 0;
            award.countNW = 0;
            award.countSE = 0;
            award.countSW = 0;

            award.priusAward = "";
            award.gasGuzzlerAward = "";
            award.foulTargetAward = "";
            award.onTheFenceAwardPositive = "";
            award.onTheFenceAwardNegative = "";
            award.bangForYourBuckAward = "";

            award.overachieverAward = new List<string>();
            award.underachieverAward = new List<string>();

            //Declares file path of .csv file used.
            //Change this to the file path of your locally installed "NBA_DATA.csv" file.
            string nbaDataFilePath = "C:\\Users\\Caleb\\Desktop\\Visual Studio Files\\NBA_DATA.csv";
            
            //loops through the file for the first time
            using (StreamReader reader = new StreamReader(nbaDataFilePath))
            {
                string line;

                //skips the first line of the .csv that describes each piece of data is
                reader.ReadLine();

                //executes while the next row of the .csv is not null
                while ((line = reader.ReadLine()) != null)
                {
                    //splits the data of each row by commas
                    string[] sub = line.Split(',');

                    //executes if the current player's points are greater than or equal to the previous player's points AND current player's game time less than the previous player's game time
                    if (double.Parse(sub[6]) >= award.tempPriusPoints && double.Parse(sub[5]) < award.tempPriusGameTime)
                    {
                        award.tempPriusPoints = double.Parse(sub[6]);
                        award.tempPriusGameTime = double.Parse(sub[5]);

                        award.priusAward = sub[0];
                    }

                    //executes if the current player's points are less than or equal to the previous player's points AND current player's game time is greater than the previous player's game time
                    if (double.Parse(sub[6]) <= award.tempGuzzlerPoints && double.Parse(sub[5]) > award.tempGuzzlerGameTime)
                    {
                        award.tempGuzzlerPoints = double.Parse(sub[6]);
                        award.tempGuzzlerGameTime = double.Parse(sub[5]);

                        award.gasGuzzlerAward = sub[0];
                    }

                    //executes if the current player's free-throw% is greater than or equal to the previous player's free-throw% AND current player's shot% is less than the previous player's shot%
                    if (double.Parse(sub[10]) >= award.tempFoulFreeThrowPercentage && double.Parse(sub[9]) < award.tempFoulShotPercentage)
                    {
                        award.tempFoulFreeThrowPercentage = double.Parse(sub[10]);
                        award.tempFoulShotPercentage = double.Parse(sub[9]);

                        award.foulTargetAward = sub[0];
                    }

                    //executes if the player's rating is greater than zero AND the player's rating is less than tempPositive Rating
                    if (double.Parse(sub[3]) > 0)
                    {
                        award.overachieverAward.Add(sub[0]);
                    }
                    //executes if the player's rating is less than zero
                    else if (double.Parse(sub[3]) < 0)
                    {
                        award.underachieverAward.Add(sub[0]);
                    }

                    //executes if the current player's rating is greater than zero AND is less than the tempPositiveRating value
                    if (double.Parse(sub[3]) > 0 && double.Parse(sub[3]) < award.tempPositiveRating)
                    {
                        award.tempPositiveRating = double.Parse(sub[3]);
                        award.onTheFenceAwardPositive = sub[0];
                    }
                    //executes if the current player's rating is less than zero AND is greater than the tempNegativeRating value
                    else if (double.Parse(sub[3]) < 0 && double.Parse(sub[3]) > award.tempNegativeRating)
                    {
                        award.tempNegativeRating = double.Parse(sub[3]);
                        award.onTheFenceAwardNegative = sub[0];
                    }

                    //executes if the current player is a rookie AND if the current player's total play time is greater than the previous player's total play time
                    if (int.Parse(sub[2]) == 1 && (double.Parse(sub[4]) * double.Parse(sub[5])) > award.tempMostPlayTime)
                    {
                        award.tempMostPlayTime = double.Parse(sub[4]) * double.Parse(sub[5]);
                        award.bangForYourBuckAward = sub[0];
                    }

                    //adds a total amount of points of teams from the NW, NE, SE, SW regions of the U.S.
                    switch (sub[1])
                    {
                        //occurs if the player's team is in the SE region
                        case var expression when (sub[1] == "ATL" || sub[1] == "MEM" || sub[1] == "MIA" || sub[1] == "NO" || sub[1] == "OKC" || sub[1] == "ORL" || sub[1] == "UTA"):
                            award.countSE += double.Parse(sub[4]) * double.Parse(sub[6]);
                            break;
                        //occurs if the player's team is in the NE region
                        case var expression when (sub[1] == "BKN" || sub[1] == "BOS" || sub[1] == "CHA" || sub[1] == "CHI" || sub[1] == "CLE" || sub[1] == "DET" || sub[1] == "MIL" || sub[1] == "MIN" || sub[1] == "NY" || sub[1] == "PHI" || sub[1] == "TOR"):
                            award.countNE += double.Parse(sub[4]) * double.Parse(sub[6]);
                            break;
                        //occurs if the player's team is in the SW region
                        case var expression when (sub[1] == "DAL" || sub[1] == "GS" || sub[1] == "HOU" || sub[1] == "LAC" || sub[1] == "LAL" || sub[1] == "PHO" || sub[1] == "SA" || sub[1] == "SAC"):
                            award.countSW += double.Parse(sub[4]) * double.Parse(sub[6]);
                            break;
                        //occurs if the player's team is in the NW region
                        case var expression when (sub[1] == "DEN" || sub[1] == "IND" || sub[1] == "POR" || sub[1] == "WAS"):
                            award.countNW += double.Parse(sub[4]) * double.Parse(sub[6]);
                            break;
                    }

                    //calls a function to set the best of worst stats for a series of categories
                    SetBestAndWorstStats(ref stats, sub);
                }

                //calls a function to outputs the first half of the awards to the console
                OutputAwardsPartOne(award);
            }

            //loops through the file a second time
            using (StreamReader reader = new StreamReader(nbaDataFilePath))
            {
                string line;

                //skips the first line of the .csv that describes each piece of data is
                reader.ReadLine();

                //executes while the next row of the .csv is not null
                while ((line = reader.ReadLine()) != null)
                {
                    //splits the data of each row by commas
                    string[] sub = line.Split(',');

                    //calls a function to add the best and worst stats originally set by the function SetBestAndWorstStats in the first read through of the file (line 234) into a series of string type List variables
                    AddBestAndWorstStats(ref stats, sub);
                }

                //calls a function to outputs the second half of the awards to the console
                OutputAwardsPartTwo(stats);
            }
        }

        //this function sets temp holding variables to the best and worst statistics of each category
        static void SetBestAndWorstStats(ref PlayerStats stats, string[] sub)
        {
            //sets worst rating of all players
            if (double.Parse(sub[3]) <= stats.tempWorstRating)
            {
                stats.tempWorstRating = double.Parse(sub[3]);
            }

            //sets best rating of all players
            if (double.Parse(sub[3]) >= stats.tempBestRating)
            {
                stats.tempBestRating = double.Parse(sub[3]);
            }

            //sets least games played of all players
            if (int.Parse(sub[4]) <= stats.tempLeastGamesPlayed)
            {
                stats.tempLeastGamesPlayed = int.Parse(sub[4]);
            }

            //sets most games played of all players
            if (int.Parse(sub[4]) >= stats.tempMostGamesPlayed)
            {
                stats.tempMostGamesPlayed = int.Parse(sub[4]);
            }

            //sets least points made of all players
            if ((double.Parse(sub[4]) * double.Parse(sub[6])) <= stats.tempLeastPoints)
            {
                stats.tempLeastPoints = double.Parse(sub[4]) * double.Parse(sub[6]);
            }

            //sets most points made of all players
            if ((double.Parse(sub[4]) * double.Parse(sub[6])) >= stats.tempMostPoints)
            {
                stats.tempMostPoints = double.Parse(sub[4]) * double.Parse(sub[6]);
            }

            //sets least rebounds made of all players
            if ((int.Parse(sub[4]) * double.Parse(sub[7])) <= stats.tempLeastRebounds)
            {
                stats.tempLeastRebounds = int.Parse(sub[4]) * double.Parse(sub[7]);
            }

            //sets most rebound made of all players
            if ((int.Parse(sub[4]) * double.Parse(sub[7])) >= stats.tempMostRebounds)
            {
                stats.tempMostRebounds = (int.Parse(sub[4]) * double.Parse(sub[7]));
            }

            //sets least assists made of all players
            if ((int.Parse(sub[4]) * double.Parse(sub[8])) <= stats.tempLeastAssists)
            {
                stats.tempLeastAssists = int.Parse(sub[4]) * double.Parse(sub[8]);
            }

            //sets most assists made of all players
            if ((int.Parse(sub[4]) * double.Parse(sub[8])) >= stats.tempMostAssists)
            {
                stats.tempMostAssists = int.Parse(sub[4]) * double.Parse(sub[8]);
            }

            //sets worst shot percentage made of all players
            if (double.Parse(sub[9]) <= stats.tempWorstShotPercentage)
            {
                stats.tempWorstShotPercentage = double.Parse(sub[9]);
            }

            //sets best shot percentage made of all players
            if (double.Parse(sub[9]) >= stats.tempBestShotPercentage)
            {
                stats.tempBestShotPercentage = double.Parse(sub[9]);
            }

            //sets worst free throw percentage made of all players
            if (double.Parse(sub[10]) <= stats.tempWorstFreeThrowPercentage)
            {
                stats.tempWorstFreeThrowPercentage = double.Parse(sub[10]);
            }

            //sets best free throw percentage made of all players
            if (double.Parse(sub[10]) >= stats.tempBestFreeThrowPercentage)
            {
                stats.tempBestFreeThrowPercentage = double.Parse(sub[10]);
            }
        }

        //this function adds the players responsible for the best and worst stats of each category to a series of lists
        static void AddBestAndWorstStats(ref PlayerStats stats, string[] sub)
        {
            //adds current player to a list if their rating matches the worst rating out of all players
            if (double.Parse(sub[3]) == stats.tempWorstRating)
            {
                stats.worstRatingList.Add(sub[0]);
            }

            //adds current player to a list if their rating matches the best rating out of all players
            if (double.Parse(sub[3]) >= stats.tempBestRating)
            {
                stats.bestRatingList.Add(sub[0]);
            }

            //adds current player to a list if their total games played matches the least total games played out of all players
            if (int.Parse(sub[4]) == stats.tempLeastGamesPlayed)
            {
                stats.leastGamesList.Add(sub[0]);
            }

            //adds current player to a list if their total games played matches the most total games played out of all players
            if (int.Parse(sub[4]) == stats.tempMostGamesPlayed)
            {
                stats.mostGamesList.Add(sub[0]);
            }

            //adds current player to a list if their total points scored matches the least total points scored out of all players
            if ((double.Parse(sub[4]) * double.Parse(sub[6])) == stats.tempLeastPoints)
            {
                stats.leastPointsList.Add(sub[0]);
            }

            //adds current player to a list if their total points scored matches the most total points scored out of all players
            if ((double.Parse(sub[4]) * double.Parse(sub[6])) == stats.tempMostPoints)
            {
                stats.mostPointsList.Add(sub[0]);
            }

            //adds current player to a list if their total rebounds matches the least total rebounds out of all players
            if ((int.Parse(sub[4]) * double.Parse(sub[7])) == stats.tempLeastRebounds)
            {
                stats.leastReboundsList.Add(sub[0]);
            }

            //adds current player to a list if their total rebounds matches the most total rebounds out of all players
            if ((int.Parse(sub[4]) * double.Parse(sub[7])) == stats.tempMostRebounds)
            {
                stats.mostReboundsList.Add(sub[0]);
            }

            //adds current player to a list if their total assists matches the least total assists out of all players
            if ((int.Parse(sub[4]) * double.Parse(sub[8])) == stats.tempLeastAssists)
            {
                stats.leastAssistsList.Add(sub[0]);
            }

            //adds current player to a list if their total assists matches the most total assists out of all players
            if ((int.Parse(sub[4]) * double.Parse(sub[8])) == stats.tempMostAssists)
            {
                stats.mostAssistsList.Add(sub[0]);
            }

            //adds current player to a list if their shot percentage matches the worst shot percentage out of all players
            if (double.Parse(sub[9]) == stats.tempWorstShotPercentage)
            {
                stats.worstShotPercentageList.Add(sub[0]);
            }

            //adds current player to a list if their shot percentage matches the best shot percentage out of all players
            if (double.Parse(sub[9]) == stats.tempBestShotPercentage)
            {
                stats.bestShotPercentageList.Add(sub[0]);
            }

            //adds current player to a list if their free throw percentage matches the worst free throw percentage out of all players
            if (double.Parse(sub[10]) == stats.tempWorstFreeThrowPercentage)
            {
                stats.worstFreeThrowPercentageList.Add(sub[0]);
            }

            //adds current player to a list if their free throw percentage matches the best free throw percentage out of all players
            if (double.Parse(sub[10]) >= stats.tempBestFreeThrowPercentage)
            {
                stats.bestFreeThrowPercentageList.Add(sub[0]);
            }
        }

        //this function outputs the first half of recipients for a series of awards
        static void OutputAwardsPartOne(AwardStats award)
        {
            //the "Prius Award" is for the player who scored the most points with the least amount of game time
            Console.WriteLine("Prius Award: \n" +
                                 "~~~~~~~~~~~~~~~~~~~\n" +
                                 award.priusAward + "\n");

            Console.ReadKey();
            Console.Clear();

            //the "Gas Guzzler Award" is for the player who scored the least points with the most amount of game time
            Console.WriteLine("Gas Guzzler Award: \n" +
                              "~~~~~~~~~~~~~~~~~~~\n" +
                              award.gasGuzzlerAward + "\n");

            Console.ReadKey();
            Console.Clear();

            //the "Foul Target Award" is for the player who has the highest free-throw% with the lowest shot%
            Console.WriteLine("Foul Target Award: \n" +
                              "~~~~~~~~~~~~~~~~~~~ \n" +
                              award.foulTargetAward);

            Console.ReadKey();
            Console.Clear();

            //the "Overachiver Award" is for the players who are above average in rating
            Console.WriteLine("Overachiever Award Recipients: \n" +
                              "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            //outputs the players who were above average in ratings
            foreach (string element in award.overachieverAward)
            {
                Console.WriteLine(element);
            }

            Console.ReadKey();
            Console.Clear();

            //the Underachiever Award is for the players who are below average in rating
            Console.WriteLine("Underachiever Award Recipients: \n" +
                              "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            //outputs the players who were below average in ratings
            foreach (string element in award.underachieverAward)
            {
                Console.WriteLine(element);
            }

            Console.ReadKey();
            Console.Clear();

            //the "On The Fence Award" is for the player closest to a neutral rating of 0 
            Console.WriteLine("On The Fence Award(s): \n" +
                              "~~~~~~~~~~~~~~~~~~~~~~~");

            //flips tempNegativeRating to a positive value for comparison with its positive counterpart
            award.tempNegativeRating *= -1;

            //if tempPositiveRating is greater than tempNegativeRating, this means tempNegativeRating is closer to zero
            if (award.tempPositiveRating > award.tempNegativeRating)
            {
                Console.WriteLine(award.onTheFenceAwardNegative);
            }
            //if tempPositiveRating is less than tempNegativeRating, this means tempPositiveRating is closer to zero
            else if (award.tempPositiveRating < award.tempNegativeRating)
            {
                Console.WriteLine(award.onTheFenceAwardPositive);
            }

            Console.ReadKey();
            Console.Clear();

            //the "Bang for Your Buck Award" goes to the rookie with the most amount of play time
            Console.WriteLine("Bang for Your Buck Award: \n" +
                              "~~~~~~~~~~~~~~~~~~~~~~~ \n" +
                              award.bangForYourBuckAward);

            Console.ReadKey();
            Console.Clear();

            //the "Gordon Gecko Award" goes to the region that scored the most total points
            Console.WriteLine("Gordon Gecko Award: \n" +
            "~~~~~~~~~~~~~~~~~~~~~~~");

            //compares the points scored for each region and determines the maximum value of the four regions
            //each if/else compares this max value with the value stored in each region's count and will output the region if the max value matches the region's count
            if (new[] { award.countNE, award.countNW, award.countSE, award.countSW }.Max() == award.countNW)
            {
                Console.WriteLine("NW Region");
            }
            else if (new[] { award.countNE, award.countNW, award.countSE, award.countSW }.Max() == award.countNE)
            {
                Console.WriteLine("NE Region");
            }
            else if (new[] { award.countNE, award.countNW, award.countSE, award.countSW }.Max() == award.countSW)
            {
                Console.WriteLine("SW Region");
            }
            else if (new[] { award.countNE, award.countNW, award.countSE, award.countSW }.Max() == award.countSE)
            {
                Console.WriteLine("SE Region");
            }

            Console.ReadKey();
            Console.Clear();
        }
        //this function outputs the second half of recipients for a series of awards
        static void OutputAwardsPartTwo(PlayerStats stats)
        {
            //the "Charlie Brown Awards" are the players who did the worst in a series of categories
            Console.WriteLine("Charlie Brown Awards: \n" +
                              "~~~~~~~~~~~~~~~~~~~~ \n" +
                              "Worst Rating:");

            //outputs the players who have the worst rating
            foreach (string element in stats.worstRatingList)
            {
                Console.WriteLine(element);
            }

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Charlie Brown Awards: \n" +
                              "~~~~~~~~~~~~~~~~~~~~ \n" +
                              "Least Games Played: \n");

            //outputs the players who played the least amount of games
            foreach (string element in stats.leastGamesList)
            {
                Console.WriteLine(element);
            }

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Charlie Brown Awards: \n" +
                              "~~~~~~~~~~~~~~~~~~~~ \n" +
                              "Least Points Scored: \n");

            //outputs the players who scored the least amount of points
            foreach (string element in stats.leastPointsList)
            {
                Console.WriteLine(element);
            }

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Charlie Brown Awards: \n" +
                              "~~~~~~~~~~~~~~~~~~~~ \n" +
                              "Least Rebounds Made: \n");

            //outputs the players who scored the least amount of rebounds
            foreach (string element in stats.leastReboundsList)
            {
                Console.WriteLine(element);
            }

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Charlie Brown Awards: \n" +
                              "~~~~~~~~~~~~~~~~~~~~ \n" +
                              "Least Assists Made: \n");

            //outputs the players who had the least assists 
            foreach (string element in stats.leastAssistsList)
            {
                Console.WriteLine(element);
            }

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Charlie Brown Awards: \n" +
                              "~~~~~~~~~~~~~~~~~~~~ \n" +
                              "Worst Shot Percentage: \n");

            //outputs the players who had the worst shot percentage
            foreach (string element in stats.worstShotPercentageList)
            {
                Console.WriteLine(element);
            }

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Charlie Brown Awards: \n" +
                              "~~~~~~~~~~~~~~~~~~~~ \n" +
                              "Worst Free Throw Percentage: \n");
            
            //outputs the players with the worst free throw percentage
            foreach (string element in stats.worstFreeThrowPercentageList)
            {
                Console.WriteLine(element);
            }

            Console.ReadKey();
            Console.Clear();

            //the "Tiger Uppercut Awards" are the players who did the best in a series of categories
            Console.WriteLine("Tiger Uppercut Awards: \n" +
                              "~~~~~~~~~~~~~~~~~~~~ \n" +
                              "Best Rating:");

            //outputs the players with the best rating
            foreach (string element in stats.bestRatingList)
            {
                Console.WriteLine(element);
            }

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Tiger Uppercut Awards: \n" +
                              "~~~~~~~~~~~~~~~~~~~~ \n" +
                              "Most Games Played: \n");

            //outputs the players who played the most games
            foreach (string element in stats.mostGamesList)
            {
                Console.WriteLine(element);
            }

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Tiger Uppercut Awards: \n" +
                              "~~~~~~~~~~~~~~~~~~~~ \n" +
                              "Most Points Scored: \n");

            //outputs the players who had the most points scored
            foreach (string element in stats.mostPointsList)
            {
                Console.WriteLine(element);
            }

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Tiger Uppercut Awards: \n" +
                              "~~~~~~~~~~~~~~~~~~~~ \n" +
                              "Most Rebounds Made: \n");

            //outputs the players who had the most amount of rebounds
            foreach (string element in stats.mostReboundsList)
            {
                Console.WriteLine(element);
            }

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Tiger Uppercut Awards: \n" +
                              "~~~~~~~~~~~~~~~~~~~~ \n" +
                              "Most Assists Made: \n");

            //outputs the players who had the most amount of assists
            foreach (string element in stats.mostAssistsList)
            {
                Console.WriteLine(element);
            }

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Tiger Uppercut Awards: \n" +
                              "~~~~~~~~~~~~~~~~~~~~ \n" +
                              "Best Shot Percentage: \n");

            //outputs the players who had the best shot percentage
            foreach (string element in stats.bestShotPercentageList)
            {
                Console.WriteLine(element);
            }

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Tiger Uppercut Awards: \n" +
                              "~~~~~~~~~~~~~~~~~~~~ \n" +
                              "Best Free Throw Percentage: \n");

            //outputs the players who had the best free throw percentage
            foreach (string element in stats.bestFreeThrowPercentageList)
            {
                Console.WriteLine(element);
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}
