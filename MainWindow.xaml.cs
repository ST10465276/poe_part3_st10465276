using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace part2
{//start of namespace
    public partial class MainWindow : Window
    {//start of class

        //creating an instance for a class user_check
        //object name username_check
        private user_check username_check = new user_check();

        //creating an instance for a class chats_colours
        //object name chats_color
        private chats_colours chats_color = new chats_colours();

        //creating an instance for check questions class
        //with an object name question_asked
        check_questions question_asked = new check_questions();

        //database class tasks
        add_tasks adding_tasks = new add_tasks();

        //declaring a global variable
        int user_respond_count = 0;
        string last_question = string.Empty;

        private int currentQ = 0;
        private int quizScore = 0;
        private string currentCorrectAnswer = "";
        private List<string[]> questions = new List<string[]>();


        //creating an instance for the class activity_log with an object name logs
        activity_log logs = new activity_log();

        public MainWindow()
        {//start of constructor

            InitializeComponent();

            //auto greet the user with voice 
            Greet();


        }//end of constructor

        //part 3 POE
        //all below code it is of Part 3 POE


        private void load_quiz()
        {//start of load_quiz method

            // Initialize all questions [Question, CorrectAnswer, Wrong1, Wrong2, Wrong3]
            questions.Add(new string[] { "What is Phishing?", "Attackers pose as trusted sources to steal information","A software that protects against viruses", "A type of firewall security", "An encryption method for passwords" });
            questions.Add(new string[] { "What does a Firewall do?", "Controls network traffic based on security rules", "Scans for viruses on your computer", "Creates strong passwords automatically", "Backs up your personal files" });
            questions.Add(new string[] { "What makes a strong password?", "Long, complex, and not easy to guess", "Your birthday and name", "The word 'password123'", "Using the same password for all accounts" });
            questions.Add(new string[] { "What should you do if your account gets hacked?", "Urgently secure your account and log out of all devices", "Ignore it and create a new account", "Share your password with friends for help", "Wait for the hacker to fix it" });
            questions.Add(new string[] { "What is a VPN used for?", "Protects your private details on public Wi-Fi", "Makes your computer run faster", "Blocks all advertisements online", "Automatically creates backups" });
            questions.Add(new string[] { "How can you protect your privacy online?", "Check privacy settings and don't click suspicious links", "Share all your information on social media", "Use the same password everywhere", "Never update your apps" });
            questions.Add(new string[] { "What is cybersecurity?", "Protecting systems and networks from digital threats", "Creating video games", "Building computer hardware", "Designing websites" });
            questions.Add(new string[] { "What should you do if you detect fraud on your bank account?", "Contact your bank immediately", "Wait to see if it happens again", "Send money to verify your account", "Ignore the charges" });
            questions.Add(new string[] { "How can you identify a malicious chatbot?", "It creates urgency and asks for sensitive information", "It responds very quickly", "It uses formal language", "It asks simple questions" });
            questions.Add(new string[] { "What is the purpose of cybersecurity awareness?", "To educate users on staying safe online", "To sell antivirus software", "To track user activity", "To slow down internet speed" });
            questions.Add(new string[] { "What to do when receiving an unknown email asking for your personal details?", "Ignore or report the email", "Send your details immediately", "Forward the email to your family", "Click the link to verify" });
            questions.Add(new string[] { "Why is it important to keep software updated?", "To improve security", "To lower storage space", "To increase internet costs", "To cause vulnerabilities" });
            questions.Add(new string[] { "Which vital information should never be shared online?", "Your passwords and personal details", "Your favorite colour", "Your favourite movie", "Your hobbies"});
            questions.Add(new string[] { "What is the safest way to access a website?", "Typing the website address directly into the browswer", "Using links from people", "Clicking random links", "Following pop-up ads"});
            questions.Add(new string[] { "What is social engineering in cybersecurity?", "Manipulating people into revealing their private details", "Creating social media accounts", "Building websites", "Designing computer networks"});

            currentQ = 0;
            quizScore = 0;
            score_text.Text = "Score: 0";
            question_number.Text = "Question 1/15";
            display_question();
            logs.store_log("you are starting the quiz to play");
        }


        //method to display the questions to the user
        private void display_question()
        {//start of display_question method 
            if (currentQ < questions.Count)
            {
                string[] q = questions[currentQ];

                //set question
                question_text.Text = q[0];
                currentCorrectAnswer = q[1];

                //create list of all answers (1 correct + 3 wrong)
                List<string> answers = new List<string>();
                answers.Add(q[1]); //
                answers.Add(q[2]); // Wrong answer 1
                answers.Add(q[3]); // Wrong answer 2
                answers.Add(q[4]); // Wrong answer 3

                //shuffle answers
                Random rnd = new Random();
                for (int i = 0; i < answers.Count; i++)
                {
                    int randomIndex = rnd.Next(answers.Count);
                    string temp = answers[i];
                    answers[i] = answers[randomIndex];
                    answers[randomIndex] = temp;
                }

                //display shuffled answers
                answer1.Content = answers[0];
                answer2.Content = answers[1];
                answer3.Content = answers[2];
                answer4.Content = answers[3];

                //reset buttons
                answer1.Background = Brushes.White;
                answer2.Background = Brushes.White;
                answer3.Background = Brushes.White;
                answer4.Background = Brushes.White;
                answer1.IsEnabled = true;
                answer2.IsEnabled = true;
                answer3.IsEnabled = true;
                answer4.IsEnabled = true;
                result_text.Text = "";
                next_btn.Visibility = Visibility.Hidden;
            }
            else
            {
                //quiz finished
                check_lastScore();
                question_text.Text = "QUIZ COMPLETE!";
                question_number.Text = "Final Score: " + quizScore + "/15";

                string message = quizScore == 15 ?
                    "Perfect! You're a cybersecurity expert! " :
                    "Good try! Keep learning to stay safe online.\n\nYour Score: " + quizScore + "/15";

                //show popup asking if they want to restart quiz
                MessageBoxResult result = MessageBox.Show(
                    message + "\n\nDo you want to restart the quiz?",
                    "Quiz Complete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    //reset and restart quiz
                    quizScore = 0;
                    currentQ = 0;
                    quizScore = 0;
                    score_text.Text = "Score: 0";
                    question_number.Text = "Question 1/15";
                    score_text.Text = "Score: 0";
                    result_text.Text = "";
                   
                    next_btn.Visibility = Visibility.Hidden;

                    answer1.Visibility = Visibility.Visible;
                    answer2.Visibility = Visibility.Visible;
                    answer3.Visibility = Visibility.Visible;
                    answer4.Visibility = Visibility.Visible;
                    answer1.IsEnabled = true;
                    answer2.IsEnabled = true;
                    answer3.IsEnabled = true;
                    answer4.IsEnabled = true;
                    answer1.Background = System.Windows.Media.Brushes.White;
                    answer2.Background = System.Windows.Media.Brushes.White;
                    answer3.Background = System.Windows.Media.Brushes.White;
                    answer4.Background = System.Windows.Media.Brushes.White;

                    display_question();
                    logs.store_log("you  completed the quiz with a score of "+quizScore);
                    logs.store_log("you restarted the quiz game after completing with a score of "+quizScore);
                }
                else
                {



                    //if click on no reset 
                   // check_lastScore();
                    question_text.Text = "QUIZ COMPLETE!";
                    question_number.Text = "Final Score: " + quizScore + "/15";
                    result_text.Text = quizScore == 15 ? "Perfect! You're a cybersecurity expert!" : "Good try! Keep learning to stay safe online, Score: " + quizScore + "/15";
                    result_text.Foreground = Brushes.Blue;
                    result_text.Visibility = Visibility.Visible;
                    next_btn.Visibility = Visibility.Collapsed;
                    answer1.Visibility = Visibility.Collapsed;
                    answer2.Visibility = Visibility.Collapsed;
                    answer3.Visibility = Visibility.Collapsed;
                    answer4.Visibility = Visibility.Collapsed;


                    logs.store_log("you  completed the quiz with a score of " +quizScore);
                }
            }
        }//end of display_question method



        //method to check last score with the current one
        private void check_lastScore()
        {//start of check last score method

            //get the last score value
            string last_score = this.score_last.Text;

            //then check if the last score is empty or not
            //to avoid casting it to int empty
            if (string.IsNullOrEmpty(last_score)  )
            {
                //then it must be the first time for the user to have the last score based on their 1st time playing , just assign
                this.score_last.Text =" "+ quizScore;
                this.scored.Text = "Last Score: ";
                MessageBox.Show("New best record is stored , which is  " + quizScore );
                logs.store_log("You have a new record stored which is  "+quizScore+" from the quiz game you played");
                //then return
                return;
            }
            else
            {
                //here compare and show that they have a new best record or they still have to beat the record
                //last_score must be an int datatype now
                int last_user_score = int.Parse(last_score);

                //then check
                if (quizScore > last_user_score)
                {
                    //and assign 
                    this.score_last.Text = " " + quizScore;
                    this.scored.Text = "Last Score: ";

                    //then show message about the new record   
                    logs.store_log("New best record is stored , which is  " + quizScore + ",from the last one which was " + last_score);
                    MessageBox.Show("New best record is stored , which is  " + quizScore + ",from the last one which was " + last_score);
                }

                else if (quizScore == last_user_score)
                {

                    //then show message that they have to do better
                    MessageBox.Show("Your best score is the same as last time, please improve your cybersecurity knowledge "+ last_user_score);
                
                }
                else
                {

                    //just show the message to the user
                    MessageBox.Show("Your best score record did not change, the best score is still " + last_score);
                }



            }
        }//end of check last score method


        //method to check if correct or wrong answer from the quiz
        private void check_answer(object sender, RoutedEventArgs e)
        {//start of check_answer method

            Button clicked = sender as Button;
            string selected = clicked.Content.ToString();

            if (selected == currentCorrectAnswer)
            {
                quizScore++;
                score_text.Text = "Score: " + quizScore;
                result_text.Text = "CORRECT!";
                result_text.Foreground = Brushes.Green;
                clicked.Background = Brushes.LightGreen;
            }
            else
            {
                result_text.Text = "WRONG!";
                result_text.Foreground = Brushes.Red;
                clicked.Background = Brushes.LightPink;

                //Highlight the correct answer
                if (answer1.Content.ToString() == currentCorrectAnswer) answer1.Background = Brushes.LightGreen;
                else if (answer2.Content.ToString() == currentCorrectAnswer) answer2.Background = Brushes.LightGreen;
                else if (answer3.Content.ToString() == currentCorrectAnswer) answer3.Background = Brushes.LightGreen;
                else if (answer4.Content.ToString() == currentCorrectAnswer) answer4.Background = Brushes.LightGreen;
            }

            //disable all answer buttons
            answer1.IsEnabled = false;
            answer2.IsEnabled = false;
            answer3.IsEnabled = false;
            answer4.IsEnabled = false;

            next_btn.Visibility = Visibility.Visible;
        }//end of check_answer method


        private void next_question(object sender, RoutedEventArgs e)
        {//start of next_question method

            currentQ++;
            question_number.Text = "Question " + (currentQ + 1) + "/15";
            display_question();

        }//end of next_question method




        private void answer_clicked(object sender, RoutedEventArgs e)
        {//start of answer_clicked method

            Button clicked = sender as Button;
            string selected = clicked.Content.ToString();

            if (selected == currentCorrectAnswer)
            {//start of if
                quizScore++;
                score_text.Text = "Score: " + quizScore;
                result_text.Text = "CORRECT!";
                result_text.Foreground = Brushes.Green;
                clicked.Background = Brushes.LightGreen;
            }
            else
            {
                result_text.Text = "WRONG!";
                result_text.Foreground = Brushes.Red;
                clicked.Background = Brushes.LightPink;

                //Highlight the correct answer
                if (answer1.Content.ToString() == currentCorrectAnswer)
                    answer1.Background = Brushes.LightGreen;
                else if (answer2.Content.ToString() == currentCorrectAnswer)
                    answer2.Background = Brushes.LightGreen;
                else if (answer3.Content.ToString() == currentCorrectAnswer)
                    answer3.Background = Brushes.LightGreen;
                else if (answer4.Content.ToString() == currentCorrectAnswer)
                    answer4.Background = Brushes.LightGreen;

            }//end of else

            //disable all answer buttons
            answer1.IsEnabled = false;
            answer2.IsEnabled = false;
            answer3.IsEnabled = false;
            answer4.IsEnabled = false;

            next_btn.Visibility = Visibility.Visible;

        }//end of answer_clicked method


        //checking tasks
        private void check_taks(string usernames, string interests, string ai_answer, bool founds)
        {//start of check_taks

            //check the tasks from the db of the user
            string appendTask = "                     REMINDER LIST OF THE TASKS NOT DONE.                     \n\n";
            bool due_today = false;
            bool found_tasks = false;
            int tasks_count = 0;

            DataTable userTasks = adding_tasks.pendingTasks(username_check.return_username());

            foreach (DataRow row in userTasks.Rows)
            {//start of foreach

                string taskName = row["task_name"].ToString();
                string status = row["task_status"].ToString();
                string taskDueDate = row["task_reminder"].ToString().Replace("12:00AM", "").Trim();
                DateTime today = DateTime.Now.Date;
                string compare_date = today.ToString("MMM dd, yyyy").Replace("12:00AM", "").Trim();
                tasks_count++;

                //MessageBox.Show(taskDueDate + " and " + compare_date.Replace(",", "").Trim());

                if (taskDueDate == compare_date.Replace(",", "").Trim())
                {
                    appendTask += tasks_count + ": " + taskName + " , which is due \" Today \" on " + taskDueDate + ". \n";
                    due_today = true;
                    found_tasks = true;
                }
                else
                {
                    appendTask += tasks_count + ": " + taskName + " , which is due on \" " + taskDueDate + "\" . \n";
                    found_tasks = true;
                }


            }//end of foreach


            //notify the user about tasks due today
            if (due_today)
            {
                MessageBox.Show("Note: There are some tasks that are due today !!");
            }


            //check if tasks are found and then bind or append them

            if (founds)
            {//start of if

                if (found_tasks)
                {
                    chats_color.remind_user(" Just a reminder as someone who is interested in " + interests + "\n\nHere's what you can know to stay safe \n\n" + ai_answer + "\n\n" + appendTask + "\n\n To mark complete or delete go to View Tasks from Main Menu.", chats_view);
                    logs.store_log("CyberBot: Just a reminder as someone who is interested in " + interests + "\n\nHere's what you can know to stay safe \n\n" + ai_answer + "\n\n" + appendTask + "\n\n To mark complete or delete go to View Tasks from Main Menu.");
                }
                else
                {
                    chats_color.remind_user(" Just a reminder as someone who is interested in " + interests + "\n\nHere's what you can know to stay safe \n\n" + ai_answer, chats_view);
                    logs.store_log("CyberBot: Just a reminder as someone who is interested in " + interests + "\n\nHere's what you can know to stay safe \n\n" + ai_answer);
                }

            }//end of if
            else if (!founds)
            {//start of else if



                if (found_tasks)
                {
                    chats_color.remind_user(appendTask + "\n\n To mark complete or delete go to View Tasks from Main Menu.", chats_view);
                    logs.store_log("CyberBot: " + appendTask + "\n\n To mark complete or delete go to View Tasks from Main Menu.");
                    
                }

            }//end of else if 
        }//end of check_tasks

        //All Task DataBase

        //show the user's completed tasks
        private void show_completed_tasks(object sender, RoutedEventArgs e)
        {//start of show_completed_tasks handler

            DataTable completed = adding_tasks.completedTasks(username_check.return_username());
            if (completed != null && completed.Rows.Count > 0)
            {
                tasks.ItemsSource = completed.DefaultView;
                // Disable the entire ACTION column
                (tasks.View as GridView).Columns[4].Width = 0;
            }
            else
            {
                //Nothing found"
                DataTable emptyTable = new DataTable();
                emptyTable.Columns.Add("task_name", typeof(string));
                emptyTable.Columns.Add("task_description", typeof(string));
                emptyTable.Columns.Add("task_reminder", typeof(string));
                emptyTable.Columns.Add("task_status", typeof(string));

                DataRow row = emptyTable.NewRow();
                row["task_name"] = "Nothing found based on the tasks, add tasks from chats to manage here";
                row["task_description"] = "";
                row["task_reminder"] = "";
                row["task_status"] = "";
                emptyTable.Rows.Add(row);
                // Disable the entire ACTION column
                (tasks.View as GridView).Columns[0].Width = 400;

                (tasks.View as GridView).Columns[4].Width = 0;
                tasks.ItemsSource = emptyTable.DefaultView;
                no_task();

            }

        }//end of show_completed_tasks handler


        //show the user's pending not done tasks
        private void show_pending_tasks(object sender, RoutedEventArgs e)
        {//start of show_pending_tasks handler

            DataTable pending = adding_tasks.pendingTasks(username_check.return_username());
            if (pending != null && pending.Rows.Count > 0)
            {
                tasks.ItemsSource = pending.DefaultView;
                // Show the ACTION column again
                (tasks.View as GridView).Columns[4].Width = 240;
            }
            else
            {
                //Nothing found"
                DataTable emptyTable = new DataTable();
                emptyTable.Columns.Add("task_name", typeof(string));
                emptyTable.Columns.Add("task_description", typeof(string));
                emptyTable.Columns.Add("task_reminder", typeof(string));
                emptyTable.Columns.Add("task_status", typeof(string));

                DataRow row = emptyTable.NewRow();
                row["task_name"] = "Nothing found based on the tasks, add tasks from chats to manage here";
                row["task_description"] = "";
                row["task_reminder"] = "";
                row["task_status"] = "";
                emptyTable.Rows.Add(row);
                // Disable the entire ACTION column
                (tasks.View as GridView).Columns[4].Width = 0;
                no_task();

                tasks.ItemsSource = emptyTable.DefaultView;
            }


        }//end of show_pending_tasks handler


        //mark tasks complete
        private void mark_task_complete(object sender, RoutedEventArgs e)
        {//start of mark_task_complete handler

            Button btn = sender as Button;
            DataRowView rowView = btn.DataContext as DataRowView;
            int taskId = Convert.ToInt32(rowView.Row["id"]);

            adding_tasks.markTaskAsCompleted(taskId);
            logs.store_log("CyberBot: You marked a task done..");

            //refresh current view
            if (pending_btn.Background == Brushes.Orange)
                show_pending_tasks(sender, e);
            else
                show_completed_tasks(sender, e);

        }//end of mark_task_complete handler

        private void delete_task(object sender, RoutedEventArgs e)
        {//start of delete_task handler

            Button btn = sender as Button;
            DataRowView rowView = btn.DataContext as DataRowView;
            int taskId = Convert.ToInt32(rowView.Row["id"]);

            //confirm deletion of task
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this task?", "Confirm Delete", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {//start of if
                adding_tasks.deleteTask(taskId);

                //refresh current view
                logs.store_log("CycberBot: you confirmed to delete a task..");
                show_pending_tasks(sender, e);


            }//end of if
        }//end of delete_task handler


        //open the chats with ai page
        private void chat_card_click(object sender, MouseButtonEventArgs e)
        {//start of chat_card_click handler

            options_grid.Visibility = Visibility.Collapsed;
            chat_grid.Visibility = Visibility.Visible;

        }//end of chat_card_click handler


        //open the tasks page
        private void view_tasks_card_click(object sender, MouseButtonEventArgs e)
        {
            options_grid.Visibility = Visibility.Hidden;
            tasks_grid.Visibility = Visibility.Visible;


            DataTable pending = adding_tasks.pendingTasks(username_check.return_username());
            if (pending != null && pending.Rows.Count > 0)
            {
                tasks.ItemsSource = pending.DefaultView;
            }
            else
            {
                //Nothing found"
                DataTable emptyTable = new DataTable();
                emptyTable.Columns.Add("task_name", typeof(string));
                emptyTable.Columns.Add("task_description", typeof(string));
                emptyTable.Columns.Add("task_reminder", typeof(string));
                emptyTable.Columns.Add("task_status", typeof(string));

                DataRow row = emptyTable.NewRow();
                row["task_name"] = "Nothing found based on the tasks, add tasks from chats to manage here";
                row["task_description"] = "";
                row["task_reminder"] = "";
                row["task_status"] = "";
                emptyTable.Rows.Add(row);
                  // Disable the entire ACTION column
            (tasks.View as GridView).Columns[4].Width = 0;
                (tasks.View as GridView).Columns[0].Width = 400;
                no_task();
                tasks.ItemsSource = emptyTable.DefaultView;
            }
        }
        //no task method 
        private void no_task()
        {//start of no task method

            //message to show the user
            MessageBox.Show("Hey "+ username_check.return_username() + " , you have no task added, to add the task please add it from the chats");

        }//end no task method


        //open the log page
        private void view_logs_card_click(object sender, MouseButtonEventArgs e)
        {//start of view_logs_card_click handler

            options_grid.Visibility = Visibility.Hidden;
             activities_grid.Visibility = Visibility.Visible;
            logs_view.Items.Clear();
            logs.read_log(logs_view);

        }//end of view_logs_card_click handler


        //open the quiz page
        private void quiz_card_click(object sender, MouseButtonEventArgs e)
        {//start of quiz_card_click handler

            options_grid.Visibility = Visibility.Hidden;
            quiz_grid.Visibility = Visibility.Visible;
            load_quiz();

            //reset and restart quiz
            quizScore = 0;
            currentQ = 0;
            quizScore = 0;
            score_text.Text = "Score: 0";
            question_number.Text = "Question 1/15";
            score_text.Text = "Score: 0";
            result_text.Text = "";

            next_btn.Visibility = Visibility.Hidden;

            answer1.Visibility = Visibility.Visible;
            answer2.Visibility = Visibility.Visible;
            answer3.Visibility = Visibility.Visible;
            answer4.Visibility = Visibility.Visible;
            answer1.IsEnabled = true;
            answer2.IsEnabled = true;
            answer3.IsEnabled = true;
            answer4.IsEnabled = true;
            answer1.Background = System.Windows.Media.Brushes.White;
            answer2.Background = System.Windows.Media.Brushes.White;
            answer3.Background = System.Windows.Media.Brushes.White;
            answer4.Background = System.Windows.Media.Brushes.White;

            display_question();

        }//end of quiz_card_click handler



        //back to main menu
        private void back_main(object sender, RoutedEventArgs e)
        {//start of back_main method

            options_grid.Visibility = Visibility.Visible;

            chat_grid.Visibility = Visibility.Hidden;
            tasks_grid.Visibility = Visibility.Hidden;
            quiz_grid.Visibility= Visibility.Hidden;
            activities_grid.Visibility= Visibility.Hidden;

        }//end of back_main method


        //end of poe part 3 



        //Part two code 

        private void start_click(object sender, RoutedEventArgs e)
        {//start of start handler

            //setting the logo grid to hidden
            //setting the username grid to visible
            logo_grid.Visibility = Visibility.Hidden;
            username_grid.Visibility = Visibility.Visible;

        }//end of start handler

        private void submit_username(object sender, RoutedEventArgs e)
        {//start of submit_username handler

            //collecting the username
            string name = user_input.Text.ToString();

            //calling the username_checking 
           bool found = username_check.username_checking(name,error_username,username_grid,options_grid);

            if (found) 
            {
                logs.store_log("CyberBot: welcoming you, to cyberbot after capturing your name");
                //calling the welcome method
                username_check.welcome_user(welcoming_users,  chats_view);
            }



        }//end of submit_username handler

        private void send_question(object sender, RoutedEventArgs e)
        {//start of send_question handler

            //collecting username, ai name and user_questions
            string collect_username = username_check.return_username();
            string ai_name = "CyberBot";
            string  user_questions = enter_question.Text.ToString();

            //clear question 
            enter_question.Clear();
         
            //check if the user entered something
            if (question_asked.question_check(user_questions, chats_view))
            {//start of if

                    if (last_question!=""&&user_questions.ToLower().Equals("tell me more") || user_questions.ToLower().Equals("give me another tip") || user_questions.ToLower().Equals("explain more"))
            {
                    logs.store_log("CyberBot: you asked for more about " + last_question );
                    //calling the username
                    chats_color.user_chats(collect_username, chats_view, user_questions);
                    user_questions = last_question;
                }
                else
                {

                    //calling the username
                    chats_color.user_chats(collect_username, chats_view, user_questions);
                }

               last_question = user_questions;



                //calling the username
               // chats_color.user_chats(collect_username, chats_view, user_questions);
                
                //searching for the answers
                string ai_answer = question_asked.searching_response(user_questions,collect_username);

                if (ai_answer == "please enter questions related to cybersecurity." || ai_answer == "i didn't quite understand that. could you please rephrase your question?" || ai_answer == "please enter interests related to cybersecurity")
                {
                    logs.store_log("CycberBot: you asked for unrelated cyber security question");
                    logs.store_log("CyberBot: replied with " + ai_answer);
                    chats_color.ai_error(ai_name,chats_view,ai_answer);
                }
                else
                {

                    //show the response to the user
                    logs.store_log("CycberBot: you asked for "+user_questions);
                    logs.store_log("CyberBot: replied with "+ai_answer);
                    chats_color.ai_chats(ai_name, chats_view, ai_answer);

                }
            }//end of if

            //call the method to auto show the interested topics
            interested_topics(ai_name, collect_username.ToLower());

             //calling the user_respond_count variable
             user_respond_count++;

        }//end of send_question handler

        //method to auto show the users interests
        private void interested_topics(string ai_name , string usernames)
        {//start of interested_topics method

            //check if user_repond_count is equals to 3 
            if (user_respond_count == 3)
            {//start of if
                //read the user's interests from file
                string filename = "interests.txt";
                string interest_hold = string.Empty;
                string ai_hold = string.Empty;
                bool found_all = false;

                if (File.Exists(filename))
                {
                    string[] lines = File.ReadAllLines(filename);

                    //find the user's line
                    foreach (string line in lines)
                    {//start of foreach
                        if (line.ToLower().StartsWith(usernames))
                        {  

                            //get the interests part
                            int colonIndex = line.IndexOf("interested in:");
                            if (colonIndex >= 0)
                            {

                                string interests = line.Substring(colonIndex + 14).Trim();
                                //show reminder of interests
                                 string ai_answer = question_asked.searching_response(interests,usernames);

                                // Remove extra spaces (multiple spaces become single space)
                                ai_answer = System.Text.RegularExpressions.Regex.Replace(ai_answer, @"\s+", " ");

                                // Format as sentence
                                if (!string.IsNullOrWhiteSpace(ai_answer))
                                {
                                    ai_answer = char.ToUpper(ai_answer[0]) + ai_answer.Substring(1) +
                                               (ai_answer.EndsWith(".") || ai_answer.EndsWith("!") || ai_answer.EndsWith("?") ? "" : ".");
                                }

                                interest_hold = interests;
                                ai_hold = ai_answer;
                                found_all = true;


                                   //chats_color.remind_user(" Just a reminder as someone who is interested in " + interests + "\n\nHere's what you must know to stay safe \n\n" + ai_answer, chats_view);

                                //break the loop
                                break;
                            }
                        }
                    }//end of foreach
                }

                //check the tasks
                check_taks(usernames, interest_hold, ai_hold , found_all);
                //reset counting
                user_respond_count = 0;

            }//end of if

        }//end of interested_topic method


        //sound method
        private void Greet()
        {//start of sound method

            //creating an instance for the sound greeting class with a constructor
            new Greet_users() { };
            logs.store_log("CycberBot: greeting you with voice ...");

        }//end of sound method

    }//end of class
}//end of namespace
