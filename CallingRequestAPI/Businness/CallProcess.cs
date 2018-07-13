using CallingRequestAPI.DataAccessors;
using System.Collections.Generic;

namespace CallingRequestAPI.Businness
{
    public class CallProcess
    {
        private readonly BufferDataAccessor dataAccessor;

        public CallProcess()
        {
            this.dataAccessor = new BufferDataAccessor();
        }

        public bool SendMessage(int senderID, int receiverID, string traffic)
        {
            var call = new Dictionary<string, object>();
            call.Add("SenderID", senderID);
            call.Add("ReceiverID", receiverID);
            call.Add("Traffic", traffic);

            return this.dataAccessor.InsertBuffer(call);
        }

        public Dictionary<string, object> GetMessage(int receiverID)
        {
            return this.dataAccessor.GetBuffer(receiverID);
        }

        public bool ClearBuffer(int senderID, int receiverID)
        {
            var buffer = new Dictionary<string, object>();
            buffer.Add("SenderID", senderID);
            buffer.Add("ReceiverID", receiverID);

            return this.dataAccessor.DeleteBuffer(buffer);
        }
    }
}
