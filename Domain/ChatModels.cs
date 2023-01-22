using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ChatModels
    {
        public class Message
        {
            [Key]
            public Guid Id { get; set; }
            public string Content { get; set; }
            public DateTime SendTime { get; set; }
            public int SenderId { get; set; }
            public Guid RoomId { get; set; }

            #region Foreign Key

            [ForeignKey(nameof(RoomId))]
            public Room Room { get; set; }
            #endregion
        }
        public class Room
        {
            [Key]
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int OwnerId { get; set; }
            //public int SenderUserId { get; set; }

            #region Foreign Key
            public User Owner { get; set; }
            #endregion
        }
        public class UserRoom
        {
            [Key]
            public Guid Id { get; set; }
            public int UserId { get; set; }
            public Guid RoomId { get; set; }
            //public int SenderUserId { get; set; }

            #region Foreign Key
            [ForeignKey(nameof(UserId))]
            public User User { get; set; }
            [ForeignKey(nameof(RoomId))]
            public Room Room { get; set; }
            #endregion
        }

    }
}
