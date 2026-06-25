using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace part2
{//start of namespace
    public class nlp_training
    {//start of nlp_training class

        //all ML microsoft classes instances with object names
        private MLContext mlContext = new MLContext();
        private ITransformer model;
        private PredictionEngine<TrainingData, AnswerPrediction> predictionEngine;


         //method to train the CyberBot
        public void Train()
        {//start of train method

            //get data
            var data = new searching_answers();
            var answers = data.return_answers().Cast<string>().ToList();
            var ignore = data.ignores().Cast<string>().ToList();

            //prepare data
            var trainingData = new List<TrainingData>();

            for (int i = 0; i < answers.Count; i++)
            {
                string clean = CleanText(answers[i], ignore);
                trainingData.Add(new TrainingData { Question = clean, Answer = answers[i] });
            }

            //convert to IDataView
            var dataView = mlContext.Data.LoadFromEnumerable(trainingData);

            //TRAIN
            var pipeline = mlContext.Transforms.Conversion
                .MapValueToKey("Label", "Answer")
                .Append(mlContext.Transforms.Text.FeaturizeText("Features", "Question"))
                .Append(mlContext.MulticlassClassification.Trainers
                    .SdcaMaximumEntropy("Label", "Features"))
                .Append(mlContext.Transforms.Conversion
                    .MapKeyToValue("PredictedLabel", "PredictedLabel"));

            model = pipeline.Fit(dataView);

            // Save model
            mlContext.Model.Save(model, dataView.Schema, "nlp_model.zip");

            // Create prediction engine
            predictionEngine = mlContext.Model.CreatePredictionEngine<TrainingData, AnswerPrediction>(model);
        }//end of train method

        //method to load the trained model
        public void LoadModel()
        {//start of LoadModel method
            if (File.Exists("nlp_model.zip"))
            {
                DataViewSchema schema;
                model = mlContext.Model.Load("nlp_model.zip", out schema);
                predictionEngine = mlContext.Model.CreatePredictionEngine<TrainingData, AnswerPrediction>(model);
            }
        }//end of LoadModel method

        
        private string CleanText(string text, List<string> ignore)
        {//start of CleanText method

            string clean = text.ToLower();
            foreach (string word in ignore)
                clean = clean.Replace(word, "");

            clean = new string(clean.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray());
            return System.Text.RegularExpressions.Regex.Replace(clean, @"\s+", " ").Trim();

        }//end of CleanText method

        //check and learn the method
        public string CheckAndLearn(string userInput, string username = "user")
        {//start of CheckAndLearn method

            string message = string.Empty;
            string task_name = string.Empty;

            //check if user entered valid question (not empty and has at least 2 words)
            if (!string.IsNullOrWhiteSpace(userInput) && userInput.Split(' ').Length >= 2)
            {//start of if

                bool found = false;
                ArrayList found_answers = new ArrayList();

                //turn the users input to array by split
                string[] find_words = userInput.ToLower().Split(new char[] { ' ', ',', '.', '?', '!', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

                //get data
                var data = new searching_answers();
                ArrayList ai_answers = data.return_answers();
                ArrayList ai_questions = data.user_questions();
                ArrayList ignore = data.ignores();

                //try to predict using ML model first
                if (predictionEngine != null)
                {//start of if
                    var prediction = predictionEngine.Predict(new TrainingData { Question = userInput });
                    if (!string.IsNullOrEmpty(prediction.PredictedAnswer))
                    {//start of nested if

                        // Check if predicted answer exists in answers
                        bool answerExists = false;
                        foreach (string answer in ai_answers)
                        {//start of foreach
                            if (answer.ToLower() == prediction.PredictedAnswer.ToLower())
                            {
                                answerExists = true;
                                break;
                            }
                        }//end of foreach

                        if (answerExists)
                        {
                            found_answers.Add(prediction.PredictedAnswer);
                            found = true;
                        }
                    }//end of nested if
                }//end of if

                //if ML didn't find anything, use arrays keyword matching
                if (!found)
                {//start of if for keyword matching

                    for (int index = 0; index < find_words.Length; index++)
                    {
                        string searched_by_word = find_words[index].ToString();

                        //check for user interests
                        if (searched_by_word.ToLower() == "interested")
                        {
                            string store_interests = string.Empty;
                            bool found_interest = false;
                            HashSet<string> currentInterests = new HashSet<string>();

                            foreach (string interest in find_words)
                            {//start of foreach
                                string clean = interest.ToLower().Trim();
                                clean = System.Text.RegularExpressions.Regex.Replace(clean, @"[^a-zA-Z0-9\s]", "");

                                if (!ignore.Contains(clean) && clean != "interested" && clean != "and" && clean != "in" && clean.Length >= 3)
                                {
                                    found_interest = true;
                                    currentInterests.Add(clean);
                                }
                            }//end of foreach

                            store_interests = string.Join(", ", currentInterests);

                            if (found_interest && !string.IsNullOrWhiteSpace(store_interests))
                            {//start of if for found_interest

                                string filename = "interests.txt";
                                bool userFound = false;

                                if (File.Exists(filename))
                                {//start of if for file exists

                                    string[] lines = File.ReadAllLines(filename);

                                    for (int i = 0; i < lines.Length; i++)
                                    {
                                        if (lines[i].StartsWith(username))
                                        {
                                            userFound = true;
                                            string existing = lines[i].Replace(username + " interested in:", "").ToLower();
                                            HashSet<string> existingSet = new HashSet<string>(existing.Split(',').Select(x => x.Trim()).Where(x => x != ""));

                                            foreach (string item in currentInterests)
                                            {
                                                existingSet.Add(item);
                                            }

                                            string finalList = string.Join(", ", existingSet);
                                            lines[i] = username + " interested in: " + finalList;
                                            File.WriteAllLines(filename, lines);

                                            found_answers.Add("great, i added " + store_interests + " to your interests");
                                            break;
                                        }
                                    }
                                }//end of if for file exists

                                if (!userFound)
                                {
                                    File.AppendAllText(filename, username + " interested in: " + store_interests + "\n");
                                    found_answers.Add("great, i will remember that you are interested in " + store_interests);
                                }
                            }//end of if for found_interest
                            else
                            {
                                found_answers.Add("Please specify what you're interested in (e.g., 'I am interested in cybersecurity')");
                            }

                            found = true;
                            break;
                        }

                        //check if the word is not ignored
                        if (!ignore.Contains(searched_by_word))
                        {
                            //search for answers using keyword matching
                            foreach (string randomize in ai_answers)
                            {//start of foreach 
                                if (randomize.ToLower().Contains(searched_by_word.ToLower()))
                                {
                                    found = true;
                                    if (!found_answers.Contains(randomize))
                                    {
                                        task_name += searched_by_word + " ";
                                        found_answers.Add(randomize);
                                    }
                                }
                            }//end of foreach
                        }
                    }
                }//end of if for keyword matching

                //learn from user input if not found
                if (!found || found_answers.Count == 0)
                {
                    //save unknown question for learning
                    string unknownFile = "unknown_questions.txt";
                    string learnFile = "learned_questions.txt";

                    //check if this question was already learned
                    bool alreadyLearned = false;
                    if (File.Exists(learnFile))
                    {
                        string[] learnedLines = File.ReadAllLines(learnFile);
                        foreach (string line in learnedLines)
                        {
                            if (line.StartsWith(userInput.ToLower()))
                            {
                                alreadyLearned = true;
                                break;
                            }
                        }
                    }
                     //if statement to check if question is already learned
                    if (!alreadyLearned)
                    {//start of alreasdylearned if statement

                        //save to unknown questions for training later
                        File.AppendAllText(unknownFile, userInput + "|" + username + "|" + DateTime.Now + "\n");

                        //try to learn from context if possible
                        string contextAnswer = ExtractContextAnswer(userInput, ai_answers);
                        if (!string.IsNullOrEmpty(contextAnswer))
                        {
                            File.AppendAllText(learnFile, userInput.ToLower() + "|" + contextAnswer + "\n");
                            found_answers.Add(contextAnswer);
                            found = true;
                        }
                    }//end of alreadylearned if 

                    //if statement for error message if not found
                    if (!found)
                    {
                        message = "i didn't quite understand that. could you please rephrase your question?";
                    }
                }//end of if statement for learning from user input

                //build the response message
                if (found && found_answers.Count > 0)
                {
                    int count = found_answers.Count;
                    int counting = 0;

                    foreach (string get_answer in found_answers)
                    {
                        if (counting == count - 1)
                        {
                            message += get_answer;
                        }
                        else
                        {
                            message += get_answer + "\n           and ";
                        }
                        counting++;
                    }
                }
                else if (string.IsNullOrEmpty(message))
                {
                    message = "i didn't quite understand that. could you please rephrase your question?";
                }
            }
            else
            {
                message = "i didn't quite understand that. could you please rephrase your question?";
            }

            return message;
        }

        //method to extract context answer from user input
        private string ExtractContextAnswer(string userInput, ArrayList ai_answers)
        {//start of ExtractContextAnswer method

            //try to extract keywords and find matching answer
            string[] words = userInput.ToLower().Split(' ');

            foreach (string word in words)
            {//start of foreach

                if (word.Length > 3)
                {
                    foreach (string answer in ai_answers)
                    {
                        if (answer.ToLower().Contains(word))
                        {
                            return answer;
                        }
                    }
                }
            }//end of foreach

            return string.Empty;

        }//end of ExtractContextAnswer method

    }//end of nlp_training class
}//end of namespace