using CallingRequestAPI.DataAccessors;
using CallingRequestAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace CallingRequestAPI.Businness
{
    public class CallingRequest
    {
        private readonly RequestDataAccessor dataAccessor;

        public CallingRequest()
        {
            this.dataAccessor = new RequestDataAccessor();
        }
        
        public IEnumerable<Request> GetRequests(int receiverID)
        {
            return this.dataAccessor.GetRequests(receiverID).Select(Mapper.CreateRequest);
        }

        public IEnumerable<Request> GetRequests(int senderID,int receiverID)
        {
            return this.dataAccessor.GetRequests(senderID, receiverID).Select(Mapper.CreateRequest);
        }

        public bool DoRequest(Request request)
        {
            var requestSK = Mapper.SplitRequest(request);
            switch(request.State)
            {
                case State.Call:
                    return this.DoCallingRequest(requestSK);
                case State.Accept:
                    return this.AcceptRequest(requestSK);
                case State.Reject:
                    return this.RejectRequest(requestSK);
                case State.UnknownUser:
                    return this.RejectRequestAutomatically(requestSK);
            }
            return false;
        }

        private bool DoCallingRequest(Dictionary<string, object> request)
        {
            return this.dataAccessor.InsertRequest(request);
        }

        private bool RejectRequest(Dictionary<string, object> request)
        {
            return this.dataAccessor.InsertRequest(request);
        }

        private bool AcceptRequest(Dictionary<string, object> request)
        {
            return this.dataAccessor.InsertRequest(request);
        }

        private bool RejectRequestAutomatically(Dictionary<string, object> request)
        {
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
