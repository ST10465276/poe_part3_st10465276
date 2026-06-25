using System.Collections;

namespace part2
{//start of namespace
    public class searching_answers
    {//start of searching_answers class


        //creating a method called return_answers for answers
        public ArrayList return_answers()
        {//start of method

            ArrayList add_answers = new ArrayList();

            add_answers.Add("greeting i'm doing well, thanks for asking! how are you doing ?");
            add_answers.Add("greeting i'm great, so how can i help you ?");
            add_answers.Add("greeting i'm doing good! hope you are also well ?");
            add_answers.Add("greeting i'm wonderful, how may i provide assistance ?");
           
            add_answers.Add("purpose my purpose is to educate you on ways to stay safe online and guide you on your questions.");
            add_answers.Add("purpose is helping users get a better understanding on online safety and digital protection.");
            add_answers.Add("purpose is to assist with cybersecurity awareness and safety guidance.");
            add_answers.Add("purpose i will guide you on ways to protect your personal information on the internet.");
           
            add_answers.Add("cybersecurity it is about protecting systems and networks from digital threats.");
            add_answers.Add("cybersecurity involves safeguarding devices and digital accounts from attacks.");
            add_answers.Add("cybersecurity focuses on securing digital information and systems.");
            add_answers.Add("cybersecurity reduces security risks and protects users from cybercrimes.");

            add_answers.Add("phishing phishing is a scam where attackers pretend to be trusted sources to steal information.");
            add_answers.Add("phishing be cautious of emails asking for personal information.");
            add_answers.Add("phishing fake messages or websites are used to trick people  into revealing sensitive data.");
            add_answers.Add("phishing these cybercriminals  use deception to make individuals believe that they are legitimate.");

            add_answers.Add("firewall a firewall controls network traffic based on security rules.");
            add_answers.Add("firewall it helps block unwanted access to your device or network.");
            add_answers.Add("firewall acts as a protective barrier between trusted and untrusted sources.");
            add_answers.Add("firewall prevents hackers or cyber threats from accessing a system.");
         
            add_answers.Add("password passwords are used to keep your accounts or devices locked from unauthorized access.");
            add_answers.Add("password should be strong, long and not easy to guess.");
            add_answers.Add("password avoid using personal details like birth dates when creating one.");
            add_answers.Add("password must not be reused for different accounts.");

            add_answers.Add("privacy check privacy settings online to control who can view your information ");
            add_answers.Add("privacy update your apps regularly to be guarded against security vulnerabilities online.");
            add_answers.Add("privacy do not click on suspicious links to maintain your privacy");
            add_answers.Add("privacy review app permissions frequently and disable access to unnecessary information.");

            add_answers.Add("hacked account immediately secure your account and log out of all devices.");
            add_answers.Add("hacked account contact support if your account has been compromised.");
            add_answers.Add("hacked account enable extra security like two-factor authentication.");
            add_answers.Add("hacked account update your recovery email and phone number if they were changed by the scammer. ");
          
            add_answers.Add("fraud contact your bank immediately if fraud is detected.");
            add_answers.Add("fraud report suspicious financial activity to the authorities.");
            add_answers.Add("fraud monitor your accounts for unusual activity.");
            add_answers.Add("fraud run a security scan on your device using updated antivirus.");
         
            add_answers.Add("malicious chatbot malicious bots often create a sense of urgency to trick users.");
            add_answers.Add("malicious chatbot fake chatbots may ask for sensitive information.");
            add_answers.Add("malicious chatbot be cautious if a bot pressures you for personal data.");
            add_answers.Add("malicious chatbot poor grammar can indicate a scam chatbot.");
           
            add_answers.Add("vpn a vpn helps protect your private details on public wi-fi.");
            add_answers.Add("vpn it encrypts your internet traffic for safety.");
            add_answers.Add("vpn improves security when using public networks.");
            add_answers.Add("vpn it creates a secure connection between your device and the internet.");

            //sentiment detection answers
           
            add_answers.Add("frustrated i understand you're frustration. let's work through the issue.");
            add_answers.Add("frustrated it's okay to feel frustrated when things aren't working. i'm here to help..");
            add_answers.Add("frustrated take a breath, we'll fix this together.");
            add_answers.Add("frustrated it's normal to feel frustrated sometimes but we can work through it together.");

            add_answers.Add("worried it's understandable to feel worried. i'm here to help you stay safe online.");
            add_answers.Add("worried do not panic, most cybersecurity issues can be fixed quickly.");
            add_answers.Add("worried i'm aware of your concern. let's make sure your information is safe.");
            add_answers.Add("worried don't feel too worried. let's go through the situation carefully.");

            add_answers.Add("confused confusion is normal. i'll explain it clearly for you.");
            add_answers.Add("confused let me break it down step by step so it makes sense.");
            add_answers.Add("confused no worries, i'll help you understand it clearer.");
            add_answers.Add("confused let's find out what is unclear so i can give you a better explaination.");

            add_answers.Add("curious it is always good to have a bit of curiosity, ask any question.");
            add_answers.Add("curious being curious can help you learn more about safety tips.");
            add_answers.Add("curious do not feel bad for being curious, instead i encourage you to ask more about your online security.");
            add_answers.Add("curious curiosity is the first step to learning, so what do you wnat to learn about?");

            add_answers.Add("happy that's fantastic to hear! i'm glad things are going well.");
            add_answers.Add("happy awesome! positivity is the best.");
            add_answers.Add("happy i'm happy for you! let me know if you need anything.");
            add_answers.Add("happy i'm pleased that the information is helpful to you. ");
            
            add_answers.Add("sad i'm sorry you're feeling this way. i'm here for you always .");
            add_answers.Add("sad that sounds tough, take things one step at a time.");
            add_answers.Add("sad hopefully things improve soon. you can talk to me anytime.");
            add_answers.Add("sad so sad to hear that. i hope i can help you get through it.");
            
            add_answers.Add("angry i understand your anger. let's try solve the issue together.");
            add_answers.Add("angry it's okay to feel angry, but i'll help you fix the problem.");
            add_answers.Add("angry take your time, i'm here to help you sort it out.");
            add_answers.Add("angry take a breath and let us see what the best solution for your problem is.");

            //return all answers
            return add_answers;

        }//end of return_answers method


