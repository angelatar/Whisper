namespace CallingRequestAPI.Models
{
    public class Request
    {
        public int SenderID { set; get; }

        public int ReceiverID { set; get; }

        public State State { set; get; }
    }
}
