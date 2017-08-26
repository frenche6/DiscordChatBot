using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Collections.Generic;
using System.Timers;

namespace DiscordBot
{
    class Program
    {
        int challengedPoints;
        //List<string> challengeUsers = new List<string>();
        Timer timer = new Timer();
        Dictionary<string, int> challengeUsers = new Dictionary<string, int>();

        static void Main(string[] args)

            => new Program().MainAsync().GetAwaiter().GetResult();


        public async Task MainAsync()
        {
            var client = new DiscordSocketClient();

            //client.Log += Log;
            client.MessageReceived += MessageReceived;

            string token = "YOUR_TOKEN_HERE";
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task MessageReceived(SocketMessage message)
        {
            //if (message.Author.Username == "YoungPleb")
            //{
            //    var responses = new List<string>
            //    {
            //        "Are you still here?",
            //        "*sigh* Still talking?",
            //        "I strongly dislike you.."
            //    };

            //    Random rnd = new Random();
            //    int responseIndex = rnd.Next(responses.Count);
            //    await message.Channel.SendMessageAsync(responses[responseIndex]);
            //}

            if (message.Content.StartsWith("!Challenge"))
            {
                var splitChar = ' ';
                var challengeString = message.Content.Split(splitChar);
                //var challenegedUser = challengeString[1];
                challengedPoints = int.Parse(challengeString[1]);
                challengeUsers.Add(message.Author.Username, challengedPoints);

                await message.Channel.SendMessageAsync("Challenge has been started by " + message.Author.Username + " for " + challengedPoints + " points.");
            }


            switch (message.Content)
            {
                case "!ping":
                    await message.Channel.SendMessageAsync("Pong!");
                    break;
                case "!Accept":
                    challengeUsers.Add(message.Author.Username, challengedPoints);
                    break;
                case "!EndChallenge":

                    break;
            }


        }





        private void Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            //return Task.CompletedTask;
        }
    }
}
