using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Users.Requests
{
    public class DeleteAccountRequestDto
    {
        public string Password { get; set; }
        public string ConfirmationText { get; set; }
    }
}