using Microsoft.ML.Data;

namespace part2
{//start of namespace

    //prediction result class
    public class AnswerPrediction
    {//start of AnswerPrediction class

        //defined columns with getters and setters to get related results
        [ColumnName("PredictedLabel")]
        public string PredictedAnswer { get; set; }
        public float[] Score { get; set; }



    }//end of AnswerPrediction class
}//end of namespace