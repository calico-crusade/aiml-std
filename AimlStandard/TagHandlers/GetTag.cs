using System.Xml;

namespace AimlStandard.TagHandlers
{
    using Utilities;

    /// <summary>
    /// The get element tells the AIML interpreter that it should substitute the contents of a 
    /// predicate, if that predicate has a value defined. If the predicate has no value defined, 
    /// the AIML interpreter should substitute the empty string "". 
    /// 
    /// The AIML interpreter implementation may optionally provide a mechanism that allows the 
    /// AIML author to designate default values for certain predicates (see [9.3.]). 
    /// 
    /// The get element must not perform any text formatting or other "normalization" on the predicate
    /// contents when returning them. 
    /// 
    /// The get element has a required name attribute that identifies the predicate with an AIML 
    /// predicate name. 
    /// 
    /// The get element does not have any content.
    /// </summary>
    public class GetTag : AIMLTagHandler
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="bot">The bot involved in this request</param>
        /// <param name="user">The user making the request</param>
        /// <param name="request">The request inputted into the system</param>
        /// <param name="result">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public GetTag(Bot bot, 
                      User user, 
                      Request request, 
                      Result result, 
                      XmlNode templateNode)
        : base (bot,user,request,result,templateNode)
        {}

        protected override string ProcessChange()
        {
            if (this.templateNode.Name.ToLower() == "get")
            {
                if (this.Bot.GlobalSettings.Count > 0)
                {
                    if (this.templateNode.Attributes.Count == 1)
                    {
                        if (this.templateNode.Attributes[0].Name.ToLower() == "name")
                        {
                            return this.User.Predicates.GrabSetting(this.templateNode.Attributes[0].Value);
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}
