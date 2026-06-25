using System;
using System.IO;
using System.Media;

namespace part2
{//start of namespace
    public class Greet_users
    {//start of Greet_users class
        public Greet_users()
        {//start of constructor

            //now get the loctaion of the project
            string project_location = AppDomain.CurrentDomain.BaseDirectory;

            //check if it is getting the Directory
            //Console.WriteLine(project_location);

            //replacing the bin\\Debug\\ so it can get the sound
            string updated_path = project_location.Replace("bin\\Debug\\", "");

            //combining the wav name as greeting.wav with the updated path
            string full_path = Path.Combine(updated_path, "voice_greeting.wav");

            //pass it to the method Playing_sound
            Play_sound(full_path);

        }//end of constructor

        //creating a method to play the sound
        private void Play_sound(string full_path)
        {//start of play_sound method

            //start of try and catch
            try
            {
                //play the sound
                using (SoundPlayer player = new SoundPlayer(full_path))
                {//start of using

                    //playing the sound till the end
                    player.Play();

                }//end of using

            }
            catch (Exception error)
            {
                //display error message
                Console.WriteLine(error.Message);

            }//end of try and catch 

        }//end of play_sound method

    }//end of class
}//end of namespace 