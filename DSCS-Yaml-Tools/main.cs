using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;


// var voice = new VoiceDb();

// var MsgId = 3;

// void extractUnkScenes(VoiceDb voiceDB){

//     var scenes = voiceDB.Scenes.Keys.ToList();

//     foreach(var sceneName in scenes)
//     {
//         var scenePath = Path.Join(".\\DSDB\\message", sceneName + ".mbe", "Sheet1.csv");
//         if(File.Exists(scenePath)) continue;
//         foreach(var voice in voiceDB.Scenes[sceneName].Values){
            
//             System.Console.WriteLine($"Scene Name: {sceneName} File: {voice}");

//         }
//     }

// }
;
var scene = new Scene(".\\DSDB\\message\\m00_d02_0501.mbe");
scene.DumpYaml(".\\");
// var serializer = new SerializerBuilder()
//     .WithNamingConvention(CamelCaseNamingConvention.Instance)
//     .Build();
// var deserializer = new DeserializerBuilder()
//     .WithNamingConvention(CamelCaseNamingConvention.Instance)
//     .Build();

// var yaml = serializer.Serialize(scene);

// File.WriteAllText(".\\test.yaml", yaml);

// string yamlStr = File.ReadAllText(".\\test.yaml");
// var yamlds = deserializer.Deserialize<Scene>(yamlStr);

// var yamlRd = new YamlStream();


// using (var reader = new StreamReader(".\\test.yaml", Encoding.UTF8)){
//     // yamlRd.Load(reader);
//     // var doc = yamlRd.Documents[0];
//     // var root = doc.RootNode;
// }



// System.Console.WriteLine(NameDb.GetName(4244).eng);

// string BASE_PATH = "C:\\Users\\Admin\\Desktop\\Code\\Cs\\DSCS-Yaml-Tools\\DSDBvo";
// string OUTPUT_PATH = "C:\\Users\\Admin\\Desktop\\Code\\Cs\\DSCS-Yaml-Tools\\DSDBvo_RN";

// string[] files = Directory.GetFiles(BASE_PATH, "*.hca");


// foreach(var file in files)
// {
//     var fileBase = Path.GetFileNameWithoutExtension(file);
//     var newFile = Path.Join(OUTPUT_PATH, voice.Lexicon[fileBase] + ".hca");

//     if(!File.Exists(newFile)) File.Copy(file, newFile);
// }


