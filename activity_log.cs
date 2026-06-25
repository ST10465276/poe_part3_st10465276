using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace part2
{//start of namespace
    public class activity_log
    {//start of activity_log class

        //ArrayList to store all the logs during the run time
        private ArrayList logs = new ArrayList();

        //then create a method to store the logs
        public void store_log(string user_activity)
        {//start of the method store_log

            //storing to the list
            logs.Add(user_activity);

        }//end of the method store_log

        //method to read the users acitivity log
        public void read_log(ListView log_view)
        {//start of method to read all the logs

            //temp boolean variable to check if something was found or not
            bool found = false;

            //use foreach to get all of them 
            foreach(string log in logs)
            {//start of foreach  

                //then add it to the view
                log_view.Items.Add(log);
                found = true;

            }//end of foreach 


            //if nothing is found
            if (!found)
            {//start of if

                //message
                MessageBox.Show("You have no activities in the CyberBot app....");
                log_view.Items.Add("You have no activities in the CyberBot app....");

            }//end of if

        }//end of method to read all the logs


    }//end of activity_log class
}//end of namespace