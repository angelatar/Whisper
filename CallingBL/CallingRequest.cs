using CallingDAL;
using System.Collections.Generic;

namespace CallingBL
{
    public class CallingRequest
    {
        private readonly DataAccessor dataAccessor;

        public CallingRequest()
        {
            this.dataAccessor = new DataAccessor();
        }

        public bool DoCallingRequest(int senderID,int receiverID)
        {
            var request = new Dictionary<string, object>();
            request.Add("SenderID", senderID);
            request.Add("ReceiverID", receiverID);
            request.Add("StateID", State.Call);

            return this.dataAccessor.InsertRequest(request);
        }

        public IEnumerable<Dictionary<string, object>> GetRequests(int receiverID)
        {
            return this.dataAccessor.GetRequests(receiverID);
        }

        public bool RejectRequest(int senderID, int receiverID)
        {
            var request = new Dictionary<string, object>();
            request.Add("SenderID", senderID);
            request.Add("ReceiverID", receiverID);
            request.Add("StateID", State.Reject);

            return this.dataAccessor.InsertRequest(request);
        }

        public bool AcceptRequest(int senderID, int receiverID)
        {
            var request = new Dictionary<string, object>();
            request.Add("SenderID", senderID);
            request.Add("ReceiverID", receiverID);
            request.Add("StateID", State.Accept);

            return this.dataAccessor.InsertRequest(request);
        }

        public bool RejectRequestAutomatically(int senderID, int receiverID)
        {
            var request = new Dictionary<string, object>();
            request.Add("SenderID", senderID);
            request.Add("ReceiverID", receiverID);
            request.Add("StateID", State.UnknownUser);

            return this.dataAccessor.InsertRequest(request);
        }

        public bool ClearRequests(int senderID, int receiverID)
        {
            var request = new Dictionary<string, object>();
            request.Add("SenderID", senderID);
            request.Add("ReceiverID", receiverID);

            return this.dataAccessor.DeleteRequests(request);
        }
    }
}
