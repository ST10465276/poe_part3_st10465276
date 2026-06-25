using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace part2
{//start of namespace
    public class check_questions
    {//start of check_questions class

        //creating an instance for the chats_color class
        //with an object name called get_chats_color
        chats_colours get_chats_color = new chats_colours();

        //creating an instance for the searching answers class
        //with an object name searching
        searching_answers searching = new searching_answers();
     
        //creating two instances for the arraylist 
        private ArrayList ai_answers, ai_questions, ignore;

        //creating an instance for a store_answers class 
        //with an object name answers 
        searching_answers answers = new searching_answers();

        //check if stored before
        bool stored = false;

        //database class tasks
        add_tasks adding_tasks = new add_tasks();

        //train the CyberBot using Machine learning , the NLP
        nlp_training cyberbot_train = new nlp_training();


        //global variables to work with tasks

        private string task_name = string.Empty;
        private string task_description = string.Empty;
        private string task_reminder = string.Empty;
        private bool add_task_found = false;

        //method to return answers
        public string searching_response(string question, string username)
        {//start of searching method

            //temp message variable
            string message = string.Empty;

            //check if not stored before
            if (!stored)
            {
                //auto store
                auto_store();
                cyberbot_train.Train();
            }

            //for adding tasks, check if what the user typed starts with add task or add a task all in lower case
            if (question.ToLower().StartsWith("add task") || question.ToLower().StartsWith("add a task"))
            {
                message += "Task added with the description \" ";
                add_task_found = true;
            }

            //remove special characters from the question
            string questions = RemoveSpecialCharacters(question);
                       
            //then recheck for reminder
            if (questions.ToLower().StartsWith("yes remind me in") && add_task_found)
            {//start of if 
               
                //then replace the yes remind me in
                string reminding = questions.Replace("yes remind me in", " ");

                // Get the number - remove any letters
                string numberOnly = System.Text.RegularExpressions.Regex.Replace(reminding, @"[^0-9]", "");
                int days = int.Parse(numberOnly);
                DateTime reminderDate = DateTime.Now.AddDays(days);

                message += "Got it!! I'll remind you in " + reminding + " on " + reminderDate.ToString("MMMM dd, yyyy") + ".";

                task_reminder = reminderDate.ToString("MMMM dd, yyyy");

                //then store to the database all the tasks
                //username , task name , description and reminder day
                adding_tasks.add_each_tasks(username, task_name, task_description , task_reminder);
                //clear all
                task_description = string.Empty;
                task_name= string.Empty;
                task_name = string.Empty;
                add_task_found= false;

                //then terminate
                return message;

            }//end of if 

            //reminder for today
            if (questions.ToLower().StartsWith("yes remind me today") && add_task_found)
            {
                //then replace the yes remind me in
                string reminding = questions.Replace("yes remind me today", " ");

                DateTime reminderDate = DateTime.Now;

                message += "Got it!! Today's reminder is set, the system will remind you soon.";

                task_reminder = reminderDate.ToString("MMMM dd, yyyy");

                //then store to the database all the tasks
                //username , task name , description and reminder day
                adding_tasks.add_each_tasks(username, task_name, task_description, task_reminder);
                //clear all
                task_description = string.Empty;
                task_name = string.Empty;
                task_name = string.Empty;
                add_task_found = false;

                //then terminate
                return message;

            }

                //then recheck for reminder
                if (questions.ToLower().StartsWith("no") && add_task_found)
            {
                //then replace the yes remind me in
                string reminding = questions.Replace("yes remind me in", " ");

                message += "Got it!! no reminder set.";

                task_reminder ="none";

                //then store to the database all the tasks
                //username , task name , description and reminder day
                adding_tasks.add_each_tasks(username, task_name, task_description, task_reminder);

                //clear all
                task_description = string.Empty;
                task_name = string.Empty;
                task_name = string.Empty;
                add_task_found = false;

                //then terminate
                return message;
            }

            //check if user entered valid question (not empty and has at least 2 words)
            if (!string.IsNullOrWhiteSpace(questions) && questions.Length > 0)
            {//start of if 

                //local variable declaration
                bool found = false;
                int index_found = 0;

                //turn the users input to array by split
                string[] find_words = questions.ToLower().Split(new char[] { ' ', ',', '.', '?', '!', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
                ArrayList found_answers = new ArrayList();

                //using for loop for searching the answer
                for (int index = 0; index < find_words.Length; index++)
                {//start of for loop

                    //temp variable
                    string searched_by_word = find_words[index].ToString();
                    string answer = string.Empty;

                    //inner for loop to check answers by words while ignoring
                    for (int ignores = 0; ignores < ai_questions.Count; ignores++)
                    {
                        //temp variable
                        string searched = ai_questions[ignores].ToString();

                        //using if statement to check whether the word is not ignored
                        if (!ignore.Contains(searched_by_word))
                        {//start of if

                            index_found = ignores;

                            //get all answers 
                            ArrayList all_found = new ArrayList();
                            Random get_answer = new Random();

                            //start of interests

                            //if/else statement for user interest message
                            if (searched_by_word.ToLower() == "interested")
                            {//start of if

                                string store_interests = string.Empty;
                                bool found_interest = false;
                                HashSet<string> currentInterests = new HashSet<string>();

                                foreach (string interest in find_words)
                                {//start of foreach

                                    //clean input
                                    string clean = interest.ToLower().Trim();
                                    clean = System.Text.RegularExpressions.Regex.Replace(clean, @"[^a-zA-Z0-9\s]", "");

                                    //filter word noise
                                    if (!ignore.Contains(clean) && clean != "interested" && clean != "and" && clean != "in" && clean.Length >= 3)
                                    {
                                        found_interest = true;
                                        currentInterests.Add(clean);
                                    }
                                }//end of foreach

                                // prepare interests
                                store_interests = string.Join(", ", currentInterests);

                                if (found_interest && !string.IsNullOrWhiteSpace(store_interests))
                                {
                                    string filename = "interests.txt";
                                    bool userFound = false;

                                    if (File.Exists(filename))
                                    {
                                        string[] lines = File.ReadAllLines(filename);

                                        for (int i = 0; i < lines.Length; i++)
                                        {
                                            if (lines[i].StartsWith(username))
                                            {
                                                userFound = true;

                                                //get all the interests
                                                string existing = lines[i].Replace(username + " interested in:", "").ToLower();

                                                HashSet<string> existingSet = new HashSet<string>(existing.Split(',').Select(x => x.Trim()).Where(x => x != ""));

                                                //remove duplicates
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
                                    }

                                    if (!userFound)
                                    {
                                        File.AppendAllText(
                                            filename,
                                            username + " interested in: " + store_interests + "\n"
                                        );

                                        found_answers.Add("great, i will remember that you are interested in " + store_interests);
                                    }
                                }
                                else
                                {
                                    found_answers.Add("Please specify what you're interested in (e.g., 'I am interested in cybersecurity')");
                                }

                                found = true;
                                break;

                            }//end of if

                            //end of interests

                            foreach (string randomize in ai_answers)
                            {//start of foreach

                                if (randomize.ToLower().Contains(searched_by_word.ToLower()))
                                {
                                    //if question is found, assign to true then assign index
                                    found = true;
                                    all_found.Add(randomize.Substring(searched_by_word.Length));
                                   
                                }
                            }//end of foreach

                            //ONLY proceed if we actually found matches
                            if (all_found.Count > 0)
                            {
                                int indexes = get_answer.Next(all_found.Count);
                                string selected = all_found[indexes].ToString();

                                //avoid duplicate answers
                                if (!found_answers.Contains(selected))
                                {
                                    task_name += searched_by_word+" ";
                                    found_answers.Add(selected);
                                }
                            }

                        }//end of if
                    }//end of inner for loop
                }//end of for loop

                //display the found answer or error message
                //using if else statements 
                if (found && found_answers.Count > 0)
                {//start of if

                    int count = found_answers.Count;
                    int counting = 0;

                    foreach (string get_answer in found_answers)
                    {
                        if (counting == count - 1)
                        {
                            // Last item
                            message += get_answer;
                        }
                        else
                        {
                            // Not last item
                            message += get_answer + "\n           and ";
                        }
                        counting++;
                    }
                }//end of if
                else
                {//start of else
                 // Check and learn from user input
                    string response = cyberbot_train.CheckAndLearn(question);
                   

                    // It will learn from unknown questions
                    string unknownResponse = cyberbot_train.CheckAndLearn(question);
                    //display error message for message not found
                    message =unknownResponse;

                }//end of else
            }//end of if
            else
            {//start of else 

                //display error message when question is empty or invalid
                //check and learn from user input
                string response = cyberbot_train.CheckAndLearn(question);

                //it will learn from unknown questions
                string unknownResponse = cyberbot_train.CheckAndLearn(question);

                //display error message for message not found
                message = unknownResponse;

            }//end of else


            if (question.ToLower().StartsWith("add task") || question.ToLower().StartsWith("add a task"))
            {//start if statement for adding a task then ask to set a reminder 

                message += "  .\"  Would you like a reminder ? ";
                task_description = message;

            }//end of if statement for asking to set a reminder


            //return
            return message;

        }//end of searching method

        //method to remove special characters and require at least 2 words
        private string RemoveSpecialCharacters(string input)
        {//start of method

            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            StringBuilder sanitized = new StringBuilder();

            foreach (char c in input)
            {
                //keep letters, numbers, spaces, and basic punctuation
                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '\'' || c == '-')
                {
                    sanitized.Append(c);
                }
                else
                {
                    // Replace other special characters with space
                    sanitized.Append(' ');
                }
            }

            // Clean up extra spaces and trim
            string result = sanitized.ToString();
            result = System.Text.RegularExpressions.Regex.Replace(result, @"\s+", " ").Trim();

            return result;
        }//end of method to remove special characters

        //method to auto store
        private void  auto_store()
        {//start of auto_store method

            //store answers before user interaction
            ai_answers = answers.return_answers();
            //store questions before user interaction
            ai_questions = answers.user_questions();
            //store the ignore
            ignore = answers.ignores();
            //then assign to true
            stored = true;

        }//end of auto_store method

        //check questions
        public Boolean question_check(string question, ListView chats_view)
        {//start of questions
            //using if statement to check username
            if (question != "")
            {//start of if 
                return true;
            }
            else
            {//start of else
                get_chats_color.ai_error("CyberBot", chats_view, "Please enter a question related to cybersecurity, to get a response.....");
            }//end of else

            return false;

        }//end of questions
    }//end of class
}//end of namespace