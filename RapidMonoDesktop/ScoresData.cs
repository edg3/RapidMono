using LocalDB;
using System;
using System.Collections.Generic;

namespace RapidMonoDesktop;

class ScoreItem
{
    public DateTime Date;
    public int Score = 0;
}
static class ScoresData
{
    public static List<ScoreItem> Scores;
    private const string ConnectionString = @"C:\Work\Databases\Game1.sdf";

    public static void CreateDatabase()
    {
        using (var context = new ScoresDataContext(ConnectionString))
        {
            if (!context.DatabaseExists())
            {
                context.CreateDatabase();
            }
        }
    }

    public static void AddScore(Score score)
    {
        using (var context = new ScoresDataContext(ConnectionString))
        {
            if (context.DatabaseExists())
            {
                //context.Scores.InsertOnSubmit(score);
                //context.SubmitChanges();
            }
        }
    }

    public static IList<Score> GetScores()
    {
        IList<Score> scores = new List<Score>();
        using (var context = new ScoresDataContext(ConnectionString))
        {
            //scores = (from sco in context.Scores select sco).ToList();
        }

        return scores;
    }
}
