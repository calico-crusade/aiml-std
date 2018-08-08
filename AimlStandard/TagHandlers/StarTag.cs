using System;
using System.Xml;

namespace AimlStandard.TagHandlers
{
    using Utilities;

    /// <summary>
    /// The star element indicates that an AIML interpreter should substitute the value "captured" 
    /// by a particular wildcard from the pattern-specified portion of the match path when returning 
    /// the template. 
    /// 
    /// The star element has an optional integer index attribute that indicates which wildcard to use. 
    /// The minimum acceptable value for the index is "1" (the first wildcard), and the maximum 
    /// acceptable value is equal to the number of wildcards in the pattern. 
    /// 
    /// An AIML interpreter should raise an error if the index attribute of a star specifies a wildcard 
    /// that does not exist in the category element's pattern. Not specifying the index is the same as 
    /// specifying an index of "1". 
    /// 
    /// The star element does not have any content. 
    /// </summary>
    public class StarTag : AIMLTagHandler
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="bot">The bot involved in this request</param>
        /// <param name="user">The user making the request</param>
        /// <param name="request">The request inputted into the system</param>
        /// <param name="result">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public StarTag (Bot bot, 
                        User user, 
                        Request request, 
                        Result result, 
                        XmlNode templateNode)
        : base (bot,user,request,result,templateNode)
        {}

        protected override string ProcessChange()
        {
            if (this.templateNode.Name.ToLower() == "star")
            {
                if (this.Request.InputStar.Count > 0)
                {
                    if (this.templateNode.Attributes.Count == 0)
                    {
                        // return the first (latest) star in the arraylist
                        return (string)this.Request.InputStar[0];
                    }
                    else if (this.templateNode.Attributes.Count == 1)
                    {
                        if (this.templateNode.Attributes[0].Name.ToLower() == "index")
                        {
                            try
                            {
                                int index = Convert.ToInt32(this.templateNode.Attributes[0].Value);
                                index--;
                                if ((index >= 0) & (index < this.Request.InputStar.Count))
                                {
                                    return (string)this.Request.InputStar[index];
                                }
                                else
                                {
                                    this.Bot.WriteToLog("InputStar out of bounds reference caused by input: " + this.Request.RawInput);
                                }
                            }
                            catch
                            {
                                this.Bot.WriteToLog("Index set to non-integer value whilst processing star tag in response to the input: " + this.Request.RawInput);
                            }
                        }
                    }
                }
                else
                {
                    this.Bot.WriteToLog("A star tag tried to reference an empty InputStar collection when processing the input: "+this.Request.RawInput);
                }
            }
            return string.Empty;
        }
    }
}
