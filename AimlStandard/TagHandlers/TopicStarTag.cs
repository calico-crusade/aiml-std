using System;
using System.Xml;

namespace AimlStandard.TagHandlers
{
    using Utilities;

    /// <summary>
    /// The topicstar element tells the AIML interpreter that it should substitute the contents of 
    /// a wildcard from the current topic (if the topic contains any wildcards).
    /// 
    /// The topicstar element has an optional integer index attribute that indicates which wildcard 
    /// to use; the minimum acceptable value for the index is "1" (the first wildcard). Not 
    /// specifying the index is the same as specifying an index of "1". 
    /// 
    /// The topicstar element does not have any content. 
    /// </summary>
    public class TopicStarTag : AIMLTagHandler
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="bot">The bot involved in this request</param>
        /// <param name="user">The user making the request</param>
        /// <param name="request">The request inputted into the system</param>
        /// <param name="result">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public TopicStarTag(Bot bot, 
                        User user, 
                        Request request, 
                        Result result, 
                        XmlNode templateNode)
        : base (bot,user,request,result,templateNode)
        {}

        protected override string ProcessChange()
        {
            if (this.templateNode.Name.ToLower() == "topicstar")
            {
                if (this.templateNode.Attributes.Count == 0)
                {
                    if (this.Request.TopicStar.Count > 0)
                    {
                        return (string)this.Request.TopicStar[0];
                    }
                    else
                    {
                        this.Bot.WriteToLog("ERROR! An out of bounds index to topicstar was encountered when processing the input: " + this.Request.RawInput);
                    }
                }
                else if (this.templateNode.Attributes.Count == 1)
                {
                    if (this.templateNode.Attributes[0].Name.ToLower() == "index")
                    {
                        if (this.templateNode.Attributes[0].Value.Length > 0)
                        {
                            try
                            {
                                int result = Convert.ToInt32(this.templateNode.Attributes[0].Value.Trim());
                                if (this.Request.TopicStar.Count > 0)
                                {
                                    if (result > 0)
                                    {
                                        return (string)this.Request.TopicStar[result - 1];
                                    }
                                    else
                                    {
                                        this.Bot.WriteToLog("ERROR! An input tag with a bady formed index (" + this.templateNode.Attributes[0].Value + ") was encountered processing the input: " + this.Request.RawInput);
                                    }
                                }
                                else
                                {
                                    this.Bot.WriteToLog("ERROR! An out of bounds index to topicstar was encountered when processing the input: " + this.Request.RawInput);
                                }
                            }
                            catch
                            {
                                this.Bot.WriteToLog("ERROR! A thatstar tag with a bady formed index (" + this.templateNode.Attributes[0].Value + ") was encountered processing the input: " + this.Request.RawInput);
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}
