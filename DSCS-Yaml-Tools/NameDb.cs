using CsvHelper;
using System.Globalization;
using System.Text;
using CsvHelper.Configuration.Attributes;

public class NameDb
{
	public  List<SpeakerName> Names { get; set; } = new List<SpeakerName>();
	public NameDb()
	{
		using var reader = new StreamReader("./DSDB/text/charname.mbe/Sheet1.csv", Encoding.UTF8);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        	this.Names = csv.GetRecords<SpeakerName>().ToList();
    }
}

public class SpeakerName {

	[Name("ID")]
	public int Id { get; set; }

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
