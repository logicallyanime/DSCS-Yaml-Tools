using CsvHelper;
using System.Globalization;
using System.Text;
using CsvHelper.Configuration.Attributes;
using System.IO;
using YamlDotNet.Serialization;
using System.Diagnostics.Contracts;
using YamlDotNet.Serialization.NamingConventions;
using System.Linq;


public class Scene
{
    private string SceneName { get; set; }
	public  List<Message> Messages { get; set; } = new List<Message>();

	public string GetSceneName(string scenePath){
		if(!scenePath.Contains(".mbe") || scenePath == null)
			throw new ArgumentException($"scenePath: {scenePath} is not a valid scene path");
		return Path.GetFileNameWithoutExtension(scenePath);
	}
	public Scene(string scenePath)
	{
        var filePath = Path.Join(scenePath, "Sheet1.csv");
		SceneName = GetSceneName(scenePath);
        if(!File.Exists(filePath)){
            throw new FileNotFoundException(@"The file: {scenePath} does not exist", filePath);
        }
		using var reader = new StreamReader(filePath, Encoding.UTF8);
		using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
		{
			this.Messages = csv.GetRecords<Message>().ToList();
			
		}
	}
	public Scene(){}

	// TODO: Make Constructor Overload (string yamlPath, string scenePath)
	//       Make it so you can deserialize from YAML into this class, and then deserialize the CSV skipping over already populated fields
	public Scene(string yamlPath, string scenePath){
		var csvScenePath = Path.Join(scenePath, "Sheet1.csv");
		// Validate Files
		if(!File.Exists(yamlPath))
			throw new FileNotFoundException($"File not found: {yamlPath}");

		if(!File.Exists(csvScenePath))
			throw new FileNotFoundException($"File not found: {csvScenePath}");

		// Deserialize YAML
		var deserializer = new DeserializerBuilder()
			.WithNamingConvention(CamelCaseNamingConvention.Instance)
			.Build();
		var scene = deserializer.Deserialize<Scene>(File.ReadAllText(yamlPath));
		SceneName = scene.SceneName;
		Messages = scene.Messages;


		// Validate Scene Name
		if(SceneName != GetSceneName(scenePath))
			throw new Exception($"Scene Name Mismatch: \nYAML: {SceneName}\nCSV: {GetSceneName(scenePath)}");


		// Deserialize CSV
		List<Message> ogMsgs = [];
		using var reader = new StreamReader(csvScenePath, Encoding.UTF8);
		using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
		{
			ogMsgs = csv.GetRecords<Message>().ToList();
			
		}

		// Validate Message Count
		if(ogMsgs.Count != Messages.Count){
			throw new Exception($"Message Count Mismatch: \nCSV: {ogMsgs.Count}\nYAML: {Messages.Count}");
		}

		var ogMsgsEnum = ogMsgs.GetEnumerator();

		// Aplly change made to the yaml to base file
		Messages = Messages.Select(msg => {
			if(ogMsgsEnum.Current.MsgId != msg.MsgId){
				throw new Exception($"Message ID {msg.MsgId} does not match {ogMsgsEnum.Current.MsgId}");
			}
			ogMsgsEnum.Current.eng = msg.eng;
			ogMsgsEnum.Current.jpn = msg.jpn;
			ogMsgsEnum.Current.engc = msg.engc;
			msg = ogMsgsEnum.Current;
			ogMsgsEnum.MoveNext();
			return msg;
		}).ToList();
	}

	public void DumpYaml(string outDir){

		if (SceneName == null)
			throw new Exception("Scene Name is null");
		
		var serializer = new SerializerBuilder()
			.WithNamingConvention(CamelCaseNamingConvention.Instance)
			.Build();
		var yaml = serializer.Serialize(this);
		File.WriteAllText(Path.Join(outDir, $"{SceneName}.yaml"), yaml);
	}

	// TODO: Write a CSV Dumper Function
	public void DumpCsv(){}
}

public class Message {

	[Name("ID")]
	public int MsgId { get; set; }


	private string? _speaker;
    [Name("Speaker")]
    public string? Speaker { 
		get => _speaker;

		set{
			_speaker = value;
			var dbName = NameDb.GetName(int.Parse(value)).eng;
			SpeakerName = dbName;
			if(dbName == "[p]") SpeakerName = "Aiba";
		}
	}

	[Ignore]
	public string? SpeakerName { get; set; }

    [Name("Japanese")]
	public string? jpn { get; set; }

	[Name("English")]
	public string? eng { get; set; }

	[Name("Chinese")]
	[YamlIgnore]
	public string? chn { get; set; }

	[Name("EnglishCensored")]
	public string? engc { get; set; }

	[Name("Korean")]
	[YamlIgnore]
	public string? kor { get; set; }

	[Name("German")]
	[YamlIgnore]
	public string? ger { get; set; }

}

public class MessageRoot
{
    public required List<Message> Messages { get; set; }
}
