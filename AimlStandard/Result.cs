using System;
using System.Collections;
using System.Text;

namespace AimlStandard
{
    /// <summary>
    /// Encapsulates information about the result of a request to the bot
    /// </summary>
    public class Result
    {
        /// <summary>
        /// The bot that is providing the answer
        /// </summary>
        public Bot Bot { get; set; }

        /// <summary>
        /// The user for whom this is a result
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// The request from the user
        /// </summary>
        public Request Request { get; set; }

        /// <summary>
        /// The raw input from the user
        /// </summary>
        public string RawInput
        {
            get
            {
                return this.Request.RawInput;
            }
        }

        /// <summary>
        /// The normalized sentence(s) (paths) fed into the graphmaster
        /// </summary>
        public ArrayList NormalizedPaths = new ArrayList();

        /// <summary>
        /// The amount of time the request took to process
        /// </summary>
        public TimeSpan Duration;

        /// <summary>
        /// The result from the bot with logging and checking
        /// </summary>
        public string Output
        {
            get
            {
                if (OutputSentences.Count > 0)
                {
                    return this.RawOutput;
                }
                else
                {
                    StringBuilder paths = new StringBuilder();
                    foreach (string pattern in this.NormalizedPaths)
                    {
                        paths.Append(pattern + Environment.NewLine);
                    }
                    this.Bot.WriteToLog("The bot could not find any response for the input: " + this.RawInput+ " with the path(s): "+Environment.NewLine+paths.ToString() + " from the user with an id: "+this.User.UserID);
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Returns the raw sentences without any logging 
        /// </summary>
        public string RawOutput
        {
            get
            {
                StringBuilder result = new StringBuilder();
                foreach (string sentence in OutputSentences)
                {
                    string sentenceForOutput = sentence;
                    if (!this.CheckEndsAsSentence(sentenceForOutput))
                    {
                        sentenceForOutput += ".";
                    }
                    result.Append(sentenceForOutput + " ");
                }
                return result.ToString().Trim();
            }
        }

        /// <summary>
        /// The templates retrieved from the bot's graphmaster that are to be converted into
        /// the collection of Sentences
        /// </summary>
        public ArrayList Templates { get; set; } = new ArrayList();

        /// <summary>
        /// The individual sentences produced by the bot that form the complete response
        /// </summary>
        public ArrayList OutputSentences { get; set; } = new ArrayList();

        /// <summary>
        /// The individual sentences that constitute the raw input from the user
        /// </summary>
        public ArrayList InputSentences { get; set; } = new ArrayList();

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="user">The user for whom this is a result</param>
        /// <param name="bot">The bot providing the result</param>
        /// <param name="request">The request that originated this result</param>
        public Result(User user, Bot bot, Request request)
        {
            this.User = user;
            this.Bot = bot;
            this.Request = request;
            this.Request.Result = this;
        }

        /// <summary>
        /// Returns the raw output from the bot
        /// </summary>
        /// <returns>The raw output from the bot</returns>
        public override string ToString()
        {
            return this.Output;
        }

        /// <summary>
        /// Checks that the provided sentence ends with a sentence splitter
        /// </summary>
        /// <param name="sentence">the sentence to check</param>
        /// <returns>True if ends with an appropriate sentence splitter</returns>
        private bool CheckEndsAsSentence(string sentence)
        {
            foreach (string splitter in this.Bot.Splitters)
            {
                if (sentence.Trim().EndsWith(splitter))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
