using CallingRequestAPI.DataAccessors;
using CallingRequestAPI.Models;
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

        public bool SendCall(Call call)
        {
           return this.dataAccessor.InsertBuffer(Mapper.SplitCall(call));
        }

        public Call GetCall(int senderID,int receiverID)
        {
            return Mapper.CreateCall(dataAccessor.GetBuffer(senderID, receiverID));
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
