using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace BotApplication4.Dialogs
{

    
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }


        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // Calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // If the user enters a single character into the ChatBot - Assumes an error and asks for a question
            if (activity.Text.Length <= 1)
            {
                await context.PostAsync($"Please ask me another question. I usually work best with full sentences!");
            }

            // if "degree apprenticeship" is entered Chatbot will tell you the duration of the degree apprenticeship lasts for
            else if (activity.Text.Contains("degree apprenticeship") || activity.Text.Contains("Degree Apprenticeship") && !activity.Text.Contains("Degree Apprenticeship"))
            {
                await context.PostAsync($"The Capgemini Degree Apprenticeship lasts for 4 Years and 6 Months");
            }

            // if "cyber security" keywords are entered Chatbot will tell you how long the apprenticeship lasts for
            else if (activity.Text.Contains("cyber security") || activity.Text.Contains("Cyber Security"))
            {
                await context.PostAsync($"The Capgemini Cyber Security Apprenticeship lasts for 18 Months");
            }

            // If the word "finance" is used Chatbot will tell you the duration of the finance apprenticeship
            else if (activity.Text.Contains("finance") || activity.Text.Contains("Finance"))
            {
                await context.PostAsync($"The Capgemini Finance Apprenticeship lasts for 3 Years");
            }

            // How long is the graduate scheme? 
            else if (activity.Text.Contains("how long") && activity.Text.Contains("Graduate Scheme") || activity.Text.Contains("graduate scheme")
                        || activity.Text.Contains("Graduate scheme") || activity.Text.Contains("grad scheme")
                        || activity.Text.Contains("graduate programme"))
            {
                await context.PostAsync($"The Capgemini Graduate Scheme lasts for 2 Years");
            }

            // If Chatbot detects both "grades" and "degree" in the same sentence it assumes 
            // you are asking for the required grades for the degree apprenticeship
            else if (activity.Text.Contains("grades") && activity.Text.Contains("degree") || activity.Text.Contains("entry requirements") ||
                        activity.Text.Contains("grades do I need") || activity.Text.Contains("required grades"))
            {
                await context.PostAsync($"The required grades for the Degree Apprenticeship are: Three Cs at A-level " +
                    $"or equivalent certifications");
            }

            // the user can send Chatbot a common greeting and Chatbot will respond accordingly
            else if (activity.Text.Contains("Hi") || //had to remove "hi" as it was finding the keyword in every instance of "apprenticeship"
                        activity.Text.Contains("Hello") || activity.Text.Contains("hello") ||
                        activity.Text.Contains("Hey") || activity.Text.Contains("hey") ||
                        activity.Text.Contains("Howdy") || activity.Text.Contains("howdy") ||
                        activity.Text.Contains("Wassup") || activity.Text.Contains("wassup") ||
                        activity.Text.Contains("Good morning") || activity.Text.Contains("good morning") ||
                        activity.Text.Contains("Good afternoon") || activity.Text.Contains("good afternoon"))
            {
                await context.PostAsync($"Hi, I'm Chatbot. Do you have a question for me today?");
            }

            // The user can ask Chatbot for the date and time
            else if (activity.Text.Contains("can you tell me the time") || activity.Text.Contains("time"))
            {
                string formattedTime = DateTime.Now.ToString("HH:MM");
                await context.PostAsync($"The time is: " + formattedTime);
            }

            else if (activity.Text.Contains("can you tell me the date") || activity.Text.Contains("date"))
            {
                string formattedDate = DateTime.Now.ToString("dd/MM/yyyy");
                await context.PostAsync($"Today's date is: " + formattedDate);
            }

            else if (activity.Text.Contains("sponsor") || activity.Text.Contains("sponsorship") || activity.Text.Contains("visa"))
            {

                await context.PostAsync($"Unfortunately Capgemini does not offer Visa sponsorships at this time");
            }

            else if (activity.Text.Contains("start") && activity.Text.Contains("when can I start") ||
                        activity.Text.Contains("starting dates") || activity.Text.Contains("starting"))
            {
                await context.PostAsync($"There are typically four intakes a year in September, November, March and June. " +
                    $"Visit this link to find the relevant timings for your chosen scheme: https://careers.uk.capgemini.com/apprentices/");
            }

            else if (activity.Text.Contains("Apprenticeship schemes") || activity.Text.Contains("apprentice scheme") ||
                        activity.Text.Contains("Apprenticeship Scheme") || activity.Text.Contains("Apprenticeship Schemes") ||
                        activity.Text.Contains("apprenticeship scheme") || activity.Text.Contains("what available apprenticeship schemes are there") ||
                        activity.Text.Contains("what apprenticeship schemes are available"))
            {
                await context.PostAsync($"Capgemini currently offer three different types of schemes: " +
                                            $"\r\n - A Technology Degree Apprenticeship " +
                                                $"\r\n - A Finance Apprenticeship " +
                                                    $"\r\n - A Cyber Security Higher Apprenticeship " +
                                                        $"\r\n For more information check out our pages \r\n" +
                                                            $"\r\n To find out more about our schemes visit: https://careers.uk.capgemini.com/apprentices/");
            }
            else if (activity.Text.Contains("joke") || activity.Text.Contains("jokes") || activity.Text.Contains("Jokes") || activity.Text.Contains("Joke"))
            {
                await context.PostAsync($"A joke? I know a good one! " +
                    $"\r\n Did you hear about the Pirate who shared Davie Jone's treasure map with his crew? " +
                    $"\r\n - He breached the data privacy conditions of GDP-ARRRRRR");
            }

            else if (activity.Text.Contains("graduate roles") || activity.Text.Contains("graduate level positions") ||
                        activity.Text.Contains("different roles"))
            {
                await context.PostAsync($" - As a business specialist there is: Finance Specialist, Sales Associate, HR graduate" +
                    $"\r\n - As a Business and Technology specialist there is: Banking Graduate, Associate Consultant, " +
                    $"SAP Functional Applications Consultant, Oracle Functional Applications Consultant, " +
                    $"SAP Applications Consultant, Salesforce Functional Consultant, Junior Delivery Manager, Business Analyst, " +
                    $"Project Management Office Analyst, Test Consultant, Data Analyst, Data and Analytics Consultant" +
                    $" \r\n - As a tech specialist there is: SAP Technical Applications Consultant, Cyber Security Consultant, " +
                    $"Software Developer, Insights and Data Consultant, Salesforce Technical Consultant, Data analyst" +
                    $"\r\n - As a Management consulting graduate there is: Core management consulting, " +
                    $"Analytics Consulting Academy, Financial Services Consulting Academy, Business and Technology Innovation Academy" +
                    $"\r\n To find out more about our schemes visit: https://www.capgemini.com/gb-en/careers/your-career-path/graduates/");
            }

            else 
            {
                if (activity.Text.Length <= 1)
                    return;
                else
                {
                    await context.PostAsync($"I'm sorry I don't understand that question, please re-phrase and try again");
                }
                    
            }
            context.Wait(MessageReceivedAsync);
        }
    }                           
}

