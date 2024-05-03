using RapidMono;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RapidMonoDesktop;

public class ScoreItem
{
    public int ID { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public int Score { get; set; } = 0;
}
static class ScoresData
{
    public static List<ScoreItem> Scores;
    private static ScoreDB _db;

    public static void CreateDatabase()
    {
        if (_db is null) _db = new ScoreDB();
        _db.Database.EnsureCreated();
    }

    public static void AddScore(ScoreItem score)
    {
        _db.ScoreItems.Add(score);
        _db.SaveChanges();
    }

    public static IList<ScoreItem> GetScores()
    {
        IList<ScoreItem> scores = new List<ScoreItem>();
        _db.ScoreItems.Where(s => s.Score > 0)
            .OrderByDescending(s => s.Score)
            .Take(10)
            .ToList()
            .ForEach(s => scores.Add(s));

        return scores;
    }
}
