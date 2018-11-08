using ProtoBuf;

namespace SaharaLib
{
    [ProtoContract]
    public class CreateAccountEvent : BaseEvent
    {
        [ProtoMember(1)]
        public string Email { get; set; }

        [ProtoMember(2)]
        public string UserName { get; set; }

        [ProtoMember(3)]
        public string Password { get; set; }

        public CreateAccountEvent()
        {
            Type = EventType.CreateAccount;
            Email = null;
            Password = null;
        }

        public CreateAccountEvent(string email, string password)
        {
            Type = EventType.CreateAccount;
            Email = email;
            Password = password;
        }
    }
}