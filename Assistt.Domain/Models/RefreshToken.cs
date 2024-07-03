using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Domain.Models
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public int UserId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }

        public DateTime? Used { get; set; }
        public DateTime? Revoked { get; set; }


    }
}
