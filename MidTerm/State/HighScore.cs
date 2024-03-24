using System.Runtime.Serialization;
using System;

[DataContract(Name = "HighScore")]
public class HighScore
{
    [DataMember()]
    public string name { get; set; } = "";
    [DataMember()]
    public uint score { get; set; } = 0;
    [DataMember()]
    public ushort level { get; set; } = 0;
    [DataMember()]
    public DateTime timeStamp { get; set; } = new DateTime();

    public HighScore() { }

    public HighScore(string name, uint score, ushort level, DateTime timeStamp)
    {
        this.name = name;
        this.score = score;
        this.level = level;
        this.timeStamp = timeStamp;
    }

    override public string ToString()
    {
        return $"Name: {name}\nScore: {score}\nLevel: {level}\nTime: {timeStamp}";
    }
}
