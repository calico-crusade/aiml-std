using System;
using System.Collections;

namespace AimlStandard
{
    /// <summary>
    /// Encapsulates all sorts of information about a request to the bot for processing
    /// </summary>
    public class Request
    {
        #region Attributes
        /// <summary>
        /// The raw input from the user
        /// </summary>
        public string RawInput { get; set; }

        /// <summary>
        /// The time at which this request was created within the system
        /// </summary>
        public DateTime StartedOn { get; set; }

        /// <summary>
        /// The user who made this request
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// The bot to which the request is being made
        /// </summary>
        public Bot Bot { get; set; }

        /// <summary>
        /// The final result produced by this request
        /// </summary>
        public Result Result { get; set; }

        /// <summary>
        /// Flag to show that the request has timed out
        /// </summary>
        public bool HasTimeOut { get; set; } = false;

        /// <summary>
        /// If the raw input matches a wildcard then this attribute will contain the block of 
        /// text that the user has inputted that is matched by the wildcard.
        /// </summary>
        public ArrayList InputStar { get; set; } = new ArrayList();

        /// <summary>
        /// If the "that" part of the normalized path contains a wildcard then this attribute 
        /// will contain the block of text that the user has inputted that is matched by the wildcard.
        /// </summary>
        public ArrayList ThatStar { get; set; } = new ArrayList();

        /// <summary>
        /// If the "topic" part of the normalized path contains a wildcard then this attribute 
        /// will contain the block of text that the user has inputted that is matched by the wildcard.
        /// </summary>
        public ArrayList TopicStar { get; set; } = new ArrayList();

        #endregion

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="rawInput">The raw input from the user</param>
        /// <param name="user">The user who made the request</param>
        /// <param name="bot">The bot to which this is a request</param>
        public Request(string rawInput, User user, Bot bot)
        {
            this.RawInput = rawInput;
            this.User = user;
            this.Bot = bot;
            this.StartedOn = DateTime.Now;
        }
    }
}
