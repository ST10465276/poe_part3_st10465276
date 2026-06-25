using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace part2
{//start of namespace
    public class chats_colours
    {//start of class

        //creating ai_chats method
        public void ai_chats(string ai_name, ListView chats_view, string ai_answer)
        {//start of ai_chats method

            //creating a styled border
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
                MaxWidth = 600
            };

            TextBlock messageText = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                FontSize = 13,
                Margin = new Thickness(2)
            };

            //setting the colors with your original colors
            messageText.Inlines.Add(new Run
            {
                Text = ai_name + " : ",
                Foreground = Brushes.DarkCyan,
                FontWeight = FontWeights.Bold
            });

            messageText.Inlines.Add(new Run
            {
                Text = ai_answer,
                Foreground = Brushes.DarkGreen
            });

            messageBorder.Child = messageText;
            chats_view.Items.Add(messageBorder);

            //auto scroll if reach the end of the chat
            chats_view.ScrollIntoView(chats_view.Items[chats_view.Items.Count - 1]);

        }//end of ai_chats method

        //ceating user_chats method
        public void user_chats(string username, ListView chats_view, string question)
        {//start of method

            //container
            Grid container = new Grid();
            container.HorizontalAlignment = HorizontalAlignment.Stretch;

            //creating a styled border to match the user's messages
            Border messageBorder = new Border
            {
                Margin = new Thickness(0, 3, 0, 3),
                Padding = new Thickness(8, 5, 8, 5),
                CornerRadius = new CornerRadius(8),
                Background = new SolidColorBrush(Color.FromRgb(240, 248, 255)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(173, 216, 230)),
                BorderThickness = new Thickness(1),
                HorizontalAlignment = HorizontalAlignment.Right,
                MaxWidth = 550
            };

            TextBlock messageText = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                FontSize = 13,
                Margin = new Thickness(2)
            };

            //setting username color with original colors
          

            messageText.Inlines.Add(new Run
            {
                Text = username + " : ",
                Foreground = Brushes.Magenta,
                FontWeight = FontWeights.Bold
                
            });
            messageText.Inlines.Add(new Run
            {
                Text = question,
                Foreground = Brushes.DarkGoldenrod
            });
            messageBorder.Child = messageText;

            //add border to container
            container.Children.Add(messageBorder);

            //add container to listview
            chats_view.Items.Add(container);

            //auto scroll if reach the end of the chat
            chats_view.ScrollIntoView(chats_view.Items[chats_view.Items.Count - 1]);

        }//end of method

        //creating ai_chats method
        public void ai_error(string ai_name, ListView chats_view, string message)
        {//start of ai_chats method

            //creating a styled border for error messages
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

            //setting the colors - same color for AI name
            messageText.Inlines.Add(new Run
            {
                Text = ai_name + " : ",
                Foreground = Brushes.DarkCyan,
                FontWeight = FontWeights.Bold
            });

            messageText.Inlines.Add(new Run
            {
                Text = message,
                Foreground = Brushes.Red
            });

            messageBorder.Child = messageText;
            chats_view.Items.Add(messageBorder);

            //auto scroll if reach the end of the chats
            chats_view.ScrollIntoView(chats_view.Items[chats_view.Items.Count - 1]);

        }//end of ai_chats method

        //creating method to welcome the user
        public void remind_user(string message,ListView chats_view)
        {//start of method

            //reminder of  the user using a personalised message with styled border
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
            //auto scroll if reach the end of the chats
            chats_view.ScrollIntoView(chats_view.Items[chats_view.Items.Count - 1]);
        }//end of method

    }//end of class
}//end of namespace