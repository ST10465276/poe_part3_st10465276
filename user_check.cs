using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace part2
{//start of namespace
    public class user_check
    {//start of user_check class

        //creating a global variable for the username
        private string username = string.Empty;

        //creating void method for username checking
        public Boolean username_checking(string name, TextBlock error_username, Grid username_grid, Grid chats_grid) 
        {//start of method

            //using if statement to check username
            if (name != "")
            {//start of if 

                //if statement to check if username is valid
                if (Regex.IsMatch(name, @"^[A-Za-z]{3,}$"))
                {//start of nested if

                    //username grid is set to hidden
                    username_grid.Visibility = Visibility.Hidden;

                    //chats grid is set to visible
                    chats_grid.Visibility = Visibility.Visible;

                    //assign the username with name
                    username = name;

                    //covert username to first letter uppercase and rest of the name lowercase 
                    username = char.ToUpper(name[0]) + name.Substring(1).ToLower();

                    //username success message is displayed
                    MessageBox.Show("Username is successfully entered");

                    return true;

                }//end of nested if
                else
                {//start of nested else

                    //displaying an error message for invalid username
                    error_username.Text = "Please enter valid username";

                    //displaying a pop-up error message
                    Console.Beep(1000, 1500);
                    MessageBox.Show("Please re-enter username. It must start with three or more letters and it must not contain any numbers or special characters");
                }//end of nested else

            }//end of if
            else
            {
                //displaying a pop-up error message
                Console.Beep(1000, 1500);
                MessageBox.Show("Please enter username. It must start with three or more letters and it must not contain any numbers or special characters");
                
                //displaying error message for username
                error_username.Text = "Please, username is required!";

            }//end of else

            return false;

        }//end of method

        //creating method to welcome the user
        public void welcome_user(TextBlock welcome_users,  ListView chats_view)
        {//start of welcome_user method

            //check if user was using the Cyberbot before
            string message = string.Empty;
            if (checking())
            {
               //display for a returning user

                message = "Hello " + username + ", welcome back how may I help you today?" +
                               "\n                 Please enter questions related to cybersecurity, or add tasks related to cybersecurity.";

                //add to the display too
                welcome_users.Text = "Hello " + username + ", welcome back, how may I help you today from the 4 listed options?";

            }
            else
            {
                //display for a first time user

                message = "Hello " + username + ", welcome how may I help you today?" +
                               "\n                 Please enter questions related to cybersecurity, or add tasks related to cybersecurity.";
                //add to the display too
                welcome_users.Text = "Hello " + username + ", welcome, how may I help you today,  from the 4 listed options?";
            }

            //welcoming the user using a personalised message with styled border
            Border messageBorder = new Border
            {
                Margin = new Thickness(0, 3, 0, 3),
                Padding = new Thickness(8, 5, 8, 5),
                CornerRadius = new CornerRadius(8),
                // Light blue background
                Background = new SolidColorBrush(Color.FromRgb(240, 248, 255)),
                // Light blue border
                BorderBrush = new SolidColorBrush(Color.FromRgb(173, 216, 230)),
                BorderThickness = new Thickness(1),
                HorizontalAlignment = HorizontalAlignment.Left,
                MaxWidth = 550
            };

            TextBlock messageText = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                FontSize = 13,
                Margin = new Thickness(2)
            };

            messageText.Inlines.Add(new Run
            {
                Text = "CyberBot : ",
                Foreground = Brushes.DarkCyan,
                FontWeight = FontWeights.Bold
            });

            messageText.Inlines.Add(new Run
            {
                Text = message,
                Foreground = Brushes.DarkOrange
            });

            messageBorder.Child = messageText;
            chats_view.Items.Add(messageBorder);


            
        }//end of welcome_user method


        //check the user in file
       private Boolean checking()
        {//start of checking method

            //Read into the textFIle
            string path = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\", @"\names.txt");

            if (!File.Exists(path))
            {//start of if

                File.AppendAllText(path,"user\n");

            }//end of if

            string[] names = File.ReadAllLines(path);

            //foreach
            foreach (string name in names)
            {

                if (name.ToLower().Contains(  username.ToLower() ) ) {

                    return true;

                }
            }

            //write into the textFIle
            File.AppendAllText(path, username + "\n");

            return false;

        }//end of checking method

        //creating a method to return the username
        public string return_username()
        {//start of return_username method
         
            return username;

        }//end of return_username method

    }//end of class
}//end of class