        //creating a method called user_questions for questions 
        public ArrayList user_questions()
        {//start of a user_questions method

            //creating an instance for the class ArrayList
            //object name add_questions
            ArrayList add_questions = new ArrayList();

            //adding questions
            add_questions.Add("how are you ?");
            add_questions.Add("what is your purpose ?");
            add_questions.Add("what can i ask you about ?");
            add_questions.Add("what is phishing ?");
            add_questions.Add("what is a firewall ?");
            add_questions.Add("what is cybersecurity ?");
            add_questions.Add("how to maintain privacy online?");
            add_questions.Add("what is a password and how can i securely create my password ?");
            add_questions.Add("what should i do if my account is hacked ?");
            add_questions.Add("how can i report suspicious activities on my account ?");
            add_questions.Add("what are the red flags of a malicious AI-powered chatbot?");
            add_questions.Add("is a VPN necessary when using public Wi-Fi to browse on my phone?");

            //return questions
            return add_questions;

        }//end of a user_questions method

        //method on what to ignore
        public ArrayList ignores()
        {//start of a ignores method

            //creating an instance for the class ArrayList
            //object name ignore
            ArrayList ignore = new ArrayList();

            //adding questions
            ignore.Add("a");
            ignore.Add("about");
            ignore.Add("above");
            ignore.Add("across");
            ignore.Add("after");
            ignore.Add("afterwards");
            ignore.Add("again");
            ignore.Add("against");
            ignore.Add("all");
            ignore.Add("almost");
            ignore.Add("alone");
            ignore.Add("along");
            ignore.Add("already");
            ignore.Add("also");
            ignore.Add("although");
            ignore.Add("always");
            ignore.Add("am");
            ignore.Add("among");
            ignore.Add("amongst");
            ignore.Add("amount");
            ignore.Add("an");
            ignore.Add("and");
            ignore.Add("another");
            ignore.Add("any");
            ignore.Add("anyhow");
            ignore.Add("anyone");
            ignore.Add("anything");
            ignore.Add("anyway");
            ignore.Add("anywhere");
            ignore.Add("are");
            ignore.Add("around");
            ignore.Add("as");
            ignore.Add("at");
            ignore.Add("back");
            ignore.Add("be");
            ignore.Add("became");
            ignore.Add("because");
            ignore.Add("become");
            ignore.Add("becomes");
            ignore.Add("becoming");
            ignore.Add("been");
            ignore.Add("before");
            ignore.Add("beforehand");
            ignore.Add("behind");
            ignore.Add("being");
            ignore.Add("below");
            ignore.Add("beside");
            ignore.Add("besides");
            ignore.Add("between");
            ignore.Add("beyond");
            ignore.Add("both");
            ignore.Add("but");
            ignore.Add("by");
            ignore.Add("can");
            ignore.Add("cannot");
            ignore.Add("could");
            ignore.Add("did");
            ignore.Add("do");
            ignore.Add("does");
            ignore.Add("doing");
            ignore.Add("done");
            ignore.Add("down");
            ignore.Add("during");
            ignore.Add("each");
            ignore.Add("either");
            ignore.Add("else");
            ignore.Add("elsewhere");
            ignore.Add("enough");
            ignore.Add("etc");
            ignore.Add("even");
            ignore.Add("ever");
            ignore.Add("every");
            ignore.Add("everyone");
            ignore.Add("everything");
            ignore.Add("everywhere");
            ignore.Add("except");
            ignore.Add("few");
            ignore.Add("first");
            ignore.Add("for");
            ignore.Add("former");
            ignore.Add("formerly");
            ignore.Add("from");
            ignore.Add("further");
            ignore.Add("had");
            ignore.Add("has");
            ignore.Add("have");
            ignore.Add("having");
            ignore.Add("he");
            ignore.Add("hence");
            ignore.Add("her");
            ignore.Add("here");
            ignore.Add("hereafter");
            ignore.Add("hereby");
            ignore.Add("herein");
            ignore.Add("hereupon");
            ignore.Add("hers");
            ignore.Add("herself");
            ignore.Add("him");
            ignore.Add("himself");
            ignore.Add("his");
            ignore.Add("however");
            ignore.Add("i");
            ignore.Add("if");
            ignore.Add("in");
            ignore.Add("indeed");
            ignore.Add("inside");
            ignore.Add("instead");
            ignore.Add("into");
            ignore.Add("is");
            ignore.Add("it");
            ignore.Add("its");
            ignore.Add("itself");
            ignore.Add("last");
            ignore.Add("later");
            ignore.Add("latter");
            ignore.Add("latterly");
            ignore.Add("least");
            ignore.Add("less");
            ignore.Add("lot");
            ignore.Add("many");
            ignore.Add("may");
            ignore.Add("me");
            ignore.Add("meanwhile");
            ignore.Add("might");
            ignore.Add("more");
            ignore.Add("moreover");
            ignore.Add("most");
            ignore.Add("mostly");
            ignore.Add("much");
            ignore.Add("must");
            ignore.Add("my");
            ignore.Add("myself");
            ignore.Add("name");
            ignore.Add("namely");
            ignore.Add("neither");
            ignore.Add("never");
            ignore.Add("nevertheless");
            ignore.Add("next");
            ignore.Add("no");
            ignore.Add("nobody");
            ignore.Add("none");
            ignore.Add("noone");
            ignore.Add("nor");
            ignore.Add("not");
            ignore.Add("nothing");
            ignore.Add("now");
            ignore.Add("nowhere");
            ignore.Add("of");
            ignore.Add("off");
            ignore.Add("often");
            ignore.Add("on");
            ignore.Add("once");
            ignore.Add("one");
            ignore.Add("only");
            ignore.Add("or");
            ignore.Add("other");
            ignore.Add("others");
            ignore.Add("otherwise");
            ignore.Add("ought");
            ignore.Add("our");
            ignore.Add("ours");
            ignore.Add("ourselves");
            ignore.Add("out");
            ignore.Add("outside");
            ignore.Add("over");
            ignore.Add("own");
            ignore.Add("part");
            ignore.Add("per");
            ignore.Add("perhaps");
            ignore.Add("please");
            ignore.Add("put");
            ignore.Add("rather");
            ignore.Add("re");
            ignore.Add("same");
            ignore.Add("see");
            ignore.Add("seem");
            ignore.Add("seemed");
            ignore.Add("seeming");
            ignore.Add("seems");
            ignore.Add("several");
            ignore.Add("she");
            ignore.Add("should");
            ignore.Add("show");
            ignore.Add("side");
            ignore.Add("since");
            ignore.Add("so");
            ignore.Add("some");
            ignore.Add("somehow");
            ignore.Add("someone");
            ignore.Add("something");
            ignore.Add("sometime");
            ignore.Add("sometimes");
            ignore.Add("somewhere");
            ignore.Add("still");
            ignore.Add("such");
            ignore.Add("take");
            ignore.Add("than");
            ignore.Add("that");
            ignore.Add("the");
            ignore.Add("their");
            ignore.Add("theirs");
            ignore.Add("them");
            ignore.Add("themselves");
            ignore.Add("then");
            ignore.Add("thence");
            ignore.Add("there");
            ignore.Add("thereafter");
            ignore.Add("thereby");
            ignore.Add("therefore");
            ignore.Add("therein");
            ignore.Add("thereupon");
            ignore.Add("these");
            ignore.Add("they");
            ignore.Add("this");
            ignore.Add("those");
            ignore.Add("though");
            ignore.Add("through");
            ignore.Add("throughout");
            ignore.Add("thru");
            ignore.Add("thus");
            ignore.Add("to");
            ignore.Add("together");
            ignore.Add("too");
            ignore.Add("toward");
            ignore.Add("towards");
            ignore.Add("under");
            ignore.Add("unless");
            ignore.Add("until");
            ignore.Add("up");
            ignore.Add("upon");
            ignore.Add("us");
            ignore.Add("used");
            ignore.Add("very");
            ignore.Add("via");
            ignore.Add("was");
            ignore.Add("we");
            ignore.Add("well");
            ignore.Add("were");
            ignore.Add("what");
            ignore.Add("whatever");
            ignore.Add("when");
            ignore.Add("whence");
            ignore.Add("whenever");
            ignore.Add("where");
            ignore.Add("whereafter");
            ignore.Add("whereas");
            ignore.Add("whereby");
            ignore.Add("wherein");
            ignore.Add("whereupon");
            ignore.Add("wherever");
            ignore.Add("whether");
            ignore.Add("which");
            ignore.Add("while");
            ignore.Add("whither");
            ignore.Add("who");
            ignore.Add("whoever");
            ignore.Add("whole");
            ignore.Add("whom");
            ignore.Add("whose");
            ignore.Add("why");
            ignore.Add("will");
            ignore.Add("with");
            ignore.Add("within");
            ignore.Add("without");
            ignore.Add("would");
            ignore.Add("yes");
            ignore.Add("yet");
            ignore.Add("you");
            ignore.Add("your");
            ignore.Add("yours");
            ignore.Add("yourself");
            ignore.Add("yourselves"); 
            ignore.Add(" ");



            //return questions
            return ignore;

        }//end of a ignores method

    }//end of class
}//end of namespace