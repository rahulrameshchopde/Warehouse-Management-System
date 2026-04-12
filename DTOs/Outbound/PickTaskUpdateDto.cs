using System.ComponentModel.DataAnnotations;

using WarehousePro.API.Models.Enums;

namespace WarehousePro.API.DTOs.Outbound

{

    public class PickTaskUpdateDto

    {

        [Required]

        public PickTaskStatus Status { get; set; }

    }

}
