namespace WarehouseProject.DTOs.Order
{
    public class OrderCreateDto
    {
        public string OrderNumber { get; set; } = string.Empty;

            public string CustomerName { get; set; } = string.Empty;

            public string? DeliveryAddress { get; set; }


              public DateTime OrderDate { get; set; }

            public DateTime? RequiredDate { get; set; }

            public List<OrderItemCreateDto> Items { get; set; } = new();

        }

        public class OrderItemCreateDto

        {

            public int ItemID { get; set; }

            public int Quantity { get; set; }

        }

    }

