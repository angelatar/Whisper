using System.Collections.Generic;

namespace CallingRequestAPI.Models
{
    public class Mapper
    {
        public static Request CreateRequest(Dictionary<string, object> requestSk)
        {
            return new Request()
            {
                SenderID = (int)requestSk["SenderID"],
                ReceiverID = (int)requestSk["ReceiverID"],
                State = (State)requestSk["StateID"]
            };
        }

        public static Dictionary<string, object> SplitRequest(Request request)
        {
            var requestSK = new Dictionary<string, object>();
            requestSK.Add("SenderID", request.SenderID);
            requestSK.Add("ReceiverID", request.ReceiverID);
            requestSK.Add("StateID", (int)request.State);

            return requestSK;
        }

        public static Call CreateCall(Dictionary<string, object> messageSk)
        {
            return new Call()
            {
                SenderID = (int)messageSk["SenderID"],
                ReceiverID = (int)messageSk["ReceiverID"],
                Traffic = messageSk["Traffic"].ToString()
            };
        }

        public static Dictionary<string, object> SplitCall(Call call)
        {
            var callSK = new Dictionary<string, object>();
            callSK.Add("SenderID", call.SenderID);
            callSK.Add("ReceiverID", call.ReceiverID);
            callSK.Add("Traffic", call.Traffic);

            return callSK;
        }
    }
}
