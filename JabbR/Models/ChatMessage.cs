﻿using System;
using System.ComponentModel.DataAnnotations;

namespace JabbR.Models
{
    public class ChatMessage
    {
        [Key]
        public int Key { get; set; }

        public string Content { get; set; }
        public string Id { get; set; }        
        public virtual ChatRoom Room { get; set; }
        public virtual ChatUser User { get; set; }
        public DateTimeOffset When { get; set; }
        public bool HtmlEncoded { get; set; }

        // After content providers run this is updated with the content
        public string HtmlContent { get; set; }

        public int? RoomKey { get; set; }
        public int? UserKey { get; set; }
    }
}