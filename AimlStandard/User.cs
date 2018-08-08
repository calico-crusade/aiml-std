using System;
using System.Collections;

namespace AimlStandard
{
    using Utilities;

    /// <summary>
    /// Encapsulates information and history of a user who has interacted with the bot
    /// </summary>
    public class User
    {

        #region Attributes

        /// <summary>
        /// The bot this user is using
        /// </summary>
        public Bot Bot { get; set; }

        /// <summary>
        /// The GUID that identifies this user to the bot
        /// </summary>
        public string UserID { get; }

        /// <summary>
        /// A collection of all the result objects returned to the user in this session
        /// </summary>
        private readonly ArrayList Results = new ArrayList();

		/// <summary>
		/// the value of the "topic" predicate
		/// </summary>
        public string Topic
        {
            get
            {
                return this.Predicates.GrabSetting("topic");
            }
        }

		/// <summary>
		/// the predicates associated with this particular user
		/// </summary>
        public SettingsDictionary Predicates;

		#endregion
		
		#region Methods

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="UserID">The GUID of the user</param>
        /// <param name="bot">the bot the user is connected to</param>
		public User(string UserID, Bot bot)
		{
            if (UserID.Length > 0)
            {
                this.UserID = UserID;
                this.Bot = bot;
                this.Predicates = new SettingsDictionary(this.Bot);
                this.Bot.DefaultPredicates.Clone(this.Predicates);
                this.Predicates.AddSetting("topic", "*");
            }
            else
            {
                throw new Exception("The UserID cannot be empty");
            }
		}

        /// <summary>
        /// Returns the string to use for the next that part of a subsequent path
        /// </summary>
        /// <returns>the string to use for that</returns>
        public string GetLastBotOutput()
        {
            if (this.Results.Count > 0)
            {
                return ((Result)Results[0]).RawOutput;
            }
            else
            {
                return "*";
            }
        }

        /// <summary>
        /// Returns the first sentence of the last output from the bot
        /// </summary>
        /// <returns>the first sentence of the last output from the bot</returns>
        public string GetThat()
        {
            return this.GetThat(0,0);
        }

        /// <summary>
        /// Returns the first sentence of the output "n" steps ago from the bot
        /// </summary>
        /// <param name="n">the number of steps back to go</param>
        /// <returns>the first sentence of the output "n" steps ago from the bot</returns>
        public string GetThat(int n)
        {
            return this.GetThat(n, 0);
        }

        /// <summary>
        /// Returns the sentence numbered by "sentence" of the output "n" steps ago from the bot
        /// </summary>
        /// <param name="n">the number of steps back to go</param>
        /// <param name="sentence">the sentence number to get</param>
        /// <returns>the sentence numbered by "sentence" of the output "n" steps ago from the bot</returns>
        public string GetThat(int n, int sentence)
        {
            if ((n >= 0) & (n < this.Results.Count))
            {
                Result historicResult = (Result)this.Results[n];
                if ((sentence >= 0) & (sentence < historicResult.OutputSentences.Count))
                {
                    return (string)historicResult.OutputSentences[sentence];
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Returns the first sentence of the last output from the bot
        /// </summary>
        /// <returns>the first sentence of the last output from the bot</returns>
        public string GetResultSentence()
        {
            return this.GetResultSentence(0, 0);
        }

        /// <summary>
        /// Returns the first sentence from the output from the bot "n" steps ago
        /// </summary>
        /// <param name="n">the number of steps back to go</param>
        /// <returns>the first sentence from the output from the bot "n" steps ago</returns>
        public string GetResultSentence(int n)
        {
            return this.GetResultSentence(n, 0);
        }

        /// <summary>
        /// Returns the identified sentence number from the output from the bot "n" steps ago
        /// </summary>
        /// <param name="n">the number of steps back to go</param>
        /// <param name="sentence">the sentence number to return</param>
        /// <returns>the identified sentence number from the output from the bot "n" steps ago</returns>
        public string GetResultSentence(int n, int sentence)
        {
            if ((n >= 0) & (n < this.Results.Count))
            {
                Result historicResult = (Result)this.Results[n];
                if ((sentence >= 0) & (sentence < historicResult.InputSentences.Count))
                {
                    return (string)historicResult.InputSentences[sentence];
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Adds the latest result from the bot to the Results collection
        /// </summary>
        /// <param name="latestResult">the latest result from the bot</param>
        public void AddResult(Result latestResult)
        {
            this.Results.Insert(0, latestResult);
        }
        #endregion
    }
}