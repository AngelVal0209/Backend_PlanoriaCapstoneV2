using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Shared.Pagination
{
    public class CursorPaginatedResponseDto<T>
    {
        public List<T> Data { get; set; }
        public string NextCursor { get; set; }
        public string PreviousCursor { get; set; }
        public bool HasMore { get; set; }
        public int Limit { get; set; }
    }
}
