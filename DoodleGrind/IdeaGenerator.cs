using Syn.WordNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleGrind
{
    internal class IdeaGenerator
    {
        private readonly WordNetEngine wordnet;

        public IdeaGenerator() 
        {
            this.wordnet = new WordNetEngine();
            InitWordNetEngine(this.wordnet);
        }

        public string[] GetRandomWords(int count)
        {
            Random random = new();

            List<PartOfSpeech> keys = new List<PartOfSpeech>(this.wordnet.AllWords.Keys);
            keys.Remove(PartOfSpeech.Adverb); // exclude adverbs for now
            PartOfSpeech randomKey = keys[random.Next(keys.Count)];

            List<string> words = new List<string>(this.wordnet.AllWords[randomKey]);

            List<string> randomWords = [];
            for (int i = 0; i < count; i++)
            {
                randomWords.Add(words[random.Next(words.Count)]);
            }

            return randomWords.ToArray();
        }

        private static void InitWordNetEngine(WordNetEngine wordNet)
        {
            string projectRootPath = Helpers.GetProjectRootPath();
            string directory = Path.Combine(projectRootPath, "Data", "wordnet");

            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.adj")), PartOfSpeech.Adjective);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.adv")), PartOfSpeech.Adverb);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.noun")), PartOfSpeech.Noun);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.verb")), PartOfSpeech.Verb);

            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.adj")), PartOfSpeech.Adjective);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.adv")), PartOfSpeech.Adverb);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.noun")), PartOfSpeech.Noun);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.verb")), PartOfSpeech.Verb);

            System.Diagnostics.Debug.WriteLine("Loading database...");
            wordNet.Load();
            System.Diagnostics.Debug.WriteLine("Load completed.");
        }
    }
}
