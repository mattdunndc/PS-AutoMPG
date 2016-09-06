using Encog.Util.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_AutoMPG
{
   public static  class Config
    {

        public static FileInfo BasePath = new FileInfo(@"C:\Users\MattDunn\Downloads\introduction-to-machine-learning-encog\9-introduction-to-machine-learning-encog-m9-case-studies-exercise-files\CaseStudy2\Code\PS-AutoMPG\PS-AutoMPG\Data\");
        //public static FileInfo BasePath = new FileInfo(@"C:\app\");



        public static FileInfo TeamsJ = FileUtil.CombinePath(BasePath, "EPL-TEAMS20160830.json");
        public static FileInfo TeamsCSV = FileUtil.CombinePath(BasePath, "EPL-TEAMS20160830.csv");

        public static FileInfo PlayersJ = FileUtil.CombinePath(BasePath, "EPL20160903.json");
        public static FileInfo PlayersCSV = FileUtil.CombinePath(BasePath, "EPL20160903.csv");

        #region "Step1"

        public static FileInfo BaseFile = FileUtil.CombinePath(BasePath, "AutoMPG.csv");
        public static FileInfo ShuffledBaseFile = FileUtil.CombinePath(BasePath, "AutoMPG_Shuffled.csv");
        //public static FileInfo BaseFile = FileUtil.CombinePath(BasePath, "EPL20160903New3.csv");
        //public static FileInfo ShuffledBaseFile = FileUtil.CombinePath(BasePath, "EPL_Shuffled.csv");

        #endregion


        #region "Step2"
        public static FileInfo TrainingFile = FileUtil.CombinePath(BasePath, "AutoMPG_Train.csv");
        public static FileInfo EvaluateFile = FileUtil.CombinePath(BasePath, "AutoMPG_Eval.csv");
        //public static FileInfo TrainingFile = FileUtil.CombinePath(BasePath, "EPL_Train.csv");
        //public static FileInfo EvaluateFile = FileUtil.CombinePath(BasePath, "EPL_Eval.csv");
        #endregion


        #region "Step3"

        public static FileInfo NormalizedTrainingFile = FileUtil.CombinePath(BasePath, "AutoMPG_Train_Norm.csv");
        public static FileInfo NormalizedEvaluateFile = FileUtil.CombinePath(BasePath, "AutoMPG_Eval_Norm.csv");
        public static FileInfo AnalystFile = FileUtil.CombinePath(BasePath, "AutoMPG_Analyst.ega");
        //public static FileInfo NormalizedTrainingFile = FileUtil.CombinePath(BasePath, "EPL_Train_Norm.csv");
        //public static FileInfo NormalizedEvaluateFile = FileUtil.CombinePath(BasePath, "EPL_Eval_Norm.csv");
        //public static FileInfo AnalystFile = FileUtil.CombinePath(BasePath, "EPL_Analyst.ega");

        #endregion


        #region "Step4"

        public static FileInfo TrainedNetworkFile = FileUtil.CombinePath(BasePath, "AutoMPG_Train.eg");
        //public static FileInfo TrainedNetworkFile = FileUtil.CombinePath(BasePath, "EPL_Train.eg");
        #endregion


        #region "Step5"
        #endregion

        #region "Step6"

        //public static FileInfo ValidationResult = FileUtil.CombinePath(BasePath, "AutoMPG_ValidationResult.csv");
        public static FileInfo ValidationResult = FileUtil.CombinePath(BasePath, "EPL_ValidationResult.csv");

        #endregion



    }
}
