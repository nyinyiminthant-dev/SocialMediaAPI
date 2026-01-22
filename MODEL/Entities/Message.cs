using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entity;

[Table("MessageTbl")]
public class Message
{
    [Key]
    public int Message_Id { get; set; } 
    public int Sender_Id { get; set; }
    public int Receiver_Id { get; set; }
    public string? Content { get; set; }
    public DateTime Sent_At { get; set; }
}
