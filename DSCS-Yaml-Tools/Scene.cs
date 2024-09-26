using CsvHelper;
using System.Globalization;
using System.Text;
using CsvHelper.Configuration.Attributes;
using System.IO;


public class Scene
{
	public  List<Message> Messages { get; set; } = new List<Message>();
    public string SceneName { get; set; }
    public string ScenePath { get; set; }
	public Scene(string sceneName)
	{
        this.SceneName = sceneName;
        this.ScenePath = Path.Join(".\\DSDB\\message", sceneName + ".mbe", "Sheet1.csv");
        if(!File.Exists(ScenePath)){
            throw new FileNotFoundException(@"The file: {scenePath} does not exist", ScenePath);
        }
		using var reader = new StreamReader(ScenePath, Encoding.UTF8);
		using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
		{
			this.Messages = csv.GetRecords<Message>().ToList();
			
		}
	}
}

public class Message {

	[Name("ID")]
	public int MsgId { get; set; }

    [Name("Speaker")]
    public string? Speaker { get; set; }

    [Name("Japanese")]
	public string? jpn { get; set; }

	[Name("English")]
	public string? eng { get; set; }

	[Name("Chinese")]
	public string? chn { get; set; }

	[Name("EnglishCensored")]
	public string? engc { get; set; }

	[Name("Korean")]
	public string? kor { get; set; }

	[Name("German")]
	public string? ger { get; set; }

}
