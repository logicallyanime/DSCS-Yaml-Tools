using CsvHelper;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using CsvHelper.Configuration.Attributes;

public class VoiceDb
{
	public Dictionary<string, Dictionary<int, string>> Scenes { get; set; } = [];

	public Dictionary<string, string> Lexicon { get; set; } = [];
	private Regex regex = new Regex(@"((?:\w\d{2,3}_){2}\d{4})_(\d{3,5})");
	public VoiceDb()
	{
		using var reader = new StreamReader("./DSDB/data/voice.csv", Encoding.UTF8);
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
			csv.Read();
			csv.ReadHeader();
			while (csv.Read())
			{
				var name = csv.GetField<string>("name") ?? throw new ArgumentNullException($"name is null at index {csv.CurrentIndex}");
				var cri_id = csv.GetField<int>("cri_contents_id");
				if(csv.GetField<string>("cri_dbname") != "DSDBvo") continue;
				Lexicon[cri_id.ToString("x6")] = name;

				Match match = regex.Match(name);

				if(!match.Success) continue;

				
				string sceneName = match.Groups[1].Value;
				int messageid = int.Parse(match.Groups[2].Value);

				if(!Scenes.ContainsKey(sceneName)) Scenes.Add(sceneName, new Dictionary<int, string>());


				if(Scenes[sceneName].ContainsKey(messageid)) throw new Exception(
					$"messageid {messageid} already exists in scene {sceneName} CSV index: {csv.CurrentIndex}"
				);

				Scenes[sceneName].Add(messageid, cri_id.ToString("x6"));

			}


		};
    }
}
