using Encog.App.Analyst;
using Encog.App.Analyst.CSV.Normalize;
using Encog.App.Analyst.CSV.Segregate;
using Encog.App.Analyst.CSV.Shuffle;
using Encog.App.Analyst.Wizard;
using Encog.Engine.Network.Activation;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using Encog.Persist;
using Encog.Util.CSV;
using Encog.Util.Simple;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PS_AutoMPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("###################### Step 1 #########################");
            Step1();
            Console.ReadLine(); //waits for enter

            Console.WriteLine("###################### Step 2 #########################");
            Step2();
            Console.ReadLine(); //waits for enter

            Console.WriteLine("###################### Step 3 #########################");
            Step3();
            Console.ReadLine(); //waits for enter

            Console.WriteLine("###################### Step 4 #########################");
            Step4();
            //Console.ReadLine(); //waits for enter
            Console.WriteLine("###################### Step 5 #########################");
            Step5();
            Console.ReadLine(); //waits for enter
            Console.WriteLine("###################### Step 6 #########################");
            Step6();
            Console.WriteLine("Press any key to exit..");
            Console.ReadLine(); //waits for ENTER

            //StepJSON();

        }

        static JObject GetTeam(string key, string propName,string json)
        {
            //string matchIdToFind = "Man Utd";
            //string propName = "name";
            JArray jA = JArray.Parse(json);
            JObject match = jA.Values<JObject>()
                    .Where(m => m[propName].Value<string>() == key)
                    .FirstOrDefault();

            if (match != null)
            {
                foreach (JProperty prop in match.Properties())
                {
                    Console.WriteLine(prop.Name + ": " + prop.Value);
                }
            }
            else
            {
                Console.WriteLine("match not found");
            }

            //Console.ReadLine(); //waits for ENTER
            
            return match;

        }

        static void StepJSON()
        {
            var json = File.ReadAllText(Config.TeamsJ.FullName);
            //dynamic dynJson = JsonConvert.DeserializeObject(json);
            //foreach (var item in dynJson)
            //{
            //    //if item.name = ""
            //    Console.WriteLine("{0} {1} {2} {3}\n", item.id, item.name,
            //        item.current_event_fixture[0].opponent, item.short_name);
            //    Console.ReadLine(); //waits for ENTER
            //}


            /////
            //JToken token = GetTeam("Chelsea", "name", json); ;

            //var Team = (string) token.SelectToken("name");
            //var opp = (string)token.SelectToken("current_event_fixture[0].opponent");
            //var isHome = (string)token.SelectToken("current_event_fixture[0].is_home");
            //int s0 = (int)token.SelectToken("strength_overall_home");
            //int s1 = (int)token.SelectToken("strength_overall_away");
            //int s2 = (int)token.SelectToken("strength_attack_home");
            //int s3 = (int)token.SelectToken("strength_attack_away");
            //int s4 = (int)token.SelectToken("strength_defence_home");
            //int s5 = (int)token.SelectToken("strength_defence_away");


            //Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}\n", Team, s0,s1,s2,s3,s4,s5,opp,isHome);
            //Console.ReadLine(); //waits for ENTER

            //
            using (StreamWriter sw = new StreamWriter(@"C:\app\EPL20160903New.csv"))
            using (CsvWriter cw = new CsvWriter(sw))
            {
                using (var sr = new StreamReader(@"c:\app\EPL20160903.csv"))
                {
                    var reader = new CsvReader(sr);

                    //CSVReader will now read the whole file into an enumerable
                    IEnumerable<EPLPlayer> records = reader.GetRecords<EPLPlayer>();

                    cw.WriteHeader<EPLPlayerOutput>();
                    //LOOP through stream, write out recs
                    
                    foreach (EPLPlayer record in records)
                    {
                        Console.WriteLine("{0} ", record.web_name);
                        //cw.WriteRecord<EPLTeam>(record);
                        cw.WriteField(record.id);
                        cw.WriteField(record.web_name);
                        cw.WriteField(record.first_name);
                        cw.WriteField(record.second_name);
                        cw.WriteField(record.value_form);
                        cw.WriteField(record.value_season);
                        cw.WriteField(record.in_dreamteam);
                        cw.WriteField(record.dreamteam_count);

                        cw.WriteField(record.form);
                        cw.WriteField(record.total_points);
                        cw.WriteField(record.event_points);

                        cw.WriteField(record.points_per_game);
                        cw.WriteField(record.ep_this);
                        cw.WriteField(record.ep_next);

                        cw.WriteField(record.minutes);
                        cw.WriteField(record.goals_scored);
                        cw.WriteField(record.assists);

                        cw.WriteField(record.clean_sheets);
                        cw.WriteField(record.goals_conceded);
                        cw.WriteField(record.own_goals);

                        cw.WriteField(record.penalties_saved);
                        cw.WriteField(record.penalties_missed);
                        cw.WriteField(record.yellow_cards);
                        cw.WriteField(record.red_cards);

                        cw.WriteField(record.saves);
                        cw.WriteField(record.bonus);
                        cw.WriteField(record.bps);

                        cw.WriteField(record.influence);
                        cw.WriteField(record.creativity);
                        cw.WriteField(record.threat);
                        cw.WriteField(record.ict_index);

                        cw.WriteField(record.ea_index);
                        cw.WriteField(record.element_type);
                        cw.WriteField(record.team);

                        JToken token2 = GetTeam(record.team, "id", json);
                        var isHome = (string) token2.SelectToken("current_event_fixture[0].is_home");
                        isHome = isHome.ToUpper();
                        var strengthOverall = (isHome == "TRUE")
                            ? (string) token2.SelectToken("strength_overall_home")
                            : (string) token2.SelectToken("strength_overall_away");
                        cw.WriteField(isHome); //  home
                        cw.WriteField(strengthOverall);  //home away
                        var strengthPos = "";
                        var offense = "3 4";
                        if (offense.Contains(record.element_type))
                        {
                            strengthPos = (isHome == "True")
                            ? (string)token2.SelectToken("strength_attack_home")
                            : (string)token2.SelectToken("strength_attack_away");
                        }
                        else
                        {
                            strengthPos = (isHome == "TRUE")
                           ? (string)token2.SelectToken("strength_defence_home")
                           : (string)token2.SelectToken("strength_defence_away");

                        }
                        ;
                        cw.WriteField(strengthPos);

                        var opp = (string) token2.SelectToken("current_event_fixture[0].opponent");
                        cw.WriteField(opp); ///   get next fixture opponent

                        JToken token3 = GetTeam(opp, "id", json);
                        var strengthOverallOpp = (isHome == "TRUE")
                            ? (string)token3.SelectToken("strength_overall_away")
                            : (string)token3.SelectToken("strength_overall_home");  //opposite player
                        cw.WriteField(strengthOverallOpp);  //home away

                        /// look at opp Def attack strength for position
                        var strengthOppPosition = "";
                        if (offense.Contains(record.element_type))
                        {
                            strengthOppPosition = (isHome == "TRUE")
                             ? (string)token3.SelectToken("strength_defence_away")
                           : (string)token3.SelectToken("strength_defence_home");
                        }
                        else
                        {
                            strengthOppPosition = (isHome == "TRUE")
                           ? (string)token3.SelectToken("strength_attack_away")
                           : (string)token3.SelectToken("strength_attack_home");

                        }
                        cw.WriteField(strengthOppPosition);

                        //cw.WriteField(record.strength_overall_home);
                        //cw.WriteField(record.strength_overall_away);
                        //cw.WriteField(record.strength_attack_home);
                        //cw.WriteField(record.strength_attack_away);
                        //cw.WriteField(record.strength_defence_home);
                        //cw.WriteField(record.strength_defence_away);
                        cw.NextRecord();
                    }

                    Console.ReadLine(); //waits for ENTER
                }
            }


            //string team,team_short;
            //using (GenericParser parser = new GenericParser())

            //{
            //    parser.SetDataSource(Config.TeamsCSV.FullName);
            //    parser.FirstRowHasHeader = true;

            //    while (parser.Read())
            //    {
            //        //strID = parser["ID"];
            //        team = parser["name"];
            //        team_short = parser["short_name"];
            //        Console.WriteLine("{0}", team);
            //        // Your code here ...
            //    }
            //    Console.ReadLine(); //waits for ENTER
            //}

        }

        #region "Step1 : Shuffle"

        static void Step1()
        {
            Console.WriteLine("Step 1: Shuffle CSV Data File");
            Shuffle(Config.BaseFile);
        }

        static void Shuffle(FileInfo source)
        {
            //Shuffle the CSV File
            var shuffle = new ShuffleCSV();
            shuffle.Analyze(source, true, CSVFormat.English);
            shuffle.ProduceOutputHeaders = true;
            shuffle.Process(Config.ShuffledBaseFile);

        }


        #endregion

        
        #region "Step2 : Segregate"

        static void Step2()
        {
            Console.WriteLine("Step 2: Generate training and Evaluation  file");
            Segregate(Config.ShuffledBaseFile);

        }

        static void Segregate(FileInfo source)
        {
            //Segregate source file into training and evaluation file
            var seg = new SegregateCSV();
            seg.Targets.Add(new SegregateTargetPercent(Config.TrainingFile, 75));
            seg.Targets.Add(new SegregateTargetPercent(Config.EvaluateFile, 25));
            seg.ProduceOutputHeaders = true;
            seg.Analyze(source, true, CSVFormat.English );
            seg.Process();

        
        }

        #endregion


        #region "Step3 : Normalize"
        static void Step3()
        {
            Console.WriteLine("Step 3: Normalize Training and Evaluation Data");

            //Analyst
            var analyst = new EncogAnalyst();


            //Wizard
            var wizard = new AnalystWizard(analyst);
            wizard.Wizard(Config.BaseFile, true, AnalystFileFormat.DecpntComma);
            //Cylinders
            analyst.Script.Normalize.NormalizedFields[0].Action = Encog.Util.Arrayutil.NormalizationAction.Equilateral;
            //displacement
            analyst.Script.Normalize.NormalizedFields[1].Action = Encog.Util.Arrayutil.NormalizationAction.Normalize;
            //HorsePower
            analyst.Script.Normalize.NormalizedFields[2].Action = Encog.Util.Arrayutil.NormalizationAction.Normalize;
            //weight
            analyst.Script.Normalize.NormalizedFields[3].Action = Encog.Util.Arrayutil.NormalizationAction.Normalize;
            //Acceleration
            analyst.Script.Normalize.NormalizedFields[4].Action = Encog.Util.Arrayutil.NormalizationAction.Normalize;
            //year
            analyst.Script.Normalize.NormalizedFields[5].Action = Encog.Util.Arrayutil.NormalizationAction.Equilateral;
            //Origin
            analyst.Script.Normalize.NormalizedFields[6].Action = Encog.Util.Arrayutil.NormalizationAction.Equilateral;
            //Name
            analyst.Script.Normalize.NormalizedFields[7].Action = Encog.Util.Arrayutil.NormalizationAction.Ignore;
            //mpg
            analyst.Script.Normalize.NormalizedFields[8].Action = Encog.Util.Arrayutil.NormalizationAction.Normalize;

            ////Wizard
            //var wizard = new AnalystWizard(analyst);
            //wizard.Wizard(Config.BaseFile, true, AnalystFileFormat.DecpntComma);
            ////Cylinders
            //analyst.Script.Normalize.NormalizedFields[0].Action = Encog.Util.Arrayutil.NormalizationAction.Ignore;
            ////displacement
            //analyst.Script.Normalize.NormalizedFields[1].Action = Encog.Util.Arrayutil.NormalizationAction.Ignore;
            ////HorsePower
            //analyst.Script.Normalize.NormalizedFields[2].Action = Encog.Util.Arrayutil.NormalizationAction.Ignore;
            ////weight
            //analyst.Script.Normalize.NormalizedFields[3].Action = Encog.Util.Arrayutil.NormalizationAction.Normalize;
            ////displacement
            //analyst.Script.Normalize.NormalizedFields[4].Action = Encog.Util.Arrayutil.NormalizationAction.Ignore;
            ////HorsePower
            ////year
            //analyst.Script.Normalize.NormalizedFields[5].Action = Encog.Util.Arrayutil.NormalizationAction.Ignore;
            ////year
            //analyst.Script.Normalize.NormalizedFields[6].Action = Encog.Util.Arrayutil.NormalizationAction.Equilateral;
            ////Origin
            //analyst.Script.Normalize.NormalizedFields[7].Action = Encog.Util.Arrayutil.NormalizationAction.Ignore;
            ////Acceleration
            //analyst.Script.Normalize.NormalizedFields[8].Action = Encog.Util.Arrayutil.NormalizationAction.Normalize;
            ////Acceleration
            //analyst.Script.Normalize.NormalizedFields[9].Action = Encog.Util.Arrayutil.NormalizationAction.Normalize;

            //Norm for Trainng
            var norm = new AnalystNormalizeCSV();
            norm.ProduceOutputHeaders = true;
          
            norm.Analyze(Config.TrainingFile,true, CSVFormat.English, analyst);
            norm.Normalize(Config.NormalizedTrainingFile);

            //Norm of evaluation
            norm.Analyze(Config.EvaluateFile, true, CSVFormat.English, analyst);
            norm.Normalize(Config.NormalizedEvaluateFile);

            //save the analyst file
            analyst.Save(Config.AnalystFile);
        }


        #endregion

        
        #region "Step4 : Create Network"

        static void Step4()
        {
            Console.WriteLine("Step 4: Create Neural Network");
            CreateNetwork(Config.TrainedNetworkFile);
        }

        static void CreateNetwork(FileInfo networkFile)
        {
            var network = new BasicNetwork();
            network.AddLayer(new BasicLayer(new ActivationLinear(), true, 22));
            network.AddLayer(new BasicLayer(new ActivationTANH(), true, 6));
            network.AddLayer(new BasicLayer(new ActivationTANH(), false, 1));
            //network.AddLayer(new BasicLayer(new ActivationLinear(), true, 5));
            //network.AddLayer(new BasicLayer(new ActivationTANH(), true, 3));
            //network.AddLayer(new BasicLayer(new ActivationTANH(), false, 1));
            network.Structure.FinalizeStructure();
            network.Reset();
            EncogDirectoryPersistence.SaveObject(networkFile, (BasicNetwork)network);

        }

        #endregion

        #region "Step5 : Train Network"

        static void Step5()
        {
            Console.WriteLine("Step 5: Train Neural Network");
            TrainNetwork();
        }

        static void TrainNetwork()
        {
            var network = (BasicNetwork)EncogDirectoryPersistence.LoadObject(Config.TrainedNetworkFile);
            var trainingSet = EncogUtility.LoadCSV2Memory(Config.NormalizedTrainingFile.ToString(),
                network.InputCount, network.OutputCount, true, CSVFormat.English, false);


            var train = new ResilientPropagation(network, trainingSet);
            int epoch = 1;
            do
            {
                train.Iteration();
                Console.WriteLine("Epoch : {0} Error : {1}", epoch, train.Error);
                epoch++;
            } while (train.Error > 0.01);

            EncogDirectoryPersistence.SaveObject(Config.TrainedNetworkFile, (BasicNetwork)network);

        }

        #endregion


        #region "Step6 : Evaluate"
        static void Step6()
        {
            Console.WriteLine("Step 6: Evaluate Network");
            Evaluate();
        }
        
        static void Evaluate()
        {
            var network = (BasicNetwork)EncogDirectoryPersistence.LoadObject(Config.TrainedNetworkFile);
            var analyst = new EncogAnalyst();
            analyst.Load(Config.AnalystFile.ToString());
            var evaluationSet = EncogUtility.LoadCSV2Memory(Config.NormalizedEvaluateFile.ToString(), 
                network.InputCount, network.OutputCount, true, CSVFormat.English, false);
            
            using(var file = new System.IO.StreamWriter(Config.ValidationResult.ToString()))
            {
                file.WriteLine("Ideal,Actual");
                foreach (var item in evaluationSet)
                {
                    // WAS 8
                    var NormalizedActualoutput = (BasicMLData)network.Compute(item.Input);
                    var Actualoutput = analyst.Script.Normalize.NormalizedFields[8].DeNormalize(NormalizedActualoutput.Data[0]);
                    var IdealOutput = analyst.Script.Normalize.NormalizedFields[8].DeNormalize(item.Ideal[0]);

                    //Write to File
                    var resultLine = IdealOutput.ToString() + "," + Actualoutput.ToString();
                    file.WriteLine(resultLine);
                    Console.WriteLine("Ideal : {0}, Actual : {1}", IdealOutput, Actualoutput);

                }

            }

          






        }

        #endregion

        public static DataTable ReadCsvFile(string csvName)
        {

            DataTable dtCsv = new DataTable();
            string Fulltext;

                string FileSaveWithPath = csvName;  //Config.TeamsJ.FullName;
                using (StreamReader sr = new StreamReader(FileSaveWithPath))
                {
                    while (!sr.EndOfStream)
                    {
                        Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                        string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                        for (int i = 0; i < rows.Count() - 1; i++)
                        {
                            string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                            {
                                if (i == 0)
                                {
                                    for (int j = 0; j < rowValues.Count(); j++)
                                    {
                                        dtCsv.Columns.Add(rowValues[j]); //add headers  
                                    }
                                }
                                else
                                {
                                    DataRow dr = dtCsv.NewRow();
                                    for (int k = 0; k < rowValues.Count(); k++)
                                    {
                                        dr[k] = rowValues[k].ToString();
                                    }
                                    dtCsv.Rows.Add(dr); //add other rows  
                                }
                            }
                        }
                    }
                }
            
            return dtCsv;
        }

    }
}
